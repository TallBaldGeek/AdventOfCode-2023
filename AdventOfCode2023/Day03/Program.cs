using System.Buffers;
using System.Reflection;
using Utilities;

namespace Day03
{
	internal class Program
	{
		private static string noCharacterLine = "............................................................................................................................................";
		private static int lineLength = 140;

		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			//part1();
			part2();
		}

		private static void part1()
		{
			SearchValues<char> digitSearchValues = SearchValues.Create(".0123456789");
			var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
			Console.WriteLine($"found {_input.Length} lines");
			var padded = new List<string>
			{
				noCharacterLine
			};
			padded.AddRange(_input);
			padded.Add(noCharacterLine);
			//......124..................418.......587......770...........672.................564............................438..........512......653....
			//............................................................................................................................................
			var validPartNumbers = new List<int>();
			for (int i = 1; i < padded.Count - 1; i++)
			{
				//padded makes it safe to check line 2 through end-1
				var previousLine = padded[i - 1];
				var currentLine = padded[i];
				var nextLine = padded[i + 1];

				var charIdx = 0;
				while (charIdx < lineLength)
				{
					if (char.IsDigit(currentLine[charIdx]))
					{
						var numstart = charIdx;
						while ((charIdx + 1) < lineLength && char.IsDigit(currentLine[charIdx + 1]))
						{
							charIdx++;
						}
						var numEnd = charIdx;
						var digits = currentLine.AsSpan().Slice(numstart, numEnd - numstart + 1).ToString();
						if (numstart > 0 && !digitSearchValues.Contains(currentLine[numstart - 1]))
						{
							//prior character is a special
							validPartNumbers.Add(int.Parse(digits));
						}
						else if (numEnd < lineLength - 1 && !digitSearchValues.Contains(currentLine[numEnd + 1]))
						{
							//next character same line is a special
							validPartNumbers.Add(int.Parse(digits));
						}
						else
						{
							//look for diagonal on the prior line
							var adjacentLineStart = numstart;
							if (adjacentLineStart > 0)
							{
								adjacentLineStart--;
							}
							var adjacentLineEnd = numEnd;
							if (adjacentLineEnd < lineLength)
							{
								adjacentLineEnd++;
							}
							var adjacentSliceLength = adjacentLineEnd - adjacentLineStart + 1;
							while (adjacentLineStart + adjacentSliceLength > lineLength)
							{
								adjacentSliceLength--;
							}
							var priorLineSlice = previousLine.AsSpan().Slice(adjacentLineStart, adjacentSliceLength);
							var nextLineSlice = nextLine.AsSpan().Slice(adjacentLineStart, adjacentSliceLength);
							if (priorLineSlice.IndexOfAnyExcept(digitSearchValues) > -1 ||
								nextLineSlice.IndexOfAnyExcept(digitSearchValues) > -1)
							{
								validPartNumbers.Add(int.Parse(digits));
							}
						}


					}
					charIdx++;
					//currentLine.First(line => char.IsDigit(line))
					//if (currentLine[charIdx].Is)
				}
			}

			var total = validPartNumbers.Sum();
			Console.WriteLine($"Total of valid part numbers is {total}");

			//correct answer is 549908
		}

		private static void part2()
		{
			SearchValues<char> digitSearchValues = SearchValues.Create("0123456789");//not looking for "."
			var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
			var padded = new List<string>
			{
				noCharacterLine
			};
			padded.AddRange(_input);
			padded.Add(noCharacterLine);
			var gearRatios = new List<int>();
			for (int i = 1; i < padded.Count - 1; i++)
			{
				//padded makes it safe to check line 2 through end-1
				var previousLine = padded[i - 1];
				var currentLine = padded[i];
				var nextLine = padded[i + 1];

				var charIdx = 0;
				while (charIdx < lineLength)
				{
					if (currentLine[charIdx] == '*')
					{

							var number = currentLine.AsSpan().Slice(numberIdx, charIdx - numberIdx).ToString();
							numbers.Add(int.Parse(number));
						}

						//look right
						if (charIdx < lineLength && char.IsDigit(currentLine[charIdx + 1]))
						{
							var numberStartIdx = charIdx + 1;
							var numberEndIdx = numberStartIdx;
							while (numberEndIdx < lineLength && char.IsDigit(currentLine[numberEndIdx + 1]))
							{
								numberEndIdx++;
							}

							var number = currentLine.AsSpan().Slice(numberStartIdx, numberEndIdx - numberStartIdx + 1).ToString();
							numbers.Add(int.Parse(number));
						}

						/*
						 need to account for this
							......666
							...*.....
							298.67...
						   
						 */
						//look up
						var upNumber = searchForPossibleNumber(previousLine, charIdx);
						if (!string.IsNullOrEmpty(upNumber))
						{
							numbers.Add(int.Parse(upNumber));
						}

						//look down
						var downNumber = searchForPossibleNumber(nextLine, charIdx);
						if (!string.IsNullOrEmpty(downNumber))
						{
							numbers.Add(int.Parse(downNumber));
						}

						switch (numbers.Count)
						{
							case > 2:
								throw new Exception("You done messed up");
							case 2:
								gearRatios.Add(numbers[0] * numbers[1]);
								break;
						}
					}

					charIdx++;
				}
			}

			var total = gearRatios.Sum();
			Console.WriteLine($"Total of valid part numbers is {total}");

			//guess 76174505 is too low
			//correct answer is 
		}

		private static string searchForPossibleNumber(string line, int index)
		{
			var foundNumber = string.Empty;
			var adjacentEnd = -1; //adjacentIdx < lineLength ? adjacentIdx + 1 : lineLength;
			if (index < lineLength && char.IsDigit(line[index + 1]))
			{
				//number in prior line diagonal above, need to find the end of it
				/*
				.....123...
				....*.702..
				 */
				adjacentEnd = index;
				while (adjacentEnd < lineLength && char.IsDigit(line[adjacentEnd + 1]))
				{
					adjacentEnd++;
				}
			}
			else if (char.IsDigit(line[index]))
			{
				//End of number in prior line directly above. We know because it was not above and right (prior check)
				/*
				..123......
				....*.702..
				 */
				adjacentEnd = index;
			}
			else if (index > 0 && char.IsDigit(line[index - 1]))
			{
				adjacentEnd = index - 1;
			}

			if (adjacentEnd > -1)
			{
				//There was an adjacent number and we know the end of it. Find the start
				var adjacentStart = adjacentEnd;
				while (adjacentStart > 0 && char.IsDigit(line[adjacentStart - 1]))
				{
					adjacentStart--;
				}
				foundNumber = line.AsSpan().Slice(adjacentStart, adjacentEnd - adjacentStart + 1).ToString();
			}
			return foundNumber;
		}
	}
}