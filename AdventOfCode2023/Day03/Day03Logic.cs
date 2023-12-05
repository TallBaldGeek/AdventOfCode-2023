using System.Buffers;
using System.Reflection;
using Utilities;

namespace Day03
{
	public static class Day03Logic
	{
		private static string noCharacterLine = "............................................................................................................................................";
		private static int LINE_LENGTH = 140;

		public static string[] GetInput()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
		}

		public static int PartOne(string[] input)
		{
			var digitSearchValues = SearchValues.Create(".0123456789");
			Console.WriteLine($"found {input.Length} lines");
			var padded = new List<string>
			{
				noCharacterLine
			};
			padded.AddRange(input);
			padded.Add(noCharacterLine);
			var validPartNumbers = new List<int>();
			for (int i = 1; i < padded.Count - 1; i++)
			{
				//padded makes it safe to check line 2 through end-1
				var previousLine = padded[i - 1];
				var currentLine = padded[i];
				var nextLine = padded[i + 1];
				var charIdx = 0;
				while (charIdx < LINE_LENGTH)
				{
					if (char.IsDigit(currentLine[charIdx]))
					{
						var numstart = charIdx;
						while ((charIdx + 1) < LINE_LENGTH && char.IsDigit(currentLine[charIdx + 1]))
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
						else if (numEnd < LINE_LENGTH - 1 && !digitSearchValues.Contains(currentLine[numEnd + 1]))
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
							if (adjacentLineEnd < LINE_LENGTH)
							{//don't go past the beginning of a line
								adjacentLineEnd++;
							}
							var adjacentSliceLength = adjacentLineEnd - adjacentLineStart + 1;
							while (adjacentLineStart + adjacentSliceLength > LINE_LENGTH)
							{//don't go past the end of a line
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
				}
			}

			return validPartNumbers.Sum();
		}


		public static int PartTwo(string[] input)
		{
			var padded = new List<string>
			{
				noCharacterLine
			};
			padded.AddRange(input);
			padded.Add(noCharacterLine);
			var gearRatios = new List<int>();
			for (var i = 1; i < padded.Count - 1; i++)
			{
				//padded makes it safe to check line 2 through end-1
				var previousLine = padded[i - 1];
				var currentLine = padded[i];
				var nextLine = padded[i + 1];
				var charIdx = 0;
				while (charIdx < LINE_LENGTH)
				{
					if (currentLine[charIdx] == '*')
					{
						var numbers = new List<int>();
						//look left
						if (charIdx > 0 && char.IsDigit(currentLine[charIdx - 1]))
						{
							var numberIdx = charIdx;
							while (numberIdx > 0 && char.IsDigit(currentLine[numberIdx - 1]))
							{
								numberIdx--;
							}

							var number = currentLine.AsSpan().Slice(numberIdx, charIdx - numberIdx).ToString();
							numbers.Add(int.Parse(number));
						}

						//look right
						if (charIdx < LINE_LENGTH && char.IsDigit(currentLine[charIdx + 1]))
						{
							var numberStartIdx = charIdx + 1;
							var numberEndIdx = numberStartIdx;
							while (numberEndIdx < LINE_LENGTH && char.IsDigit(currentLine[numberEndIdx + 1]))
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
						if (char.IsDigit(previousLine[charIdx]))
						{
							//there's a number right above, which means there can't be one above diagonal
							/*
							 ..555...
							 ...*....
							 */
							var upNumber = searchForPossibleNumberAtSameIndex(previousLine, charIdx);
							if (!string.IsNullOrEmpty(upNumber))
							{
								numbers.Add(int.Parse(upNumber));
							}
						}
						else
						{
							var uprightNumber = searchForPossibleNumberDiagonalRight(previousLine, charIdx);
							if (!string.IsNullOrEmpty(uprightNumber))
							{
								numbers.Add(int.Parse(uprightNumber));
							}

							var upleftNumber = searchForPossibleNumberDiagonalLeft(previousLine, charIdx);
							if (!string.IsNullOrEmpty(upleftNumber))
							{
								numbers.Add(int.Parse(upleftNumber));
							}
						}

						//look down
						//var downNumber = searchForPossibleNumberAtSameIndex(nextLine, charIdx);
						//if (!string.IsNullOrEmpty(downNumber))
						//{
						//	numbers.Add(int.Parse(downNumber));
						//}

						if (char.IsDigit(nextLine[charIdx]))
						{
							//there's a number right above, which means there can't be one above diagonal
							/*
							 ..555...
							 ...*....
							 */
							var downNumber = searchForPossibleNumberAtSameIndex(nextLine, charIdx);
							if (!string.IsNullOrEmpty(downNumber))
							{
								numbers.Add(int.Parse(downNumber));
							}
						}
						else
						{
							var downrightNumber = searchForPossibleNumberDiagonalRight(nextLine, charIdx);
							if (!string.IsNullOrEmpty(downrightNumber))
							{
								numbers.Add(int.Parse(downrightNumber));
							}

							var downleftNumber = searchForPossibleNumberDiagonalLeft(nextLine, charIdx);
							if (!string.IsNullOrEmpty(downleftNumber))
							{
								numbers.Add(int.Parse(downleftNumber));
							}
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

			return gearRatios.Sum();
		}

		private static string searchForPossibleNumberAtSameIndex(string line, int index)
		{
			if (char.IsDigit(line[index]))
			{
				//Number in prior line directly above.
				/*
				...123.....
				....*.702..
				 */
				var adjacentEnd = index;

				//There was an adjacent number and we need to find the end of it
				while (adjacentEnd < LINE_LENGTH && char.IsDigit(line[adjacentEnd + 1]))
				{
					adjacentEnd++;
				}
				//and the start
				var adjacentStart = index;
				while (adjacentStart > 0 && char.IsDigit(line[adjacentStart - 1]))
				{
					adjacentStart--;
				}
				return line.AsSpan().Slice(adjacentStart, adjacentEnd - adjacentStart + 1).ToString();
			}
			return string.Empty;
		}
		private static string searchForPossibleNumberDiagonalRight(string line, int index)
		{
			if (index > 0 && char.IsDigit(line[index - 1]))
			{
				//number in prior line diagonal above right, need to find the end of it
				/*
				.....123...
				....*.702..
				 */
				var adjacentEnd = index - 1;
				var adjacentStart = adjacentEnd;
				while (adjacentStart > 0 && char.IsDigit(line[adjacentStart - 1]))
				{
					adjacentStart--;
				}
				return line.AsSpan().Slice(adjacentStart, adjacentEnd - adjacentStart + 1).ToString();
			}

			return string.Empty;
		}

		private static string searchForPossibleNumberDiagonalLeft(string line, int index)
		{
			if (index < LINE_LENGTH && char.IsDigit(line[index + 1]))
			{
				//number in prior line diagonal above left, need to find the beginning of it
				/*
				.123...123.
				....*.702..
				 */
				var adjacentEnd = index;
				while (adjacentEnd < LINE_LENGTH && char.IsDigit(line[adjacentEnd + 1]))
				{
					adjacentEnd++;
				}
				var adjacentStart = adjacentEnd;
				while (adjacentStart > 0 && char.IsDigit(line[adjacentStart - 1]))
				{
					adjacentStart--;
				}
				return line.AsSpan().Slice(adjacentStart, adjacentEnd - adjacentStart + 1).ToString();
			}

			return string.Empty;
		}

	}


}
