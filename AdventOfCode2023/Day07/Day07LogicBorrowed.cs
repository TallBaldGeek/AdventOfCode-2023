namespace Day07
{
	public class Day07LogicBorrowed
	{
		public int Solve(string[] input, string cardOrder, string jokers)
		{
			//stolen from https://topaz.github.io/paste/#XQAAAQCSBgAAAAAAAAA7GEqmJ5Le0P3LfcA85sdnlcENceEvmxqfWj1yTKiWfu9rF7GKftrYn8L2o1/vnTLXPxHBpFetSatc2DVD2CtbgKCwIzOD6qAiSICDfF4nYcQmmX4cMXSQpNCIpuLml04cYFVY87PknwsTTMg11GqZA5QydydP1VImF7/oscAVNFKgUA/zctc+3gc7ghpKggTyENmpzY211m7kd47ze+kGeH6LDqd5pE6s9q714KkNa+Hc3RduUrKikeHL6JlpVMK/UdnkIr7Nkyb+qRrXMmP3JMgEsJZTfOHKQjNSd8llCO1l21o9xtBy48lyOGPneomSubFd2XFXJLF6dMWvxrggYpudYUq8/N3Y9WoNZzfLddIpKTuv1mSqIa2L2BiOx/V0sMwO9zGtooOUEHbrJ8tFuEV4Sw+ZTwBoGfHewfPjZmPpjWTz8TH602awHXS30aRA8l11WsjEj/Up0nF2YynmqjQ7L+ir0tNaqC8Z7pWzg+mjNxEV9A6pnq63K9Icw5y8afOsx8MtAPsxcpBeJW8XG2Hp/+tgVdkcWPOh1pHemGIOvGwHHqo8KHHeZTAyyz2PbUWzZ2/j0YcmsHu8uw55RPze5xjFlyt2ED4zCigUQTsT6v3zUQLrOTkgpMuU6CWN1y5zgG7jxl6W2UDlbX74rlnRBj/1kyXTqGY/d4k85IsQOA8fWnSV2UK34Vy+y/1KU/J3WSc5wLUtSAIedfv28e1voYDKtQMNSMbq3yxDHNuK/usw8nf1nKIm2pec/LYWLVl941Of5LQNx009u/whxwYGJ3jfFB9zUBVqyGJU3FZl9Dq2jDSHoArUg3uxJKlEhyVvn4ARuv+mAPgKRCBfuYR+IdIUQGlbO0RAlcXWP+sNw5Na8V14SaB9KLJP/9Gwkpw=

			//var input = File.ReadAllLines("input.txt");

			var parseLine = (string line, string cardOrder, string jokers) =>
			{
				var parts = line.Split(' ');
				var hand = parts[0];
				var bid = int.Parse(parts[1]);

				HandType handType = HandType.FiveOfAKind;
				var handWithoutJokers = jokers != "" ? hand.Replace(jokers, "") : hand;
				var numJokers = hand.Length - handWithoutJokers.Length;
				var groups =
					  handWithoutJokers
					  .GroupBy(x => x)
					  .Select(x => x.Count())
					  .OrderByDescending(x => x)
					  .Concat(new[] { 0 })
					  .ToArray();
				groups[0] += numJokers;
				handType = groups switch
				{
					[5, ..] => HandType.FiveOfAKind,
					[4, ..] => HandType.FourOfAKind,
					[3, 2, ..] => HandType.FullHouse,
					[3, ..] => HandType.ThreeOfAKind,
					[2, 2, ..] => HandType.TwoPair,
					[2, ..] => HandType.OnePair,
					[..] => HandType.HighCard,
				};

				var weight = hand.Select((card, index) => cardOrder.IndexOf(card) << (4 * (5 - index))).Sum();

				return (hand, handType, weight, bid);
			};

			var solve = (string cardOrder, string jokers) =>
			{
				var hands = input.Select(line => parseLine(line, cardOrder, jokers));
				var orderedHands = hands.OrderBy(x => x.handType).ThenBy(x => x.weight);
				var result = orderedHands.Select((hand, index) => hand.bid * (index + 1)).Sum();
				return result;
			};

			return solve(cardOrder, jokers);
		}

	}
	enum HandType
	{
		HighCard,
		OnePair,
		TwoPair,
		ThreeOfAKind,
		FullHouse,
		FourOfAKind,
		FiveOfAKind,
	};

}
