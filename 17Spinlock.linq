<Query Kind="Program" />

void Main()
{
	SpinlockPartOne(Input).Dump();
	SpinlockPartTwo(Input).Dump();
}

int SpinlockPartOne(int input)
{
	var buffer = new List<int> { 0 };
	var position = 0;
	for (var i = 1; i <= 2017; i++)
	{
		position = (position + input) % buffer.Count;
		buffer.Insert(++position, i);
	}
	return buffer[(position + 1) % buffer.Count];
}

int SpinlockPartTwo(int input)
{
	return Enumerable.Range(1, 50_000_001).Aggregate((result: 0, position: 0), (acc, i) => (acc.position == 0 ? i - 1 : acc.result, (acc.position + input + 1) % i)).result;
}

// 1: 638, 2: 1222153
const int Example = 3;

// 1: 1547, 2: 31154878
const int Input = 369;
