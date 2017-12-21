<Query Kind="Program" />

void Main()
{
	FractalArt(Input, 5).Dump();
	FractalArt(Input, 18).Dump();
}

int FractalArt(string[] input, int iterations)
{
	var map = input.ToMap();
	
	return Enumerable.Range(0, iterations).Aggregate(Initial, (acc, _) => acc.Next(map)).CountPixels();
}

static class Extensions 
{
	public static int CountPixels(this IEnumerable<string> source)
	{
		return source.Sum(i => i.ToCharArray().Count(c => c == '#'));
	}
	
	public static string[] Next(this string[] source, Dictionary<string, string> map)
	{
		var size = source.Length;
		var increment = size % 2 == 0 ? 2 : 3;
		var result = new List<string>();
		for (var i = 0; i < size; i += increment)
		{
			var intermediate = Enumerable.Repeat(string.Empty, increment + 1);
			for (var j = 0; j < size; j += increment)
			{
				var key = source.Skip(i).Take(increment).Select(s => s.Substring(j, increment)).ToKey();
				var rows = map[key].Split('/');
				intermediate = intermediate.Zip(rows, (a, b) => a + b);
			}
			result.AddRange(intermediate);
		}
		return result.ToArray();
	}
	
	public static Dictionary<string, string> ToMap(this IEnumerable<string> source)
	{
		var map = new Dictionary<string, string>();
		foreach (var item in source.SelectMany(ToKeyValuePairs))
		{
			map[item.key] = item.value;
		}
		return map;
	}

	public static IEnumerable<(string key, string value)> ToKeyValuePairs(this string source)
	{
		var parts = source.Split(' ');
		return parts[0].ToKeys().Select(k => (k, parts[2]));
	}
	
	public static IEnumerable<string> ToKeys(this string source)
	{
		var parts = source.Split('/');

		yield return source;
		yield return parts.Rotate90().ToKey();
		yield return parts.Rotate180().ToKey();
		yield return parts.Rotate270().ToKey();
		yield return parts.FlipHorizontally().ToKey();
		yield return parts.FlipHorizontally().Rotate90().ToKey();
		yield return parts.FlipHorizontally().Rotate180().ToKey();
		yield return parts.FlipHorizontally().Rotate270().ToKey();
	}	
	
	private static IEnumerable<string> FlipVertically(this IEnumerable<string> rows) => rows.Reverse();
	private static IEnumerable<string> FlipHorizontally(this IEnumerable<string> rows) => rows.Select(Reverse);
	private static IEnumerable<string> Rotate90(this IEnumerable<string> rows) => rows.Select(i => i.ToCharArray()).ZipAll().Select(i => string.Concat(i.Reverse()));
	private static IEnumerable<string> Rotate180(this IEnumerable<string> rows) => rows.FlipVertically().FlipHorizontally();
	private static IEnumerable<string> Rotate270(this IEnumerable<string> rows) => rows.Rotate180().Rotate90();
	
	private static string ToKey(this IEnumerable<string> rows) => string.Join("/", rows);

	private static string Reverse(string source)
	{
		var charArray = source.ToCharArray();
		Array.Reverse(charArray);
		return new string(charArray);
	}
	
	private static IEnumerable<IEnumerable<T>> ZipAll<T>(this IEnumerable<IEnumerable<T>> source)
	{
		var enumerators = source.Select(i => i.GetEnumerator()).ToList();
		while (enumerators.All(i => i.MoveNext()))
		{
			yield return enumerators.Select(i => i.Current);
		}
	}
}

string[] Initial => new[]
{
	".#.",
	"..#",
	"###",	
};

// 1: 12
string[] Example => new[]
{
	"../.# => ##./#../...",
	".#./..#/### => #..#/..../..../#..#",
};

