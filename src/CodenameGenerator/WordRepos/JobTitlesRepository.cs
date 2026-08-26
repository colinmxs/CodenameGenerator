namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A list of occupations. Some contain multiple words.
    /// </summary>
    public class JobTitlesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public JobTitlesRepository() : base(() => WordData.Load("JobTitles"))
        {
        }
    }
}
