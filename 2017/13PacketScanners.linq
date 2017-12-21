<Query Kind="Program" />

void Main()
{
	PacketScannersPartOne(Input).Dump();
	PacketScannersPartTwo(Input).Dump();
}

int PacketScannersPartOne(string[] input)
{
	return input.ToLayers().GetTripSeverity();
}

int PacketScannersPartTwo(string[] input)
{
	var layers = input.ToLayers().ToList();
	return Enumerable.Range(0, int.MaxValue).First(layers.IsSuccessful);
}

static class Extensions
{
	public static int GetTripSeverity(this IEnumerable<(int depth, int range)> layers)
	{
		return layers.Where(layer => IsCollision(layer, 0)).Sum(layer => layer.depth * layer.range);
	}

	public static bool IsSuccessful(this IEnumerable<(int depth, int range)> layers, int delay = 0)
	{
		return !layers.Any(layer => IsCollision(layer, delay));
	}

	public static IEnumerable<(int depth, int range)> ToLayers(this IEnumerable<string> input)
	{
		return input.Select(Parse);

		(int, int) Parse(string value) => (int.Parse(value.Substring(0, value.IndexOf(':'))), int.Parse(value.Substring(value.IndexOf(' '))));
	}

	private static bool IsCollision((int depth, int range) layer, int delay = 0) => (layer.depth + delay) % (2 * layer.range - 2) == 0;
}

// 1: 24, 2: 10
string[] Example => new[]
{
	"0: 3",
	"1: 2",
	"4: 4",
	"6: 4",
};

// 1: 632, 2: 3849742
string[] Input => new[]
{
	"0: 3",
	"1: 2",
	"2: 6",
	"4: 4",
	"6: 4",
	"8: 8",
	"10: 9",
	"12: 8",
	"14: 5",
	"16: 6",
	"18: 8",
	"20: 6",
	"22: 12",
	"24: 6",
	"26: 12",
	"28: 8",
	"30: 8",
	"32: 10",
	"34: 12",
	"36: 12",
	"38: 8",
	"40: 12",
	"42: 12",
	"44: 14",
	"46: 12",
	"48: 14",
	"50: 12",
	"52: 12",
	"54: 12",
	"56: 10",
	"58: 14",
	"60: 14",
	"62: 14",
	"64: 14",
	"66: 17",
	"68: 14",
	"72: 14",
	"76: 14",
	"80: 14",
	"82: 14",
	"88: 18",
	"92: 14",
	"98: 18",
};
