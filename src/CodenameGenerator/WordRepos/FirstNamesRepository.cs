namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A list of first names, all genders.
    /// </summary>
    public class FirstNamesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public FirstNamesRepository() : base(() => WordData.Load("FirstNames"))
        {
        }
    }
}
