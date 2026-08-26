namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// The state names for all 50 U.S. states.
    /// </summary>
    public class StateNamesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public StateNamesRepository() : base(() => WordData.Load("StateNames"))
        {
        }
    }
}
