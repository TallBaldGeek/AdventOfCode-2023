namespace Day05
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			//var input = Day05Logic.GetSample();
			var input = Day05Logic.GetInput();

			var logic = new Day05Logic();
			//Console.WriteLine($"The lowest location number for any initial seed numbers is {logic.PartOne(input)}");
			//correct answer is 484023871

			//Console.WriteLine($"The lowest location number for any initial seed numbers is {logic.PartTwo(input)}");
			var partTwo = new PartTwo();
			Console.WriteLine($"The lowest location number for any initial seed numbers is {partTwo.Process()}");

		}
	}
}
