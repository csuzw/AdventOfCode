<Query Kind="Program" />

void Main()
{
	DiskDefragmentationPartOne(Input).Dump();
	DiskDefragmentationPartTwo(Input).Dump();
}

int DiskDefragmentationPartOne(string input)
{
	return input.ToDiskDefragmentationGrid().Sum(i => i.CountBits());
}

int DiskDefragmentationPartTwo(string input)
{
	var visited = new HashSet<int>();
	var result = 0;
	var grid = input.ToDiskDefragmentationGrid();
	for (var i = 0; i < 128 * 128; i++)
	{
		if (visited.Contains(i) || !grid.Get(i)) continue;
		result += 1;
		VisitGroup(i);
	}
	return result;

	void VisitGroup(int index)
	{
		visited.Add(index);
		foreach (var adjacentIndex in index.AdjacentIndices())
		{
			if (!visited.Contains(adjacentIndex) && grid.Get(adjacentIndex)) VisitGroup(adjacentIndex);
		}
	}
}

static class Extensions
{
	public static int CountBits(this byte value)
	{
		return Enumerable.Range(0, 8).Count(i => value.Get(i));
	}
	
	public static IEnumerable<int> AdjacentIndices(this int index)
	{
		(var x, var y) = (index % 128, index / 128);
		if (x + 1 < 128) yield return index + 1;
		if (x - 1 >= 0) yield return index - 1;
		if (y + 1 < 128) yield return index + 128;
		if (y - 1 >= 0) yield return index - 128;
	}

	public static bool Get(this byte source, int index)
	{
		return (source & (1 << (7 - (index % 8)))) != 0;
	}

	public static bool Get(this byte[] source, int index)
	{
		if (index < 0 || index >= source.Length * 8) return false;
		return source[index / 8].Get(index);
	}
	
	public static byte[] ToDiskDefragmentationGrid(this string input)
	{
		return Enumerable.Range(0, 128).SelectMany(i => $"{input}-{i}".ToKnotHash()).ToArray();
	}

	public static IEnumerable<byte> ToKnotHash(this string key)
	{
		return key
			.ToLengths()
			.ToSparseHash()
			.ToDenseHash();
	}

	public static IEnumerable<byte> ToLengths(this string key)
	{
		return key
			.ToCharArray()
			.Select(i => (byte)i)
			.Concat(new byte[] { 0x11, 0x1f, 0x49, 0x2f, 0x17 });
	}

	public static IEnumerable<byte> ToSparseHash(this IEnumerable<byte> lengths)
	{
		const int size = 256;
		var position = 0;
		var skip = 0;
		var state = Enumerable.Range(0, size).Select(i => (byte)i).ToArray();
		for (var _ = 0; _ < 64; _++)
		{
			foreach (var length in lengths)
			{
				if (length > 1) state = state.Select((v, i) => ((i < position && i + size >= position + length) || i >= position + length) ? v : state[(2 * position + length + size - i - 1) % size]).ToArray();
				position = (position + length + skip++) % size;
			}
		}
		return state;
	}

	public static IEnumerable<byte> ToDenseHash(this IEnumerable<byte> sparseHash)
	{
		return sparseHash
			.Select((v, i) => (value: v, index: i))
			.GroupBy(i => i.index / 16)
			.Select(g => g.Aggregate((byte)0, (acc, i) => (byte)(acc ^ i.value)));
	}
}

// 1: 8108, 2: 1242
const string Example = "flqrgnkx";

// 1: 8106, 2:
const string Input = "oundnydw";
