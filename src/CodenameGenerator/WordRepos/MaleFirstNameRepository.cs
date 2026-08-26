namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A list of male first names, western origin.
    /// </summary>
    public class MaleFirstNameRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public MaleFirstNameRepository() : base(() => WordData.Load("MaleFirstNames"))
        {
        }
    }
}
