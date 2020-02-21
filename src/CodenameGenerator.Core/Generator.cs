namespace CodenameGenerator.Core
{
    using System;
    using System.Collections.Generic;

    public class Generator
    {
        private readonly Random random;

        private string _separator;
        /// <summary>
        /// Value used to separate the different words that make up a generated code name.
        /// </summary>
        public string Separator
        {
            get
            {
                return _separator;
            }
            set
            {
                _separator = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private IWordBank[] _parts;
        /// <summary>
        /// WordBanks which will be accessed sequentially to provide words for code name generation.
        /// </summary>
        public IWordBank[] Parts
        {
            get
            {
                return _parts;
            }
            set
            {
                _parts = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// The casing format for generated code names.
        /// </summary>
        public Casing Casing { get; set; }

        private string _endsWith;
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
                _endsWith = value ?? throw new ArgumentNullException(nameof(value)); 
            }
        }

        /// <summary>
        /// Default constructor. If called w/o ctor parameters, resulting Generator object will be created with the default Parts, Separator, and Casing properties.
        /// </summary>
        /// <param name="separator">The Separator</param>
        /// <param name="casing">The Casing</param>
        /// <param name="seed">The Random Generator Seed</param>
        public Generator(string separator = " ", Casing casing = Casing.LowerCase, int? seed = null, params IWordBank[] wordBanks)
        {
            Separator = separator;
            Casing = casing;
            random = (seed == null) ? new Random() : new Random(seed.Value);
            Parts = wordBanks;
            EndsWith = "";
        }

        /// <summary>
        /// params-based Setter method for the Parts property.
        /// </summary>
        /// <param name="wordBanks">Comma-delimited WordBank instances</param>
        /// <example>
        /// generator.SetParts(WordBank.Adjectives, WordBank.Nouns)
        /// </example>
        public void SetParts(params IWordBank[] wordBanks)
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
                var part = Parts[i].GetWord(random);
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