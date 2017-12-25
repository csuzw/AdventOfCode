<Query Kind="Program" />

void Main()
{
    TheHaltingProblemPartOne(Input).Dump();
}

int TheHaltingProblemPartOne(string input)
{
    return new TuringMachine(input).Execute();
}

class TuringMachine
{
    public char State { get; private set; }
    public int Steps { get; private set; }
    
    private readonly Dictionary<char, Func<bool, (bool value, int move, char state)>> _rules = new Dictionary<char, Func<bool, (bool, int, char)>>();
    
    public TuringMachine(string rules)
    {
        Initialize(rules);
    }
    
    public int Execute()
    {
        var state = State;
        var cell = new Cell();
        for (var _ = 0; _ < Steps; _++)
        {
            var rule = _rules[state];
            var result = rule(cell.Value);
            cell.Value = result.value;
            cell = cell.Move(result.move);
            state = result.state;
        }
        return cell.Count();
    }
    
    private void Initialize(string rules)
    {
        var lines = rules.GetLines(true).ToList();
        SetState(lines[0]);
        SetSteps(lines[1]);
        for (var i = 2; i < lines.Count; i += 9)
        {
            AddRule(lines.Skip(i).Take(9).ToArray());
        }
    }

    private void SetState(string line)
    {
        State = line[15];
    }
    
    private void SetSteps(string line)
    {
        var parts = line.Split(' ');
        Steps = int.Parse(parts[5]);
    }

    private void AddRule(string[] line)
    {
        var state = line[0][9];
        var off = (line[2].EndsWith("1."), line[3].EndsWith("right.") ? 1 : -1, line[4][26]);
        var on = (line[6].EndsWith("1."), line[7].EndsWith("right.") ? 1 : -1, line[8][26]);

        _rules.Add(state, i => i ? on : off);
    }
    
    private class Cell
    {
        private Cell _previous;
        private Cell _next;

        public Cell(Cell previous = null, Cell next = null)
        {
            _previous = previous;
            _next = next;
        }

        public bool Value { get; set; }
        public Cell Previous => _previous = _previous ?? new Cell(null, this);
        public Cell Next => _next = _next ?? new Cell(this, null);

        public Cell Move(int value) => value == 0 ? this : value > 0 ? Next.Move(value - 1) : Previous.Move(value + 1);
        public int Count(int value = 0) => (Value ? 1 : 0) + (value <= 0 && _previous != null ? Previous.Count(-1) : 0) + (value >= 0 && _next != null ? Next.Count(1) : 0); 
    }
}

static class Extensions
{
    public static IEnumerable<string> GetLines(this string str, bool removeEmptyLines = false)
    {
        using (var sr = new StringReader(str))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (removeEmptyLines && String.IsNullOrWhiteSpace(line)) continue;
                yield return line;
            }
        }
    }
}

// 1: 3
string Example => @"
Begin in state A.
Perform a diagnostic checksum after 6 steps.

In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state B.

In state B:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state A.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.
";

// 1: 4387
string Input => @"
Begin in state A.
Perform a diagnostic checksum after 12208951 steps.

In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state E.

In state B:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state C.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the right.
    - Continue with state A.

In state C:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state D.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the right.
    - Continue with state C.

In state D:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state E.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state F.

In state E:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state A.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state C.

In state F:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state E.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.
";
