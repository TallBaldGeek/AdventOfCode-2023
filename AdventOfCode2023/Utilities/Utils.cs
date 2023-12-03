using System.Reflection;
using System.Text;

namespace Utilities
{
	public static class Utils
	{
		public static string[] ReadAllResourceLines(Assembly assembly, string resourceName)
		{
			//var executingAssembly = Assembly.GetExecutingAssembly();
			var assemblyName = assembly.GetName().Name;
			using var stream = assembly.GetManifestResourceStream($"{assemblyName}.{resourceName}")!;
			using var streamReader = new StreamReader(stream, Encoding.UTF8);
			{
				return EnumerateLines(streamReader).ToArray();
			}
		}

		private static IEnumerable<string> EnumerateLines(TextReader reader)
		{
			string line;

			while ((line = reader.ReadLine()) != null)
			{
				yield return line;
			}
		}
	}
}
