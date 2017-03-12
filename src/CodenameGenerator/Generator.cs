using System;

namespace CodenameGenerator
{
    public enum Word
    {
        Nouns,
        Adjectives,
        FirstNames,
        LastNames,
        Titles
    }
    //http://stackoverflow.com/questions/38855909/alternative-to-enum-in-design-pattern
    public class WordBank
    {
        protected readonly string Name;
        protected readonly Word Value;
        protected readonly IWordRepository Repo;

        public static readonly WordBank Nouns = new WordBank(Word.Nouns, "Nouns", null);
        public static readonly WordBank Adjectives = new WordBank(Word.Adjectives, "Adjectives", null);
        public static readonly WordBank FirstNames = new WordBank(Word.FirstNames, "FirstNames", null);
        public static readonly WordBank LastNames = new WordBank(Word.LastNames, "LastNames", null);
        public static readonly WordBank Titles = new WordBank(Word.Titles, "Titles", null);

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
            throw new NotImplementedException();
        }
    }
    public class Generator
    {
        public char Separator { get; private set; }
        public WordBank[] Parts { get; private set; }
        public string Generate()
        {
            throw new NotImplementedException();
        }

        public string[] GenerateMany(int count)
        {
            throw new NotImplementedException();
        }        

        public void SetSeparator(char separator)
        {
            throw new NotImplementedException();
        }
    }
}
