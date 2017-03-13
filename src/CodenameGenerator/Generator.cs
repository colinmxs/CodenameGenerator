using System;

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
        }
        public string Separator { get; set; }
        public WordBank[] Parts { get; private set; }
        public string Generate()
        {
            var name = "";
            for (int i = 0; i < Parts.Length; i++)
            {
                var part = Parts[i];
                var words = part.Get();
                var index = _random.Next(words.Length);

                if (string.IsNullOrEmpty(name))
                    name = words[index];
                else
                    name += words[index];
                name += Separator;
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
    }
}
