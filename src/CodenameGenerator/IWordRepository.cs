using System;

namespace CodenameGenerator
{
    public interface IWordRepository
    {
        string[] Get();
    }

    public abstract class FileRepository : IWordRepository
    {
        public string[] Get()
        {
            throw new NotImplementedException();
        }

        protected abstract string GetFileNamespace();
    }    
}
