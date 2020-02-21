namespace CodenameGenerator.Lite
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class FileBackedWordBank : WordBank
    {
        private readonly string fileName;
        private const string folderName = "Data";

        internal FileBackedWordBank(string fileName, int lineCount)
        {
            WordCount = lineCount;
            this.fileName = fileName;
        }

        internal override async Task<string> GetWordAsync(int index)
        {
            var part = File.ReadLines($"{folderName}/{fileName}").Skip(index).Take(1).Single();
            return part;
        }
    }
}
