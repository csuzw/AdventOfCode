<Query Kind="Program" />

void Main()
{
	DuelingGeneratorsPartOne(Input).Dump();
	DuelingGeneratorsPartTwo(Input).Dump();
}

int DuelingGeneratorsPartOne((long A, long B) input)
{
	var generatorA = new GeneratorA(input.A, true);
	var generatorB = new GeneratorB(input.B, true);
	
	return generatorA.Zip(generatorB, AreLower16BitsTheSame).Take(40_000_000).Count(i => i);
}

int DuelingGeneratorsPartTwo((long A, long B) input)
{
	var generatorA = new GeneratorA(input.A);
	var generatorB = new GeneratorB(input.B);

	return generatorA.Zip(generatorB, AreLower16BitsTheSame).Take(5_000_000).Count(i => i);
}

bool AreLower16BitsTheSame(long a, long b) => (a & 0xffff) == (b & 0xffff);

class GeneratorA : Generator
{
	public override long Factor => 16_807;
	public override Func<long, bool> MeetsCriteria => i => i % 4 == 0;
	
	public GeneratorA(long seed, bool ignoreCriteria = false) : base(seed, ignoreCriteria) {}
}

class GeneratorB : Generator
{
	public override long Factor => 48_271;
	public override Func<long, bool> MeetsCriteria => i => i % 8 == 0;

	public GeneratorB(long seed, bool ignoreCriteria = false) : base(seed, ignoreCriteria) {}
}

abstract class Generator : IEnumerable<long>
{
	public abstract long Factor { get; }
	public abstract Func<long, bool> MeetsCriteria { get; }
	public bool IgnoreCriteria { get; }
	public long Seed { get; }
	
	protected Generator(long seed, bool ignoreCriteria = false)
	{
		Seed = seed;
		IgnoreCriteria = ignoreCriteria;
	}

	public IEnumerator<long> GetEnumerator()
	{
		var current = Seed;
		while (true)
		{		
			current = (current * Factor) % int.MaxValue;
			if (IgnoreCriteria || MeetsCriteria(current)) yield return current;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

// 1: 588, 2: 309
(long A, long B) Example => (65, 8_921);

// 1: 626, 2: 306
(long A, long B) Input => (679, 771);
