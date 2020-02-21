namespace CodenameGenerator.Lite
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    public class Generator
    {
        private readonly Random random;

        private string _separator;
        /// <summary>
        /// Value used to separate the different words that make up a generated code name.
        /// </summary>
        public string Separator
        {
            get
            {
                return _separator;
            }
            set
            {
                _separator = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private WordBank[] _parts;
        /// <summary>
        /// WordBanks which will be accessed sequentially to provide words for code name generation.
        /// </summary>
        public WordBank[] Parts
        {
            get
            {
                return _parts;
            }
            set
            {
                _parts = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// The casing format for generated code names.
        /// </summary>
        public Casing Casing { get; set; }

        private string _endsWith;
        /// <summary>
        /// String value used as a suffix for the generated code name.
        /// </summary>
        public string EndsWith
        {
            get
            {
                return _endsWith;
            }
            set
            {
                _endsWith = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private Generator(int? seed = null)
        {
            random = (seed == null) ? new Random() : new Random(seed.Value);
        }

        public Generator(string separator = " ", Casing casing = Casing.LowerCase, int? seed = null, params WordBank[] wordBanks) : this(seed)
        {
            Separator = separator;
            Casing = casing;
            Parts = wordBanks;
            EndsWith = "";
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
        /// <returns>A code name</returns>
        public async Task<string> GenerateAsync()
        {
            // get indexes
            var indexes = new int[Parts.Length];
            for (int i = 0; i < Parts.Length; i++)
            {
                indexes[i] = random.Next(Parts[i].WordCount);
            }

            // get words
            var wordTasks = new List<Task<string>>();
            var words = new string[Parts.Length];

            for (int i = 0; i < Parts.Length; i++)
            {
                wordTasks.Add(Parts[i].GetWordAsync(indexes[i]));
            }

            words = await Task.WhenAll(wordTasks).ConfigureAwait(false);

            // apply casing and add separator

            var name = string.Empty;
            for (int i = 0; i < words.Length; i++)
            {
                // some "words" from the wordbank contain spaces. split here so we can handle casing properly
                var partWords = words[i].Split(' ');
                foreach (var partWord in partWords)
                {
                    var word = partWord;
                    switch (Casing)
                    {
                        case Casing.LowerCase:
                            word = word.ToLower(CultureInfo.InvariantCulture);
                            break;
                        case Casing.UpperCase:
                            word = word.ToUpperInvariant();
                            break;
                        case Casing.PascalCase:
                            word = word.FirstCharToUpper();
                            break;
                        case Casing.CamelCase:
                            if (string.IsNullOrEmpty(name))
                            {
                                word = word.ToLower(CultureInfo.InvariantCulture);
                            }
                            else
                                word = word.FirstCharToUpper();
                            break;
                    }

                    if (string.IsNullOrEmpty(name))
                        name = word;
                    else
                        name += word;
                    name += Separator;
                }
            }
            if (Separator.Length > 0)
                name = name.Remove(name.Length - Separator.Length);
            return name + EndsWith;
        }

        /// <summary>
        /// Generates the specified number of code names.
        /// </summary>
        /// <param name="count">The number of code names to generate</param>
        /// <returns>An array of code names</returns>
        public async Task<string[]> GenerateMany(int count)
        {
            var names = new string[count];
            var i = 0;
            while (i < count)
            {
                names[i] = await GenerateAsync().ConfigureAwait(false);
                i++;
            }
            return names;
        }

        /// <summary>
        /// Generates a code name that does not match any of the supplied reserved names.
        /// </summary>
        /// <param name="reserved">An array of names that should not match the generated code name</param>
        /// <returns>A unique code name</returns>
        public async Task<string> GenerateUniqueAsync(string[] reserved)
        {
            var name = await GenerateAsync().ConfigureAwait(false);
            if (Array.Exists(reserved, element => element == name))
            {
                return await GenerateUniqueAsync(reserved).ConfigureAwait(false);
            }
            return name;
        }
    }
}