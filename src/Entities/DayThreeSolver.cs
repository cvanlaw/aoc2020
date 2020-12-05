using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DayThreeSolver
    {
        private readonly string _inputFile;

        public DayThreeSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var lines = new List<string>();
            var builder = new StringBuilder();

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;
                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    lines.Add(line);
                }
            }

            (int x, int y) slope = (3, 1);
            var treeCount = 0;

            (int x, int y) currentPosition = (0, 0);
            var maxY = lines.Count;
            var maxX = lines.First().Length;

            while (currentPosition.y < maxY)
            {
                currentPosition.x += slope.x;

                if (currentPosition.x >= maxX)
                {
                    currentPosition.x -= maxX;
                }

                currentPosition.y += slope.y;

                if (currentPosition.y >= maxY)
                {
                    break;
                }

                if (lines[currentPosition.y].ToCharArray()[currentPosition.x] == '#')
                {
                    treeCount++;
                }
            }

            builder.Append($"1st Answer: {treeCount}");

            return builder.ToString();
        }
    }
}