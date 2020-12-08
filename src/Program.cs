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
            var solution = await dayOneSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 1: {solution}.");

            var dayTwoSolver = new DayTwoSolver("../puzzle_input/day_2.txt");
            var solution2 = await dayTwoSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 2: {solution2}.");

            var dayThreeSolver = new DayThreeSolver("../puzzle_input/day_3.txt");
            var solution3 = await dayThreeSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 3: {solution3}.");

            var dayFourSolver = new DayFourSolver("../puzzle_input/day_4.txt");
            var solution4 = await dayFourSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 4: {solution4}.");

            var dayFiveSolver = new DayFiveSolver("../puzzle_input/day_5.txt");
            var solution5 = await dayFiveSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 5: {solution5}.");

            var daySixSolver = new DaySixSolver("../puzzle_input/day_6.txt");
            var solution6 = await daySixSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 6: {solution6}.");

            var daySevenSolver = new DaySevenSolver("../puzzle_input/day_7.txt");
            var solution7 = await daySevenSolver.SolveAsync().ConfigureAwait(false);
            Console.WriteLine($"Day 7: {solution7}.");
        }
    }
}