namespace CodenameGenerator
{
    using System;

    /// <summary>
    /// Supplies the words a <see cref="WordBank"/> draws from.
    /// </summary>
    /// <remarks>
    /// Words are resolved lazily and cached, so declaring a repository is cheap:
    /// nothing is read or allocated until <see cref="Get"/> is first called.
    /// Instances are safe to share between threads.
    /// </remarks>
    public class WordRepository
    {
        private readonly Lazy<string[]> _words;

        /// <summary>
        /// Creates a repository over an in-memory list of words.
        /// </summary>
        /// <param name="words">The words to draw from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="words"/> is null.</exception>
        public WordRepository(string[] words)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            _words = new Lazy<string[]>(() => words);
        }

        /// <summary>
        /// Creates a repository whose words are produced on first use.
        /// </summary>
        /// <param name="wordFactory">Factory invoked once, on first call to <see cref="Get"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="wordFactory"/> is null.</exception>
        protected WordRepository(Func<string[]> wordFactory)
        {
            if (wordFactory == null)
            {
                throw new ArgumentNullException(nameof(wordFactory));
            }

            _words = new Lazy<string[]>(wordFactory);
        }

        /// <summary>
        /// The words in this repository, loading them on first call.
        /// </summary>
        /// <remarks>
        /// Returns the repository's own array rather than a copy, to keep generation
        /// allocation-free. Treat it as read-only; mutating it affects every
        /// <see cref="Generator"/> sharing this repository.
        /// </remarks>
        /// <returns>The words.</returns>
        public string[] Get()
        {
            return _words.Value;
        }
    }
}
