using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DayFourSolver
    {
        private readonly string _inputFile;

        public DayFourSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var passports = new List<Dictionary<string, string>>();
            var builder = new StringBuilder();
            var regex = new Regex(@"(\w*):(\S*)\b+", RegexOptions.Compiled);

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;
                var tempInnerList = new List<KeyValuePair<string, string>>();

                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        passports.Add(tempInnerList.ToDictionary(k => k.Key, v => v.Value));
                        tempInnerList = new List<KeyValuePair<string, string>>();
                        continue;
                    }

                    tempInnerList.AddRange(
                        regex.Matches(line)
                            .Select(m =>
                                new KeyValuePair<string, string>(
                                    m.Groups[1].ToString(),
                                    m.Groups[2].ToString())));

                }

                if (tempInnerList.Any())
                {
                    passports.Add(tempInnerList.ToDictionary(k => k.Key, v => v.Value));
                }
            }

            var numberOfValidPassports = passports.Count(p => p.ContainsKey("byr")
                    && p.ContainsKey("iyr")
                    && p.ContainsKey("eyr")
                    && p.ContainsKey("hgt")
                    && p.ContainsKey("hcl")
                    && p.ContainsKey("ecl")
                    && p.ContainsKey("pid"));
            builder.Append($"1st Answer: {numberOfValidPassports}");

            return builder.ToString();
        }
    }
}