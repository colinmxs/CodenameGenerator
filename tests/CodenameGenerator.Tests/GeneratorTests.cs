using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodenameGenerator.Tests
{
    [TestClass]
    public class GeneratorTests
    {
        private Generator _generator;

        [TestInitialize]
        public void Init()
        {
            _generator = new Generator();
        }

        [TestMethod]
        public void Generate()
        {
            string result = _generator.Generate();
            Assert.IsNotNull(result);
            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(8)]
        public void GenerateMany(int count)
        {

            string[] results = _generator.GenerateMany(count);
            Assert.IsNotNull(results);
            Assert.AreEqual(count, results.Length);
            foreach (var result in results)
            {
                Assert.AreNotEqual("", result);
            }
        }
        
        [TestMethod]
        [DataRow("-")]
        [DataRow("_")]
        [DataRow(".")]
        public void Generate_SpecifySeparator(string separator)
        {
            _generator.Separator = separator;
            string result = _generator.Generate();
            Assert.IsTrue(result.Contains(separator.ToString()));
        }

        [TestMethod]
        [DataRow(2, "-.-")]
        [DataRow(4, ".")]
        [DataRow(6, "-")]
        public void GenerateMany_SpecifySeparator(int count, string separator)
        {
            _generator.Separator = separator;
            var results = _generator.GenerateMany(count);
            foreach (var result in results)
            {
                Assert.IsTrue(result.Contains(separator.ToString()));
            }
        }

        [TestMethod]
        public void Defaults()
        {
            Assert.IsTrue(_generator.Separator == " ");
            Assert.IsTrue(_generator.Parts.Length == 2);
            Assert.IsTrue(_generator.Parts[0] == Word.Adjective);
            Assert.IsTrue(_generator.Parts[1] == Word.Noun);
        }

        [TestMethod]
        public void Generate_SpecifyParts()
        {
            _generator.SetParts(WordBank.Titles, WordBank.FirstNames, WordBank.LastNames);
            Assert.IsTrue(_generator.Parts[0] == Word.Title);
            Assert.IsTrue(_generator.Parts[1] == Word.FirstName);
            Assert.IsTrue(_generator.Parts[2] == Word.LastName);
        }
    }
}
