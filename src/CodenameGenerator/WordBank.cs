using CodenameGenerator.WordRepos;

namespace CodenameGenerator
{    
    //http://stackoverflow.com/questions/38855909/alternative-to-enum-in-design-pattern
    public class WordBank
    {
        protected readonly string Name;
        protected readonly Word Value;
        protected readonly IStringRepository Repo;

        public static readonly WordBank Nouns = new WordBank(Word.Noun, "Nouns", new FileRepository("nouns.csv"));
        public static readonly WordBank Adjectives = new WordBank(Word.Adjective, "Adjectives", new FileRepository("adjectives.csv"));
        public static readonly WordBank FirstNames = new WordBank(Word.FirstName, "FirstNames", new FileRepository("firstnames.csv"));
        public static readonly WordBank LastNames = new WordBank(Word.LastName, "LastNames", new FileRepository("lastnames.csv"));
        public static readonly WordBank Titles = new WordBank(Word.Title, "Titles", new TitleRepository());
        public static readonly WordBank Days = new WordBank(Word.Day, "Day", new DaysRepository());
        public static readonly WordBank FemaleFirstNames = new WordBank(Word.FemaleFirstName, "FemaleFirstName", new FemaleFirstNameRepository());
        public static readonly WordBank MaleFirstNames = new WordBank(Word.MaleFirstName, "MaleFirstName", new MaleFirstNameRepository());
        public static readonly WordBank Months = new WordBank(Word.Month, "Month", new MonthRepository());
        public static readonly WordBank StateNames = new WordBank(Word.StateName, "StateNames", new StateNamesRepository());

        public WordBank(Word value, string name, IStringRepository repo)
        {
            Name = name;
            Value = value;
            Repo = repo;
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator Word(WordBank @enum)
        {
            return @enum.Value;
        }

        public static implicit operator string(WordBank @enum)
        {
            return @enum.Name;
        }

        public string[] Get()
        {
            return Repo.Get();
        }
    }
}
