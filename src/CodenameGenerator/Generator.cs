using System;


namespace CodenameGenerator
{
    public class Generator
    {
        private readonly Random _random = new Random();

        public Generator(string separator, Casing casing)
        {
            Separator = separator;
            Casing = casing;
        }

        public Generator(string separator, Casing casing, params WordBank[] banks)
        {
            Separator = separator;
            Casing = casing;
            Parts = banks;
        }

        public Generator()
        {
        }

        public string Separator = " ";
        public WordBank[] Parts = new WordBank[] { WordBank.Adjectives, WordBank.Nouns };
        public Casing Casing = Casing.LowerCase;
        public string Generate()
        {
            var name = "";
            for (int i = 0; i < Parts.Length; i++)
            {
                var repositoryContents = Parts[i].Get();                
                var index = _random.Next(repositoryContents.Length);
                var part = repositoryContents[index];
                var partWords = part.Split(' ');
                foreach (var partWord in partWords)
                {
                    var word = partWord;
                    switch (Casing)
                    {
                        case Casing.LowerCase:
                            word = word.ToLower();
                            break;
                        case Casing.UpperCase:
                            word = word.ToUpper();
                            break;
                        case Casing.PascalCase:
                            word = word.FirstCharToUpper();
                            break;
                        case Casing.CamelCase:
                            if (string.IsNullOrEmpty(name))
                            {
                                word = word.ToLower();
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
                return name.Remove(name.Length - Separator.Length);
            return name;
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