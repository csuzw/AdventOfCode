<Query Kind="Program" />

void Main()
{
	DiskDefragmentationPartOne(Input).Dump();
}

int DiskDefragmentationPartOne(string input)
{
	return Enumerable.Range(0, 128)
		.Select(i => $"{input}-{i}".ToKnotHash())
		.Sum(i => new BitArray(i.ToArray()).Cast<bool>().Count(b => b));
}

// this enables horrific single line solution!
//int DiskDefragmentationPartOne(string input) => Enumerable.Range(0, 128).Select(i => $"{input}-{i}").Sum(key => new BitArray(key.ToCharArray().Select(i => (byte)i).Concat(new byte[] { 0x11, 0x1f, 0x49, 0x2f, 0x17 }).Select((v, i) => (value: v, index: i)).Aggregate((IEnumerable<byte>)new byte[(key.Length + 5) * 64], (acc, i) => acc.Select((v, j) => j % (key.Length + 5) == i.index ? i.value : v)).Aggregate((position: 0, skip: 0, state: Enumerable.Range(0, 256).Select(i => (byte)i).ToArray()), (acc, i) => ((acc.position + i + acc.skip) % 256, acc.skip + 1, i > 1 ? acc.state.Select((v, j) => ((j < acc.position && j + 256 >= acc.position + i) || j >= acc.position + i) ? v : acc.state[(2 * acc.position + i + 256 - j - 1) % 256]).ToArray() : acc.state)).state.Select((v, i) => (value: v, index: i)).GroupBy(i => i.index / 16).Select(g => g.Aggregate((byte)0, (acc, i) => (byte)(acc ^ i.value))).ToArray()).Cast<bool>().Count(b => b));

static class Extensions
{
	public static IEnumerable<byte> ToKnotHash(this string key)
	{
		return key
			.ToCharArray()
			.Select(i => (byte)i)
			.Concat(new byte[] { 0x11, 0x1f, 0x49, 0x2f, 0x17 })
			.ToSparseHash(64)
			.Select((v, i) => (value: v, index: i))
			.GroupBy(i => i.index / 16)
			.Select(g => g.Aggregate((byte)0, (acc, i) => (byte)(acc ^ i.value)));
	}

	public static byte[] ToSparseHash(this IEnumerable<byte> lengths, int repeat)
	{
		var size = 256;
		var position = 0;
		var skip = 0;
		var state = Enumerable.Range(0, size).Select(i => (byte)i).ToArray();
		for (var _ = 0; _ < repeat; _++)
		{
			foreach (var length in lengths)
			{
				if (length > 1) state = state.Select((v, i) => ((i < position && i + size >= position + length) || i >= position + length) ? v : state[(2 * position + length + size - i - 1) % size]).ToArray();
				position = (position + length + skip++) % size;
			}
		}
		return state;
	}
}

// 1: 8108, 2: 1242
const string Example = "flqrgnkx";

// 1: , 2:
const string Input = "oundnydw";
