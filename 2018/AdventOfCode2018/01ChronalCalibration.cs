using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class ChronalCalibration : IAdventOfCodeProblem
    {
        public string PartOne(string[] input)
        {
            return input.Select(ConvertInputLine).Sum().ToString();
        }

        public string PartTwo(string[] input)
        {
            var frequency = 0;
            var hashSet = new HashSet<int> { frequency };

            foreach (var increment in input.Select(ConvertInputLine).Repeat())
            {
                frequency += increment;
                if (!hashSet.Add(frequency)) return frequency.ToString();
            }
            throw new AnswerNotFoundException();
        }

        public int ConvertInputLine(string line)
        {
            return int.Parse(line);
        }
    }
}
