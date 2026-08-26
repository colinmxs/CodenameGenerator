namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A metric butt-ton of nouns, ranging from common to not so common.
    /// </summary>
    public class NounsRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public NounsRepository() : base(() => WordData.Load("Nouns"))
        {
        }
    }
}
