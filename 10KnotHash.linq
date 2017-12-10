<Query Kind="Program" />

void Main()
{
	KnotHashPartOne(Input).Dump();
	KnotHashPartTwo(Input).Dump();
}

int KnotHashPartOne(string input)
{	
	var sparseHash = input
		.Split(',')
		.Select(i => byte.Parse(i))
		.ToSparseHashSequence(1)
		.Last();
	return sparseHash[0] * sparseHash[1];
}
string KnotHashPartTwo(string input)
{
	var sparseHash = input
		.ToCharArray()
		.Select(i => (byte)i)
		.Concat(new byte[] {0x11,0x1f,0x49,0x2f,0x17})
		.ToSparseHashSequence(64)
		.Last();
	var denseHash = sparseHash
		.Select((v, i) => (value: v, index: i))
		.GroupBy(i => i.index / 16)
		.Select(g => g.Aggregate(0x0, (acc, i) => (byte)(acc ^ i.value)));
	return denseHash
		.Aggregate(new StringBuilder(), (acc, i) => acc.Append($"{i:x2}"))
		.ToString();
}

static class Extensions
{
	public static IEnumerable<byte[]> ToSparseHashSequence(this IEnumerable<byte> lengths, int repeat)
	{
		var size = 256;
		var position = 0;
		var skip = 0;
		var state = Enumerable.Range(0, size).Select(i => (byte)i).ToArray();	
		yield return state;
		for (var _ = 0; _ < repeat; _++)
		{
			foreach (var length in lengths)
			{
				if (length > 1) state = state.Select((v, i) => ((i < position && i + size >= position + length) || i >= position + length) ? v : state[(2 * position + length + size - i - 1) % size]).ToArray();
				yield return state;
				position = (position + length + skip++) % size;
			}
		}
	}
}

// 1: 9656, 2: 20b7b54c92bf73cf3e5631458a715149
string Input => "206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3";
