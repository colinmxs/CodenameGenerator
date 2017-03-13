using System;

namespace CodenameGenerator
{    
    //http://stackoverflow.com/questions/38855909/alternative-to-enum-in-design-pattern
    public class WordBank
    {
        protected readonly string Name;
        protected readonly Word Value;
        protected readonly IWordRepository Repo;

        public static readonly WordBank Nouns = new WordBank(Word.Nouns, "Nouns", new FileRepository("nouns.csv"));
        public static readonly WordBank Adjectives = new WordBank(Word.Adjectives, "Adjectives", new FileRepository("adjectives.csv"));
        public static readonly WordBank FirstNames = new WordBank(Word.FirstNames, "FirstNames", new FileRepository("firstnames.csv"));
        public static readonly WordBank LastNames = new WordBank(Word.LastNames, "LastNames", new FileRepository("lastnames.csv"));
        public static readonly WordBank Titles = new WordBank(Word.Titles, "Titles", new TitleRepository());

        protected WordBank(Word value, string name, IWordRepository repo)
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
