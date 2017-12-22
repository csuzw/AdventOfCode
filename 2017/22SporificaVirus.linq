<Query Kind="Program" />

void Main()
{
	SporificaVirusPartOne(Example, 7).Dump();			// 5
	SporificaVirusPartOne(Example, 70).Dump();			// 41
	SporificaVirusPartOne(Example, 10_000).Dump();      // 5587
	SporificaVirusPartOne(Input, 10_000).Dump();
	SporificaVirusPartTwo(Example, 100).Dump(); 		// 26
	//SporificaVirusPartTwo(Example, 10_000_000).Dump();  // 2511944
	//SporificaVirusPartTwo(Input, 10_000_000).Dump();
}

int SporificaVirusPartOne(string[] input, int iterations)
{
	return SporificaVirus(input, iterations, Next, Count);

	State Next(Dictionary<(int, int), State> grid, (int, int) position)
	{
		if (!grid.ContainsKey(position)) return grid[position] = State.Infected - 1;
		grid.Remove(position);
		return State.Clean - 1;
	}
	bool Count(State state) => state == State.Infected - 1;
}

int SporificaVirusPartTwo(string[] input, int iterations)
{
	return SporificaVirus(input, iterations, Next, Count);

	State Next(Dictionary<(int, int), State> grid, (int, int) position)
	{
		if (!grid.ContainsKey(position)) return grid[position] = State.Weakened;
		if (grid[position] == State.Flagged)
		{
			grid.Remove(position);
			return State.Clean;
		}
		return grid[position] = grid[position] += 1;
	}
	bool Count(State state) => state == State.Infected;
}

int SporificaVirus(string[] input, int iterations, Func<Dictionary<(int, int), State>, (int, int), State> next, Func<State, bool> count)
{
	var directions = new(int dx, int dy)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };

	var infected = input.ToGrid();
	var position = (x: 0, y: 0);
	var direction = 0;
	var result = 0;
	for (var _ = 0; _ < iterations; _++)
	{
		var state = next(infected, position);
		direction = (direction + (int)state) % 4;
		position = (position.x + directions[direction].dx, position.y + directions[direction].dy);
		if (count(state)) result += 1;
	}

	return result;
}

enum State
{
	Clean = 2,
	Weakened = 3,
	Infected = 4,
	Flagged = 5,
}

static class Extensions
{
	public static Dictionary<(int x, int y), State> ToGrid(this string[] source)
	{
		var infected = new Dictionary<(int, int), State>();
		
		(var height, var width) = (source.Length, source[0].Length);
		(var offsetX, var offsetY) = (width / 2, height / 2);
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (source[i][j] == '#') infected[(j - offsetX, i - offsetY)] = State.Infected;
			}
		}

		return infected;
	}
}

// 1: 7 = 5, 70 = 41, 10000 = 5587, 2: 100 = 26, 10000000 = 2511944
string[] Example => new[]
{
	"..#",
	"#..",
	"...",	
};

// 1: 5339, 2: 2512380
string[] Input => new[]
{
	"..##.##.######...#.######",
	"##...#...###....##.#.#.##",
	"###.#.#.#..#.##.####.#.#.",
	"..##.##...#..#.##.....##.",
	"##.##...#.....#.#..#.####",
	".###...#.........###.####",
	"#..##....###...#######..#",
	"###..#.####.###.#.#......",
	".#....##..##...###..###.#",
	"###.#..#.##.###.#..###...",
	"####.#..##.#.#.#.#.#...##",
	"##.#####.#......#.#.#.#.#",
	"..##..####...#..#.#.####.",
	".####.####.####...##.#.##",
	"#####....#...#.####.#..#.",
	".#..###..........#..#.#..",
	".#.##.#.#.##.##.#..#.#...",
	"..##...#..#.....##.####..",
	"..#.#...######..##..##.#.",
	".####.###....##...####.#.",
	".#####..#####....####.#..",
	"###..#..##.#......##.###.",
	".########...#.#...###....",
	"...##.#.##.#####.###.####",
	".....##.#.#....#..#....#.",
};
