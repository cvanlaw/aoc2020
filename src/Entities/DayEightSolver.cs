using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Entities
{
    public class DayEightSolver
    {
        private readonly string _inputFile;

        public DayEightSolver(string puzzleInput)
        {
            if (!File.Exists(puzzleInput))
            {
                throw new ArgumentException($"Unable to read file {puzzleInput}!");
            }

            this._inputFile = puzzleInput;
        }

        public async Task<string> SolveAsync()
        {
            var instructions = new HashSet<Instruction>();

            var builder = new StringBuilder();

            using (var reader = new StreamReader(this._inputFile))
            {
                string line = null;

                var lineNumber = 1;
                while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                {
                    instructions.Add(new Instruction(lineNumber++, line));
                }
            }

            builder.Append($"\n1st Answer: {SolvePartOne(instructions)}");
            builder.Append($"\n2nd Answer: {SolvePartTwo(instructions)}");
            return builder.ToString();
        }

        private string SolvePartTwo(HashSet<Instruction> instructions)
        {
            var executedInstructions = new HashSet<int>();
            var accumulator = 0;
            var instructionsToTest = instructions.Where(x => string.Equals(x.Operation, "jmp", StringComparison.OrdinalIgnoreCase) || string.Equals(x.Operation, "nop", StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var testInstruction in instructionsToTest)
            {
                bool isFailed = false;
                accumulator = 0;
                executedInstructions = new HashSet<int>();
                for (var i = 0; i < instructions.Count;)
                {
                    if (executedInstructions.Contains(i))
                    {
                        isFailed = true;
                        break;
                    }

                    var currentInstruction = instructions.ElementAt(i);
                    var operation = currentInstruction.Operation;
                    executedInstructions.Add(i);

                    if (i + 1 == testInstruction.Id)
                    {
                        operation = operation switch
                        {
                            "jmp" => "nop",
                            "nop" => "jmp",
                            _ => operation
                        };
                    }

                    if (string.Equals(operation, "acc", StringComparison.OrdinalIgnoreCase))
                    {
                        accumulator += currentInstruction.Argument;
                    }
                    else if (string.Equals(operation, "jmp", StringComparison.OrdinalIgnoreCase))
                    {
                        i += currentInstruction.Argument;
                        continue;
                    }

                    i++;
                }

                if (!isFailed)
                {
                    return $"Instruction {testInstruction} {accumulator} is the solution.";
                }
            }

            return null;
        }

        private string SolvePartOne(HashSet<Instruction> instructions)
        {
            var executedInstructions = new HashSet<int>();
            var accumulator = 0;

            for (var i = 0; i < instructions.Count;)
            {
                if (executedInstructions.Contains(i))
                {
                    break;
                }

                executedInstructions.Add(i);
                var currentInstruction = instructions.ElementAt(i);

                if (string.Equals(currentInstruction.Operation, "acc", StringComparison.OrdinalIgnoreCase))
                {
                    accumulator += currentInstruction.Argument;
                }
                else if (string.Equals(currentInstruction.Operation, "jmp", StringComparison.OrdinalIgnoreCase))
                {
                    i += currentInstruction.Argument;
                    continue;
                }

                i++;
            }

            return $"\n1st Answer: {accumulator}";
        }

        private class Instruction
        {
            public Instruction(int lineNumber, string line)
            {
                var splitLine = line.Split(" ");

                this.Id = lineNumber;
                this.Operation = splitLine[0];
                this.Argument = int.Parse(splitLine[1]);
            }

            public int Id { get; }

            public string Operation { get; }

            public int Argument { get; }

            public override int GetHashCode()
            {
                unchecked // disable overflow, for the unlikely possibility that you
                {         // are compiling with overflow-checking enabled
                    int hash = 27;
                    hash = (13 * hash) + this.Id.GetHashCode();
                    hash = (13 * hash) + this.Operation.GetHashCode();
                    hash = (13 * hash) + this.Argument.GetHashCode();
                    return hash;
                }
            }

            public override string ToString()
            {
                return $"{this.Id}: {this.Operation} {this.Argument}";
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as Instruction);
            }

            public bool Equals(Instruction that)
            {
                if (that == null)
                {
                    return false;
                }

                return this.Id == that.Id
                    && this.Argument == that.Argument
                    && string.Equals(this.Operation, that.Operation, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}