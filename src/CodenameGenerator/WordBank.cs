using CodenameGenerator.WordRepos;

namespace CodenameGenerator
{    
    //http://stackoverflow.com/questions/38855909/alternative-to-enum-in-design-pattern
    public class WordBank
    {
        protected readonly string Name;
        protected readonly Word Value;
        protected readonly WordRepository Repo;

        public static readonly WordBank Nouns = new WordBank(Word.Noun, "Nouns", new NounsRepository());
        public static readonly WordBank Adjectives = new WordBank(Word.Adjective, "Adjectives", new AdjectivesRepository());
        public static readonly WordBank FirstNames = new WordBank(Word.FirstName, "FirstNames", new FirstNamesRepository());
        public static readonly WordBank LastNames = new WordBank(Word.LastName, "LastNames", new LastNamesRepository());
        public static readonly WordBank Titles = new WordBank(Word.Title, "Titles", new TitleRepository());
        public static readonly WordBank MaleTitles = new WordBank(Word.MaleTitle, "MaleTitles", new MaleTitleRepository());
        public static readonly WordBank FemaleTitles = new WordBank(Word.FemaleTitle, "FemaleTitles", new FemaleTitleRepository());
        public static readonly WordBank Days = new WordBank(Word.Day, "Days", new DaysRepository());
        public static readonly WordBank FemaleFirstNames = new WordBank(Word.FemaleFirstName, "FemaleFirstNames", new FemaleFirstNameRepository());
        public static readonly WordBank MaleFirstNames = new WordBank(Word.MaleFirstName, "MaleFirstNames", new MaleFirstNameRepository());
        public static readonly WordBank Months = new WordBank(Word.Month, "Months", new MonthRepository());
        public static readonly WordBank StateNames = new WordBank(Word.StateName, "StateNames", new StateNamesRepository());
        public static readonly WordBank JobTitles = new WordBank(Word.JobTitle, "JobTitles", new JobTitlesRepository());
        public static readonly WordBank Countries = new WordBank(Word.Country, "Countries", new CountriesRepository());
        public static readonly WordBank Cities = new WordBank(Word.City, "Cities", new CitiesRepository());
        public static readonly WordBank Adverbs = new WordBank(Word.Adverb, "Adverbs", new AdverbsRepository());
        public static readonly WordBank Verbs = new WordBank(Word.Verb, "Verbs", new VerbsRepository());


        public WordBank(Word value, string name, WordRepository repo)
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
