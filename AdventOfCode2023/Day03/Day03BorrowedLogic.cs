namespace Day03
{
	public static class Day03BorrowedLogic
	{
		public static int PartTwo(string[] input)
		{
			var width = input[0].Length;
			var height = input.Length;

			var map = new char[width, height];
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					map[x, y] = input[y][x];
				}
			}

			var runningTotal = 0;
			var currentNumber = 0;
			var asterisks = new Dictionary<Point, List<int>>();
			var neighboringAsterisks = new HashSet<Point>();

			for (var y = 0; y < height; y++)
			{
				void EndCurrentNumber()
				{
					if (currentNumber != 0 && neighboringAsterisks.Count > 0)
					{
						foreach (var neighboringAsterisk in neighboringAsterisks)
						{
							var x = neighboringAsterisk.X;
							var y = neighboringAsterisk.Y;
							if (!asterisks.ContainsKey((x, y)))
							{
								asterisks[(x, y)] = [];
							}

							asterisks[(x, y)].Add(currentNumber);
						}
					}
					currentNumber = 0;
					neighboringAsterisks.Clear();
				}

				for (var x = 0; x < height; x++)
				{
					var character = map[x, y];
					// check if we are reading a number
					if (char.IsDigit(character))
					{
						var value = character - '0';
						currentNumber = currentNumber * 10 + value;
						foreach (var direction in Directions.WithDiagonals)
						{
							var neigbhorX = x + direction.X;
							var neigbhorY = y + direction.Y;
							if (neigbhorX < 0 || neigbhorX >= width || neigbhorY < 0 || neigbhorY >= height)
							{
								continue;
							}

							var neighborCharacter = map[neigbhorX, neigbhorY];
							if (neighborCharacter == '*')
							{
								neighboringAsterisks.Add((neigbhorX, neigbhorY));
							}
						}
					}
					else
					{
						EndCurrentNumber();
					}
				}

				EndCurrentNumber();
			}

			foreach (var (point, numbers) in asterisks)
			{
				if (numbers.Count == 2)
				{
					runningTotal += numbers[0] * numbers[1];
				}
			}

			return runningTotal;
		}

	}

	public static class Directions
	{
		public static Point[] WithoutDiagonals { get; } =
		[
			(0, 1),
			(1, 0),
			(0, -1),
			(-1, 0),
		];

		public static Point[] WithDiagonals { get; } =
		[
			(0, 1),
			(1, 0),
			(0, -1),
			(-1, 0),
			(1, 1),
			(-1, 1),
			(1, -1),
			(-1, -1)
		];

		public static Point3d[] WithoutDiagonals3d { get; } =
		[
			(1, 0, 0),
			(0, 1, 0),
			(0, 0, 1),
			(-1, 0, 0),
			(0, -1, 0),
			(0, 0, -1),
		];
	}

	public record struct Point(int X, int Y)
	{
		public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

		public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

		public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

		public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

		public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
	}

	public record struct Point3d(int X, int Y, int Z)
	{
		public static Point3d operator +(Point3d a, Point3d b) => new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

		public static Point3d operator -(Point3d a, Point3d b) => new Point3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

		public Point3d Normalize() => new Point3d(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0, Z != 0 ? Z / Math.Abs(Z) : 0);

		public static implicit operator Point3d((int X, int Y, int Z) tuple) => new Point3d(tuple.X, tuple.Y, tuple.Z);

		public int ManhattanDistance(Point3d b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y) + Math.Abs(Z - b.Z);
	}
}
