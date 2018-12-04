using AdventOfCode2018;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.IO;

namespace Tests
{
    public class AdventOfCodeProblemTests
    {
        private const string UnknownResult = "UnknownResult";

        [Test, TestCaseSource(nameof(PartOneTestCases))]
        public string PartOneTest(IAdventOfCodeProblem problem, string fileName)
        {
            return RunTest(problem, fileName, problem.PartOne);
        }

        [Test, TestCaseSource(nameof(PartTwoTestCases))]
        public string PartTwoTest(IAdventOfCodeProblem problem, string fileName)
        {
            return RunTest(problem, fileName, problem.PartTwo);
        }

        private string RunTest(IAdventOfCodeProblem problem, string fileName, Func<string[], string> action)
        {
            var input = GetInput(fileName);

            var result = action(input);

            Console.WriteLine($"{problem.GetType().Name} = '{result}'");

            return result;
        }

        public static IEnumerable PartOneTestCases => GetPartOneTestCases();
        public static IEnumerable PartTwoTestCases => GetPartTwoTestCases();

        private static IEnumerable GetPartOneTestCases()
        {
            yield return new TestCaseData(new ChronalCalibration(), "01ChronalCalibration.dat").Returns("580");
        }

        private static IEnumerable GetPartTwoTestCases()
        {
            yield return new TestCaseData(new ChronalCalibration(), "01ChronalCalibration.dat").Returns("81972");
        }

        private static string[] GetInput(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return new string[0];

            return File.ReadAllLines($@"Inputs\{fileName}");
        }
    }
}