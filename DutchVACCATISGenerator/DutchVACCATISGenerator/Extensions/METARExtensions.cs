using System.Linq;

namespace DutchVACCATISGenerator.Extensions
{
    public static class METARExtensions
    {
        public static bool HasVariableWind(this string input)
        {
            return input.Length > 5 && 
                input.Substring(0, 3).All(char.IsDigit) && 
                input.Contains('V') && 
                input.Substring(4).All(char.IsDigit);
        }
    }
}
