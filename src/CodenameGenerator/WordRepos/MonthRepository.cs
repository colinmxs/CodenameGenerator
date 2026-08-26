namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// The months of the year.
    /// </summary>
    public class MonthRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public MonthRepository() : base(() => WordData.Load("Months"))
        {
        }
    }
}
