namespace CodenameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Generates random code names from one or more <see cref="WordBank"/>s.
    /// </summary>
    /// <remarks>
    /// An unseeded generator is safe to use from multiple threads concurrently.
    /// A seeded one is not: its whole purpose is a deterministic sequence, which
    /// only holds if a single thread draws from it.
    /// </remarks>
    public class Generator
    {
        /// <summary>
        /// How many draws <see cref="GenerateUnique(string[])"/> makes before giving up.
        /// </summary>
        private const int UniqueAttemptLimit = 10000;

        /// <summary>
        /// The seeded source, or null when this generator uses the shared source.
        /// </summary>
        private readonly Random? _seededRandom;

        private string _separator = " ";
        private WordBank[] _parts = new WordBank[0];
        private string _endsWith = string.Empty;

        /// <summary>
        /// String value used to separate the different words that make up a generated code name.
        /// Setting null is treated as an empty separator.
        /// </summary>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value ?? string.Empty; }
        }

        /// <summary>
        /// Array of WordBanks which will be accessed sequentially (by order of array index)
        /// to provide words for code name generation. Setting null is treated as no parts.
        /// </summary>
        public WordBank[] Parts
        {
            get { return _parts; }
            set { _parts = value ?? new WordBank[0]; }
        }

        /// <summary>
        /// The casing format for generated code names.
        /// </summary>
        public Casing Casing { get; set; }

        /// <summary>
        /// String value used as a suffix for the generated code name.
        /// Setting null is treated as no suffix.
        /// </summary>
        public string EndsWith
        {
            get { return _endsWith; }
            set { _endsWith = value ?? string.Empty; }
        }

        /// <summary>
        /// Default constructor. If called w/o ctor parameters, resulting Generator object will be
        /// created with the default Parts, Separator, and Casing properties.
        /// </summary>
        /// <param name="separator">The Separator</param>
        /// <param name="casing">The Casing</param>
        /// <param name="seed">
        /// Seed for reproducible output. When null, a thread-safe shared random source is used.
        /// </param>
        public Generator(string separator = " ", Casing casing = Casing.LowerCase, int? seed = null)
        {
            Parts = new WordBank[] { WordBank.Adjectives, WordBank.Nouns };
            Separator = separator;
            Casing = casing;
            EndsWith = string.Empty;
            _seededRandom = seed.HasValue ? new Random(seed.Value) : null;
        }

        /// <summary>
        /// Constructor offering params for specifying WordBanks.
        /// </summary>
        /// <param name="separator">The Separator</param>
        /// <param name="casing">The Casing</param>
        /// <param name="wordBanks">Comma-delimited WordBank instances</param>
        /// <example>
        /// new Generator("-", Casing.LowerCase, WordBank.Titles, WordBank.FirstNames, WordBank.LastNames)
        /// </example>
        public Generator(string separator, Casing casing, params WordBank[] wordBanks)
            : this(separator, casing)
        {
            Parts = wordBanks;
        }

        /// <summary>
        /// params-based Setter method for the Parts property.
        /// </summary>
        /// <param name="wordBanks">Comma-delimited WordBank instances</param>
        /// <example>
        /// generator.SetParts(WordBank.Adjectives, WordBank.Nouns)
        /// </example>
        public void SetParts(params WordBank[] wordBanks)
        {
            Parts = wordBanks;
        }

        /// <summary>
        /// Generates a code name based on current configuration of Separator, Parts, and Casing properties.
        /// </summary>
        /// <returns>A code name. Just the <see cref="EndsWith"/> suffix if there are no Parts.</returns>
        /// <exception cref="InvalidOperationException">One of the Parts contains no words.</exception>
        public string Generate()
        {
            // Snapshot the configuration so a concurrent property change cannot
            // tear a single generation.
            var parts = _parts;
            var separator = _separator;
            var endsWith = _endsWith;
            var casing = Casing;
            var random = _seededRandom ?? SharedRandom;

            var name = new StringBuilder();
            for (var i = 0; i < parts.Length; i++)
            {
                var choices = parts[i].Get();
                if (choices.Length == 0)
                {
                    throw new InvalidOperationException(
                        "WordBank '" + parts[i] + "' (index " + i + ") contains no words.");
                }

                var part = choices[random.Next(choices.Length)];
                var partWords = part.Split(' ');
                for (var w = 0; w < partWords.Length; w++)
                {
                    var isFirstWord = name.Length == 0;
                    if (!isFirstWord)
                    {
                        name.Append(separator);
                    }

                    name.Append(ApplyCasing(partWords[w], casing, isFirstWord));
                }
            }

            return name.Append(endsWith).ToString();
        }

        /// <summary>
        /// Generates the specified number of code names.
        /// </summary>
        /// <param name="count">The number of code names to generate</param>
        /// <returns>An array of code names</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public string[] GenerateMany(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Count cannot be negative.");
            }

            var names = new string[count];
            for (var i = 0; i < count; i++)
            {
                names[i] = Generate();
            }

            return names;
        }

        /// <summary>
        /// Generates a code name that does not match any of the supplied reserved names.
        /// </summary>
        /// <param name="reserved">An array of names that should not match the generated code name</param>
        /// <returns>A unique code name</returns>
        /// <exception cref="InvalidOperationException">
        /// No unused code name was found within <see cref="UniqueAttemptLimit"/> attempts, which
        /// usually means the reserved set covers nearly every combination the current Parts allow.
        /// </exception>
        public string GenerateUnique(string[] reserved)
        {
            if (reserved == null || reserved.Length == 0)
            {
                return Generate();
            }

            var taken = new HashSet<string>(reserved, StringComparer.Ordinal);
            for (var attempt = 0; attempt < UniqueAttemptLimit; attempt++)
            {
                var name = Generate();
                if (!taken.Contains(name))
                {
                    return name;
                }
            }

            throw new InvalidOperationException(
                "Could not generate a code name outside the " + reserved.Length +
                " reserved names within " + UniqueAttemptLimit +
                " attempts. Add more Parts or larger WordBanks to widen the range of possible names.");
        }

        /// <summary>
        /// Applies the casing style to a single word, using invariant culture so that
        /// results do not change with the current thread's locale.
        /// </summary>
        /// <param name="word">The word to case.</param>
        /// <param name="casing">The casing style.</param>
        /// <param name="isFirstWord">Whether this is the first word of the code name.</param>
        /// <returns>The cased word.</returns>
        private static string ApplyCasing(string word, Casing casing, bool isFirstWord)
        {
            switch (casing)
            {
                case Casing.LowerCase:
                    return word.ToLowerInvariant();
                case Casing.UpperCase:
                    return word.ToUpperInvariant();
                case Casing.PascalCase:
                    return word.FirstCharToUpper();
                case Casing.CamelCase:
                    return isFirstWord ? word.ToLowerInvariant() : word.FirstCharToUpper();
                default:
                    return word;
            }
        }

#if NET6_0_OR_GREATER
        /// <summary>
        /// The thread-safe shared random source used when no seed was supplied.
        /// </summary>
        private static Random SharedRandom
        {
            get { return Random.Shared; }
        }
#else
        /// <summary>
        /// Counter used to give each thread's Random a distinct seed.
        /// </summary>
        private static int _seedCounter;

        [ThreadStatic]
        private static Random? _threadRandom;

        /// <summary>
        /// A per-thread random source, standing in for Random.Shared on frameworks
        /// that do not have it. Random instances are not thread-safe, so each thread
        /// gets its own, distinctly seeded.
        /// </summary>
        private static Random SharedRandom
        {
            get
            {
                var random = _threadRandom;
                if (random == null)
                {
                    var seed = Interlocked.Increment(ref _seedCounter) ^ Environment.TickCount;
                    random = new Random(seed);
                    _threadRandom = random;
                }

                return random;
            }
        }
#endif
    }
}
