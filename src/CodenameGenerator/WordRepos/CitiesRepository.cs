namespace CodenameGenerator.WordRepos
{
    /// <summary>
    /// An expansive list of cities on Earth.
    /// </summary>
    public class CitiesRepository : WordRepository
    {
        /// <summary>
        /// Creates the repository. The word list is loaded from an embedded
        /// resource on first use, not when this instance is created.
        /// </summary>
        public CitiesRepository() : base(() => WordData.Load("Cities"))
        {
        }
    }
}
