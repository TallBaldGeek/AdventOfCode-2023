using System.Reflection;
using Utilities;

namespace Day06
{
	public class Day06Logic
	{
		private const int STARTING_SPEED = 0;
		private const int ACCELERATION = 1;

		public static string[] GetInput()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
		}

		public static string[] GetSample()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "sample.txt");
		}

		public int PartOne(string[] input)
		{
			/*
			Time:      7  15   30
			Distance:  9  40  200
			 */
			var times = input[0]
				.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
				.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(int.Parse)
				.ToArray();
			var distances = input[1]
				.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
				.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(int.Parse)
				.ToArray();

			var priorRaceStats = new List<RaceResults>();
			for (var i = 0; i < times.Length; i++)
			{
				priorRaceStats.Add(new RaceResults(times[i], distances[i]));
			}

			var winningStrategyPossibilities = new List<int>();
			foreach (var priorRaceStat in priorRaceStats)
			{
				winningStrategyPossibilities.Add(priorRaceStat.GetWinningStrategies().Count);
			}

			return winningStrategyPossibilities.Aggregate((tot, curr) => tot * curr);
		}
	}

	record RaceResults(int Duration, int Distance)
	{
		public List<WinningStrategy> GetWinningStrategies()
		{
			var list = new List<WinningStrategy>();
			for (int buttonPressDuration = Duration - 1; buttonPressDuration > 0; buttonPressDuration--)
			{
				var boatMovementDuration = Duration - buttonPressDuration;
				var speed = buttonPressDuration;
				var currentDistance = speed * boatMovementDuration;
				if (currentDistance > Distance)
				{
					list.Add(new WinningStrategy(buttonPressDuration));
				}
			}
			return list;
		}

	}

	record WinningStrategy(int ButtonPressDuration)
	{
	}
}
