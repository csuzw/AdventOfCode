<Query Kind="Program" />

void Main()
{
	DuetPartOne(Input).Dump();
	DuetPartTwo(Input).Dump();
}

long DuetPartOne(string[] input)
{
	var processor = new PartOneProcessor();
	var instructions = input.Select(i => new Instruction(i)).ToArray();
	processor.Execute(instructions);
	return processor.Result;
}

long DuetPartTwo(string[] input)
{
	var processor = new PartTwoProcessor();
	var instructions = input.Select(i => new Instruction(i)).ToArray();
	processor.Execute(instructions);
	return processor.Result;
}

class PartOneProcessor
{
	public long Result { get; private set; }

	private readonly Dictionary<string, Func<Instruction, int>> _operations;
	private readonly Dictionary<string, long> _registers;
	
	private bool _recover = false;
	
	public PartOneProcessor()
	{
		_operations = new Dictionary<string, Func<Instruction, int>>
		{
			{"snd", Send},
			{"set", Set},
			{"add", Add},
			{"mul", Multiply},
			{"mod", Modulo},
			{"rcv", Receive},
			{"jgz", JumpIfGreaterThanZero},
		};

		_registers = new Dictionary<string, long>();
	}

	public void Execute(Instruction[] instructions)
	{
		var i = 0;
		while (!_recover && i >= 0 && i < instructions.Length)
		{
			i += Execute(instructions[i]);
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
	}

	private int Send(Instruction instruction)
	{
		Result = GetValue(instruction.Arg0);
		return 1;
	}

	private int Receive(Instruction instruction)
	{
		_recover = (GetValue(instruction.Arg0) != 0);
		return 1;
	}

	private int Set(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg1));
		return 1;
	}

	private int Add(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg0) + GetValue(instruction.Arg1));
		return 1;
	}

	private int Multiply(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg0) * GetValue(instruction.Arg1));
		return 1;
	}

	private int Modulo(Instruction instruction)
	{
		SetValue(instruction.Arg0, GetValue(instruction.Arg0) % GetValue(instruction.Arg1));
		return 1;
	}

	private int JumpIfGreaterThanZero(Instruction instruction)
	{
		return GetValue(instruction.Arg0) > 0 ? (int)GetValue(instruction.Arg1) : 1;
	}

	private long GetRegisterValue(string registerName)
	{
		_registers.TryGetValue(registerName, out var registerValue);
		return registerValue;
	}
}

class PartTwoProcessor
{
	public long Result { get; private set; }

	private readonly Dictionary<string, Func<int, Instruction, int>> _operations;
	private readonly (Dictionary<string, long> registers, Queue<long> messages, int position)[] _processors;

	public PartTwoProcessor()
	{
		_operations = new Dictionary<string, Func<int, Instruction, int>>
		{
			{"snd", Send},
			{"set", Set},
			{"add", Add},
			{"mul", Multiply},
			{"mod", Modulo},
			{"rcv", Receive},
			{"jgz", JumpIfGreaterThanZero},
		};

		_processors = new[]
		{
			(new Dictionary<string, long> { { "p", 0 } }, new Queue<long>(), 0),
			(new Dictionary<string, long> { { "p", 1 } }, new Queue<long>(), 0),
		};
	}

	public void Execute(Instruction[] instructions)
	{
		var currentId = 0;
		var deadlock = false;
		while (IsRunning(0) || IsRunning(1))
		{
			var processor = _processors[currentId];
			var inc = Execute(currentId, instructions[processor.position]);
			_processors[currentId] = (processor.registers, processor.messages, processor.position + inc);
			if (inc == 0)
			{
				if (deadlock) break;
				currentId = (currentId + 1) % 2;
			}
			deadlock = inc == 0;
		}

		bool IsRunning(int id) => _processors[id].position >= 0 && _processors[id].position < instructions.Length;
	}

	protected int Execute(int id, Instruction instruction)
	{
		return _operations[instruction.OpCode](id, instruction);
	}

	protected int Send(int id, Instruction instruction)
	{
		_processors[(id + 1) % 2].messages.Enqueue(GetValue(id, instruction.Arg0));
		Result += id;
		return 1;
	}

	protected int Receive(int id, Instruction instruction)
	{
		var messages = _processors[id].messages;
		if (!messages.Any()) return 0;

		SetValue(id, instruction.Arg0, messages.Dequeue());
		return 1;
	}

	protected long GetValue(int id, string arg)
	{
		return int.TryParse(arg, out var result)
			? result
			: GetRegisterValue(id, arg);
	}

	protected void SetValue(int id, string name, long value)
	{
		_processors[id].registers[name] = value;
	}

	private int Set(int id, Instruction instruction)
	{
		SetValue(id, instruction.Arg0, GetValue(id, instruction.Arg1));
		return 1;
	}

	private int Add(int id, Instruction instruction)
	{
		SetValue(id, instruction.Arg0, GetValue(id, instruction.Arg0) + GetValue(id, instruction.Arg1));
		return 1;
	}

	private int Multiply(int id, Instruction instruction)
	{
		SetValue(id, instruction.Arg0, GetValue(id, instruction.Arg0) * GetValue(id, instruction.Arg1));
		return 1;
	}

	private int Modulo(int id, Instruction instruction)
	{
		SetValue(id, instruction.Arg0, GetValue(id, instruction.Arg0) % GetValue(id, instruction.Arg1));
		return 1;
	}

	private int JumpIfGreaterThanZero(int id, Instruction instruction)
	{
		return GetValue(id, instruction.Arg0) > 0 ? (int)GetValue(id, instruction.Arg1) : 1;
	}

	private long GetRegisterValue(int id, string registerName)
	{
		_processors[id].registers.TryGetValue(registerName, out var registerValue);
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

// 1: 4, 2:
string[] Example => new[]
{
	"set a 1",
	"add a 2",
	"mul a a",
	"mod a 5",
	"snd a",
	"set a 0",
	"rcv a",
	"jgz a -1",
	"set a 1",
	"jgz a -2",
};

// 1: 2951, 2:
string[] Input => new[]
{
	"set i 31",
	"set a 1",
	"mul p 17",
	"jgz p p",
	"mul a 2",
	"add i -1",
	"jgz i -2",
	"add a -1",
	"set i 127",
	"set p 316",
	"mul p 8505",
	"mod p a",
	"mul p 129749",
	"add p 12345",
	"mod p a",
	"set b p",
	"mod b 10000",
	"snd b",
	"add i -1",
	"jgz i -9",
	"jgz a 3",
	"rcv b",
	"jgz b -1",
	"set f 0",
	"set i 126",
	"rcv a",
	"rcv b",
	"set p a",
	"mul p -1",
	"add p b",
	"jgz p 4",
	"snd a",
	"set a b",
	"jgz 1 3",
	"snd b",
	"set f 1",
	"add i -1",
	"jgz i -11",
	"snd a",
	"jgz f -16",
	"jgz a -19",
};
