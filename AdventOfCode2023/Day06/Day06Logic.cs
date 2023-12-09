using System.Reflection;
using Utilities;

namespace Day06
{
	public class Day06Logic
	{
		public static string[] GetInput()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
		}

		public static string[] GetSample()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "sample.txt");
		}

		public long PartOne(string[] input)
		{
			/*
			Time:      7  15   30
			Distance:  9  40  200
			 */
			var times = input[0]
				.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
				.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(long.Parse)
				.ToArray();
			var distances = input[1]
				.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
				.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(long.Parse)
				.ToArray();

			var priorRaceStats = new List<RaceResults>();
			for (var i = 0; i < times.Length; i++)
			{
				priorRaceStats.Add(new RaceResults(times[i], distances[i]));
			}

			var winningStrategyPossibilities = new List<long>();
			foreach (var priorRaceStat in priorRaceStats)
			{
				winningStrategyPossibilities.Add(priorRaceStat.GetWinningStrategies().Count);
			}

			return winningStrategyPossibilities.Aggregate((tot, curr) => tot * curr);
		}

		public long PartTwo(string[] input)
		{
			var inputTime = input[0]
				.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
				.Replace(" ", string.Empty)
				.Trim();
			var inputDistance = input[1]
				.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
				.Replace(" ", string.Empty)
				.Trim();
			var recordTime = long.Parse(inputTime);
			var recordDistance = long.Parse(inputDistance);

			var results = new RaceResults(recordTime, recordDistance);
			var strategies = results.GetWinningStrategies();

			return strategies.Count;
		}
	}

	record RaceResults(long Duration, long Distance)
	{
		public List<WinningStrategy> GetWinningStrategies()
		{
			var list = new List<WinningStrategy>();
			for (long buttonPressDuration = Duration - 1; buttonPressDuration > 0; buttonPressDuration--)
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

	record WinningStrategy(long ButtonPressDuration)
	{
	}
}
