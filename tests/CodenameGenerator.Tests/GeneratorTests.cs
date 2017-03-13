using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

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
        public void Defaults()
        {
            Assert.IsTrue(_generator.Separator == " ");
            Assert.IsTrue(_generator.Parts.Length == 2);
            Assert.IsTrue(_generator.Parts[0] == Word.Adjective);
            Assert.IsTrue(_generator.Parts[1] == Word.Noun);
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
        public void Generate_SetSeparator(string separator)
        {
            _generator.Separator = separator;
            string result = _generator.Generate();
            Assert.IsTrue(result.Contains(separator.ToString()));
        }

        [TestMethod]
        [DataRow(2, "-.-")]
        [DataRow(4, ".")]
        [DataRow(6, "-")]
        public void GenerateMany_SetSeparator(int count, string separator)
        {
            _generator.Separator = separator;
            var results = _generator.GenerateMany(count);
            foreach (var result in results)
            {
                Assert.IsTrue(result.Contains(separator.ToString()));
            }
        }        

        [TestMethod]
        public void Generate_SetParts()
        {
            _generator.SetParts(TestWordBank.Titles, TestWordBank.FirstNames, TestWordBank.LastNames);
            Assert.IsTrue(_generator.Parts[0] == Word.Title);
            Assert.IsTrue(_generator.Parts[1] == Word.FirstName);
            Assert.IsTrue(_generator.Parts[2] == Word.LastName);

            var result = _generator.Generate();
            var strings = result.Split(new string[] { _generator.Separator }, StringSplitOptions.RemoveEmptyEntries);
            Assert.IsTrue(strings.Length == 3);
            Assert.IsTrue(strings[0] == "Aunt" || strings[0] == "Uncle");
            Assert.IsTrue(strings[1] == "David" || strings[1] == "Roger");
            Assert.IsTrue(strings[2] == "Smith" || strings[2] == "Jones");
        }        

        [TestMethod]
        [DataRow("cat dog")]
        [DataRow("dog cat")]
        [DataRow("cat cat")]
        [DataRow("dog dog")]        
        public void GenerateUnique(string reserved)
        {
            _generator.SetParts(TestWordBank.Nouns);
            var result = _generator.GenerateUnique(new string[] { reserved });
            Assert.AreNotEqual(reserved, result);
        }

        [TestMethod]
        public void CasingEnum()
        {
            var pascalCase = Casing.PascalCase;
            var camelCase = Casing.CamelCase;
            var upperCase = Casing.UpperCase;
        }

        [TestMethod]
        public void Generate_SetCasing()
        {
            _generator.Separator = "";
            _generator.SetCasing(Casing.PascalCase);
            var result = _generator.Generate();
            var regex = "^[A-Z][a-z]+([A-Z][a-z]+)+$";
            Assert.IsTrue(Regex.IsMatch(result, regex));

            _generator.SetCasing(Casing.CamelCase);
            result = _generator.Generate();
            regex = "[A-Z]([A-Z0-9]*[a-z][a-z0-9]*[A-Z]|[a-z0-9]*[A-Z][A-Z0-9]*[a-z])[A-Za-z0-9]*";
            Assert.IsTrue(Regex.IsMatch(result, regex));

            _generator.SetCasing(Casing.UpperCase);
            result = _generator.Generate();
            regex = "\b[A-Z][A-Z0-9]+\b";
            Assert.IsTrue(Regex.IsMatch(result, regex));
        }
    }
}
