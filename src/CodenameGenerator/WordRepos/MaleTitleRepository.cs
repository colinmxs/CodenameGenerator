namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// An arbitrary list of male titles.
    /// </summary>
    public class MaleTitleRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public MaleTitleRepository() : base(() => WordData.Load("MaleTitles"))
        {
        }
    }
}
