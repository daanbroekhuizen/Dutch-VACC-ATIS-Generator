using System.Linq;
using System.Text.RegularExpressions;

namespace DutchVACCATISGenerator.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if input string is numbers only.
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>Boolean indicating if the string is numbers only</returns>
        public static bool IsNumbersOnly(this string input)
        {
            return input.All(char.IsDigit);
        }

        /// <summary>
        /// Check if input string is a certain size.
        /// </summary>
        /// /// <param name="input">String to check</param>
        /// <param name="lenght">Size to check to</param>
        /// <returns>Boolean indicating if the string is the size inputted</returns>
        public static bool IsLength(this string input, int lenght)
        {
            return (input.Length == lenght);
        }

        /// <summary>
        /// Splits a string by the given word.
        /// </summary>
        /// <param name="input">String to split</param>
        /// <param name="splitBy">Word to split by</param>
        /// <returns>Split string as array</returns>
        public static string[] SplitBy(this string input, string splitBy)
        {
            var regex = new Regex($@"\b{splitBy}\b");

            var split = regex.Split(input);

            //Trim every entry.
            for (var i = 0; i < split.Length; i++)
                split[i] = split[i].Trim();

            return split;
        }
    }
}
