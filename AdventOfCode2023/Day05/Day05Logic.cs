using System.Reflection;
using Utilities;

namespace Day05
{
	public class Day05Logic
	{
		private readonly Dictionary<string, List<MappingRange>> _mappings = new();
		const string _seedToSoil = "seed-to-soil";
		const string _soilToFertilizer = "soil-to-fertilizer";
		const string _fertilizerToWater = "fertilizer-to-water";
		const string _waterToLight = "water-to-light";
		const string _lightToTemp = "light-to-temperature";
		const string _tempToHumidity = "temperature-to-humidity";
		const string _humidityToLoc = "humidity-to-location";
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
			https://adventofcode.com/2023/day/5
			Each line within a map contains three numbers: the destination range start, the source range start, and the range length.

		
			Multiple operations:
			Seed -> soil
			soil-> fertilizer, 
			fertilizer -> water, 
			water -> light 
			light -> temperature
			temperature -> humidity
			humidity -> location
			Seed 79, soil 81, fertilizer 81, water 81, light 74, temperature 78, humidity 78, location 82



Rather than list every source number and its corresponding destination number one by one, the maps describe entire ranges of 
			numbers that can be converted. Each line within a map contains three numbers: the destination range start, 
			the source range start, and the range length.

Consider again the example seed-to-soil map:

50 98 2
52 50 48
The first line has a destination range start of 50, a source range start of 98, and a range length of 2. 
			This line means that the source range starts at 98 and contains two values: 98 and 99. 
			The destination range is the same length, but it starts at 50, so its two values are 50 and 51. 
			With this information, you know that seed number 98 corresponds to soil number 50 and that seed number 99 
			corresponds to soil number 51.

The second line means that the source range starts at 50 and contains 48 values: 50, 51, ..., 96, 97. 
			This corresponds to a destination range starting at 52 and also containing 48 values: 52, 53, ..., 98, 99. 
			So, seed number 53 corresponds to soil number 55.

Any source numbers that aren't mapped correspond to the same destination number. 
			So, seed number 10 corresponds to soil number 10.

So, the entire list of seed numbers and their corresponding soil numbers looks like this:

seed  soil
0     0
1     1
...   ...
48    48
49    49
50    52
51    53
...   ...
96    98
97    99
98    50
99    51
With this map, you can look up the soil number required for each initial seed number:

Seed number 79 corresponds to soil number 81.
Seed number 14 corresponds to soil number 14.
Seed number 55 corresponds to soil number 57.
Seed number 13 corresponds to soil number 13.
			 */

			var seeds = input[0].Split(':')[1]
						.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
						.Select(long.Parse)
						.ToList();
			//2276375722 real value for input
			//2147483647 - max int

			populateMappings(input);

			var mappedLocations = new List<long>();
			foreach (var seed in seeds)
			{
				long loc = walkAllMappings(seed);
				mappedLocations.Add(loc);
			}
			mappedLocations.Sort();
			return mappedLocations.First();
		}

		private long walkAllMappings(long seed)
		{
			var soil = mapSourceToDest(_seedToSoil, seed);
			var fert = mapSourceToDest(_soilToFertilizer, soil);
			var water = mapSourceToDest(_fertilizerToWater, fert);
			var light = mapSourceToDest(_waterToLight, water);
			var temp = mapSourceToDest(_lightToTemp, light);
			var humidity = mapSourceToDest(_tempToHumidity, temp);
			var loc = mapSourceToDest(_humidityToLoc, humidity);
			return loc;
		}

		private void populateMappings(string[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i].Contains("map:"))
				{
					var mapType = input[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[0];
					i++;
					i = populateRange(input, i, mapType);
					/*
					switch (mapType)
					{
						case "seed-to-soil":
							i = populateRange(input, seedToSoil, i);
							break;
						case "soil-to-fertilizer":
							i = populateRange(input, soilToFertilizer, i);
							break;
						case "fertilizer-to-water":
							i = populateRange(input, fertilizerToWater, i);
							break;
						case "water-to-light":
							i = populateRange(input, waterToLight, i);
							break;
						case "light-to-temperature":
							i = populateRange(input, lightToTemp, i);
							break;
						case "temperature-to-humidity":
							i = populateRange(input, tempToHumidity, i);
							break;
						case "humidity-to-location":
							i = populateRange(input, humidityToLoc, i);
							break;
						default:
							break;
					}
					*/
				}
			}
		}

		public long PartTwo(string[] input)
		{
			var seedRanges = new List<SeedRange>();
			var seedInput = input[0].Split(':')[1]
						.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
						.Select(long.Parse)
						.ToList();
			for (int i = 0; i < seedInput.Count; i += 2)
			{
				seedRanges.Add(new SeedRange()
				{
					SeedBegin = seedInput[i],
					Length = seedInput[i + 1]
				});
			}

			populateMappings(input);

			var mappedLocations = new List<long>();
			foreach (var seedRange in seedRanges)
			{
				/*
                Assumption is that I don't have to check EVERY seed in a range. As you go through mappings
				the end result for any range of seeds is that the dest number INCREASES as you move up the range
				So the lowest value possible for a humidity-to-location mapping would result from the FIRST
				seed in a given range. 
				BUT need to check all ranges to see which results in the lowest mapping
                 */
				var end = seedRange.SeedBegin + seedRange.Length;
				for (long i = seedRange.SeedBegin; i < end; i++)
				{
					var loc = walkAllMappings(i);
					mappedLocations.Add(loc);
				}
			}
			mappedLocations.Sort();
			return mappedLocations.First();
		}
		long mapSourceToDest(string mappingName, long sourceNum)
		{
			//if (sourceToDest.ContainsKey(sourceNum))
			//{
			//	return sourceToDest[sourceNum];
			//}

			var ranges = _mappings[mappingName];
			//foreach (var range in ranges)
			//{

			//}
			var match = ranges.Where(r => sourceNum >= r.SourceBegin && sourceNum <= r.SourceBegin + r.Length).FirstOrDefault();
			if (match == null)
			{
				return sourceNum;
			}
			return match.DestinationBegin + (sourceNum - match.SourceBegin);
		}

		int populateRange(string[] input, int i, string key)
		{
			var ranges = new List<MappingRange>();
			while (i < input.Length && !string.IsNullOrEmpty(input[i]) && char.IsDigit(input[i][0]))
			{
				var segments = input[i]
					.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
					.Select(long.Parse)
					.ToArray();
				var sourceStart = segments[1];
				var destStart = segments[0];
				var length = segments[2];
				//50		98			2
				//destStart sourceStart length
				ranges.Add(new MappingRange
				{
					SourceBegin = sourceStart,
					//SourceEnd = sourceStart + length,
					DestinationBegin = destStart,
					//DestinationEnd = destStart + length
					Length = length
				});

				//for (var j = 0; j < length; j++)
				//{
				//	var sourceNum = sourceStart + j;
				//	var destNum = destStart + j;
				//	sourceToDest.Add(sourceNum, destNum);
				//}
				i++;
			}

			_mappings.Add(key, ranges);
			return i;
		}
	}

	public class SeedRange
	{
		public long SeedBegin { get; set; }
		public long Length { get; set; }
	}
	public class MappingRange
	{
		public long SourceBegin { get; set; }
		//public long SourceEnd { get; set; }
		public long DestinationBegin { get; set; }
		//public long DestinationEnd { get; set; }
		public long Length { get; set; }
	}

}
