using System;
using System.Linq;

namespace CodenameGenerator
{
    public class Generator
    {
        private readonly Random _random;
        public Generator()
        {
            Separator = " ";
            Parts = new WordBank[] { WordBank.Adjectives, WordBank.Nouns };
            _random = new Random();
            Casing = Casing.LowerCase;
        }
        public string Separator { get; set; }
        public WordBank[] Parts { get; private set; }
        public Casing Casing { get; set; }

        public string Generate()
        {
            var name = "";
            for (int i = 0; i < Parts.Length; i++)
            {
                var words = Parts[i].Get();                
                var index = _random.Next(words.Length);
                var word = words[index];

                switch (Casing)
                {
                    case Casing.LowerCase:
                        word = word.ToLower();
                        break;
                    case Casing.UpperCase:
                        word = word.ToUpper();
                        break;
                    case Casing.PascalCase:
                        word = FirstCharToUpper(word);
                        break;
                    case Casing.CamelCase:
                        if (string.IsNullOrEmpty(name))
                        {
                            word = word.ToLower();
                        }
                        else
                            word = FirstCharToUpper(word);
                        break;
                }
                
                if (string.IsNullOrEmpty(name))                    
                    name = word;
                else
                    name += word;
                name += Separator;
            }
            if (Separator.Length > 0)
                return name.Remove(name.Length - Separator.Length);
            return name;
        }

        //http://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-for-maximum-performance
        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public string[] GenerateMany(int count)
        {
            var names = new string[count];
            var i = 0;
            while(i < count)
            {
                names[i] = Generate();
                i++;
            }
            return names;
        }                

        public void SetParts(params WordBank[] wordBanks)
        {
            Parts = wordBanks;
        }

        public string GenerateUnique(string[] reserved)
        {
            var name = Generate();
            if (Array.Exists(reserved, element => element == name))
            {
                return GenerateUnique(reserved);
            }
            return name;
        }        
    }
}
