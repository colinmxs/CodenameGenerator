using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CodenameGenerator.Lite.Tests
{
    [TestClass]
    public class WordBankTests
    {
        [TestMethod]
        public async Task WordBanks()
        {
            var timer = new Stopwatch();
            timer.Start();
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

            var gen = new Generator(" ", Casing.CamelCase, null, nouns, adjectives, firstNames, lastNames, titles, femaleTitles, maleTitles, stateNames, days, femaleFirstNames, maleFirstNames, months, jobTitles);
            var name = await gen.GenerateMany(1000);
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }
    }
}
