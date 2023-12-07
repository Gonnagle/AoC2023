using System.Diagnostics;
using System.Reflection;
using AoC2023;
using FluentAssertions;
using Xunit.Abstractions;

namespace AoC2023Tests
{
    public class DailyTests(ITestOutputHelper testOutputHelper)
    {
        private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

        private readonly Dictionary<int, Result> _expectedExampleResults = new()
        {
            {1, new Result("142", "281")},
            {2, new Result("8", "")},
            {7, new Result("6440", "")},
        };
        public record Result(string Part1, string Part2);

        public static IEnumerable<object[]> ExampleFiles()
            => TestUtilities.ExampleFiles();

        public static IEnumerable<object[]> InputFiles()
            => TestUtilities.InputFiles();

        [Theory]
        [MemberData(nameof(ExampleFiles))]
        public void ExamplePart1YieldsExpectedResult(string filePath, int day, bool partTwo)
        {
            if (partTwo) return; // Skip
            
            testOutputHelper.WriteLine($"{day}, {filePath}");
            var lines = File.ReadLines(filePath);
            var methodInfo = ResolveMethodToTest(day, false);

            var result = methodInfo.Invoke(null, new object?[]{lines})!.ToString();
            
            _testOutputHelper.WriteLine($"Day {day} / Part 1 Example Result: {result}");
            testOutputHelper.WriteLine(result);
            result.Should().Be(_expectedExampleResults[day].Part1);
        }

        [Theory]
        [MemberData(nameof(InputFiles))]
        public void Part1Execution(string filePath, int day)
        {
            testOutputHelper.WriteLine($"{day}, {filePath}");
            var lines = File.ReadLines(filePath);
            var methodInfo = ResolveMethodToTest(day, false);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = methodInfo.Invoke(null, new object?[]{lines})!.ToString();

            _testOutputHelper.WriteLine($"Day {day} / Part 1 Result in {stopWatch.ElapsedMilliseconds} ms:\n{result}");
            testOutputHelper.WriteLine($"In {stopWatch.ElapsedMilliseconds} ms");
            testOutputHelper.WriteLine(result);
        }

        [Theory]
        [MemberData(nameof(ExampleFiles))]
        public void ExamplePart2YieldsExpectedResult(string filePath, int day, bool partTwo)
        {
            testOutputHelper.WriteLine($"{day}, {filePath}");
            var lines = File.ReadLines(filePath);
            var methodInfo = ResolveMethodToTest(day, true);

            var result = methodInfo.Invoke(null, new object?[] { lines })!.ToString();

            _testOutputHelper.WriteLine($"Day {day} / Part 2 Example Result: {result}");
            testOutputHelper.WriteLine(result);
            result.Should().Be(_expectedExampleResults[day].Part2);
        }

        [Fact]
        public void CurrentDay(){
            var result = Day1.Part1(File.ReadLines("Inputs/Day1Example.txt"));
            result.Should().Be(_expectedExampleResults[1].Part1);
        }

        [Theory]
        [MemberData(nameof(InputFiles))]
        public void Part2Execution(string filePath, int day)
        {
            testOutputHelper.WriteLine($"{day}, {filePath}");
            var lines = File.ReadLines(filePath);
            var methodInfo = ResolveMethodToTest(day, true);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = methodInfo.Invoke(null, new object?[] { lines })!.ToString();
            
            _testOutputHelper.WriteLine($"Day {day} / Part 2 Result in {stopWatch.ElapsedMilliseconds} ms:\n{result}");
            testOutputHelper.WriteLine(result);
        }
        private static MethodInfo ResolveMethodToTest(int day, bool part2)
        {
            var className = $"Day{day}";
            var assembly = Assembly.GetAssembly(typeof(Day1));
            var type = assembly!.GetExportedTypes().First(x => x.Name == className);
            return part2 ? type.GetMethod(nameof(Day1.Part2))! : type.GetMethod(nameof(Day1.Part1))!;
        }
    }
}