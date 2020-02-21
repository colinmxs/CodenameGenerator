using System;

namespace CodenameGenerator.Core
{
    public interface IWordBank
    {
        string GetWord(Random random);
        string GetWord();
    }
}
