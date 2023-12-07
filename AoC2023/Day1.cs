namespace AoC2023
{
    public static class Day1
    {
        private static readonly Dictionary<string, Tuple<string, char>> Numbers = new()
        {
            {"on", new("one", '1')},
            {"tw", new("two", '2')},
            {"th", new("three", '3')},
            {"fo", new("four", '4')},
            {"fi", new("five", '5')},
            {"si", new("six", '6')},
            {"se", new("seven", '7')},
            {"ei", new("eight", '8')},
            {"ni", new("nine", '9')}
        };

        public static string Part1(IEnumerable<string> lines)
        {
            var sum = lines.Select(line => line.Where(char.IsDigit).ToArray()).Select(digits => int.Parse($"{digits.First()}{digits.Last()}")).Sum();

            return sum.ToString();
        }

        public static string Part2(IEnumerable<string> lines)
        {
            var sum = (from line in lines let first = FindFirst(line) let last = FindLast(line) select int.Parse($"{first}{last}")).Sum();
            return sum.ToString();
        }

        private static char FindLast(string line)
        {
            for (var i = line.Length - 1; i >= 0; --i)
            {
                if (char.IsDigit(line[i]))
                {
                    return line[i];
                }
                if (i + 2 < line.Length && Numbers.ContainsKey(line[i..(i + 2)]))
                {
                    var candidate = Numbers[line[i..(i + 2)]];
                    if (line[i..].StartsWith(candidate.Item1))
                    {
                        return candidate.Item2;
                    }
                }
            }
            throw new Exception("No number found");
        }
        
        private static char FindFirst(string line)
        {
            for (var i = 0; i < line.Length; ++i)
            {
                if (char.IsDigit(line[i]))
                {
                    return line[i];
                }
                var rest = line[i..];
                if (i + 2 < line.Length && Numbers.ContainsKey(rest[..2]))
                {
                    var candidate = Numbers[rest[..2]];
                    if (rest.StartsWith(candidate.Item1))
                    {
                        return candidate.Item2;
                    }
                }
            }
            throw new Exception("No number found");
        }
    }
}