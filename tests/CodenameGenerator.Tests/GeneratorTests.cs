namespace CodenameGenerator.Tests
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void CTorTests()
        {
            var separator = "-";
            var casing = Casing.PascalCase;

            var generator = new Generator(separator, casing);
            Assert.AreEqual("-", generator.Separator);
            Assert.AreEqual(casing, generator.Casing);

            generator = new Generator(separator, casing, WordBank.FirstNames, WordBank.LastNames);
            Assert.AreEqual("-", generator.Separator);
            Assert.AreEqual(casing, generator.Casing);

            var parts = generator.Generate().Split('-');
            Assert.AreEqual(2, parts.Length);
            Assert.IsTrue(WordBank.FirstNames.Get().Contains(parts[0]));
            Assert.IsTrue(WordBank.LastNames.Get().Contains(parts[1]));
        }

        [TestMethod]
        public void Defaults()
        {
            var generator = new Generator();
            Assert.AreEqual(" ", generator.Separator);
            Assert.AreEqual(2, generator.Parts.Length);
            Assert.IsTrue(generator.Parts[0] == Word.Adjective);
            Assert.IsTrue(generator.Parts[1] == Word.Noun);
            Assert.AreEqual(Casing.LowerCase, generator.Casing);
            Assert.AreEqual("", generator.EndsWith);
        }

        [TestMethod]
        public void Generate()
        {
            var generator = new Generator();
            var result = generator.Generate();
            Assert.IsNotNull(result);
            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(int.MaxValue)]
        public void SeededTests(int seed)
        {
            var first = new Generator(seed: seed);
            var second = new Generator(seed: seed);
            Assert.AreEqual(first.Generate(), second.Generate());
        }

        [TestMethod]
        public void Seeded_ProducesTheSameSequence()
        {
            var first = new Generator(seed: 42).GenerateMany(25);
            var second = new Generator(seed: 42).GenerateMany(25);
            CollectionAssert.AreEqual(first, second);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(8)]
        public void GenerateMany(int count)
        {
            var results = new Generator().GenerateMany(count);
            Assert.IsNotNull(results);
            Assert.AreEqual(count, results.Length);
            foreach (var result in results)
            {
                Assert.AreNotEqual("", result);
            }
        }

        [TestMethod]
        public void GenerateMany_NegativeCount_Throws()
        {
            var generator = new Generator();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => generator.GenerateMany(-1));
        }

        [TestMethod]
        [DataRow("-")]
        [DataRow("_")]
        [DataRow(".")]
        public void Generate_SetSeparator(string separator)
        {
            var generator = new Generator();
            generator.Separator = separator;
            Assert.IsTrue(generator.Generate().Contains(separator));
        }

        [TestMethod]
        [DataRow(2, "-.-")]
        [DataRow(4, ".")]
        [DataRow(6, "-")]
        public void GenerateMany_SetSeparator(int count, string separator)
        {
            var generator = new Generator();
            generator.Separator = separator;
            foreach (var result in generator.GenerateMany(count))
            {
                Assert.IsTrue(result.Contains(separator));
            }
        }

        [TestMethod]
        public void Generate_SetEndsWith()
        {
            var generator = new Generator();
            var emailSuffix = "@gmail.com";
            generator.EndsWith = emailSuffix;
            Assert.IsTrue(generator.Generate().EndsWith(emailSuffix, StringComparison.Ordinal));
        }

        [TestMethod]
        public void Generate_SetParts()
        {
            var generator = new Generator();
            generator.SetParts(TestWordBank.Titles, TestWordBank.FirstNames, TestWordBank.LastNames);
            Assert.IsTrue(generator.Parts[0] == Word.Title);
            Assert.IsTrue(generator.Parts[1] == Word.FirstName);
            Assert.IsTrue(generator.Parts[2] == Word.LastName);

            var strings = generator.Generate().Split(
                new string[] { generator.Separator }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(3, strings.Length);
            Assert.IsTrue(strings[0] == "aunt" || strings[0] == "uncle");
            Assert.IsTrue(strings[1] == "david" || strings[1] == "roger");
            Assert.IsTrue(strings[2] == "smith" || strings[2] == "jones");
        }

        /// <summary>
        /// A word bank entry may itself contain spaces; the separator is applied
        /// between those sub-words too. Pinning this documented behaviour.
        /// </summary>
        [TestMethod]
        public void Generate_MultiWordPart_SeparatesEveryWord()
        {
            var generator = new Generator("-");
            generator.SetParts(TestWordBank.JobTitles);
            var result = generator.Generate();
            Assert.IsTrue(
                result == "airline-traffic-controller" || result == "hospital-man",
                "unexpected result: " + result);
        }

        [TestMethod]
        [DataRow("cat dog")]
        [DataRow("dog cat")]
        [DataRow("cat cat")]
        [DataRow("dog dog")]
        public void GenerateUnique(string reserved)
        {
            var generator = new Generator();
            generator.SetParts(TestWordBank.Nouns);
            Assert.AreNotEqual(reserved, generator.GenerateUnique(new string[] { reserved }));
        }

        [TestMethod]
        public void GenerateUnique_AvoidsEveryReservedName()
        {
            var generator = new Generator();
            generator.SetParts(TestWordBank.Nouns);
            var result = generator.GenerateUnique(new string[] { "dog" });
            Assert.AreEqual("cat", result);
        }

        [TestMethod]
        public void GenerateUnique_NoNamesLeft_Throws()
        {
            // Two possible names, both reserved. This used to recurse until the
            // process died; now it gives up and explains why.
            var generator = new Generator();
            generator.SetParts(TestWordBank.Nouns);
            var ex = Assert.ThrowsException<InvalidOperationException>(
                () => generator.GenerateUnique(new string[] { "dog", "cat" }));
            Assert.IsTrue(ex.Message.Contains("reserved"), "unexpected message: " + ex.Message);
        }

        [TestMethod]
        public void GenerateUnique_NullOrEmptyReserved_Generates()
        {
            var generator = new Generator();
            generator.SetParts(TestWordBank.Nouns);
            Assert.AreNotEqual("", generator.GenerateUnique(null!));
            Assert.AreNotEqual("", generator.GenerateUnique(new string[0]));
        }

        [TestMethod]
        public void CasingEnum()
        {
            CollectionAssert.AreEquivalent(
                new[] { Casing.CamelCase, Casing.PascalCase, Casing.UpperCase, Casing.LowerCase },
                (Casing[])Enum.GetValues(typeof(Casing)));
        }

        [TestMethod]
        [DataRow(Casing.PascalCase, "AirlineTrafficController", "HospitalMan")]
        [DataRow(Casing.CamelCase, "airlineTrafficController", "hospitalMan")]
        [DataRow(Casing.UpperCase, "AIRLINETRAFFICCONTROLLER", "HOSPITALMAN")]
        [DataRow(Casing.LowerCase, "airlinetrafficcontroller", "hospitalman")]
        public void Generate_SetCasing(Casing casing, string first, string second)
        {
            var generator = new Generator(string.Empty, casing);
            generator.SetParts(TestWordBank.JobTitles);
            var result = generator.Generate();
            Assert.IsTrue(result == first || result == second, "unexpected result: " + result);
        }

        /// <summary>
        /// Casing must not depend on the ambient culture. Under tr-TR the
        /// culture-sensitive ToLower/ToUpper produce dotless and dotted I.
        /// </summary>
        [TestMethod]
        [DataRow(Casing.LowerCase, "ISTANBUL", "istanbul")]
        [DataRow(Casing.UpperCase, "istanbul", "ISTANBUL")]
        public void Generate_Casing_IsCultureInvariant(Casing casing, string word, string expected)
        {
            var originalCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("tr-TR");
                var bank = new WordBank(Word.Noun, "Turkish", new WordRepository(new[] { word }));
                var generator = new Generator(string.Empty, casing);
                generator.SetParts(bank);
                Assert.AreEqual(expected, generator.Generate());
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }

        [TestMethod]
        public void HandleNulls()
        {
            var generator = new Generator();
            generator.Separator = null!;
            generator.EndsWith = null!;
            generator.Parts = null!;

            Assert.AreEqual("", generator.Separator);
            Assert.AreEqual("", generator.EndsWith);
            Assert.AreEqual(0, generator.Parts.Length);
            Assert.AreEqual("", generator.Generate());
        }

        /// <summary>
        /// No parts plus a non-empty separator used to throw
        /// ArgumentOutOfRangeException while trimming the trailing separator.
        /// </summary>
        [TestMethod]
        public void Generate_NoParts_ReturnsSuffixOnly()
        {
            var generator = new Generator("-");
            generator.SetParts();
            Assert.AreEqual("", generator.Generate());

            generator.EndsWith = "@example.com";
            Assert.AreEqual("@example.com", generator.Generate());
        }

        [TestMethod]
        public void Generate_EmptyWordBank_ThrowsWithBankName()
        {
            var generator = new Generator();
            generator.SetParts(TestWordBank.Empty);
            var ex = Assert.ThrowsException<InvalidOperationException>(() => generator.Generate());
            Assert.IsTrue(ex.Message.Contains("Empty"), "unexpected message: " + ex.Message);
        }

        /// <summary>
        /// An unseeded generator must be usable concurrently. Sharing one
        /// non-thread-safe Random used to corrupt its internal state, after which
        /// every draw returned index 0.
        /// </summary>
        [TestMethod]
        public void Generate_Unseeded_IsThreadSafe()
        {
            var generator = new Generator();
            generator.SetParts(TestWordBank.Nouns);

            var results = new ConcurrentBag<string>();
            var failures = new ConcurrentBag<Exception>();

            Parallel.For(0, 20000, _ =>
            {
                try
                {
                    results.Add(generator.Generate());
                }
                catch (Exception ex)
                {
                    failures.Add(ex);
                }
            });

            Assert.AreEqual(0, failures.Count, "threw: " + string.Join("; ", failures.Select(f => f.Message)));
            Assert.AreEqual(20000, results.Count);

            var distinct = new HashSet<string>(results, StringComparer.Ordinal);
            Assert.IsTrue(
                distinct.SetEquals(new[] { "dog", "cat" }),
                "expected both words across 20000 concurrent draws, got: " + string.Join(",", distinct));
        }

        /// <summary>
        /// Changing configuration while another thread generates must not produce a
        /// torn result: each Generate() call works from a snapshot.
        /// </summary>
        [TestMethod]
        public void Generate_ConfigurationChangedConcurrently_DoesNotTear()
        {
            var generator = new Generator(string.Empty);
            generator.SetParts(TestWordBank.Nouns);
            var allowed = new HashSet<string>(new[] { "dog", "cat", "hairy", "dirty" }, StringComparer.Ordinal);
            var failures = new ConcurrentBag<string>();

            Parallel.Invoke(
                () =>
                {
                    for (var i = 0; i < 5000; i++)
                    {
                        generator.SetParts(i % 2 == 0 ? TestWordBank.Nouns : TestWordBank.Adjectives);
                    }
                },
                () =>
                {
                    for (var i = 0; i < 5000; i++)
                    {
                        var result = generator.Generate();
                        if (!allowed.Contains(result))
                        {
                            failures.Add(result);
                        }
                    }
                });

            Assert.AreEqual(0, failures.Count, "unexpected results: " + string.Join(",", failures.Distinct()));
        }
    }
}
