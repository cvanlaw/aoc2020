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

            List<(int x, int y)> slopes = new List<(int x, int y)> { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            var treeProduct = 0;
            var maxY = lines.Count;
            var maxX = lines.First().Length;

            foreach (var slope in slopes)
            {
                var treeCount = 0;
                (int x, int y) currentPosition = (0, 0);

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

                if (slope.x == 3 && slope.y == 1)
                {
                    builder.Append($"\n1st Answer: {treeCount}");
                }

                treeProduct = treeProduct == 0 ? treeCount : treeProduct * treeCount;
            }

            builder.Append($"\n2nd Answer: {treeProduct}");

            return builder.ToString();
        }
    }
}