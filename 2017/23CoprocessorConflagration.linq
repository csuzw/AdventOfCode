<Query Kind="Program" />

void Main()
{
	CoprocessorConflagrationPartOne(Input).Dump();
	CoprocessorConflagrationPartTwo(Input).Dump();
}

long CoprocessorConflagrationPartOne(string[] input)
{
	var processor = new Processor();
	var instructions = input.Select(i => new Instruction(i)).ToArray();
	processor.Execute(instructions);
	return processor.Result;
}

long CoprocessorConflagrationPartTwo(string[] input)
{
	var result = 0;
	for (var i = 108_400; i <= 125_400; i += 17)
	{
		if (!i.IsPrime()) result += 1;
	}
	return result;
}

static class Extensions
{
	public static bool IsPrime(this int source)
	{
		var sqrt = (int)Math.Sqrt(source);  

		for (var i = 2; i <= sqrt; i++)
		{
			if (source % i == 0) return false;
		}
		return true;
	}
}

class Processor
{
	public long Result { get; private set; }
	public int Cycle { get; private set; }
	public bool DebugMode { get; private set; }

	private readonly Dictionary<string, Func<Instruction, int>> _operations;
	private readonly Dictionary<string, long> _registers;

	public Processor(bool debugMode = false)
	{
		DebugMode = debugMode;
		
		_operations = new Dictionary<string, Func<Instruction, int>>
		{
			{"set", Set},
			{"sub", Sub},
			{"mul", Multiply},
			{"jnz", JumpIfNotZero},
		};

		_registers = new Dictionary<string, long>
		{
			{"a", debugMode ? 1 : 0},
			{"b", 0},
			{"c", 0},
			{"d", 0},
			{"e", 0},
			{"f", 0},
			{"g", 0},
			{"h", 0},
		};
	}

	public void Execute(Instruction[] instructions, int maxCycles = int.MaxValue)
	{
		var i = 0;
		while (i >= 0 && i < instructions.Length && Cycle < maxCycles)
		{
			i += Execute(instructions[i]);
			Cycle += 1;
		}
	}

	private int Execute(Instruction instruction)
	{
		return _operations[instruction.OpCode](instruction);
	}

	private long GetValue(string arg)
	{
		return int.TryParse(arg, out var result)
			? result
			: GetRegisterValue(arg);
	}

	private void SetValue(string name, long value)
	{
		_registers[name] = value;
		if (DebugMode) $"{Cycle:00000}: {name} = {value}".Dump();
	}

	private int Set(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg1));
		return 1;
	}

	private int Sub(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg0) - GetValue(instruction.Arg1));
		return 1;
	}

	private int Multiply(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg0) * GetValue(instruction.Arg1));
		Result += 1;
		return 1;
	}

	private int JumpIfNotZero(Instruction instruction)
	{
		return GetValue(instruction.Arg0) != 0 ? (int)GetValue(instruction.Arg1) : 1;
	}

	private long GetRegisterValue(string registerName)
	{
		_registers.TryGetValue(registerName, out var registerValue);
		return registerValue;
	}
}

class Instruction
{
	public string OpCode { get; }
	public string Arg0 { get; }
	public string Arg1 { get; }

	public Instruction(string input)
	{
		var parts = input.Split(' ');
		OpCode = parts[0];
		Arg0 = parts[1];
		Arg1 = parts.Length > 2 ? parts[2] : string.Empty;
	}
}

string[] Input => new[]
{
	"set b 84",
	"set c b",
	"jnz a 2",
	"jnz 1 5",
	"mul b 100",
	"sub b -100000",
	"set c b",
	"sub c -17000",
	"set f 1",
	"set d 2",
	"set e 2",
	"set g d",
	"mul g e",
	"sub g b",
	"jnz g 2",
	"set f 0",
	"sub e -1",
	"set g e",
	"sub g b",
	"jnz g -8",
	"sub d -1",
	"set g d",
	"sub g b",
	"jnz g -13",
	"jnz f 2",
	"sub h -1",
	"set g b",
	"sub g c",
	"jnz g 2",
	"jnz 1 3",
	"sub b -17",
	"jnz 1 -23",
};
