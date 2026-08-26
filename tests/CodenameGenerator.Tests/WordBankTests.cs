namespace CodenameGenerator.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WordBankTests
    {
        [TestMethod]
        public void All_ExposesEveryBuiltInBank()
        {
            Assert.AreEqual(17, WordBank.All.Count);
            CollectionAssert.AllItemsAreNotNull(WordBank.All.ToArray());
            CollectionAssert.AllItemsAreUnique(WordBank.All.ToArray());
            Assert.IsTrue(WordBank.All.Contains(WordBank.Nouns));
        }

        [TestMethod]
        public void All_CoversEveryWordCategory()
        {
            var covered = WordBank.All.Select(bank => (Word)bank).Distinct().OrderBy(w => w).ToArray();
            var declared = ((Word[])Enum.GetValues(typeof(Word))).OrderBy(w => w).ToArray();
            CollectionAssert.AreEqual(declared, covered);
        }

        /// <summary>
        /// Every built-in bank must load from its embedded resource and contain
        /// clean, usable words.
        /// </summary>
        [TestMethod]
        public void All_LoadUsableWords()
        {
            foreach (var bank in WordBank.All)
            {
                var words = bank.Get();
                Assert.IsNotNull(words, bank + " returned null");
                Assert.IsTrue(words.Length > 0, bank + " is empty");

                foreach (var word in words)
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(word), bank + " contains a blank word");
                    Assert.AreEqual(word.Trim(), word, bank + " contains padded word: '" + word + "'");
                }
            }
        }

        [TestMethod]
        public void All_ContainTheExpectedNumberOfWords()
        {
            var total = WordBank.All.Sum(bank => bank.Get().Length);
            Assert.AreEqual(298738, total, "the bundled word lists changed size");
        }

        [TestMethod]
        public void Get_CachesTheWordList()
        {
            Assert.AreSame(WordBank.Days.Get(), WordBank.Days.Get());
        }

        [TestMethod]
        public void Repository_LoadsLazilyAndOnlyOnce()
        {
            var repo = CountingRepository.Create("dog", "cat");
            Assert.AreEqual(0, repo.Loads, "constructing a repository must not load its words");

            var words = repo.Get();
            Assert.AreEqual(1, repo.Loads);
            Assert.AreEqual(2, words.Length);

            repo.Get();
            Assert.AreEqual(1, repo.Loads, "words should be cached after the first load");
        }

        [TestMethod]
        public void Repository_NullWords_Throws()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new WordRepository(null!));
        }

        [TestMethod]
        public void ToString_IsTheBankName()
        {
            Assert.AreEqual("Nouns", WordBank.Nouns.ToString());
            Assert.AreEqual("JobTitles", WordBank.JobTitles.ToString());
        }

        [TestMethod]
        public void ImplicitOperatorTests()
        {
            Word word = WordBank.Nouns;
            Assert.AreEqual(Word.Noun, word);

            string name = WordBank.Adjectives;
            Assert.AreEqual("Adjectives", name);
        }

        [TestMethod]
        public void Nouns()
        {
            var nouns = WordBank.Nouns.Get();
            Assert.IsTrue(nouns.Contains("dog"));
            Assert.IsFalse(nouns.Contains("dilapidated"));
        }

        [TestMethod]
        public void Adjectives()
        {
            var adjectives = WordBank.Adjectives.Get();
            Assert.IsTrue(adjectives.Contains("dirty"));
            Assert.IsFalse(adjectives.Contains("hospital"));
        }

        [TestMethod]
        public void FirstNames()
        {
            var firstNames = WordBank.FirstNames.Get();
            Assert.IsTrue(firstNames.Contains("Dan"));
            Assert.IsFalse(firstNames.Contains("horse"));
        }

        [TestMethod]
        public void LastNames()
        {
            var lastNames = WordBank.LastNames.Get();
            Assert.IsTrue(lastNames.Contains("Smith"));
            Assert.IsFalse(lastNames.Contains("hospital"));
        }

        [TestMethod]
        public void Titles()
        {
            var titles = WordBank.Titles.Get();
            Assert.IsTrue(titles.Contains("Mister"));
            Assert.IsFalse(titles.Contains("hospital"));
        }

        [TestMethod]
        public void MaleTitles()
        {
            var titles = WordBank.MaleTitles.Get();
            Assert.IsTrue(titles.Contains("Mister"));
            Assert.IsFalse(titles.Contains("Mrs"));
        }

        [TestMethod]
        public void FemaleTitles()
        {
            var titles = WordBank.FemaleTitles.Get();
            Assert.IsTrue(titles.Contains("Mrs"));
            Assert.IsFalse(titles.Contains("Mister"));
        }

        [TestMethod]
        public void Days()
        {
            var days = WordBank.Days.Get();
            CollectionAssert.AreEqual(
                new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                days);
        }

        /// <summary>
        /// The bundled list was missing April until v2.0.0.
        /// </summary>
        [TestMethod]
        public void Months()
        {
            var months = WordBank.Months.Get();
            CollectionAssert.AreEqual(
                new[]
                {
                    "January", "February", "March", "April", "May", "June",
                    "July", "August", "September", "October", "November", "December",
                },
                months);
        }

        [TestMethod]
        public void StateNames()
        {
            var states = WordBank.StateNames.Get();
            Assert.AreEqual(50, states.Length);
            Assert.IsTrue(states.Contains("Idaho"));
            Assert.IsFalse(states.Contains("Boise"));
        }

        [TestMethod]
        public void FemaleFirstNames()
        {
            var names = WordBank.FemaleFirstNames.Get();
            Assert.IsTrue(names.Contains("Sarah"));
            Assert.IsFalse(names.Contains("Robert"));
        }

        [TestMethod]
        public void MaleFirstNames()
        {
            var names = WordBank.MaleFirstNames.Get();
            Assert.IsTrue(names.Contains("Robert"));
            Assert.IsFalse(names.Contains("Sarah"));
        }

        [TestMethod]
        public void JobTitles()
        {
            var jobs = WordBank.JobTitles.Get();
            Assert.IsTrue(jobs.Contains("Accountant"));
            Assert.IsFalse(jobs.Contains("weathered"));
        }

        [TestMethod]
        public void Cities()
        {
            var cities = WordBank.Cities.Get();
            Assert.IsTrue(cities.Contains("chicago"));
            Assert.IsFalse(cities.Contains("chile"));
        }

        [TestMethod]
        public void Countries()
        {
            var countries = WordBank.Countries.Get();
            Assert.IsTrue(countries.Contains("United States"));
            Assert.IsFalse(countries.Contains("Chicago"));
        }

        [TestMethod]
        public void Adverbs()
        {
            var adverbs = WordBank.Adverbs.Get();
            Assert.IsTrue(adverbs.Contains("delicately"));
            Assert.IsFalse(adverbs.Contains("urinate"));
        }

        [TestMethod]
        public void Verbs()
        {
            var verbs = WordBank.Verbs.Get();
            Assert.IsTrue(verbs.Contains("urinate"));
            Assert.IsFalse(verbs.Contains("delicately"));
        }

        /// <summary>
        /// Punctuation and multi-word entries must survive the round trip through the
        /// line-delimited embedded resources. The bundled lists are ASCII-only, which
        /// this also pins: a non-ASCII word appearing would mean an encoding problem.
        /// </summary>
        [TestMethod]
        public void WordLists_RoundTripPunctuationAndSpacing()
        {
            Assert.IsTrue(WordBank.Verbs.Get().Contains("bird's-nest"), "apostrophe + hyphen entry missing");
            Assert.IsTrue(WordBank.Adverbs.Get().Contains("above-board"), "hyphenated entry missing");
            Assert.IsTrue(WordBank.Countries.Get().Contains("United States"), "multi-word entry missing");
            Assert.IsTrue(WordBank.JobTitles.Get().Contains("Electro-Mechanical Technicians"), "mixed entry missing");

            foreach (var bank in WordBank.All)
            {
                foreach (var word in bank.Get())
                {
                    foreach (var ch in word)
                    {
                        Assert.IsTrue(ch < 128, bank + " has a non-ASCII character in '" + word + "'");
                    }
                }
            }
        }
    }
}
