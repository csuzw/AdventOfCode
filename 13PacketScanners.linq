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
	return Enumerable.Range(1, int.MaxValue).First(delay => input.ToLayers().GetTripSeverity(delay) == 0);
}

static class Extensions
{
	public static int GetTripSeverity(this IEnumerable<(int depth, int range)> layers, int delay = 0)
	{
		return layers.Where(Collision).Sum(Score);

		bool Collision((int depth, int range) layer) => (layer.depth + delay) % (2 * layer.range - 2) == 0;
		int Score((int depth, int range) layer) => (layer.depth + delay) * layer.range;
	}
	
	public static IEnumerable<(int depth, int range)> ToLayers(this IEnumerable<string> input)
	{
		return input.Select(Parse);

		(int, int) Parse(string value) => (int.Parse(value.Substring(0, value.IndexOf(':'))), int.Parse(value.Substring(value.IndexOf(' '))));
	}
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
