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

            var numberOfValidPassports = passports.Count(p => PassportValidators.IsValid(p));
            builder.Append($"Answer: {numberOfValidPassports}");

            return builder.ToString();
        }

        private class PassportValidators
        {
            public static bool IsValid(Dictionary<string, string> passport)
            {
                return passport.ContainsKey("byr")
                    && passport.ContainsKey("iyr")
                    && passport.ContainsKey("eyr")
                    && passport.ContainsKey("hgt")
                    && passport.ContainsKey("hcl")
                    && passport.ContainsKey("ecl")
                    && passport.ContainsKey("pid")
                    && passport.All(kv => PassportValidators.FieldIsValid(kv));
            }

            private static bool FieldIsValid(KeyValuePair<string, string> field)
            {
                return field.Key switch
                {
                    "byr" => int.TryParse(field.Value, out var birthYear) && birthYear >= 1920 && birthYear <= 2002,
                    "iyr" => int.TryParse(field.Value, out var issueYear) && issueYear >= 2010 && issueYear <= 2020,
                    "eyr" => int.TryParse(field.Value, out var expireYear) && expireYear >= 2020 && expireYear <= 2030,
                    "hgt" => PassportValidators.HeightIsValid(field),
                    "hcl" => Regex.IsMatch(field.Value, @"^#[0-9|a-f]{6}$"),
                    "ecl" => Regex.IsMatch(field.Value, @"^(amb|blu|brn|gry|grn|hzl|oth)$"),
                    "pid" => Regex.IsMatch(field.Value, @"^[0-9]{9}$"),
                    "cid" => true,
                    _ => false
                };
            }

            private static bool HeightIsValid(KeyValuePair<string, string> heightField)
            {
                var match = (new Regex(@"((\d+)(in|cm)){1}", RegexOptions.Compiled)).Match(heightField.Value);
                var parseResult = int.TryParse(match.Groups[2].Value, out var value);
                var cmValid = (match.Groups[3].Value.Equals("cm") && value >= 150 && value <= 193);
                var inchesValid = (match.Groups[3].Value.Equals("in") && value >= 59 && value <= 76);

                return match.Success
                    && parseResult
                    && (cmValid || inchesValid);
            }

        }
    }
}