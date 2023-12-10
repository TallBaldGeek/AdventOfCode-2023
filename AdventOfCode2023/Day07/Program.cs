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

			//Console.WriteLine($"Answer for Part One is {logic.PartOne(input)}");
			//answer 249930506 is too low
			//answer 250120589 is too high
			//correct answer 

			//Console.WriteLine($"Answer for Part One is {logic.PartTwo(input)}");
			var borrowed = new Day07LogicBorrowed();

			var result1 = borrowed.Solve(input, "23456789TJQKA", "");
			Console.WriteLine($"Result1 = {result1}");
			//correct answer is 250120186

			var result2 = borrowed.Solve(input, "J23456789TQKA", "J");
			Console.WriteLine($"Result2 = {result2}");
			//correct answer is 250665248
		}
	}
}
