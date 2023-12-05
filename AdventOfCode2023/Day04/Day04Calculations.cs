namespace Day04
{
	public static class Day04Calculations
	{
		public static int PartOne(string[]? input)
		{
			var totalPoints = new List<int>();
			foreach (var line in input)
			{
				totalPoints.Add(determinePointsForGameOne(line));
			}
			return totalPoints.Sum();
		}

		public static int PartTwo(string[]? input)
		{
			var cardCopies = new Dictionary<int, int>();
			for (int i = 1; i < input.Length + 1; i++)
			{
				cardCopies.Add(i, 1);
			}
			foreach (var line in input)
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
			return cardCopies.Values.Sum();
		}
		public static int PartTwo_Refactored(string[]? input)
		{
			var cardCount = new int[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				cardCount[i] = 1;
			}

			for (int cardId = 0; cardId < input.Length; cardId++)
			{
				var gameData = input[cardId].Split(':')[1].Split('|');
				var winningNumbers = parseNumberString(gameData[0]);
				var yourNumbers = parseNumberString(gameData[1]);

				var winnerCount = winningNumbers.Count(x => yourNumbers.Contains(x));

				for (int i = 0; i < winnerCount; i++)
				{
					cardCount[cardId + 1 + i] += cardCount[cardId];
				}
			}
			return cardCount.Sum();
		}
		public static int PartTwo_Borrowed(string[]? input)
		{
			//stolen from https://github.com/MartinZikmund/advent-of-code-2023/blob/main/Day04_2/Program.cs
			int[] cardCount = new int[input.Length];
			for (int i = 0; i < cardCount.Length; i++)
			{
				cardCount[i] = 1;
			}

			for (int cardId = 0; cardId < input.Length; cardId++)
			{
				string? line = input[cardId];
				var parts = line.Split(':');
				var numbers = parts[1].Split('|');
				var pickedNumbers = numbers[0]
					.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
					.Select(int.Parse)
					.ToArray();
				var ourNumbers = numbers[1]
					.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
					.Select(int.Parse)
					.ToArray();

				var matchCount = pickedNumbers.Intersect(ourNumbers).Count();

				for (int i = 0; i < matchCount; i++)
				{
					cardCount[cardId + 1 + i] += cardCount[cardId];
				}
			}
			return cardCount.Sum();
		}

		static IEnumerable<int> parseNumberString(string input)
		{
			return input
				.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Where(s => !string.IsNullOrEmpty(s))
				.Select(int.Parse);
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
