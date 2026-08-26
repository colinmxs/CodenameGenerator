namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A humongous list of verbs.
    /// </summary>
    public class VerbsRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public VerbsRepository() : base(() => WordData.Load("Verbs"))
        {
        }
    }
}
