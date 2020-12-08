using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DaySevenSolver
    {
        private readonly string _inputFile;

        public DaySevenSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var lines = new Dictionary<string, List<(int count, string color)>>();
            var builder = new StringBuilder();
            var regex = new Regex(@"((?'count'\d*) )*(?'color'(?>\w+\b) (?>\w+\b)) (?>bag)", RegexOptions.Compiled);
            const string shinyGold = "shiny gold";
            const string noOtherColor = "no other";

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;

                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    var matches = regex.Matches(line);

                    var keyColor = matches.First().Groups["color"].Value;
                    lines.Add(keyColor, new List<(int, string)>());

                    for (var i = 1; i < matches.Count; i++)
                    {
                        var color = matches[i].Groups["color"].Value;
                        var count = int.TryParse(matches[i].Groups["count"].Value.Trim(), out var parsed) ? parsed : 0;
                        lines[keyColor].Add((count, color));
                    }
                }

            }

            bool isOrContainsShinyGold(string color)
            {
                if (string.Equals(color, shinyGold, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (!lines.ContainsKey(color))
                {
                    return false;
                }

                foreach (var childColor in lines[color])
                {
                    if (isOrContainsShinyGold(childColor.color))
                    {
                        return true;
                    }
                }

                return false;
            }

            int countChildColors(string color)
            {
                if (string.Equals(noOtherColor, color, StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }

                return lines[color].Sum(childColor => childColor.count + (childColor.count * countChildColors(childColor.color)));
            }

            var shinyGoldBags = lines.Count(bagRule => bagRule.Value.Any(color => isOrContainsShinyGold(color.color)));
            builder.Append($"\n1st Answer: {shinyGoldBags}");

            var childBagCount = countChildColors(shinyGold);
            builder.Append($"\n2nd Answer: {childBagCount}");

            return builder.ToString();
        }


    }
}