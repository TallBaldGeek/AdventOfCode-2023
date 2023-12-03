// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Utilities;

Console.WriteLine("Hello, World!");

var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");

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
const int maxRed = 12;
const int maxGreen = 13;
const int maxBlue = 14;

var possibleGameIndices = new List<int>();
foreach (var line in _input)
{
	//Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
	var wasGamePossible = true;
	var colonPos = line.IndexOf(':');
	var gameIdx = int.Parse(line.Substring(5, colonPos-5));
	var rounds = line.Substring(colonPos + 1).Split(';');
	foreach (var round in rounds)
	{
		//3 blue, 4 red;
		//1 red, 2 green, 6 blue;
		//2 green
		var diceGroups = round.Split(',');
		foreach (var dice in diceGroups)
		{
			if (dice.Contains("red"))
			{
				//dice.AsSpan().Slice()
				//var bluePos = dice.IndexOf(" blue");
				
				var numDice = int.Parse(dice.Replace("red", string.Empty).Trim());
				if (numDice > maxRed)
				{
					wasGamePossible = false;
					break;
				}
			}
			if (dice.Contains("blue"))
			{
				var numDice = int.Parse(dice.Replace("blue", string.Empty).Trim());
				if (numDice > maxBlue)
				{
					wasGamePossible = false;
					break;
				}
			}
			if (dice.Contains("green"))
			{
				var numDice = int.Parse(dice.Replace("green", string.Empty).Trim());
				if (numDice > maxGreen)
				{
					wasGamePossible = false;
					break;
				}
			}
		}
	}
	if (wasGamePossible)
	{
		possibleGameIndices.Add(gameIdx);
	}
}
var total = possibleGameIndices.Sum();
Console.WriteLine($"Total of all possible game indexes = {total}");

//correct answer = 2716