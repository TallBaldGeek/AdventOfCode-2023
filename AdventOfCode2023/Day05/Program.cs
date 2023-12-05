namespace Day05
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var input = Day05Logic.GetSample();//Day05Logic.GetInput();

			Console.WriteLine($"The lowest location number for any initial seed numbers is {Day05Logic.PartOne(input)}");
		}
	}
}
