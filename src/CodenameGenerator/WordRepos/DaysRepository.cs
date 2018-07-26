namespace CodenameGenerator.WordRepos
{
    public class DaysRepository : WordRepository
    {
        public DaysRepository() : base(
            new string[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"}) { }
    }
}
