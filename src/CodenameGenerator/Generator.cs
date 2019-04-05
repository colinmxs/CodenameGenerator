namespace CodenameGenerator
{
    using System;

    public class Generator
    {
        private readonly Random random;

        /// <summary>
        /// String value used to separate the different words that make up a generated code name.
        /// </summary>
        private string _separator;
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
        /// Array of WordBanks which will be accessed sequentially (by order of array index) to provide words for code name generation.
        /// </summary>
        private WordBank[] _parts;
        public WordBank[] Parts
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
        private string _endsWith;
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

        /// <summary>
        /// Default constructor. If called w/o ctor parameters, resulting Generator object will be created with the default Parts, Separator, and Casing properties.
        /// </summary>
        /// <param name="separator">The Separator</param>
        /// <param name="casing">The Casing</param>
        /// <param name="seed">The Random Generator Seed</param>
        public Generator(string separator = " ", Casing casing = Casing.LowerCase, int? seed = null)
        {
            Parts = new WordBank[] { WordBank.Adjectives, WordBank.Nouns };
            Separator = separator;
            Casing = casing;
            random = (seed == null) ? new Random() : new Random(seed.Value);
            EndsWith = "";
        }

        /// <summary>
        /// Constructor offering params for specifying WordBanks.
        /// </summary>
        /// <param name="separator">The Separator</param>
        /// <param name="casing">The Casing</param>
        /// <param name="wordBanks">Comma-delimited WordBank instances</param>
        /// <example>
        /// new Generator("-", Casing.LowerCase, WordBank.Titles, WordBank.FirstNames, WordBank.LastNames)
        /// </example>
        public Generator(string separator, Casing casing, params WordBank[] wordBanks) : this(separator, casing)
        {
            Parts = wordBanks;
        }

        /// <summary>
        /// params-based Setter method for the Parts property.
        /// </summary>
        /// <param name="wordBanks">Comma-delimited WordBank instances</param>
        /// <example>
        /// generator.SetParts(WordBank.Adjectives, WordBank.Nouns)
        /// </example>
        public void SetParts(params WordBank[] wordBanks)
        {
            Parts = wordBanks;
        }                

        /// <summary>
        /// Generates a code name based on current configuration of Separator, Parts, and Casing properties.
        /// </summary>
        /// <returns>A code name</returns>
        public string Generate()
        {
            var name = string.Empty;
            for (int i = 0; i < Parts.Length; i++)
            {
                var repositoryContents = Parts[i].Get();                
                var index = random.Next(repositoryContents.Length);
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
                name = name.Remove(name.Length - Separator.Length);
            return name + EndsWith;
        }

        /// <summary>
        /// Generates the specified number of code names.
        /// </summary>
        /// <param name="count">The number of code names to generate</param>
        /// <returns>An array of code names</returns>
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
        
        /// <summary>
        /// Generates a code name that does not match any of the supplied reserved names.
        /// </summary>
        /// <param name="reserved">An array of names that should not match the generated code name</param>
        /// <returns>A unique code name</returns>
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