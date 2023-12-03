using System.Reflection;
using Utilities;

namespace Day03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var _input = Utils.ReadAllResourceLines(Assembly.GetExecutingAssembly(), "input.txt");
            Console.WriteLine($"found {_input.Length} lines");

        }
    }
}
