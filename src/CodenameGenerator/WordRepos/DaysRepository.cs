namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// The days of the week.
    /// </summary>
    public class DaysRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public DaysRepository() : base(() => WordData.Load("Days"))
        {
        }
    }
}