// 1: 144, 2:
string[] Input => new[]
{
	"../.. => ##./##./.##",
	"#./.. => .../.#./##.",
	"##/.. => .../.##/#.#",
	".#/#. => ##./#../#..",
	"##/#. => .##/#.#/#..",
	"##/## => ..#/.#./.##",
	".../.../... => #.../.##./...#/#...",
	"#../.../... => ...#/..../..#./..##",
	".#./.../... => ..../.##./###./....",
	"##./.../... => ###./#.##/..#./..#.",
	"#.#/.../... => #.../.#../#..#/..#.",
	"###/.../... => ..##/.##./#.../....",
	".#./#../... => #.##/..../..../#.##",
	"##./#../... => .#.#/.#.#/##../.#..",
	"..#/#../... => .###/####/.###/##..",
	"#.#/#../... => ..../.#.#/..../####",
	".##/#../... => .##./##.#/.###/#..#",
	"###/#../... => ####/...#/###./.###",
	".../.#./... => ..##/#..#/###./###.",
	"#../.#./... => ###./..##/.#.#/.#.#",
	".#./.#./... => ..#./..#./##.#/##..",
	"##./.#./... => #..#/###./..#./#.#.",
	"#.#/.#./... => .###/#.../.#.#/.##.",
	"###/.#./... => #.##/##../#.#./...#",
	".#./##./... => #.##/#.##/#.##/.###",
	"##./##./... => ..##/#..#/.###/....",
	"..#/##./... => #..#/.##./##../####",
	"#.#/##./... => ###./###./..##/..##",
	".##/##./... => ###./##.#/.##./###.",
	"###/##./... => ##../#..#/##../....",
	".../#.#/... => ##.#/..#./..##/##..",
	"#../#.#/... => #..#/.###/.#../#.#.",
	".#./#.#/... => ####/#.##/.###/###.",
	"##./#.#/... => #.../####/...#/.#.#",
	"#.#/#.#/... => ...#/.#.#/#..#/#.##",
	"###/#.#/... => ###./#.##/##.#/..##",
	".../###/... => ..../##.#/.#../..##",
	"#../###/... => ####/..##/.##./.###",
	".#./###/... => #.#./#.#./#.../#..#",
	"##./###/... => #..#/..##/#.##/#.#.",
	"#.#/###/... => .##./##.#/.#../####",
	"###/###/... => ####/##.#/.#../#.#.",
	"..#/.../#.. => #..#/#.##/.###/.###",
	"#.#/.../#.. => .##./#.../.#.#/....",
	".##/.../#.. => .#.#/.#.#/##../####",
	"###/.../#.. => .#.#/.##./####/##.#",
	".##/#../#.. => .###/.###/.###/#...",
	"###/#../#.. => ..##/#.../#.#./..#.",
	"..#/.#./#.. => #.#./##../##../####",
	"#.#/.#./#.. => ..../..##/#..#/..#.",
	".##/.#./#.. => #.##/#..#/##.#/.##.",
	"###/.#./#.. => ...#/#.../#.#./.#..",
	".##/##./#.. => .##./#..#/.##./...#",
	"###/##./#.. => ##.#/##.#/.##./...#",
	"#../..#/#.. => ##../..#./..#./#.#.",
	".#./..#/#.. => #.#./##../#..#/#.##",
	"##./..#/#.. => #.##/###./###./.#.#",
	"#.#/..#/#.. => ..../...#/...#/#..#",
	".##/..#/#.. => #..#/#.#./..##/.##.",
	"###/..#/#.. => ##../.#.#/.#../#.#.",
	"#../#.#/#.. => ####/.##./.##./.##.",
	".#./#.#/#.. => ...#/.##./..#./.##.",
	"##./#.#/#.. => .#.#/.##./..#./.#.#",
	"..#/#.#/#.. => .#../##.#/##../#...",
	"#.#/#.#/#.. => .#.#/..#./#.../##..",
	".##/#.#/#.. => ..#./#.#./###./#...",
	"###/#.#/#.. => ..../#.#./..##/##.#",
	"#../.##/#.. => .##./##../.#../..##",
	".#./.##/#.. => ##../#.#./#.../####",
	"##./.##/#.. => ###./###./#.#./..##",
	"#.#/.##/#.. => ...#/#..#/..#./###.",
	".##/.##/#.. => ..##/####/..../#.##",
	"###/.##/#.. => .#.#/#.../.##./#...",
	"#../###/#.. => ..#./.#.#/#..#/.##.",
	".#./###/#.. => ####/..../####/#.##",
	"##./###/#.. => .###/..../#.#./####",
	"..#/###/#.. => ###./#.#./.#.#/#...",
	"#.#/###/#.. => #.#./#.#./..##/.##.",
	".##/###/#.. => #.##/.###/.##./#.##",
	"###/###/#.. => #..#/.#../.#../.##.",
	".#./#.#/.#. => .#../.##./##../..##",
	"##./#.#/.#. => .##./#.##/...#/#.#.",
	"#.#/#.#/.#. => ##.#/###./#.#./..#.",
	"###/#.#/.#. => ..../##../.###/###.",
	".#./###/.#. => .#.#/.###/..../#..#",
	"##./###/.#. => #.../..#./#..#/.#..",
	"#.#/###/.#. => .#../##.#/##.#/.###",
	"###/###/.#. => #..#/.#.#/#.#./..#.",
	"#.#/..#/##. => .#../.###/...#/#.##",
	"###/..#/##. => ...#/...#/..##/...#",
	".##/#.#/##. => #.#./###./.##./####",
	"###/#.#/##. => #.#./...#/...#/....",
	"#.#/.##/##. => ###./#.../##.#/..#.",
	"###/.##/##. => .#../#.../.###/.#..",
	".##/###/##. => #.../..#./..#./.###",
	"###/###/##. => .#../.#../####/###.",
	"#.#/.../#.# => ##.#/##../...#/##.#",
	"###/.../#.# => ###./###./#..#/###.",
	"###/#../#.# => .###/..#./.#../#...",
	"#.#/.#./#.# => ##.#/.##./.#.#/##.#",
	"###/.#./#.# => ...#/...#/#.##/.##.",
	"###/##./#.# => #.../##../#.../....",
	"#.#/#.#/#.# => ####/.#../..##/..##",
	"###/#.#/#.# => ##../####/#.##/..##",
	"#.#/###/#.# => ##../..../..../####",
	"###/###/#.# => .#../.#.#/.###/.#.#",
	"###/#.#/### => ##../####/###./...#",
	"###/###/### => ###./#..#/##../.##.",
};
