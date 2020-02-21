namespace CodenameGenerator.Lite
{
    using System;
    using System.Globalization;

    internal static class StringExtensions
    {
        /// http://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-for-maximum-performance
        /// <summary>
        /// Capitalize the first character of a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        internal static string FirstCharToUpper(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                throw new ArgumentException($"{nameof(@string)} is null or empty.");
            char[] chars = @string.ToCharArray();
            chars[0] = char.ToUpper(chars[0], CultureInfo.InvariantCulture);
            return new string(chars);
        }
    }
}