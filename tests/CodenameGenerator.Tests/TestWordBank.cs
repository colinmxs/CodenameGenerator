namespace CodenameGenerator.Tests
{
    public class TestWordBank : WordBank
    {
        public static readonly new WordBank Nouns = new WordBank(Word.Noun, "Nouns", new WordRepository(new string[] { "dog", "cat" }));
        public static readonly new WordBank Adjectives = new WordBank(Word.Adjective, "Adjectives", new WordRepository(new string[] { "hairy", "dirty" }));
        public static readonly new WordBank FirstNames = new WordBank(Word.FirstName, "FirstNames", new WordRepository(new string[] { "David", "Roger" }));
        public static readonly new WordBank LastNames = new WordBank(Word.LastName, "LastNames", new WordRepository(new string[] { "Smith", "Jones" }));
        public static readonly new WordBank Titles = new WordBank(Word.Title, "Titles", new WordRepository(new string[] { "Aunt", "Uncle" }));
        public static readonly new WordBank JobTitles = new WordBank(Word.JobTitle, "JobTitles", new WordRepository(new string[] { "Airline Traffic Controller", "Hospital Man" }));
        
        public TestWordBank(Word value, string name, WordRepository repo) : base(value, name, repo)
        {
        }
    }
}
