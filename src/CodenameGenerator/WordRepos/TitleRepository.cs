namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// An arbitrary list of titles, all genders.
    /// </summary>
    public class TitleRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public TitleRepository() : base(() => WordData.Load("Titles"))
        {
        }
    }
}
