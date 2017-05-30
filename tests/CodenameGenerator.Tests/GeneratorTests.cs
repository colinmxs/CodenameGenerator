using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
        public void CTorTests()
        {
            var @char = '-';
            var separator = @char.ToString();
            var casing = Casing.PascalCase;
            var generator = new Generator(separator, casing);
            Assert.IsTrue(generator.Separator == "-");
            Assert.IsTrue(generator.Casing == casing);

            generator = new Generator(separator, casing, WordBank.FirstNames, WordBank.LastNames);
            Assert.IsTrue(generator.Separator == "-");
            Assert.IsTrue(generator.Casing == casing);
            var name = generator.Generate();
            var parts = name.Split(@char);
            Assert.IsTrue(parts.Length == 2);            
            Assert.IsTrue(WordBank.FirstNames.Get().Contains(parts[0]));
            Assert.IsTrue(WordBank.LastNames.Get().Contains(parts[1]));
        }

        [TestMethod]
        public void Defaults()
        {
            Assert.IsTrue(_generator.Separator == " ");
            Assert.IsTrue(_generator.Parts.Length == 2);
            Assert.IsTrue(_generator.Parts[0] == Word.Adjective);
            Assert.IsTrue(_generator.Parts[1] == Word.Noun);
            Assert.IsTrue(_generator.Casing == Casing.LowerCase);
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
            Assert.IsTrue(strings[0] == "aunt" || strings[0] == "uncle");
            Assert.IsTrue(strings[1] == "david" || strings[1] == "roger");
            Assert.IsTrue(strings[2] == "smith" || strings[2] == "jones");
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
            var lowerCase = Casing.LowerCase;
        }

        [TestMethod]
        public void Generate_SetCasing()
        {
            _generator.Separator = "";
            _generator.Casing = (Casing.PascalCase);
            var result = _generator.Generate();
            var regex = @"^[A-Z][a-z]+([A-Z][a-z]+)+$";
            Assert.IsTrue(Regex.IsMatch(result, regex));

            _generator.Casing = (Casing.CamelCase);
            result = _generator.Generate();
            regex = @"^([a-z][a-z0-9]*[A-Z][A-Z0-9]*[a-z])[A-Za-z0-9]*";
            Assert.IsTrue(Regex.IsMatch(result, regex));

            _generator.Casing = (Casing.UpperCase);
            result = _generator.Generate();
            regex = @"[A-Z]";
            Assert.IsTrue(Regex.IsMatch(result, regex));

            _generator.Casing = Casing.LowerCase;
            result = _generator.Generate();
            regex = @"[a-z]";
            Assert.IsTrue(Regex.IsMatch(result, regex));
        }
    }
}