namespace CodenameGenerator.WordRepos
{
    public class MonthRepository : WordRepository
    {
        public MonthRepository() : base(
            new string[] {
                "January",
                "February",
                "March",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            }) { }
    }
}
