using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Day03;
using Day04;

namespace MyBenchmarks
{
	[MemoryDiagnoser]
	public class Day4Approaches
	{
		private string[] _input;


		[GlobalSetup]
		public void Init()
		{
			_input = Day04Calculations.GetInput();
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

	[MemoryDiagnoser]
	public class Day3Approaches
	{

		/*
		 I'd have thought my way was slower but it turns out mine's faster:
BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.3693/22H2/2022Update)
		   11th Gen Intel Core i5-1145G7 2.60GHz, 1 CPU, 8 logical and 4 physical cores
		   .NET SDK 8.0.100
		     [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
		     DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
		   
		   
		   | Method           | Mean      | Error    | StdDev    | Gen0    | Gen1   | Allocated |
		   |----------------- |----------:|---------:|----------:|--------:|-------:|----------:|
		   | Day03_2_Original |  48.69 us | 0.916 us |  1.932 us | 13.7329 |      - |  56.23 KB |
		   | Day03_2_Borrowed | 207.57 us | 5.399 us | 15.664 us | 20.9961 | 4.1504 |  86.79 KB |
		 */

		private string[] _input;

		[GlobalSetup]
		public void Init()
		{
			_input = Day03Logic.GetInput();
		}

		[Benchmark]
		public int Day03_2_Original()
		{
			return Day03Logic.PartTwo(_input);
		}

		[Benchmark]
		public int Day03_2_Borrowed()
		{
			return Day03BorrowedLogic.PartTwo(_input);
		}

	}

	public class Program
	{
		public static void Main(string[] args)
		{
			//var summary = BenchmarkRunner.Run<Day4Approaches>();
			var summary = BenchmarkRunner.Run<Day3Approaches>();
		}
	}
}
