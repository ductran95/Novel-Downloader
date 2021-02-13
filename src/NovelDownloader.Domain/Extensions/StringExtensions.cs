using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NovelDownloader.Domain.Extensions
{
    public static class StringExtensions
    {
        public static List<int> GetNumbers(this string data)
        {
            var result = new List<int>();

            var matches = Regex.Matches(data, @"\d+");

            foreach (Match match in matches)
            {
                result.Add(int.Parse(match.Value));
            }

            return result;
        }
    }
}