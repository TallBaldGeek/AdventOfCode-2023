// See https://aka.ms/new-console-template for more information
using System.Buffers;
using System.Reflection;
using System.Text;

var _input = ReadAllResourceLines("input.txt");//await File.ReadAllLinesAsync("input.txt");


var _translations = new Dictionary<string, string>
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

partOne();
partTwo();

string[] ReadAllResourceLines(string resourceName)
{
	var executingAssembly = Assembly.GetExecutingAssembly();
	var assemblyName = executingAssembly.GetName().Name;
	using var stream = executingAssembly.GetManifestResourceStream($"{assemblyName}.{resourceName}")!;
	using var streamReader = new StreamReader(stream, Encoding.UTF8);
	{
		return EnumerateLines(streamReader).ToArray();
	}
}

IEnumerable<string> EnumerateLines(TextReader reader)
{
	string line;

	while ((line = reader.ReadLine()) != null)
	{
		yield return line;
	}
}

void partOne()
{
	Console.WriteLine("PART 1 start");
	
	//SearchValues<char> digitSearchValues = SearchValues.Create("0123456789");
	char[] validDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	var calibrationValues = new List<int>();
	foreach (var value in _input)
	{
		var firstIdx = value.IndexOfAny(validDigits);
		var lastIdx = value.LastIndexOfAny(validDigits);
		if (firstIdx == -1 || lastIdx == -1)
		{
			Console.WriteLine($"didn't find what I was looking for in {value}");
		}
		var toParse = string.Concat(value.AsSpan(firstIdx, 1), value.AsSpan(lastIdx, 1));
		//Console.WriteLine(toParse );
		calibrationValues.Add(int.Parse(toParse));
	}
	Console.WriteLine("finished the loop");
	var sum = calibrationValues.Sum();
	Console.WriteLine($"PART 1 - Total is {sum}");

	//correct answer is 56042
}


void partTwo()
{
	Console.WriteLine("PART 2 start");
	var calibrationValues = new List<int>();

	foreach (var value in _input)
	{
		var firstIdx = value.Length - 1;
		var lastIdx = 0;
		var firstDigit = string.Empty;
		var lastDigit = string.Empty;
		foreach (var digit in _translations.Keys)
		{
			var pos = value.IndexOf(digit);

			if (pos > -1 && pos <= firstIdx)
			{
				firstIdx = pos;
				firstDigit = digit;
			}

			pos = value.LastIndexOf(digit);
			if (pos > -1 && pos >= lastIdx)
			{
				lastIdx = pos;
				lastDigit = digit;
			}
		}
		var calibration = int.Parse(_translations[firstDigit] + _translations[lastDigit]);
		calibrationValues.Add(calibration);
	}
	var total = calibrationValues.Sum();
	Console.WriteLine($"PART 2 - Total is {total}");
	//correct answer is 55358
}