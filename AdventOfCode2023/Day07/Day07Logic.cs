using System.Reflection;
using Utilities;

namespace Day07
{
	public class Day07Logic
	{


		public static string[] GetInput()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
		}

		public static string[] GetSample()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "sample.txt");
		}

		/*
		In Camel Cards, you get a list of hands, and your goal is to order them based on the 
		strength of each hand. 
		A hand consists of five cards labeled one of A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2. 
		The relative strength of each card follows this order, where A is the highest and 2 is the lowest.


Every hand is exactly one type. From strongest to weakest, they are:

Five of a kind, where all five cards have the same label: AAAAA
Four of a kind, where four cards have the same label and one card has a different label: AA8AA
Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
High card, where all cards' labels are distinct: 23456

		 */
		public int PartOne(string[] input)
		{
			var groupedHands = new Dictionary<HandStrength, List<HandInfo>>();
			foreach (var item in Enum.GetValues<HandStrength>())
			{
				groupedHands[item] = new List<HandInfo>();
			}
			//var ungroupedHands = new List<HandInfo>();
			//var groupedHands = new Dictionary<string, List<HandInfo>>();
			foreach (var s in input)
			{
				var segments = s.Split(' ');
				var handInfo = new HandInfo(segments[0].ToCharArray(), int.Parse(segments[1]));
				//ungroupedHands.Add();
				var strength = handInfo.GetStrength();
				groupedHands[strength].Add(handInfo);
			}

			//var fiveOfAKinds = groupedHands[HandStrength.FiveOfAKind];
			var sorter = new HandSorter();
			foreach (var key in groupedHands.Keys)
			{
				groupedHands[key].Sort(sorter);
			}
			var completeList = new List<HandInfo>();
			completeList.AddRange(groupedHands[HandStrength.HighCard]);
			completeList.AddRange(groupedHands[HandStrength.OnePair]);
			completeList.AddRange(groupedHands[HandStrength.TwoPair]);
			completeList.AddRange(groupedHands[HandStrength.ThreeOfAKind]);
			completeList.AddRange(groupedHands[HandStrength.FullHouse]);
			completeList.AddRange(groupedHands[HandStrength.FourOfAKind]);
			completeList.AddRange(groupedHands[HandStrength.FiveOfAKind]);

			var bidSum = 0;
			for (int i = 0; i < completeList.Count; i++)
			{
				bidSum += completeList[i].Bid * (i + 1);
			}
			//fiveOfAKinds.Sort((h) => h.Cards[0]);
			return bidSum;
		}
	}
	enum HandStrength
	{
		HighCard = 1,
		OnePair = 2,
		TwoPair = 3,
		ThreeOfAKind = 4,
		FullHouse = 5,
		FourOfAKind = 6,
		FiveOfAKind = 7
	}

	record HandInfo(char[] Cards, int Bid)
	{
		public override string ToString()
		{
			return $"Cards = {string.Concat(Cards)} Bid = {Bid}";
		}

		public HandStrength GetStrength()
		{
			var cardGroups = Cards.GroupBy(c => c);
			var groupCount = cardGroups.Count();
			return groupCount switch
			{
				5 =>
					//no cards in common
					HandStrength.HighCard,
				4 =>
					//one pair and 3 singles
					HandStrength.OnePair,
				3 =>
					//three of a kind + two singles
					//two pair + 1 single
					cardGroups.SingleOrDefault(x => x.Count() == 3) == null
						? HandStrength.TwoPair
						: HandStrength.ThreeOfAKind,
				2 => cardGroups.SingleOrDefault(x => x.Count() == 4) == null
						? HandStrength.FullHouse
						: HandStrength.FourOfAKind,
				_ => HandStrength.FiveOfAKind
			};
		}
	}

	class HandSorter : IComparer<HandInfo>
	{
		private static Dictionary<char, int> _cardValues = new Dictionary<char, int>()
		{
			{ '2', 2 },
			{ '3', 3 },
			{ '4', 4 },
			{ '5', 5 },
			{ '6', 6 },
			{ '7', 7 },
			{ '8', 8 },
			{ '9', 9 },
			{ 'T', 10 },
			{ 'J', 11 },
			{ 'Q', 12 },
			{ 'K', 13 },
			{ 'A', 14 }
		};

		public int Compare(HandInfo? x, HandInfo? y)
		{
			for (var i = 0; i < x.Cards.Length; i++)
			{
				if (x.Cards[i] == y.Cards[i])
				{
					continue;
				}

				var xVal = _cardValues[x.Cards[i]];
				var yVal = _cardValues[y.Cards[i]];
				return xVal.CompareTo(yVal);
			}

			return -1;
		}
	}
}
