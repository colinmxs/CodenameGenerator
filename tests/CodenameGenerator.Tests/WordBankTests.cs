using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CodenameGenerator.Tests
{
    [TestClass]
    public class WordBankTests
    {
        [TestMethod]
        public void WordBanks()
        {
            var nouns = WordBank.Nouns;
            var adjectives = WordBank.Adjectives;
            var firstNames = WordBank.FirstNames;
            var lastNames = WordBank.LastNames;
            var titles = WordBank.Titles;
            var femaleTitles = WordBank.FemaleTitles;
            var maleTitles = WordBank.MaleTitles;
            var stateNames = WordBank.StateNames;
            var days = WordBank.Days;
            var femaleFirstNames = WordBank.FemaleFirstNames;
            var maleFirstNames = WordBank.MaleFirstNames;
            var months = WordBank.Months;
            var jobTitles = WordBank.JobTitles;
        }
        [TestMethod]
        public void Nouns()
        {
            var nounBank = WordBank.Nouns;
            string[] nouns = nounBank.Get();
            Assert.IsNotNull(nouns);
            //lol
            Assert.IsTrue(!nouns.Contains("dilapidated"));
            Assert.IsTrue(nouns.Contains("dog"));
        }
        [TestMethod]        
        public void Adjectives()
        {
            var adjectiveBank = WordBank.Adjectives;
            string[] adjectives = adjectiveBank.Get();
            Assert.IsNotNull(adjectives);
            //lol
            Assert.IsTrue(adjectives.Contains("dirty"));
            Assert.IsTrue(!adjectives.Contains("hospital"));
        }
        [TestMethod]
        public void FirstNames()
        {
            var firstNamesBank = WordBank.FirstNames;
            string[] firstNames = firstNamesBank.Get();
            Assert.IsNotNull(firstNames);
            //lol
            Assert.IsTrue(firstNames.Contains("Dan"));
            Assert.IsTrue(!firstNames.Contains("horse"));
        }
        [TestMethod]
        public void LastNames()
        {
            var lastNamesBank = WordBank.LastNames;
            string[] lastNames = lastNamesBank.Get();
            Assert.IsNotNull(lastNames);
            //lol
            Assert.IsTrue(lastNames.Contains("Smith"));
            Assert.IsTrue(!lastNames.Contains("hospital"));
        }
        [TestMethod]
        public void Titles()
        {
            var titlesBank = WordBank.Titles;
            string[] titles = titlesBank.Get();
            Assert.IsNotNull(titles);
            //lol
            Assert.IsTrue(titles.Contains("Mister"));
            Assert.IsTrue(!titles.Contains("hospital"));
        }
        [TestMethod]
        public void MaleTitles()
        {
            var titlesBank = WordBank.MaleTitles;
            string[] titles = titlesBank.Get();
            Assert.IsNotNull(titles);
            //lol
            Assert.IsTrue(titles.Contains("Mister"));
            Assert.IsTrue(!titles.Contains("Mrs"));
        }
        [TestMethod]
        public void FemaleTitles()
        {
            var titlesBank = WordBank.FemaleTitles;
            string[] titles = titlesBank.Get();
            Assert.IsNotNull(titles);
            //lol
            Assert.IsTrue(titles.Contains("Mrs"));
            Assert.IsTrue(!titles.Contains("Mister"));
        }
        [TestMethod]
        public void Days()
        {
            var daysBank = WordBank.Days;
            string[] days = daysBank.Get();
            Assert.IsNotNull(days);
            Assert.IsTrue(days.Contains("Monday"));
            Assert.IsTrue(!days.Contains("January"));
        }
        [TestMethod]
        public void Months()
        {
            var monthsBank = WordBank.Months;
            string[] months = monthsBank.Get();
            Assert.IsNotNull(months);
            Assert.IsTrue(!months.Contains("Monday"));
            Assert.IsTrue(months.Contains("January"));
        }
        [TestMethod]
        public void StateNames()
        {
            var stateNames = WordBank.StateNames;
            string[] states = stateNames.Get();
            Assert.IsNotNull(states);
            Assert.IsTrue(states.Contains("Idaho"));
            Assert.IsTrue(!states.Contains("Boise"));
        }
        [TestMethod]
        public void FemaleFirstNames()
        {
            var femaleFirstNamesBank = WordBank.FemaleFirstNames;
            string[] names = femaleFirstNamesBank.Get();
            Assert.IsNotNull(names);
            Assert.IsTrue(names.Contains("Sarah"));
            Assert.IsTrue(!names.Contains("Robert"));
        }
        [TestMethod]
        public void MaleFirstNames()
        {
            var maleFirstNameBank = WordBank.MaleFirstNames;
            string[] names = maleFirstNameBank.Get();
            Assert.IsNotNull(names);
            Assert.IsTrue(names.Contains("Robert"));
            Assert.IsTrue(!names.Contains("Sarah"));
        }
        [TestMethod]
        public void JobTitles()
        {
            var jobTitles = WordBank.JobTitles;
            string[] jobs = jobTitles.Get();
            Assert.IsNotNull(jobs);
            Assert.IsTrue(jobs.Contains("Accountant"));
            Assert.IsTrue(!jobs.Contains("weathered"));
        }
        [TestMethod]
        public void Cities()
        {
            var citiesBank = WordBank.Cities;
            string[] cities = citiesBank.Get();
            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Contains("chicago"));
            Assert.IsTrue(!cities.Contains("chile"));
        }
        [TestMethod]
        public void Countries()
        {
             var countriesBank = WordBank.Countries;
            string[] countries = countriesBank.Get();
            Assert.IsNotNull(countries);
            Assert.IsTrue(countries.Contains("United States"));
            Assert.IsTrue(!countries.Contains("Chicago"));
        }
        [TestMethod]
        public void Adverbs()
        {
            var adverbsBank = WordBank.Adverbs;
            string[] adverbs = adverbsBank.Get();
            Assert.IsNotNull(adverbs);
            Assert.IsTrue(adverbs.Contains("delicately"));
            Assert.IsTrue(!adverbs.Contains("urinate"));
        }
        [TestMethod]
        public void Verbs()
        {
            var verbsBank = WordBank.Verbs;
            string[] verbs = verbsBank.Get();
            Assert.IsNotNull(verbs);
            Assert.IsTrue(verbs.Contains("urinate"));
            Assert.IsTrue(!verbs.Contains("delicately"));
        }
        [TestMethod]
        public void ImplicitOperatorTests()
        {
            var word = Word.Noun;
            var wordBank = WordBank.Nouns;
            Assert.IsTrue(word == wordBank);

            var word1 = "Adjectives";
            var wordBank1 = WordBank.Adjectives;
            Assert.IsTrue(word1 == wordBank1);
        }
    }
}
