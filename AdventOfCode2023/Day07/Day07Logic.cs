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
			var ungroupedHands = new List<HandInfo>();
			foreach (string s in input)
			{
				var segments = s.Split(' ');
				ungroupedHands.Add(new HandInfo(segments[0].ToCharArray(), int.Parse(segments[1])));
			}

			return 1;
		}
	}
	enum HandStrength
	{
		FiveOfAKind = 7,
		FourOfAKind = 6,
		FullHouse = 5,
		ThreeOfAKind = 4,
		TwoPair = 3,
		OnePair = 2,
		HighCard = 1
	}

	record HandInfo(char[] Cards, int Bid)
	{
		public HandStrength GetStrength()
		{
			return HandStrength.FiveOfAKind;
		}
	}
}
