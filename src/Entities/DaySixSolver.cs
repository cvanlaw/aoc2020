using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DaySixSolver
    {
        private readonly string _inputFile;

        public DaySixSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var lines = new List<(int, Dictionary<char, int>)>();
            var innerDictionary = new Dictionary<char, int>();
            var builder = new StringBuilder();

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;
                var numberOfPersons = 0;

                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        lines.Add((numberOfPersons, innerDictionary));
                        innerDictionary = new Dictionary<char, int>();
                        numberOfPersons = 0;
                        continue;
                    }

                    numberOfPersons++;
                    foreach (var character in line.ToArray())
                    {
                        if (!innerDictionary.ContainsKey(character))
                        {
                            innerDictionary.Add(character, 0);
                        }

                        innerDictionary[character]++;
                    }
                }

                if (innerDictionary.Any())
                {
                    lines.Add((numberOfPersons, innerDictionary));
                }
            }

            var totalYesAnswers = lines
                .Sum(group => group.Item2.Count());
            builder.Append($"\n1st Answer: {totalYesAnswers}");

            var totalAllYes = lines
                .Sum(group => group.Item2.Count(answer => answer.Value == group.Item1));
            builder.Append($"\n2nd Answer: {totalAllYes}");

            return builder.ToString();
        }


    }
}