namespace CodenameGenerator
{
    using System;

    internal static class StringExtensions
    {
        /// <summary>
        /// Returns the string with its first character upper-cased, using the
        /// invariant culture so results do not vary by locale.
        /// </summary>
        /// <param name="value">The string to capitalize. May be null or empty.</param>
        /// <returns>The capitalized string, or the input unchanged if null or empty.</returns>
        internal static string FirstCharToUpper(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var chars = value.ToCharArray();
            chars[0] = char.ToUpperInvariant(chars[0]);
            return new string(chars);
        }
    }
}
