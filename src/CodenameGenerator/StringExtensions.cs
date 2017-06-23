using System;

namespace CodenameGenerator
{
    public static class StringExtensions
    {
        //http://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-for-maximum-performance
        public static string FirstCharToUpper(this string @string)
        {
            if (String.IsNullOrEmpty(@string))
                throw new ArgumentException("There is no first letter");
            char[] chars = @string.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }
    }
}