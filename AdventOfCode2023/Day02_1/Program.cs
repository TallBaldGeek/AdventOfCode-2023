// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Utilities;


var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
var _maxDiceByColor = new Dictionary<string, int>()
{
	{"red", 12 },
	{"green", 13 },
	{"blue", 14 }
};

part1();
part2();

void part1()
{
	/*
	 * Examples:
	Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
	Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
	Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
	Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
	Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green

	The Elf would first like to know which games would have been possible if the bag contained only 
	12 red cubes, 13 green cubes, and 14 blue cubes?
	 */

	var possibleGameIndices = new List<int>();
	foreach (var line in _input)
	{
		//Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
		var gameWasPossible = true;
		var colonPos = line.IndexOf(':');
		var gameIdx = int.Parse(line.Substring(5, colonPos - 5));
		var rounds = line.Substring(colonPos + 1).Split(';');
		foreach (var round in rounds)
		{
			//3 blue, 4 red;
			//1 red, 2 green, 6 blue;
			//2 green
			var diceGroups = round.Split(',');
			foreach (var dice in diceGroups)
			{
				if (exceededMaxDice(dice, "red") ||
					exceededMaxDice(dice, "blue") ||
					exceededMaxDice(dice, "green"))
				{
					gameWasPossible = false;
					break;
				}
			}
		}

		if (gameWasPossible)
		{
			possibleGameIndices.Add(gameIdx);
		}
	}
	var total = possibleGameIndices.Sum();
	Console.WriteLine($"Total of all possible game indexes = {total}");

	//correct answer = 2716
}

bool exceededMaxDice(string dice, string searchColor)
{
	if (dice.Contains(searchColor))
	{
		var numDice = int.Parse(dice.Replace(searchColor, string.Empty).Trim());
		return numDice > _maxDiceByColor[searchColor];
	}
	return false;
}

void part2()
{
	var powers = new List<int>();
	foreach (var line in _input)
	{
		var minByColor = new Dictionary<string, int>()
		{
			{ "red", 0 },
			{ "green", 0 },
			{ "blue", 0 }
		};
		var colonPos = line.IndexOf(':');
		var gameIdx = int.Parse(line.Substring(5, colonPos - 5));
		var rounds = line.Substring(colonPos + 1).Split(';');
		foreach (var round in rounds)
		{
			//3 blue, 4 red;
			//1 red, 2 green, 6 blue;
			//2 green
			var diceGroups = round.Split(',');
			foreach (var dice in diceGroups)
			{
				foreach (var color in minByColor.Keys)
				{
					if (dice.Contains(color))
					{
						var numDice = int.Parse(dice.Replace(color, string.Empty).Trim());
						if (numDice > minByColor[color])
						{
							minByColor[color] = numDice;
						}
					}
				}
			}
		}

		powers.Add(minByColor["red"] * minByColor["blue"] * minByColor["green"]);
	}
	Console.WriteLine($"The sum of all powers is {powers.Sum()}");

	//Correct answer is 72227
}