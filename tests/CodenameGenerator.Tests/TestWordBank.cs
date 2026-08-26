namespace CodenameGenerator.Tests
{
    /// <summary>
    /// Small, deterministic stand-ins for the built-in banks. Two words each, so a
    /// test can assert on the exact set of possible outputs.
    /// </summary>
    public class TestWordBank : WordBank
    {
        public static readonly new WordBank Nouns = new WordBank(Word.Noun, "Nouns", new WordRepository(new string[] { "dog", "cat" }));
        public static readonly new WordBank Adjectives = new WordBank(Word.Adjective, "Adjectives", new WordRepository(new string[] { "hairy", "dirty" }));
        public static readonly new WordBank FirstNames = new WordBank(Word.FirstName, "FirstNames", new WordRepository(new string[] { "David", "Roger" }));
        public static readonly new WordBank LastNames = new WordBank(Word.LastName, "LastNames", new WordRepository(new string[] { "Smith", "Jones" }));
        public static readonly new WordBank Titles = new WordBank(Word.Title, "Titles", new WordRepository(new string[] { "Aunt", "Uncle" }));
        public static readonly new WordBank JobTitles = new WordBank(Word.JobTitle, "JobTitles", new WordRepository(new string[] { "Airline Traffic Controller", "Hospital Man" }));

        /// <summary>A bank with no words at all, for error-path tests.</summary>
        public static readonly WordBank Empty = new WordBank(Word.Noun, "Empty", new WordRepository(new string[0]));

        public TestWordBank(Word value, string name, WordRepository repo) : base(value, name, repo)
        {
        }
    }

    /// <summary>
    /// A repository that records how many times its word list has actually been
    /// produced, so tests can prove loading is lazy and cached.
    /// </summary>
    public sealed class CountingRepository : WordRepository
    {
        private readonly int[] _loads;

        private CountingRepository(int[] loads, string[] words)
            : base(() =>
            {
                loads[0]++;
                return words;
            })
        {
            _loads = loads;
        }

        /// <summary>How many times the word list has been produced.</summary>
        public int Loads
        {
            get { return _loads[0]; }
        }

        public static CountingRepository Create(params string[] words)
        {
            return new CountingRepository(new int[1], words);
        }
    }
}
