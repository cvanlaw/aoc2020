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

            builder.Append($"\n1st Answer {boardingPasses.Max(x => x.SeatId)}");

            var previousId = 0;
            var orderedPasses = boardingPasses.OrderBy(x => x.SeatId);
            foreach (var pass in orderedPasses)
            {
                if (pass.SeatId - previousId == 2 && previousId != 0)
                {
                    builder.Append($"\n2nd Answer: {previousId + 1}");
                }

                previousId = pass.SeatId;
            }

            return builder.ToString();
        }


    }

    public class BoardingPass
    {
        private static List<int> PossibleRows = Enumerable.Range(0, 128).ToList();
        private static List<int> PossibleColumns = Enumerable.Range(0, 8).ToList();

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
                var rowWorkingList = PossibleRows;
                foreach (var part in rowPart.ToArray())
                {
                    if (part == 'F')
                    {
                        rowWorkingList = rowWorkingList.Take(rowWorkingList.Count / 2).ToList();
                    }
                    else
                    {
                        rowWorkingList = rowWorkingList.Skip(rowWorkingList.Count / 2).ToList();
                    }
                }

                return rowWorkingList.First();
            }
        }

        public int Column
        {
            get
            {
                var columnPart = this.Value.Substring(7, 3);
                var columnWorkingList = PossibleColumns;
                foreach (var part in columnPart.ToArray())
                {
                    if (part == 'L')
                    {
                        columnWorkingList = columnWorkingList.Take(columnWorkingList.Count / 2).ToList();
                    }
                    else
                    {
                        columnWorkingList = columnWorkingList.Skip(columnWorkingList.Count / 2).ToList();
                    }
                }

                return columnWorkingList.First();
            }
        }

        public int SeatId => this.Row * 8 + this.Column;

        public override string ToString()
        {
            return this.SeatId.ToString();
        }
    }
}