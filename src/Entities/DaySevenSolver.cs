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
            var lines = new Dictionary<string, List<string>>();
            var builder = new StringBuilder();
            var regex = new Regex(@"((?>\w+\b) (?>\w+\b)) (?>bag)", RegexOptions.Compiled);
            const string shinyGold = "shiny gold";

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;

                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    var matches = regex.Matches(line);

                    var keyColor = matches.First().Groups[1].Value;
                    lines.Add(keyColor, new List<string>());

                    for (var i = 1; i < matches.Count; i++)
                    {
                        lines[keyColor].Add(matches[i].Groups[1].Value);
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
                    if (isOrContainsShinyGold(childColor))
                    {
                        return true;
                    }
                }

                return false;
            }

            var shinyGoldBags = lines.Count(bagRule => bagRule.Value.Any(color => isOrContainsShinyGold(color)));
            builder.Append($"\n1st Answer: {shinyGoldBags}");

            return builder.ToString();
        }


    }
}