using AoC2023;
using FluentAssertions;

namespace AoC2023Tests;

public class HandTests
{
    [Theory]
    [InlineData("22222", "11111", 1)]
    [InlineData("22333", "22333", 0)]
    [InlineData("23233", "22333", 1)]
    [InlineData("11111", "22222", -1)]
    [InlineData("AAAAA", "KKKKK", 1)]
    [InlineData("33332", "2AAAA", 1)]
    [InlineData("2AAAA", "33332", -1)]
    [InlineData("77888", "77788", 1)]
    [InlineData("TTTTT", "99999", 1)]
    [InlineData("A37Q2", "A37KJ", -1)]
    [InlineData("888K8", "QTQQT", 1)]
    [InlineData("A2A3A", "A2AKA", -1)]
    [InlineData("AKQQQ", "22323", -1)]
    [InlineData("A7KQJ", "A8123", -1)]
    [InlineData("AAKAQ", "AA3A3", -1)]
    [InlineData("AA3A3", "AAKAQ", 1)]
    [InlineData("2A2A9", "KAQAT", 1)]
    public void HandComparisonWorksAsExpected(string hand1Cards, string hand2Cards, int expectedOutcome)
    {
        var hand1 = new Day7.Hand($"{hand1Cards} 373");
        var hand2 = new Day7.Hand($"{hand2Cards} 373");

        var result = Day7.CompareHands(hand1, hand2);

        if (result != 0)
        {
            result /= int.Abs(result);
        }

        result.Should().Be(expectedOutcome);
    }

    [Theory]
    [InlineData('A', 14)]
    [InlineData('K', 13)]
    [InlineData('Q', 12)]
    [InlineData('J', 11)]
    [InlineData('T', 10)]
    [InlineData('9', 9)]
    [InlineData('1', 1)]
    public void CardScoringWorksAsExpected(char card, int expectedScore)
    {
        Day7.GetCardScore(card).Should().Be(expectedScore);
    }
}