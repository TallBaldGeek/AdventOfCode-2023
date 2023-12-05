using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Day04;
using System.Reflection;
using Utilities;

namespace MyBenchmarks
{
	[MemoryDiagnoser]
	public class Day4Approaches
	{
		private string[] _input;


		[GlobalSetup]
		public void Init()
		{
			_input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "day04-input.txt");
		}

		[Benchmark]
		public int Day04_2_Original()
		{
			return Day04Calculations.PartTwo(_input);
		}

		[Benchmark]
		public int Day04_2_Refactored()
		{
			return Day04Calculations.PartTwo_Refactored(_input);
		}

		[Benchmark]
		public int Day04_2_Borrowed()
		{
			return Day04Calculations.PartTwo_Borrowed(_input);
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<Day4Approaches>();
		}
	}
}
