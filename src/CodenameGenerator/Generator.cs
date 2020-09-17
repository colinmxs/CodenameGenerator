namespace CodenameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IGenerator
    {
        string Separator { get; set; }
        IEnumerable<WordBank> Parts { get; set; }
        Casing Casing { get; set; }
        string EndsWith { get; set; }
        void SetParts(params WordBank[] wordBanks);
        string Generate();
        IEnumerable<string> GenerateMany(int count);
        string GenerateUnique(IEnumerable<string> reserved);
    }
    public class Generator : IGenerator
    {
        private readonly Random _random;
        private string _separator;
        private IEnumerable<WordBank> _parts;
        private string _endsWith;

        /// <summary>
        /// String value inserted between each word.
        /// </summary>
        public string Separator
        {
            get
            {
                return _separator;
            }
            set
            {
                if (value == null) value = "";
                _separator = value;
            }
        }

        /// <summary>
        /// WordBanks which will be accessed sequentially (by order of array index) to provide words for code name generation.
        /// </summary>
        public IEnumerable<WordBank> Parts
        {
            get
            {
                return _parts;
            }
            set
            {
                if (value == null) value = new WordBank[0];
                _parts = value;
            }
        }

        /// <summary>
        /// The casing format for generated code names.
        /// </summary>
        public Casing Casing { get; set; }

        /// <summary>
        /// String value used as a suffix for the generated code name.
        /// </summary>
        public string EndsWith
        {
            get
            {
                return _endsWith;
            }
            set
            {
                if (value == null) value = "";
                _endsWith = value;
            }
        }

        public Generator(string separator = " ", Casing casing = Casing.LowerCase, int? seed = null)
        {
            Parts = new WordBank[] { WordBank.Adjectives, WordBank.Nouns };
            Separator = separator;
            Casing = casing;
            _random = (seed == null) ? new Random() : new Random(seed.Value);
            EndsWith = "";
        }

        public Generator(string separator, Casing casing, params WordBank[] wordBanks) : this(separator, casing)
        {
            Parts = wordBanks;
        }

        public void SetParts(params WordBank[] wordBanks)
        {
            Parts = wordBanks;
        }                

        public string Generate()
        {
            var name = string.Empty;
            foreach (var parts in Parts)
            {
                var repositoryContents = parts.Get();                
                var index = _random.Next(repositoryContents.Length);
                var rawWord = repositoryContents[index];
                var splitWord = rawWord.Split(' ');
                foreach (var partWord in splitWord)
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
                name = name.Remove(name.Length - Separator.Length);
            return name + EndsWith;
        }

        public IEnumerable<string> GenerateMany(int count)
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
        
        public string GenerateUnique(IEnumerable<string> reserved)
        {
            var name = Generate();
            if (reserved.Any(r => r == name))
            {
                return GenerateUnique(reserved);
            }
            return name;
        }        
    }
}