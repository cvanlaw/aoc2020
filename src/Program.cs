using AoC2020.Entities;
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
            var dayOneSolver = new DayOneSolver("../puzzle_input/day_1.txt");
            var solution = await dayOneSolver.SolveAsync();
            Console.WriteLine($"Day 1: {solution}.");
        }
    }
}