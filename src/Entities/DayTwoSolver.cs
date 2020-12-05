using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DayTwoSolver
    {
        private readonly string _inputFile;

        public DayTwoSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var lines = new List<DayTwoLine>();
            var builder = new StringBuilder();

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;
                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    lines.Add(new DayTwoLine(line));
                }
            }

            Console.WriteLine($"Searching {lines.Count} lines in file...");
            var validCount = lines.Count(x => x.IsValidOne());
            builder.Append($"\n1st Answer: {validCount}");

            var secondValidCount = lines.Count(x => x.IsValidTwo());
            builder.Append($"\n2nd Answer: {secondValidCount}");
            return builder.ToString();
        }

        private class DayTwoLine
        {
            public DayTwoLine(string input)
            {
                var match = (new Regex(@"((\d*)-(\d*))\s([a-z|A-Z]):\s(\w*)", RegexOptions.Compiled)).Match(input);

                this.MinimumCount = int.Parse(match.Groups[2].ToString());
                this.MaximumCount = int.Parse(match.Groups[3].ToString());
                this.Character = char.Parse(match.Groups[4].ToString());
                this.Password = match.Groups[5].ToString();
                this.Raw = input;
            }

            public int MinimumCount { get; set; }

            public int MaximumCount { get; set; }

            public char Character { get; set; }

            public string Password { get; set; }

            public string Raw { get; set; }

            public bool IsValidOne()
            {
                var matchingCharacters = this.Password.Where(c => c == this.Character).ToList();
                return matchingCharacters.Count >= this.MinimumCount && matchingCharacters.Count <= this.MaximumCount;
            }

            public bool IsValidTwo()
            {
                return (this.Password.ToArray()[this.MinimumCount - 1] == this.Character && this.Password.ToArray()[this.MaximumCount - 1] != this.Character)
                    || (this.Password.ToArray()[this.MaximumCount - 1] == this.Character && this.Password.ToArray()[this.MinimumCount - 1] != this.Character);
            }
        }
    }
}