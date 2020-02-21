namespace CodenameGenerator.Lite
{
    using System;
    using System.Threading.Tasks;

    public abstract class WordBank
    {
        /// <summary>
        /// Common and uncommon nouns.
        /// </summary>
        public static readonly WordBank Nouns = new FileBackedWordBank("nouns.txt", 82149);

        /// <summary>
        /// A list of last names.
        /// </summary>
        public static readonly WordBank LastNames = new FileBackedWordBank("last-names.txt", 88799);

        /// <summary>
        /// An arbitrary list of titles. All genders:
        /// "President", "Mrs", "Doctor", etc...
        /// </summary>
        public static readonly WordBank Titles = new ArrayBackedWordBank(new string[] {"Mister","Master","Miss","Mrs","Lady","Sir","Madam","Lord","Dr","Elder","Grandpa","Grandma","President","King","Queen","Princess","Aunt","Uncle"});

        /// <summary>
        /// An arbitrary list of male titles:
        /// "Master", "Sir", "Lord", etc...
        /// </summary>
        public static readonly WordBank MaleTitles = new ArrayBackedWordBank(new string[10] {"Mister","Master","Sir","Lord","Dr","Elder","Grandpa","President","King","Uncle"});

        /// <summary>
        /// A list of male first names. Western origin.
        /// </summary>
        public static readonly WordBank MaleFirstNames = new FileBackedWordBank("male-first-names.txt", 3898);

        /// <summary>
        /// The months of the year.
        /// </summary>
        public static readonly WordBank Months = new ArrayBackedWordBank(new string[12] {"January","February","March","April","May","June","July","August","September","October","November","December"});

        /// <summary>
        /// The state names for all 50 U.S. states.
        /// </summary>
        public static readonly WordBank StateNames = new FileBackedWordBank("us-states.txt", 50);

        /// <summary>
        /// A list of occupations. Some of the job titles contain multiple words.
        /// </summary>
        public static readonly WordBank JobTitles = new FileBackedWordBank("job-titles.txt", 963);

        /// <summary>
        // A list of first names. All genders.
        /// </summary>
        public static readonly WordBank FirstNames = new FileBackedWordBank("first-names.txt", 5494);

        /// <summary>
        /// An arbitrary list of female titles:
        /// "Madam", "Mrs", "Grandma", etc...
        /// </summary>
        public static readonly WordBank FemaleTitles = new ArrayBackedWordBank(new string[] {"Master","Miss","Mrs","Lady","Madam","Dr","Elder","Grandma","President","Queen","Princess","Aunt"});

        /// <summary>
        /// A list of female first names. Western origin.
        /// </summary>
        public static readonly WordBank FemaleFirstNames = new FileBackedWordBank("female-first-names.txt", 4951);

        /// <summary>
        /// The days of the week.
        /// </summary>
        public static readonly WordBank Days = new ArrayBackedWordBank(new string[7] {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"});

        /// <summary>
        /// Countries on Earth.
        /// </summary>
        public static readonly WordBank Countries = new FileBackedWordBank("countries.txt", 205);

        /// <summary>
        /// ~50K city names.
        /// </summary>
        public static readonly WordBank Cities = new FileBackedWordBank("cities.txt", 47981);

        /// <summary>
        /// Common and uncommon adverbs.
        /// </summary>
        public static readonly WordBank Adverbs = new FileBackedWordBank("adverbs.txt", 6251);

        /// <summary>
        /// Common and uncommon adjectives.
        /// </summary>
        public static readonly WordBank Adjectives = new FileBackedWordBank("adjectives.txt", 27320);

        /// <summary>
        /// Common and uncommon verbs.
        /// </summary>
        public static readonly WordBank Verbs = new FileBackedWordBank("verbs.txt", 30618);

        protected internal int WordCount { get; protected set; }
        internal abstract Task<string> GetWordAsync(int index);
    }
}
