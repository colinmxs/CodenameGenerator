namespace CodenameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Reads the built-in word lists, which ship as UTF-8 embedded resources
    /// (one word per line) rather than as compiled string arrays.
    /// </summary>
    internal static class WordData
    {
        private const string ResourcePrefix = "CodenameGenerator.WordData.";

        /// <summary>
        /// Loads a built-in word list by name, e.g. "Nouns".
        /// </summary>
        /// <param name="name">The word list name, matching the embedded resource file name.</param>
        /// <returns>The words, in file order, with duplicates preserved.</returns>
        /// <exception cref="InvalidOperationException">The named list is not embedded in this assembly.</exception>
        internal static string[] Load(string name)
        {
            var resourceName = ResourcePrefix + name + ".txt";
            var assembly = typeof(WordData).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        "Embedded word list '" + resourceName + "' was not found in " +
                        assembly.FullName + ".");
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var words = new List<string>();
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length > 0)
                        {
                            words.Add(line);
                        }
                    }

                    return words.ToArray();
                }
            }
        }
    }
}
