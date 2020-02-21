namespace CodenameGenerator.Lite
{
    using System;
    using System.Threading.Tasks;

    public class ArrayBackedWordBank : WordBank
    {
        private readonly string[] _data;
        
        public ArrayBackedWordBank(string[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            WordCount = data.Length;
            _data = data;
        }

        internal override Task<string> GetWordAsync(int index)
        {
            return Task.FromResult(_data[index]);
        }
    }
}
