using MoreLinq;
using System;
using System.Linq;

namespace AdventOfCode2018.Problems
{
    public class InventoryManagementSystem : IAdventOfCodeProblem
    {
        public string PartOne(string[] input)
        {
            (var two, var three) = input.Select(CheckLine).Aggregate((a, b) => (a.Two + b.Two, a.Three + b.Three));

            return (two * three).ToString();
        }

        public string PartTwo(string[] input)
        {
            var lengthToFind = input[0].Length - 1;

            return input.Cartesian(input, (a, b) => new string(a.ZipIfEqual(b, (c, d) => c).ToArray())).First(i => i.Length == lengthToFind);
        }

        private (int Two, int Three) CheckLine(string line)
        {
            var lookup = line.ToLookup(c => c);
            var two = lookup.Any(i => i.Count() == 2) ? 1 : 0;
            var three = lookup.Any(i => i.Count() == 3) ? 1 : 0;

            return (two, three);
        }
    }
}
