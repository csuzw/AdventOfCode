<Query Kind="Program" />

void Main()
{
	DuetPartOne(Input).Dump();
}

long DuetPartOne(string[] input)
{
	var processor = new PartOneProcessor();
	var instructions = input.Select(i => new Instruction(i)).ToArray();
	processor.Execute(instructions);
	return processor.Sound;
}

//long DuetPartTwo(string[] input)
//{
//	var p0 = new PartTwoProcessor(0);
//	var p1 = new PartTwoProcessor(1);
//	var instructions = input.Select(i => new Instruction(i)).ToArray();
//}
//
//class PartTwoProcessor : Processor, IPartner
//{
//	private IPartner _partner;
//	private readonly Queue<long> _messageQueue = new Queue<long>();
//	
//	public PartTwoProcessor(int id) : base(id)
//	{
//	}
//
//	public void Attach(IPartner partner)
//	{
//		_partner = partner;
//	}
//
//	public bool IsBlocked { get; private set; }
//
//	public void Push(long value)
//	{
//		_messageQueue.Enqueue(value);
//		IsBlocked = false;
//	}
//	
//	public override void Execute(Instruction[] instructions)
//	{
//		var i = 0;
//		while (i >= 0 && i < instructions.Length)
//		{
//			i += Execute(instructions[i]);
//		}
//	}
//
//	protected override int Receive(Instruction instruction)
//	{
//		if (_messageQueue.Any())
//		{
//			SetValue(instruction.Arg0, _messageQueue.Dequeue());
//			return 1;
//		}
//		if (_partner.IsBlocked) {} // TODO deadlock
//		
//		IsBlocked = true;
//	}
//
//	protected override int Send(Instruction instruction)
//	{
//		_partner.Push(GetValue(instruction.Arg0));
//		return 1;
//	}
//}
//
//interface IPartner
//{
//	bool IsBlocked { get; }
//	void Push(long value);
//}

class PartOneProcessor : Processor
{
	public long Sound { get; private set; }
	private bool _recover = false;
	
	public PartOneProcessor() : base(0)
	{		
	}

	public override void Execute(Instruction[] instructions)
	{
		var i = 0;
		while (!_recover && i >= 0 && i < instructions.Length)
		{
			i += Execute(instructions[i]);
		}
	}
	
	protected override int Send(Instruction instruction)
	{
		Sound = GetValue(instruction.Arg0);
		return 1;
	}

	protected override int Receive(Instruction instruction)
	{
		_recover = (GetValue(instruction.Arg0) != 0);
		return 1;
	}
}

abstract class Processor
{
	private readonly Dictionary<string, Func<Instruction, int>> _operations;
	private readonly Dictionary<string, long> _registers;

	protected Processor(int id)
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
		
		_registers = new Dictionary<string, long>()
		{
			{"p", id},	
		};
	}
	
	public abstract void Execute(Instruction[] instructions);
	
	protected int Execute(Instruction instruction)
	{
		return _operations[instruction.OpCode](instruction);
	}

	protected abstract int Send(Instruction instruction);
	protected abstract int Receive(Instruction instruction);

	protected long GetValue(string arg)
	{
		return int.TryParse(arg, out var result)
			? result
			: GetRegisterValue(arg);
	}
	
	protected void SetValue(string name, long value)
	{
		_registers[name] = value;
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
