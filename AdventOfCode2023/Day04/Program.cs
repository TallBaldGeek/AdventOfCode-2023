using System.Reflection;
using Utilities;

namespace Day04
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			partOne();
			partTwo();
		}

		private static void partOne()
		{
			var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
			var totalPoints = new List<int>();
			foreach (var line in _input)
			{
				//Card   1: 18 39  5 97 33 74 70 35 40 72 | 62 23 33 94 18  5 91 74 86 88 82 72 51 39 95 35 44 87 65 15 46 10  3  2 84
				totalPoints.Add(determinePointsForGameOne(line));
			}
			Console.WriteLine($"Total points = {totalPoints.Sum()}");
			//wrong answer = 47694 (too high)
			//correct answer = 23847
		}


		private static void partTwo()
		{
			var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");

			var cardCopies = new Dictionary<int, int>();
			for (int i = 1; i < _input.Length + 1; i++)
			{
				cardCopies.Add(i, 1);
			}
			foreach (var line in _input)
			{
				var cardNumber = int.Parse(line.Split(':')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
				var gameData = line.Split(':')[1].Split('|');
				var winningNumbers = parseNumberString(gameData[0]);
				var yourNumbers = parseNumberString(gameData[1]);

				var winnerCount = winningNumbers.Count(x => yourNumbers.Contains(x));
				var nextCard = cardNumber + 1;
				var finalCard = nextCard + winnerCount;
				var copiesOfCurrentCard = cardCopies[cardNumber];
				for (int j = 0; j < copiesOfCurrentCard; j++)
				{
					for (int i = nextCard; i < finalCard; i++)
					{
						cardCopies[i] = cardCopies[i] + 1;
					}
				}

			}
			Console.WriteLine($"Total cards = {cardCopies.Values.Sum()}");
			//correct answer = 8570000
		}


		static IEnumerable<int> parseNumberString(string input)
		{
			return input
				.Split(" ", StringSplitOptions.TrimEntries)
				.Where(s => !string.IsNullOrEmpty(s))
				.Select(s => int.Parse(s));
		}

		static int determinePointsForGameOne(string game)
		{
			var points = 0;
			var gameData = game.Split(':')[1].Split('|');
			var winningNumbers = parseNumberString(gameData[0]);
			var yourNumbers = parseNumberString(gameData[1]);
			var howMany = winningNumbers.Where(n => yourNumbers.Contains(n)).ToList();
			if (howMany.Any())
			{
				points = 1;
				for (int i = 1; i < howMany.Count; i++) { points = points * 2; };
			}
			return points;
		}
	}
}
