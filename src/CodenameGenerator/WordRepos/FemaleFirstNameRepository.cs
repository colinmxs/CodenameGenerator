namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A list of female first names, western origin.
    /// </summary>
    public class FemaleFirstNameRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public FemaleFirstNameRepository() : base(() => WordData.Load("FemaleFirstNames"))
        {
        }
    }
}
