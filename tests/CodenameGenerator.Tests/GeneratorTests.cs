using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        public void Generate_SpecifyWordCount(int count)
        {
            _generator.SetWordCount(count);
            string result = _generator.Generate();
            char separator = _generator.Separator;
            var strings = result.Split(separator);
            Assert.AreEqual(count, strings.Length);
        }

        [TestMethod]
        public void GenerateMany_SpecifyWordCount(int count1, int count2)
        {
            _generator.SetWordCount(count2);
            Assert.AreEqual(count2, _generator.WordCount);
            string[] results = _generator.GenerateMany(count1);
            char separator = _generator.Separator;
            foreach (var result in results)
            {
                var strings = result.Split(separator);
                Assert.AreEqual(count2, strings.Length);
            }            
        }

        [TestMethod]
        public void Generate_SpecifySeparator(char separator)
        {
            _generator.SetSeparator(separator);
            string result = _generator.Generate();
            Assert.IsTrue(result.Contains(separator.ToString()));
        }

        [TestMethod]
        public void GenerateMany_SpecifySeparator(int count, char separator)
        {
            _generator.SetSeparator(separator);
            var results = _generator.GenerateMany(count);
            foreach (var result in results)
            {
                Assert.IsTrue(result.Contains(separator.ToString()));
            }
        }

        [TestMethod]
        public void Generate_Defaults(char separator)
        {
            _generator.Separator = ' ';
            _generator.WordCount = 2;
        }
    }
}
