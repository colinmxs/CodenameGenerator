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
            Assert.IsTrue(titles.Contains("dirty"));
            Assert.IsTrue(!titles.Contains("hospital"));
        }
    }
}
