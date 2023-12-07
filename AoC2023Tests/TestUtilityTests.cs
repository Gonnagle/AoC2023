﻿using FluentAssertions;
using Xunit.Abstractions;

namespace AoC2023Tests
{
    public class TestUtilityTests(ITestOutputHelper testOutputHelper)
    {
        [Fact]
        public void InputFilesList()
        {
            foreach (var testFile in TestUtilities.InputFiles())
            {
                testOutputHelper.WriteLine($"{testFile[0]}, {testFile[1]}");
            }

            var testFiles = TestUtilities.InputFiles().ToList();
            testFiles.Count().Should().BeGreaterOrEqualTo(3);
            ((string)testFiles.First()[0]).Should().EndWith("Day1.txt");
            ((int)testFiles.First()[1]).Should().Be(1);
        }

        [Theory]
        [InlineData("Day1.txt", 1)]
        [InlineData("Day1Example.txt", 1)]
        [InlineData("Day23Example.txt", 23)]
        public void ResolvesDayFromFileName(string fileName, int dayNumber)
        {
            TestUtilities.ResolveDayFromFileName(fileName).Should().Be(dayNumber);
        }
    }
}
