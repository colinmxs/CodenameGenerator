namespace CodenameGenerator
{
    using System;    

    public class ArrayBackedWordBank : WordBank
    {
        private readonly string[] _data;
        
        public ArrayBackedWordBank(string[] data)
        {
            _data = data;
        }

        internal override string GetWord(Random random)
        {
            var index = random.Next(_data.Length);
            return _data[index];
        }
    }
}
