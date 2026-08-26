namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A list of last names.
    /// </summary>
    public class LastNamesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public LastNamesRepository() : base(() => WordData.Load("LastNames"))
        {
        }
    }
}
