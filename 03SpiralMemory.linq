<Query Kind="Program" />

void Main()
{
	SpiralMemoryPartOne(Input).Dump();
	SpiralMemoryPartTwo(Input).Dump();
}

int SpiralMemoryPartOne(int location)
{
	if (location <= 1) return 0;

	var ring = 1;
	var max = 9;
	while (max < location)
	{
		ring += 1;
		max += (((ring + 1) * 2) - 2) * 4;
	}
	var offset = new[] { 1, 3, 5, 7 }.Min(i => Math.Abs(location - (max - (ring * i))));

	return ring + offset;
}

int SpiralMemoryPartTwo(int value)
{
	var memory = new SpiralMemory();
	var position = memory.AccessPort;
	var current = memory.Get(position);
	while (current < value)
	{
		position = memory.GetNext(position);
		current = memory.GetSumOfAdjacent(position);
		memory.Set(position, current);
	}
	return current;
}

class SpiralMemory
{
	private readonly Dictionary<(int, int), int> _memory = new Dictionary<(int, int), int>
	{
		{ (0, 0), 1 }
	};

	public (int x, int y) AccessPort => (0, 0);

	public int Get(int x, int y)
	{
		return Get((x, y));
	}

	public int Get((int, int) position)
	{
		if (!_memory.TryGetValue(position, out var n))
		{
			_memory[position] = 0;
		}
		return n;
	}

	public void Set((int, int) position, int value)
	{
		_memory[position] = value;
	}

	public int GetSumOfAdjacent((int, int) position)
	{
		(var x, var y) = position;
		return Get(x + 1, y + 1) + Get(x + 1, y) + Get(x + 1, y - 1) + Get(x, y + 1) + Get(x, y - 1) + Get(x - 1, y + 1) + Get(x - 1, y) + Get(x - 1, y - 1);
	}

	public (int x, int y) GetNext((int, int) position)
	{
		(var x, var y) = position;
		if (Get(x - 1, y) > 0 && Get(x, y + 1) == 0) return (x, y + 1);
		if (Get(x, y - 1) > 0 && Get(x - 1, y) == 0) return (x - 1, y);
		if (Get(x + 1, y) > 0 && Get(x, y - 1) == 0) return (x, y - 1);
		return (x + 1, y);
	}
}

const int Input = 265149;
