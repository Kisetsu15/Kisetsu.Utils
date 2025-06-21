using System;
using System.Collections.Generic;
using System.Text;

namespace Kisetsu.Utils {

    /// <summary>
    /// Specifies modes for stripping characters from a string.
    /// </summary>
    public enum StripMode {
        /// <summary>No stripping is applied.</summary>
        None,
        /// <summary>Removes all whitespace characters.</summary>
        Whitespace,
        /// <summary>Keeps only letters and digits.</summary>
        Alphanumeric,
        /// <summary>Keeps only alphabetic characters.</summary>
        Alphabets,
        /// <summary>Keeps only numeric digits.</summary>
        Numeric,
        /// <summary>Removes letters, digits, and whitespace; keeps symbols.</summary>
        Symbols,
    };

    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Removes all occurrences of the specified characters from the string.
        /// </summary>
        /// <param name="str">The input string to process.</param>
        /// <param name="removeChars">An array of characters to remove.</param>
        /// <returns>A new string with the specified characters removed.</returns>
        public static string Strip(this string str, params char[] removeChars) {
            if (string.IsNullOrEmpty(str)) return str;

            var removeSet = new HashSet<char>(removeChars);
            var sb = new StringBuilder(str.Length);
            foreach (var c in str) {
                if (!removeSet.Contains(c)) sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Strips characters from the string based on the specified <see cref="StripMode"/>.
        /// </summary>
        /// <param name="str">The input string to process.</param>
        /// <param name="mode">The stripping mode to apply.</param>
        /// <returns>A new string with characters stripped according to the mode.</returns>
        public static string Strip(this string str, StripMode mode = StripMode.None) {
            Func<char, bool> filter = mode switch {
                StripMode.None => _ => true,
                StripMode.Whitespace => c => !char.IsWhiteSpace(c),
                StripMode.Alphanumeric => c => char.IsLetterOrDigit(c),
                StripMode.Alphabets => c => char.IsLetter(c),
                StripMode.Numeric => c => char.IsDigit(c),
                StripMode.Symbols => c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            if (string.IsNullOrEmpty(str)) return str;
            var sb = new StringBuilder(str.Length);
            foreach (var c in str) {
                if (filter(c)) sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all occurrences of a specified substring from the string.
        /// </summary>
        /// <param name="str">The input string to process.</param>
        /// <param name="subString">The substring to remove.</param>
        /// <returns>A new string with the specified substring removed.</returns>
        public static string Strip(this string str, string subString) {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(subString)) return str;
            return str.Replace(subString, string.Empty);
        }
    }
}
