using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DayOneSolver
    {
        private readonly string _inputFile;

        public DayOneSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var lines = new List<int>();
            var builder = new StringBuilder();

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;
                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    lines.Add(int.Parse(line));
                }
            }

            Console.WriteLine($"Searching {lines.Count} lines in file...");

            for (var i = 0; i < lines.Count - 1; i++)
            {
                var thisLine = lines[i];

                for (var j = i + 1; j < lines.Count - 2; j++)
                {
                    var nextLine = lines[j];

                    if (thisLine + nextLine == 2020)
                    {
                        builder.Append($"\n1st Answer: {thisLine * nextLine}");
                    }

                    for (var k = j + 1; k < lines.Count; k++)
                    {
                        var thirdLine = lines[k];

                        if (thisLine + nextLine + thirdLine == 2020)
                        {
                            builder.Append($"\n2nd Answer: {thisLine * nextLine * thirdLine}");
                            break;
                        }
                    }
                }
            }

            return builder.ToString();
        }
    }
}