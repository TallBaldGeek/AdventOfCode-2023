namespace Day04
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			var input = Day04Calculations.GetInput();

			Console.WriteLine($"Total points = {Day04Calculations.PartOne(input)}");
			//wrong answer = 47694 (too high)
			//correct answer = 23847


			//correct answer for part two = 8570000

			Console.WriteLine($"Total cards ({nameof(Day04Calculations.PartTwo)}) = {Day04Calculations.PartTwo(input)}");
			Console.WriteLine($"Total cards ({nameof(Day04Calculations.PartTwo_Refactored)}) = {Day04Calculations.PartTwo_Refactored(input)}");
			Console.WriteLine($"Total cards ({nameof(Day04Calculations.PartTwo_Borrowed)}) = {Day04Calculations.PartTwo_Borrowed(input)}");
		}
	}
}
