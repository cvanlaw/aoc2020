using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DayFiveSolver
    {
        private readonly string _inputFile;

        public DayFiveSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var boardingPasses = new List<BoardingPass>();
            var builder = new StringBuilder();

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;

                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    boardingPasses.Add(new BoardingPass(line));
                }
            }

            builder.Append($"1st Answer {boardingPasses.Max(x => x.SeatId)}");

            return builder.ToString();
        }


    }

    public class BoardingPass
    {
        public BoardingPass(string value)
        {
            this.Value = value;
        }
        public string Value { get; }

        public int Row
        {
            get
            {
                var rowPart = this.Value.Substring(0, 7);
                var minRow = 0;
                var maxRow = 127;
                var lastTouched = 0;

                foreach (var part in rowPart.ToArray())
                {
                    if (part == 'F')
                    {
                        maxRow = maxRow - ((maxRow - minRow) / 2);
                        lastTouched = maxRow;
                    }
                    else
                    {
                        minRow = (maxRow - minRow) / 2 + 1 + minRow;
                        lastTouched = minRow;
                    }
                }

                return minRow - 1;
            }
        }

        public int Column
        {
            get
            {
                var columnPart = this.Value.Substring(7, 3);
                var minCol = 0;
                var maxCol = 7;
                var lastTouched = 0;

                foreach (var part in columnPart.ToArray())
                {
                    if (part == 'L')
                    {
                        maxCol = maxCol - ((maxCol - minCol) / 2);
                        lastTouched = maxCol;
                    }
                    else
                    {
                        minCol = (maxCol - minCol) / 2 + 1 + minCol;
                        lastTouched = minCol;
                    }
                }

                return minCol;
            }
        }

        public int SeatId => this.Row * 8 + this.Column;
    }
}