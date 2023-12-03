using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
	public class Day1Approaches
	{
		private string[] _input;
		private readonly Dictionary<string, string> _translations = new()
		{
		{ "0", "0"  },
		{ "1", "1" },
		{"one", "1" },
		{ "2", "2" },
		{"two", "2" },
		{ "3", "3"  },
		{"three", "3" },
		{ "4", "4"  },
		{"four","4" },
		{ "5", "5"  },
		{"five","5" },
		{ "6", "6" },
		{"six","6" },
		{ "7", "7"  },
		{"seven","7" },
		{ "8","8" },
		{"eight", "8" },
		{ "9","9" },
		{"nine","9" }
	};

		[GlobalSetup]
		public async Task Init()
		{
			_input = await File.ReadAllLinesAsync("input.txt");
		}

  //      [Benchmark]
		//public byte[] Sha256() => sha256.ComputeHash(data);

		//[Benchmark]
		//public byte[] Md5() => md5.ComputeHash(data);
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<Day1Approaches>();
		}
	}
}
