using System.Reflection;
using Utilities;

namespace Day06
{
	public class Day06Logic
	{

		public static string[] GetInput()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
		}

		public static string[] GetSample()
		{
			return Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "sample.txt");
		}

	}
}
