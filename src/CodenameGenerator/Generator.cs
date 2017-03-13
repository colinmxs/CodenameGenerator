using System;

namespace CodenameGenerator
{
    public class Generator
    {
        public char Separator { get; private set; }
        public WordBank[] Parts { get; private set; }
        public string Generate()
        {
            throw new NotImplementedException();
        }

        public string[] GenerateMany(int count)
        {
            throw new NotImplementedException();
        }        

        public void SetSeparator(char separator)
        {
            throw new NotImplementedException();
        }

        public void SetParts(params WordBank[] wordBanks)
        {
            throw new NotImplementedException();
        }
    }
}
