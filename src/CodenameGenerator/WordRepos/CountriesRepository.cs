namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A list of countries on Earth. Likely outdated.
    /// </summary>
    public class CountriesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public CountriesRepository() : base(() => WordData.Load("Countries"))
        {
        }
    }
}
