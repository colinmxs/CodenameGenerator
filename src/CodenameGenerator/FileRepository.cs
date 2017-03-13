using System;
using System.IO;
using System.Reflection;

namespace CodenameGenerator
{
    public class FileRepository : IStringRepository
    {
        private readonly string _fileName;
        private string[] _words;

        internal FileRepository(string fileName)
        {
            _fileName = fileName;
        }        

        private const string FileFolderNamespace = "CodenameGenerator.Files.";

        public string[] Get()
        {
            if (_words == null)
            {
                var @namespace = FileFolderNamespace + _fileName;
                var stringArray = new string[] { };
                using (var stream = typeof(WordBank).GetTypeInfo().Assembly.GetManifestResourceStream(@namespace))
                using (var reader = new StreamReader(stream))
                {
                    //TODO: optimize
                    string csv = reader.ReadToEnd();
                    stringArray = csv.Split(new string[] { "\r\n" }, StringSplitOptions.None);                    
                }
                _words = stringArray;
            }

            return _words;
        }
    }
}
