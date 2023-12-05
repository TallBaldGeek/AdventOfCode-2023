using System.Reflection;
using Utilities;

namespace Day03
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			var input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");

			Console.WriteLine($"Total of valid part numbers is {Day03Logic.PartOne(input)}");
			//correct answer is 549908

			Console.WriteLine($"Total of valid part numbers is {Day03Logic.PartTwo(input)}");

			//guess 76174505 is too low
			//correct answer is 81166799
		}
	}
}