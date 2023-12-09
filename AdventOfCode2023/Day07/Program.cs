namespace Day07
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var logic = new Day07Logic();
			var sample = Day07Logic.GetSample();
			var input = Day07Logic.GetInput();

			Console.WriteLine($"Answer for Part One is {logic.PartOne(input)}");
			//answer 249930506 is too low
			//answer 250120589 is too high
			//correct answer 

			//Console.WriteLine($"Answer for Part One is {logic.PartTwo(input)}");

		}
	}
}
