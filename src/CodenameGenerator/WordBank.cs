namespace CodenameGenerator
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CodenameGenerator.WordRepos;

    /// <summary>
    /// A named source of words for code name generation. Behaves like an extensible
    /// enum: use one of the built-in static instances, or create your own over a
    /// custom <see cref="WordRepository"/>.
    /// </summary>
    /// <remarks>
    /// The built-in banks load their words from embedded resources on first use, so
    /// referencing a bank costs nothing until you generate from it.
    /// See http://stackoverflow.com/questions/38855909/alternative-to-enum-in-design-pattern
    /// </remarks>
    public class WordBank
    {
        /// <summary>The bank's name, e.g. "Nouns".</summary>
        protected readonly string Name;

        /// <summary>The category of word this bank supplies.</summary>
        protected readonly Word Value;

        /// <summary>The backing word source.</summary>
        protected readonly WordRepository Repo;

        /// <summary>
        /// A metric butt-ton of nouns, ranging from common to...not so common.
        /// </summary>
        public static readonly WordBank Nouns = new WordBank(Word.Noun, "Nouns", new NounsRepository());

        /// <summary>
        /// A sturdy list of adjectives. Common and uncommon.
        /// </summary>
        public static readonly WordBank Adjectives = new WordBank(Word.Adjective, "Adjectives", new AdjectivesRepository());

        /// <summary>
        /// A list of first names. All genders.
        /// </summary>
        public static readonly WordBank FirstNames = new WordBank(Word.FirstName, "FirstNames", new FirstNamesRepository());

        /// <summary>
        /// A list of last names.
        /// </summary>
        public static readonly WordBank LastNames = new WordBank(Word.LastName, "LastNames", new LastNamesRepository());

        /// <summary>
        /// An arbitrary list of titles. All genders:
        /// "President", "Mrs", "Doctor", etc...
        /// </summary>
        public static readonly WordBank Titles = new WordBank(Word.Title, "Titles", new TitleRepository());

        /// <summary>
        /// An arbitrary list of male titles:
        /// "Master", "Sir", "Lord", etc...
        /// </summary>
        public static readonly WordBank MaleTitles = new WordBank(Word.MaleTitle, "MaleTitles", new MaleTitleRepository());

        /// <summary>
        /// An arbitrary list of female titles:
        /// "Madam", "Mrs", "Grandma", etc...
        /// </summary>
        public static readonly WordBank FemaleTitles = new WordBank(Word.FemaleTitle, "FemaleTitles", new FemaleTitleRepository());

        /// <summary>
        /// The days of the week.
        /// </summary>
        public static readonly WordBank Days = new WordBank(Word.Day, "Days", new DaysRepository());

        /// <summary>
        /// A list of female first names. Western origin.
        /// </summary>
        public static readonly WordBank FemaleFirstNames = new WordBank(Word.FemaleFirstName, "FemaleFirstNames", new FemaleFirstNameRepository());

        /// <summary>
        /// A list of male first names. Western origin.
        /// </summary>
        public static readonly WordBank MaleFirstNames = new WordBank(Word.MaleFirstName, "MaleFirstNames", new MaleFirstNameRepository());

        /// <summary>
        /// The months of the year.
        /// </summary>
        public static readonly WordBank Months = new WordBank(Word.Month, "Months", new MonthRepository());

        /// <summary>
        /// The state names for all 50 U.S. states.
        /// </summary>
        public static readonly WordBank StateNames = new WordBank(Word.StateName, "StateNames", new StateNamesRepository());

        /// <summary>
        /// A list of occupations. Some of the job titles contain multiple words.
        /// </summary>
        public static readonly WordBank JobTitles = new WordBank(Word.JobTitle, "JobTitles", new JobTitlesRepository());

        /// <summary>
        /// A list of countries on Earth. Likely outdated.
        /// </summary>
        public static readonly WordBank Countries = new WordBank(Word.Country, "Countries", new CountriesRepository());

        /// <summary>
        /// An EXPANSIVE list of cities on Earth. Contains ~48K cities.
        /// </summary>
        public static readonly WordBank Cities = new WordBank(Word.City, "Cities", new CitiesRepository());

        /// <summary>
        /// A giant list of adverbs.
        /// </summary>
        public static readonly WordBank Adverbs = new WordBank(Word.Adverb, "Adverbs", new AdverbsRepository());

        /// <summary>
        /// A humongous list of verbs. This is my favorite!
        /// </summary>
        public static readonly WordBank Verbs = new WordBank(Word.Verb, "Verbs", new VerbsRepository());

        /// <summary>
        /// Every built-in word bank. Declared last so that all of the static fields
        /// above are initialized by the time this runs.
        /// </summary>
        public static readonly IReadOnlyList<WordBank> All = new ReadOnlyCollection<WordBank>(
            new[]
            {
                Adjectives, Adverbs, Cities, Countries, Days, FemaleFirstNames,
                FemaleTitles, FirstNames, JobTitles, LastNames, MaleFirstNames,
                MaleTitles, Months, Nouns, StateNames, Titles, Verbs,
            });

        /// <summary>
        /// Creates a word bank.
        /// </summary>
        /// <param name="value">The category of word this bank supplies.</param>
        /// <param name="name">The bank's name.</param>
        /// <param name="repo">The word source.</param>
        public WordBank(Word value, string name, WordRepository repo)
        {
            Name = name;
            Value = value;
            Repo = repo;
        }

        /// <summary>
        /// The bank's name.
        /// </summary>
        /// <returns>The name, e.g. "Nouns".</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Converts a bank to the <see cref="Word"/> category it supplies.
        /// </summary>
        /// <param name="bank">The bank.</param>
        public static implicit operator Word(WordBank bank)
        {
            return bank.Value;
        }

        /// <summary>
        /// Converts a bank to its name.
        /// </summary>
        /// <param name="bank">The bank.</param>
        public static implicit operator string(WordBank bank)
        {
            return bank.Name;
        }

        /// <summary>
        /// The words in this bank, loading them on first call.
        /// </summary>
        /// <returns>The words. Treat the array as read-only; it is shared.</returns>
        public string[] Get()
        {
            return Repo.Get();
        }
    }
}
