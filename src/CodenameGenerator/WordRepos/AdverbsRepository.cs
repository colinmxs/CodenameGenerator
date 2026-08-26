namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A giant list of adverbs.
    /// </summary>
    public class AdverbsRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public AdverbsRepository() : base(() => WordData.Load("Adverbs"))
        {
        }
    }
}
