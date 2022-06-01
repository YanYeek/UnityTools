using System.Text.RegularExpressions;

namespace YanYeek
{
    public partial class Utils
    {
        private static string ExtractStringBetweenBeginAndEnd(string input, string begin, string end)
        {

            Regex regex = new Regex($"(?<={begin}).*?(?={end})");
            foreach (Match match in regex.Matches(input))
            {

                if (match != null && !string.IsNullOrEmpty(match.Value))
                {

                    return match.Value;
                }
            }

            return string.Empty;
        }
    }
}