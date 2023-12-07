using System.Collections.Immutable;

namespace AoC2023
{
    public static class Day7
    {
        public record Hand(string Cards, int Bid)
        {
            public Hand(string line) : this(line.Split(" ")[0], int.Parse(line.Split(" ")[1])) {}
        }

        public static int CompareHands(Hand hand1, Hand hand2)
        {
            // Count the number of each card in each hand
            var hand1Counts = CountCards(hand1);
            var hand2Counts = CountCards(hand2);
            // Get the values sorted in descending order
            /*
             * 5,0
             * 4,1
             * 3,2
             * 3,1,1
             * 2,2,1
             * 2,1,1,1
             * 1,1,1,1,1
             */
            var hand1CardCounts = hand1Counts.Values.OrderByDescending(x => x).ToList();
            var hand2CardCounts = hand2Counts.Values.OrderByDescending(x => x).ToList();

            // Bigger amount of groups means automatically weaker set
            var order = hand2CardCounts.Count - hand1CardCounts.Count;
            if (order != 0) return order;
            
            // Compare counts of biggest sets
            // * (3,1,1 vs 2,2,1) => 3 > 2 -> bigger
            // * (3,1,1 vs 3,2) => 3 == 3 -> equal, 1 < 2 -> smaller
            for (var i = 0; i < hand1CardCounts.Count; ++i)
            {
                order = hand1CardCounts[i] - hand2CardCounts[i];
                if (order != 0) return order;
            }

            for (var i = 0; i < hand1.Cards.Length; ++i)
            {
                var hand1CardScore = GetCardScore(hand1.Cards[i]);
                var hand2CardScore = GetCardScore(hand2.Cards[i]);
                
                order = hand1CardScore - hand2CardScore;
                if (order != 0) return order;
            }
            return order;
        }

        public static int GetCardScore(char card)
        {
            return card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => card - '0'
            };
        }

        public static string Part1(IEnumerable<string> lines)
        {
            var hands = lines.Select(x => new Hand(x)).ToList();
            hands.Sort(CompareHands);
            // Count each hands Bid multiplied with order in the hands list and sum all of these together
            var sum = hands.Select((hand, index) => hand.Bid * (index + 1)).Sum();

            return sum.ToString();
        }

        public static string Part2(IEnumerable<string> lines)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<char, int> CountCards(Hand hand)
        {
            var cardCounts = new Dictionary<char, int>();
            foreach (var card in hand.Cards)
            {
                if (cardCounts.ContainsKey(card))
                {
                    cardCounts[card]++;
                }
                else
                {
                    cardCounts[card] = 1;
                }
            }
            return cardCounts;
        }
    }
}