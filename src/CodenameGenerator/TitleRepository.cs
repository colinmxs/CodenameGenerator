namespace CodenameGenerator
{
    public class TitleRepository : IWordRepository
    {
        private readonly string[] _titles = new string[]
        {
            "Mister",
            "Master",
            "Miss",
            "Mrs",
            "Lady",
            "Sir",
            "Madam",
            "Lord",
            "Dr",
            "Elder",
            "Grandpa",
            "Grandma",
            "President",
            "King",
            "Queen",
            "Princess",
            "Aunt",
            "Uncle"
        };

        public string[] Get()
        {
            return _titles;
        }   
    }
}
