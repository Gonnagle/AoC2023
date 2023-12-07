namespace AoC2023
{
    public static class Day2
    {
        public static string Part1(IEnumerable<string> lines)
        {
            var sum = 0;
            
            foreach (var line in lines)
            {
                var indexOfGameInfoSeparator  = line.IndexOf(':');
                var gameId = int.Parse(line[..indexOfGameInfoSeparator].Split(" ")[1]);
                var grabs = line[(indexOfGameInfoSeparator + 2)..].Split(";");
                var okGame = true;
                
                foreach (var grab in grabs)
                {
                    // Convert comma separated string into dictionary with red, blue and green counts
                    var grabInfo = grab.Trim().Split(",").Select(s => s.Trim().Split(" ")).ToDictionary(s => s[1], s => int.Parse(s[0]));
                    var redCount = grabInfo.TryGetValue("red", out var value ) ? value : 0;
                    var blueCount = grabInfo.TryGetValue("blue", out value ) ? value : 0;
                    var greenCount = grabInfo.TryGetValue("green", out value ) ? value : 0;
                    if (redCount > 12 || blueCount > 13 || greenCount > 14)
                    {
                        okGame = false;
                        break;
                    }
                }
                if (okGame)
                {
                    sum += gameId;
                }
            }
            return sum.ToString();
        }

        public static string Part2(IEnumerable<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}