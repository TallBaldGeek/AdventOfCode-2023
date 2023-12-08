namespace Day06
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var logic = new Day06Logic();
			var sample = Day06Logic.GetSample();
			var input = Day06Logic.GetInput();

			//Console.WriteLine($"Answer for Part One is {logic.PartOne(input)}");
			//correct answer 1155175

			Console.WriteLine($"Answer for Part Two is {logic.PartTwo(input)}");

		}
	}
}
