<Query Kind="Program" />

void Main()
{
	ElectromagneticMoatPartOne(Input).Dump();
	ElectromagneticMoatPartTwo(Input).Dump();
}

int ElectromagneticMoatPartOne(string[] input)
{
	var components = new HashSet<Component>(input.Select(i => new Component(i)));
	
	return components.Where(i => i.CanConnectTo(0)).Max(i => i.StrongestBridge(components.Clone()));
}

int ElectromagneticMoatPartTwo(string[] input)
{
	var components = new HashSet<Component>(input.Select(i => new Component(i)));

	return components
		.Where(i => i.CanConnectTo(0))
		.Select(i => i.LongestStrongestBridge(components.Clone()))
		.LongestAndStrongest()
		.strength;
}

struct Component
{
	public readonly int a;
	public readonly int b;
	
	public Component(int a, int b)
	{
		this.a = a;
		this.b = b;
	}
	
	public Component(string value)
	{
		var parts = value.Split('/');
		this.a = int.Parse(parts[0]);
		this.b = int.Parse(parts[1]);
	}
	
	public int Strength() => a + b;
	public bool CanConnectTo(int value) => a == value || b == value;
	
	public int StrongestBridge(HashSet<Component> available, int value = 0, int strength = 0)
	{
		available.Remove(this);
		strength += this.Strength();
		var next = a == value ? b : a;
		return available
			.Where(i => i.CanConnectTo(next))
			.Select(i => i.StrongestBridge(available.Clone(), next, strength))
			.DefaultIfEmpty(strength)
			.Max();	
	}

	public (int strength, int length) LongestStrongestBridge(HashSet<Component> available, int value = 0, int strength = 0, int length = 0)
	{
		available.Remove(this);
		strength += this.Strength();
		length += 1;
		var next = a == value ? b : a;
		return available
			.Where(i => i.CanConnectTo(next))
			.Select(i => i.LongestStrongestBridge(available.Clone(), next, strength, length))
			.DefaultIfEmpty((strength, length))
			.LongestAndStrongest();
	}
}

static class Extensions
{
	public static HashSet<T> Clone<T>(this HashSet<T> source)
	{
		return new HashSet<T>(source);
	}
	
	public static (int strength, int length) LongestAndStrongest(this IEnumerable<(int strength, int length)> source)
	{
		return source.Max((a, b) => a.length > b.length || (a.length == b.length && a.strength > b.strength));
	}

	public static T Max<T>(this IEnumerable<T> source, Func<T, T, bool> bigger)
	{
		var enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext()) return default(T);
		var max = enumerator.Current;
		while (enumerator.MoveNext())
		{
			if (bigger(enumerator.Current, max)) max = enumerator.Current;
		}
		return max;
	}
}

// 1: 31, 2: 19
string[] Example => new[]
{
	"0/2",
	"2/2",
	"2/3",
	"3/4",
	"3/5",
	"0/1",
	"10/1",
	"9/10",
};

// 1: 1868, 2:
string[] Input => new[]
{
	"25/13",
	"4/43",
	"42/42",
	"39/40",
	"17/18",
	"30/7",
	"12/12",
	"32/28",
	"9/28",
	"1/1",
	"16/7",
	"47/43",
	"34/16",
	"39/36",
	"6/4",
	"3/2",
	"10/49",
	"46/50",
	"18/25",
	"2/23",
	"3/21",
	"5/24",
	"46/26",
	"50/19",
	"26/41",
	"1/50",
	"47/41",
	"39/50",
	"12/14",
	"11/19",
	"28/2",
	"38/47",
	"5/5",
	"38/34",
	"39/39",
	"17/34",
	"42/16",
	"32/23",
	"13/21",
	"28/6",
	"6/20",
	"1/30",
	"44/21",
	"11/28",
	"14/17",
	"33/33",
	"17/43",
	"31/13",
	"11/21",
	"31/39",
	"0/9",
	"13/50",
	"10/14",
	"16/10",
	"3/24",
	"7/0",
	"50/50",
};
