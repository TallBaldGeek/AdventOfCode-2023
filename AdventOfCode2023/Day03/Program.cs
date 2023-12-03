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
            SearchValues<char> digitSearchValues = SearchValues.Create(".0123456789");
            Console.WriteLine("Hello, World!");
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
    }
}
