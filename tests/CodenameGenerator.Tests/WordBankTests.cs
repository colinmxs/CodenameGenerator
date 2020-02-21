using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace CodenameGenerator.Tests
{
    [TestClass]
    public class WordBankTests
    {
        [TestMethod]
        public void WordBanks()
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
            var name = gen.GenerateMany(1000);
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }
    }
}
