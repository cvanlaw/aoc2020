using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2020
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = new List<int>();
            using (var reader = new StreamReader("../puzzle_input/day_1.txt"))
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
                        Console.WriteLine($"Answer: {thisLine * nextLine}");
                    }

                    for (var k = j + 1; k < lines.Count; k++)
                    {
                        var thirdLine = lines[k];

                        if (thisLine + nextLine + thirdLine == 2020)
                        {
                            Console.WriteLine($"2nd Answer: {thisLine * nextLine * thirdLine}");
                            break;
                        }
                    }
                }
            }


        }
    }
}