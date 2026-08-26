namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// A sturdy list of adjectives, common and uncommon.
    /// </summary>
    public class AdjectivesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public AdjectivesRepository() : base(() => WordData.Load("Adjectives"))
        {
        }
    }
}
