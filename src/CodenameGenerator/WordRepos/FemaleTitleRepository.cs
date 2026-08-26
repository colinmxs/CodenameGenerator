namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// An arbitrary list of female titles.
    /// </summary>
    public class FemaleTitleRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public FemaleTitleRepository() : base(() => WordData.Load("FemaleTitles"))
        {
        }
    }
}
