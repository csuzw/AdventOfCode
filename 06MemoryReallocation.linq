<Query Kind="Program" />

void Main()
{
	MemoryReallocationPartOne(Input).Dump();
	MemoryReallocationPartTwo(Input).Dump();
}

int MemoryReallocationPartOne(int[] input)
{
	var set = new HashSet<string>();
	return input
		.AsMemoryReallocationSequence()
		.Select(i => i.ToStringRepresentation())
		.TakeWhile(i => set.Add(i))
		.Count();
}

int MemoryReallocationPartTwo(int[] input)
{
	var map = new Dictionary<string, int>();
	var recurrentItem = input
		.AsMemoryReallocationSequence()
		.Select((m, i) => (Key: m.ToStringRepresentation(), Index: i))
		.First(i => 
		{
			if (map.ContainsKey(i.Key)) return true;
			map.Add(i.Key, i.Index);
			return false;
		});
	return recurrentItem.Index - map[recurrentItem.Key];
}

static class Extensions
{
	public static string ToStringRepresentation(this int[] input)
	{
		return string.Join("|", input);
	}
	
	public static IEnumerable<int[]> AsMemoryReallocationSequence(this int[] input)
	{
		var length = input.Length;
		var state = new int[length];
		input.CopyTo(state, 0);
		yield return state;
		while (true)
		{		
			(var value, var index) = state.Select((v, i) => (v, i)).Aggregate((acc, next) => next.v > acc.v ? next : acc);	
			state = state.Select((v, i) => (i == index ? 0 : v) + (value / length) + ((length + i - index - 1) % length < value % length ? 1 : 0)).ToArray();
			yield return state;
		}
	}
}

int[] Example => new[] {0, 2, 7, 0}; 											// expect 5    then 4
int[] Input => new[] {11, 11, 13, 7, 0, 15, 5, 5, 4, 4, 1, 1, 7, 1, 15, 11}; 	// expect 4074 then 2793
