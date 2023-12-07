namespace AoC2023
{
    public static class DayTemplate
    {
        public static string Part1(IEnumerable<string> lines)
        {
            var sum = lines.Select(line => line.Where(char.IsDigit).ToArray())
                .Select(digits => int.Parse($"{digits.First()}{digits.Last()}")).Sum();

            return sum.ToString();
        }

        public static string Part2(IEnumerable<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}