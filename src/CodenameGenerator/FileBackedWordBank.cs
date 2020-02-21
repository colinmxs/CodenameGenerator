namespace CodenameGenerator
{
    using System;
    using System.IO;
    using System.Linq;

    public class FileBackedWordBank : WordBank
    {
        private readonly int count;
        private readonly string fileName;
        private const string folderName = "Data";

        internal FileBackedWordBank(string fileName, int lineCount)
        {
            count = lineCount;
            this.fileName = fileName;
        }

        internal override string GetWord(Random random)
        {
            var index = random.Next(count);
            var part = File.ReadLines($"{folderName}/{fileName}").Skip(index).Take(1).Single();
            return part;
        }
    }
}
