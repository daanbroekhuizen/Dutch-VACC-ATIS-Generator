using System.Linq;

namespace DutchVACCATISGenerator.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if input string is numbers only.
        /// </summary>
        /// <param name="input">String to check.</param>
        /// <returns>Boolean indicating if the string is numbers only.</returns>
        public static bool IsNumbersOnly(this string input)
        {
            return input.All(char.IsDigit);
        }

        /// <summary>
        /// Check if input string is a certain size.
        /// </summary>
        /// /// <param name="input">String to check.</param>
        /// <param name="lenght">Size to check to.</param>
        /// <returns>Boolean indicating if the string is the size inputted.</returns>
        public static bool IsLength(this string input, int lenght)
        {
            return (input.Length == lenght);
        }
    }
}
