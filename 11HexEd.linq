<Query Kind="Program" />

void Main()
{
	HexEdPartOne(Input).Dump();
	HexEdPartTwo(Input).Dump();
}

int HexEdPartOne(string input) => input.Split(',').ToDistances().Last();
int HexEdPartTwo(string input) => input.Split(',').ToDistances().Max();

static class Extensions
{
	public static IEnumerable<int> ToDistances(this IEnumerable<string> steps)
	{
		(var n, var ne, var se, var s, var sw, var nw) = (0, 0, 0, 0, 0, 0);
		foreach (var step in steps)
		{
			switch (step)
			{
				case "n": ReduceN(); break;
				case "ne": ReduceNE(); break;
				case "se": ReduceSE(); break;
				case "s": ReduceS(); break;
				case "sw": ReduceSW(); break;
				case "nw": ReduceNW(); break;
			}
			yield return n + ne + se + s + sw + nw;
		}
		
		void ReduceN()  { if (se > 0) { se--; ReduceNE(); } else if (sw > 0) { sw--; ReduceNW(); } else if (s > 0)  { s--;  } else { n++;  } }
		void ReduceNE() { if (nw > 0) { nw--; ReduceN();  } else if (s > 0)  { s--; ReduceSE();  } else if (sw > 0) { sw--; } else { ne++; } }
		void ReduceSE() { if (sw > 0) { sw--; ReduceS();  } else if (n > 0)  { n--; ReduceNE();  } else if (nw > 0) { nw--; } else { se++; } }
		void ReduceS()  { if (ne > 0) { ne--; ReduceSE(); } else if (nw > 0) { nw--; ReduceSW(); } else if (n > 0)  { n--;  } else { s++;  } }
		void ReduceSW() { if (se > 0) { se--; ReduceS();  } else if (n > 0)  { n--; ReduceNW();  } else if (ne > 0) { ne--; } else { sw++; } }
		void ReduceNW() { if (ne > 0) { ne--; ReduceN();  } else if (s > 0)  { s--; ReduceSW();  } else if (se > 0) { se--; } else { nw++; } }
	}
}

(string input, int output)[] Examples => new[]
{
	("ne,ne,ne", 3),
	("ne,ne,sw,sw", 0),
	("ne,ne,s,s", 2),
	("se,sw,se,sw,sw", 3),
};

// 1: 664, 2: 1447
string Input => "s,s,sw,se,s,nw,nw,ne,n,ne,n,n,n,n,n,n,n,ne,n,ne,ne,se,ne,n,ne,n,n,ne,se,sw,se,s,se,se,se,se,s,se,se,s,se,se,nw,se,se,se,s,s,nw,s,s,se,nw,s,n,s,nw,s,s,s,s,s,s,s,s,s,s,s,sw,s,s,s,s,s,sw,sw,s,sw,s,nw,sw,sw,s,sw,ne,sw,sw,s,se,sw,sw,sw,sw,sw,sw,sw,nw,sw,sw,sw,se,sw,nw,nw,sw,sw,sw,s,sw,nw,se,nw,se,nw,sw,nw,nw,se,n,sw,s,s,s,nw,sw,sw,nw,se,nw,sw,sw,sw,nw,sw,sw,nw,nw,nw,nw,ne,n,nw,nw,ne,nw,nw,nw,nw,nw,se,nw,nw,n,nw,nw,nw,sw,n,nw,nw,nw,nw,n,s,nw,ne,nw,s,nw,nw,nw,n,nw,nw,nw,nw,nw,nw,s,sw,n,n,nw,nw,n,n,nw,nw,n,nw,n,n,nw,n,s,n,nw,ne,n,nw,n,nw,n,n,n,n,se,s,n,s,n,s,n,n,n,nw,n,s,n,n,n,n,n,ne,n,n,n,n,s,n,n,n,n,sw,n,n,n,nw,n,n,n,n,nw,se,n,ne,n,n,ne,n,ne,ne,n,n,n,n,ne,n,n,nw,n,n,n,n,ne,se,se,ne,ne,ne,n,ne,n,ne,ne,nw,ne,ne,n,n,n,ne,ne,ne,n,ne,nw,n,s,ne,ne,ne,ne,ne,n,s,ne,ne,ne,n,ne,ne,ne,sw,ne,ne,ne,s,n,ne,ne,n,ne,ne,ne,ne,ne,se,ne,ne,se,ne,ne,ne,ne,se,ne,se,ne,nw,nw,sw,s,n,ne,ne,ne,ne,ne,sw,ne,ne,ne,sw,ne,ne,ne,ne,sw,se,ne,ne,ne,ne,se,s,se,s,nw,ne,ne,n,se,ne,ne,ne,sw,ne,s,s,nw,se,nw,ne,s,ne,se,ne,n,ne,n,s,n,ne,ne,s,ne,se,se,ne,sw,nw,s,n,nw,n,se,ne,se,se,sw,ne,ne,sw,se,se,se,se,sw,ne,se,s,ne,ne,n,se,ne,sw,ne,ne,se,se,nw,se,ne,ne,nw,sw,se,s,s,se,se,se,s,se,nw,se,ne,se,se,se,se,se,se,se,sw,nw,se,se,se,se,se,se,sw,se,sw,ne,se,se,se,se,se,se,se,se,s,se,se,se,se,se,se,ne,se,se,s,sw,s,se,se,se,se,se,se,se,s,se,sw,se,se,n,s,se,s,ne,se,se,se,s,se,s,se,se,ne,se,se,sw,s,se,se,se,se,nw,se,n,ne,s,s,nw,se,se,s,se,n,se,se,s,se,se,s,se,se,ne,se,se,se,s,s,sw,s,s,se,s,se,s,se,s,se,se,se,s,se,s,nw,s,s,se,se,se,se,sw,sw,s,se,s,se,se,s,n,se,se,se,se,s,se,se,s,se,se,se,sw,s,s,s,se,se,s,s,se,s,s,se,s,s,n,s,nw,s,n,s,sw,s,nw,s,s,se,se,sw,s,s,s,sw,se,s,n,s,se,n,s,se,se,se,s,s,s,se,ne,s,se,n,se,s,se,se,s,ne,sw,se,s,s,se,s,s,s,s,s,s,s,s,se,s,nw,s,s,s,s,s,s,s,s,s,s,s,ne,ne,s,s,s,s,s,s,s,s,s,ne,ne,s,s,s,s,s,s,s,s,nw,s,s,se,sw,s,sw,s,s,nw,s,s,s,s,s,s,s,s,s,n,ne,se,s,s,s,s,n,se,s,sw,s,sw,sw,sw,s,s,sw,s,s,s,nw,sw,s,s,s,s,s,ne,sw,s,s,sw,s,s,s,s,s,s,sw,s,s,se,s,s,sw,n,sw,s,s,sw,s,s,s,s,s,sw,s,ne,s,s,s,s,sw,ne,s,ne,n,sw,s,s,s,sw,s,sw,nw,s,s,ne,sw,sw,nw,s,s,sw,sw,s,ne,s,s,sw,se,s,s,sw,s,s,sw,s,sw,sw,s,s,s,s,sw,sw,sw,s,n,ne,s,ne,s,sw,s,se,s,sw,sw,s,sw,sw,sw,sw,s,s,s,s,se,s,sw,sw,sw,sw,n,s,sw,s,s,sw,sw,s,s,n,sw,s,sw,sw,ne,sw,sw,s,sw,sw,sw,sw,sw,s,s,sw,se,sw,sw,sw,sw,s,s,sw,s,sw,sw,nw,sw,sw,se,sw,s,s,nw,nw,s,s,sw,sw,s,n,s,sw,sw,se,s,sw,sw,ne,sw,sw,sw,sw,sw,ne,sw,s,sw,sw,n,sw,sw,sw,sw,s,sw,sw,sw,sw,sw,n,nw,s,sw,s,s,n,ne,sw,sw,sw,sw,n,sw,se,sw,sw,s,se,sw,sw,sw,sw,sw,sw,s,ne,ne,ne,sw,sw,sw,ne,s,sw,sw,sw,sw,nw,s,sw,sw,s,s,sw,sw,n,nw,nw,sw,sw,sw,se,nw,nw,sw,s,sw,sw,sw,sw,sw,sw,sw,sw,sw,n,sw,sw,sw,nw,nw,se,sw,sw,sw,sw,sw,ne,sw,nw,sw,sw,sw,sw,sw,sw,sw,sw,sw,sw,sw,sw,sw,nw,s,sw,sw,se,sw,s,sw,sw,nw,nw,nw,sw,sw,nw,sw,se,ne,sw,sw,sw,sw,ne,sw,sw,nw,sw,se,nw,sw,sw,sw,sw,sw,sw,sw,sw,sw,sw,sw,n,nw,sw,sw,sw,s,sw,sw,nw,n,s,sw,n,nw,nw,sw,sw,nw,sw,sw,se,sw,sw,nw,sw,sw,s,sw,nw,sw,nw,sw,nw,nw,nw,sw,nw,sw,sw,sw,sw,sw,sw,sw,sw,sw,s,sw,nw,nw,ne,n,nw,sw,sw,nw,sw,sw,nw,nw,nw,sw,sw,sw,ne,s,sw,nw,nw,sw,nw,sw,s,nw,n,nw,sw,sw,nw,nw,sw,nw,nw,n,sw,nw,sw,nw,sw,n,sw,nw,sw,sw,sw,sw,n,sw,n,nw,nw,s,sw,se,sw,sw,nw,n,sw,sw,sw,n,sw,nw,sw,ne,nw,sw,sw,s,n,nw,sw,nw,nw,nw,sw,sw,sw,nw,nw,ne,sw,s,sw,nw,n,sw,sw,sw,nw,ne,ne,sw,nw,nw,sw,s,s,sw,sw,nw,ne,sw,nw,sw,nw,nw,sw,sw,sw,sw,nw,nw,s,se,nw,sw,nw,ne,s,nw,nw,ne,sw,nw,nw,n,nw,nw,sw,sw,sw,nw,nw,nw,sw,nw,nw,n,sw,sw,nw,s,n,sw,nw,nw,sw,nw,n,nw,nw,nw,nw,nw,nw,sw,sw,n,n,sw,sw,nw,nw,nw,nw,ne,nw,nw,nw,sw,nw,nw,nw,nw,ne,nw,nw,nw,nw,n,nw,nw,nw,s,nw,nw,sw,nw,s,nw,ne,ne,nw,nw,sw,nw,nw,nw,nw,sw,nw,se,sw,nw,sw,nw,nw,ne,nw,n,nw,nw,sw,nw,nw,nw,sw,nw,ne,s,nw,nw,sw,s,nw,sw,sw,nw,nw,nw,sw,s,nw,nw,nw,nw,se,nw,s,nw,nw,nw,se,ne,ne,nw,nw,nw,nw,nw,sw,nw,ne,ne,nw,nw,nw,nw,nw,nw,nw,nw,nw,sw,nw,nw,nw,ne,nw,nw,s,nw,nw,ne,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,sw,nw,nw,nw,nw,nw,nw,nw,ne,nw,n,nw,nw,ne,n,nw,sw,nw,n,nw,n,sw,nw,ne,s,se,n,ne,se,nw,ne,nw,ne,nw,ne,nw,nw,nw,n,nw,nw,nw,nw,nw,nw,nw,ne,nw,s,se,nw,n,n,nw,ne,nw,nw,nw,nw,ne,nw,nw,s,nw,nw,nw,nw,nw,n,nw,nw,nw,nw,nw,n,nw,nw,s,nw,nw,nw,nw,nw,sw,nw,nw,nw,nw,nw,n,nw,nw,nw,nw,nw,sw,n,nw,nw,nw,nw,nw,nw,nw,s,nw,se,n,n,n,nw,se,nw,nw,s,s,nw,nw,n,nw,nw,s,nw,nw,nw,se,nw,nw,nw,se,nw,nw,nw,nw,se,nw,nw,nw,nw,nw,n,nw,n,ne,nw,nw,nw,se,ne,nw,nw,nw,n,nw,nw,ne,n,n,nw,nw,sw,nw,nw,nw,nw,se,nw,n,s,nw,nw,n,n,nw,se,n,nw,nw,nw,n,nw,nw,nw,n,nw,se,n,se,sw,s,n,s,nw,nw,nw,nw,n,n,s,nw,nw,se,nw,nw,se,nw,n,n,nw,n,se,nw,n,n,nw,n,nw,n,nw,nw,n,nw,n,s,nw,nw,nw,nw,ne,ne,se,sw,nw,n,n,nw,s,n,nw,nw,n,n,nw,n,nw,nw,nw,nw,nw,n,nw,n,n,sw,n,se,nw,n,n,nw,n,nw,nw,n,s,sw,nw,ne,nw,n,sw,nw,nw,n,nw,sw,s,nw,n,n,nw,se,n,nw,n,ne,n,nw,nw,n,nw,nw,n,nw,n,nw,nw,nw,n,se,sw,nw,nw,nw,sw,nw,nw,nw,nw,se,n,n,ne,n,nw,nw,n,nw,nw,n,sw,n,se,nw,nw,n,n,n,nw,n,nw,n,nw,n,ne,n,n,nw,n,n,n,nw,se,sw,n,sw,n,nw,nw,n,n,n,se,nw,sw,ne,n,se,nw,nw,n,n,n,n,n,n,nw,n,n,nw,sw,nw,n,sw,n,n,se,sw,n,n,n,nw,sw,nw,n,n,n,n,nw,n,n,nw,n,s,n,n,sw,n,nw,ne,s,nw,ne,n,n,n,ne,s,n,n,n,n,n,n,se,nw,nw,n,n,nw,n,n,s,se,n,nw,n,n,n,n,n,n,nw,n,n,n,nw,nw,nw,n,n,n,nw,nw,sw,n,se,n,s,n,n,n,n,n,n,ne,n,se,n,n,n,se,n,nw,n,nw,n,n,n,n,n,n,n,nw,n,n,n,n,n,n,ne,n,n,nw,n,n,sw,n,nw,n,n,sw,n,n,n,nw,se,n,n,n,nw,n,s,n,n,n,n,n,n,n,s,n,n,n,n,nw,n,n,sw,sw,nw,n,nw,nw,sw,n,n,n,n,n,n,n,n,n,n,n,s,n,n,n,n,nw,n,n,n,n,n,n,n,s,n,nw,n,sw,nw,ne,n,nw,n,sw,n,n,n,n,n,ne,n,nw,n,n,n,n,n,n,n,ne,n,n,n,n,ne,n,n,n,n,ne,n,n,n,n,ne,n,n,s,n,n,se,n,n,n,n,n,n,n,nw,n,ne,nw,sw,ne,nw,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,ne,n,n,se,n,ne,n,ne,n,n,n,n,ne,n,ne,se,s,ne,nw,se,n,n,n,n,se,n,n,ne,s,n,nw,n,nw,nw,n,n,n,n,n,n,n,n,n,s,n,n,n,s,n,nw,n,n,n,n,n,n,n,n,n,n,n,n,ne,ne,n,sw,n,se,n,n,n,n,n,n,n,se,n,ne,n,ne,n,n,n,n,n,sw,n,n,s,nw,n,n,n,ne,ne,n,n,n,ne,n,n,se,n,n,n,n,ne,n,n,n,s,n,se,ne,n,n,n,n,n,n,n,n,s,ne,s,nw,n,ne,s,ne,n,n,n,n,ne,n,n,n,n,n,n,n,n,n,ne,n,se,ne,n,ne,ne,ne,ne,se,n,n,ne,n,n,n,n,nw,ne,sw,ne,n,n,ne,se,n,n,n,n,se,n,n,n,ne,n,se,ne,ne,n,s,nw,n,ne,n,n,nw,n,n,ne,n,ne,n,sw,n,se,n,ne,nw,ne,ne,n,n,n,ne,n,ne,nw,n,s,n,n,n,n,ne,n,n,ne,n,nw,n,n,n,ne,n,s,n,n,n,n,n,ne,ne,n,sw,n,ne,n,n,sw,ne,n,ne,ne,n,n,ne,ne,ne,n,ne,ne,ne,n,ne,sw,n,n,ne,ne,ne,se,n,ne,ne,ne,n,nw,n,ne,n,n,n,ne,n,n,n,n,n,ne,ne,n,ne,s,nw,ne,n,ne,ne,ne,n,n,n,n,nw,n,n,ne,ne,ne,n,ne,n,ne,ne,n,ne,ne,nw,ne,nw,n,n,ne,se,ne,se,ne,n,nw,n,n,s,n,se,ne,ne,n,ne,n,ne,s,n,n,sw,ne,ne,se,n,ne,n,n,n,n,sw,ne,ne,nw,n,n,ne,ne,ne,n,ne,n,sw,ne,ne,ne,ne,n,ne,se,ne,sw,n,n,n,ne,ne,sw,ne,ne,ne,n,ne,ne,n,ne,se,ne,s,nw,n,sw,n,ne,n,n,n,n,ne,n,sw,ne,ne,nw,n,ne,se,ne,ne,ne,ne,n,ne,ne,n,ne,n,ne,ne,ne,n,s,s,ne,ne,ne,s,ne,ne,ne,sw,n,n,ne,n,s,ne,n,n,nw,n,se,sw,ne,ne,ne,s,n,n,ne,ne,n,ne,ne,nw,ne,ne,ne,s,se,ne,ne,ne,n,ne,nw,n,ne,ne,sw,n,n,ne,ne,ne,n,ne,ne,se,ne,ne,n,ne,ne,ne,sw,s,n,n,n,se,n,s,ne,ne,ne,sw,ne,ne,se,ne,ne,ne,ne,ne,n,s,se,ne,ne,ne,n,ne,sw,se,s,ne,n,ne,ne,n,n,n,ne,n,ne,ne,se,ne,ne,n,ne,ne,ne,ne,ne,s,ne,ne,ne,nw,ne,ne,ne,ne,ne,n,ne,s,ne,ne,ne,n,ne,sw,n,n,n,ne,ne,n,ne,s,n,n,n,ne,ne,n,ne,ne,ne,sw,se,sw,ne,ne,s,ne,nw,ne,nw,se,nw,n,ne,se,n,ne,ne,ne,ne,ne,s,ne,ne,ne,ne,ne,n,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,se,ne,ne,ne,sw,ne,ne,ne,ne,n,ne,s,ne,s,ne,ne,n,ne,se,ne,ne,nw,n,ne,ne,ne,s,ne,sw,ne,n,ne,ne,n,ne,ne,ne,ne,ne,ne,n,ne,ne,ne,ne,ne,se,n,ne,ne,ne,ne,sw,ne,n,ne,se,ne,ne,ne,se,se,ne,sw,n,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,sw,ne,ne,ne,sw,n,n,ne,ne,n,ne,ne,ne,sw,ne,n,ne,ne,ne,se,ne,ne,nw,nw,sw,ne,ne,ne,ne,sw,ne,se,ne,n,ne,ne,ne,nw,ne,ne,ne,ne,ne,ne,sw,ne,ne,nw,ne,ne,ne,ne,ne,ne,ne,ne,sw,ne,ne,s,sw,ne,ne,s,sw,sw,ne,ne,ne,nw,ne,n,se,ne,ne,ne,ne,n,ne,nw,ne,ne,n,se,ne,ne,ne,ne,sw,ne,ne,s,ne,s,ne,ne,ne,ne,ne,ne,ne,nw,ne,ne,ne,se,ne,ne,ne,ne,se,ne,ne,sw,ne,s,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,sw,n,ne,ne,nw,se,ne,ne,ne,ne,se,ne,sw,ne,ne,n,se,ne,ne,se,ne,sw,nw,ne,ne,ne,ne,ne,ne,s,ne,se,sw,ne,s,ne,se,se,se,ne,ne,s,ne,ne,s,ne,se,ne,nw,s,ne,se,ne,ne,ne,sw,ne,se,ne,ne,ne,sw,ne,ne,ne,ne,nw,ne,ne,nw,ne,ne,s,ne,ne,se,ne,ne,nw,ne,ne,se,se,se,ne,se,ne,se,se,ne,ne,s,ne,ne,ne,ne,ne,ne,ne,ne,ne,n,ne,ne,ne,ne,ne,ne,ne,s,se,ne,sw,ne,ne,se,ne,ne,ne,ne,s,ne,se,ne,ne,se,n,n,ne,se,s,ne,s,ne,se,nw,ne,se,ne,ne,se,ne,se,se,ne,ne,se,ne,ne,ne,n,se,ne,ne,ne,ne,s,se,se,n,ne,se,se,ne,ne,se,ne,se,se,ne,ne,sw,ne,ne,ne,ne,ne,ne,nw,se,ne,se,ne,se,ne,s,ne,ne,sw,ne,ne,ne,ne,ne,s,se,ne,ne,s,se,ne,ne,ne,nw,ne,ne,ne,se,ne,ne,ne,ne,se,ne,ne,ne,se,ne,ne,se,n,s,ne,ne,ne,se,s,n,se,se,ne,sw,ne,ne,n,ne,se,n,se,n,s,n,s,ne,se,ne,ne,ne,ne,n,ne,se,sw,se,ne,se,nw,ne,ne,ne,se,se,ne,ne,ne,ne,ne,ne,ne,ne,nw,ne,n,s,ne,ne,ne,ne,ne,se,se,se,se,sw,s,n,ne,s,ne,ne,sw,se,se,ne,ne,ne,ne,ne,ne,se,se,s,ne,se,ne,nw,n,ne,se,se,ne,se,ne,ne,se,se,se,ne,ne,sw,se,nw,se,nw,se,se,se,se,ne,n,n,ne,se,se,ne,ne,se,ne,ne,se,ne,sw,ne,se,ne,n,se,nw,sw,ne,ne,se,ne,se,ne,ne,se,ne,se,ne,ne,ne,se,ne,se,se,n,nw,ne,ne,ne,ne,sw,n,ne,ne,ne,nw,ne,se,se,ne,ne,s,nw,n,ne,ne,ne,ne,ne,ne,ne,se,ne,sw,se,ne,s,n,ne,ne,se,ne,se,se,ne,se,ne,se,se,ne,se,se,se,ne,ne,ne,ne,se,ne,ne,nw,ne,ne,se,ne,s,se,ne,se,ne,ne,ne,ne,n,se,ne,se,se,ne,ne,ne,nw,ne,se,se,nw,ne,se,se,ne,sw,ne,ne,ne,n,ne,ne,ne,n,se,ne,se,ne,n,n,se,ne,se,se,se,ne,se,se,sw,se,ne,se,ne,sw,sw,ne,ne,ne,s,n,ne,ne,nw,ne,n,se,se,se,se,ne,nw,ne,ne,ne,se,ne,se,n,n,se,n,se,se,se,se,se,ne,sw,ne,se,ne,se,se,se,ne,sw,se,s,se,se,ne,se,se,se,n,ne,se,se,ne,ne,ne,ne,se,se,ne,se,se,ne,se,ne,ne,se,se,nw,se,se,ne,se,sw,se,ne,n,ne,se,se,se,nw,se,se,se,ne,se,ne,se,se,ne,se,se,ne,ne,se,se,sw,ne,se,se,sw,se,se,s,ne,ne,se,ne,s,ne,se,se,ne,se,nw,n,se,se,s,se,ne,se,ne,ne,sw,ne,ne,n,se,s,n,ne,se,se,ne,nw,ne,ne,se,se,ne,sw,ne,ne,ne,se,sw,ne,se,se,ne,n,ne,se,nw,se,se,se,ne,se,ne,ne,ne,s,ne,nw,ne,ne,ne,se,se,se,nw,se,ne,se,se,se,ne,se,ne,se,se,se,ne,se,se,se,n,se,ne,ne,se,se,se,se,sw,ne,se,se,se,ne,se,nw,n,ne,ne,ne,sw,n,se,n,se,se,n,ne,se,se,ne,se,ne,se,nw,se,se,se,s,sw,ne,se,sw,se,se,se,ne,ne,se,ne,se,se,se,se,ne,se,se,ne,se,se,ne,se,ne,se,se,se,ne,se,ne,ne,se,s,ne,ne,nw,se,ne,n,ne,se,se,ne,se,n,ne,ne,se,ne,se,se,se,ne,se,se,ne,ne,nw,ne,s,se,se,se,se,se,sw,nw,n,se,se,s,se,se,se,nw,se,n,nw,se,ne,ne,se,nw,se,se,se,se,se,se,ne,se,se,se,se,ne,se,se,ne,se,se,se,se,ne,se,se,se,se,ne,ne,nw,se,se,se,se,se,se,s,se,se,se,n,se,ne,ne,ne,se,se,se,se,ne,se,se,ne,se,se,se,se,se,se,se,sw,se,ne,ne,s,se,se,ne,se,se,se,ne,ne,se,se,se,se,se,se,se,s,sw,se,ne,se,se,se,se,se,se,se,n,se,sw,se,se,se,ne,se,se,se,se,se,s,ne,nw,se,se,se,se,se,sw,se,ne,sw,se,ne,se,se,ne,se,ne,n,ne,se,se,ne,se,ne,nw,nw,se,se,se,se,se,se,se,se,se,se,sw,se,se,se,se,se,nw,se,n,se,se,ne,se,se,nw,se,se,se,se,se,ne,nw,nw,se,se,se,se,ne,se,se,se,ne,ne,se,se,se,se,se,se,se,se,se,se,se,sw,s,se,se,ne,nw,se,se,se,se,se,se,sw,sw,se,ne,sw,nw,se,se,se,se,se,n,se,se,ne,se,se,se,nw,se,ne,se,se,se,s,se,se,n,se,se,ne,se,s,se,ne,se,se,se,nw,se,se,n,se,s,n,se,se,se,nw,se,s,sw,se,ne,se,se,se,nw,se,sw,se,se,sw,s,se,n,ne,sw,se,n,nw,se,ne,se,se,se,se,se,ne,se,se,se,se,se,se,se,se,n,se,se,se,ne,se,se,se,sw,se,nw,se,se,ne,se,se,se,se,n,ne,se,se,se,n,se,se,se,se,se,se,se,s,s,se,se,se,s,ne,se,se,se,se,se,se,se,se,se,se,se,se,se,n,n,se,se,se,se,se,se,se,se,se,s,sw,se,se,se,n,nw,se,se,se,se,ne,ne,se,n,se,se,sw,ne,sw,se,se,ne,se,se,se,se,se,se,se,se,se,n,se,nw,se,se,se,sw,s,se,se,se,se,se,se,se,se,ne,s,se,se,se,nw,s,se,n,se,se,se,s,sw,se,se,se,se,nw,ne,se,se,se,ne,s,se,sw,se,se,se,se,se,se,s,se,s,se,sw,se,ne,s,se,se,nw,se,se,nw,n,se,se,se,nw,nw,se,se,se,se,se,nw,s,se,se,ne,se,se,se,se,se,se,se,sw,se,se,se,se,se,se,nw,se,se,s,se,se,se,se,s,s,se,se,se,s,se,se,se,s,s,n,se,se,se,se,n,n,se,sw,nw,se,s,se,nw,se,s,nw,nw,se,s,se,se,se,se,se,se,sw,nw,se,se,s,se,se,se,se,se,se,n,n,ne,se,s,s,se,se,se,se,se,se,s,se,se,s,se,se,n,se,se,s,se,s,se,s,se,sw,se,se,sw,se,ne,se,sw,se,se,se,s,nw,se,ne,n,se,se,nw,se,ne,se,se,se,s,se,se,nw,se,s,se,se,se,nw,se,se,sw,s,se,s,se,se,nw,s,se,se,s,se,se,s,se,se,se,se,se,sw,s,se,se,s,sw,nw,ne,nw,se,nw,se,s,se,se,se,se,se,s,se,se,se,se,sw,s,sw,se,se,se,s,sw,sw,s,n,se,s,se,nw,se,se,se,ne,se,se,se,se,s,se,se,s,nw,s,se,s,nw,se,se,se,se,se,n,s,se,ne,n,se,se,nw,se,s,se,n,se,nw,s,s,s,se,nw,s,s,se,s,se,se,nw,s,se,s,se,se,se,se,n,se,se,s,se,se,se,se,s,s,se,s,se,se,s,se,s,n,se,n,se,se,s,se,s,se,se,s,s,se,se,s,se,se,se,se,s,s,s,se,s,nw,s,se,se,se,ne,nw,se,se,se,se,se,se,n,se,se,se,se,se,se,nw,se,se,se,s,s,nw,se,ne,se,s,se,ne,se,se,nw,se,se,se,sw,n,se,sw,se,se,nw,ne,s,se,sw,se,s,s,s,se,s,se,n,sw,sw,se,se,se,ne,se,s,se,sw,n,se,se,se,s,s,se,s,se,n,s,ne,se,se,s,se,se,s,sw,s,se,se,ne,s,n,se,se,se,s,s,s,se,se,s,s,ne,se,s,se,nw,se,s,se,se,s,s,s,se,n,se,se,ne,se,se,s,sw,se,s,ne,se,se,se,s,s,se,se,se,se,se,se,se,s,ne,se,s,se,s,s,nw,nw,s,s,nw,s,se,se,ne,se,se,se,n,s,s,s,s,se,se,s,s,s,nw,sw,se,s,s,n,se,s,s,s,s,n,s,se,s,s,se,sw,nw,nw,se,se,se,s,sw,se,se,se,s,se,se,s,s,s,se,ne,s,se,s,s,se,s,ne,se,se,se,se,se,se,ne,se,ne,s,se,se,se,se,se,s,s,s,n,se,nw,nw,s,se,sw,se,se,s,se,se,nw,s,s,s,ne,nw,se,se,se,n,ne,se,s,se,ne,se,ne,sw,ne,se,s,sw,se,se,se,s,s,ne,s,se,se,sw,s,s,s,s,se,se,s,s,se,s,se,s,s,nw,s,s,s,s,nw,nw,se,s,s,sw,s,se,nw,s,se,s,se,s,se,n,sw,n,ne,s,s,s,se,se,nw,s,n,se,s,s,s,se,s,s,s,sw,se,se,se,se,se,se,sw,s,nw,se,n,s,se,sw,nw,se,se,se,se,s,ne,se,ne,s,s,se,se,se,s,s,s,s,n,se,sw,n,s,nw,s,se,s,se,se,se,s,se,n,s,se,nw,se,s,s,se,se,se,s,s,n,s,se,s,s,s,se,se,se,s,s,s,s,se,se,s,se,s,s,s,s,s,s,s,s,nw,nw,se,n,sw,s,s,ne,s,nw,s,se,s,s,sw,s,se,nw,se,s,s,s,s,s,s,s,se,se,s,se,ne,s,se,se,se,s,s,s,se,ne,s,ne,s,s,se,s,s,ne,s,s,se,s,s,s,s,s,se,n,sw,n,s,se,se,s,s,nw,s,sw,se,n,s,se,s,s,sw,s,s,s,s,s,nw,s,nw,se,se,s,s,nw,se,s,s,sw,sw,s,se,se,s,s,s,se,s,se,s,s,s,se,s,ne,s,s,se,s,s,se,se,s,s,s,n,s,s,s,s,se,s,s,se,s,s,se,s,s,s,nw,se,s,s,se,se,se,nw,s,se,ne,s,se,s,n,nw,se,sw,se,se,s,se,s,se,s,sw,s,se,se,se,nw,s,s,s,s,sw,s,s,s,n,s,sw,s,s,se,se,se,s,se,s,s,s,se,s,se,se,nw,s,s,se,ne,s,se,s,se,se,se,s,s,s,s,se,s,s,s,s,se,s,s,s,s,se,se,se,sw,s,se,s,s,nw,s,s,se,s,se,s,se,s,sw,s,ne,s,s,ne,s,sw,s,s,ne,n,s,se,se,s,s,s,s,se,se,s,s,se,se,s,nw,s,s,n,s,ne,se,n,s,s,s,s,s,sw,s,n,n,s,s,sw,s,sw,n,se,s,s,s,s,nw,se,s,s,s,s,s,s,se,s,s,sw,s,s,s,se,se,ne,s,s,s,ne,se,se,s,s,se,n,n,se,n,sw,s,sw,se,nw,n,s,n,s,nw,sw,s,se,se,s,s,s,s,s,s,se,s,s,se,s,s,s,s,s,sw,se,s,s,s,s,s,s,s,s,s,s,s,s,s,s,s,s,nw,se,s,se,s,s,s,s,s,s,s,s,se,nw,se,s,s,s,se,nw,s,s,ne,s,s,se,se,sw,s,ne,n,s,s,se,n,ne,se,s,s,s,s,nw,s,ne,s,s,n,s,s,s,s,s,se,s,s,s,s,se,s,s,s,s,n,s,se,s,n,s,s,sw,s,s,s,s,s,se,s,ne,s,sw,n,s,n,se,nw,nw,s,sw,ne,n,sw,n,sw,s,n,s,se,s,se,s,s,s,s,nw,se,s,s,s,se,sw,s,se,s,s,s,s,s,s,s,s,s,s,se,ne,nw,s,s,s,s,s,se,s,ne,sw,se,se,sw,sw,s,se,ne,s,nw,ne,n,s,s,s,s,s,ne,s,s,s,s,se,s,se,s,s,s,sw,s,s,se,s,s,s,s,n,se,s,ne,s,s,s,s,se,sw,se,s,s,s,s,s,se,s,n,n,s,s,sw,s,s,s,s,ne,s,sw,s,s,s,s,s,sw,s,s,n,s,n,s,s,s,n,se,s,s,s,s,s,s,s,s,s,s,s,s,ne,sw,s,s,ne,nw,se,s,s,s,s,s,s,s,s,s,s,s,s,s,s,s,s,sw,s,s,s,nw,s,s,se,se,s,n,s,se,s,s,s,se,s,nw,s,s,s,s,se,s,s,s,ne,s,s,s,se,s,s,s,s,s,s,s,n,s,s,s,s,s,ne,s,s,s,s,sw,s,s,s,s,s,s,s,s,s,s,ne,se,sw,s,sw,s,s,s,se,s,s,s,s,se,s,s,ne,s,s,s,s,se,ne,se,n,n,n,n,n,n,nw,nw,sw,sw,nw,ne,sw,ne,sw,nw,sw,ne,s,sw,sw,sw,sw,s,sw,sw,sw,sw,nw,s,s,s,nw,s,s,s,se,s,se,s,se,n,se,s,se,s,nw,sw,se,se,s,se,se,se,se,se,se,se,se,nw,se,se,se,se,se,ne,se,ne,s,se,se,se,se,se,ne,ne,ne,ne,ne,ne,se,ne,ne,ne,ne,ne,ne,s,ne,nw,ne,sw,ne,ne,ne,ne,ne,ne,ne,se,n,ne,ne,se,ne,ne,n,se,ne,ne,sw,ne,ne,n,ne,n,ne,n,n,n,sw,n,ne,n,ne,ne,se,ne,n,n,ne,n,n,s,n,n,ne,n,s,n,n,n,s,n,ne,n,n,n,n,n,se,nw,nw,n,n,nw,n,se,n,sw,s,n,nw,nw,se,nw,nw,nw,nw,n,n,se,n,n,n,nw,nw,n,nw,ne,n,nw,n,nw,sw,nw,n,n,n,ne,se,nw,n,nw,n,n,nw,n,nw,n,nw,n,nw,nw,nw,nw,nw,nw,sw,nw,nw,nw,nw,se,sw,sw,nw,se,nw,s,nw,nw,nw,nw,nw,nw,nw,s,n,nw,nw,nw,sw,nw,se,nw,nw,nw,nw,sw,sw,nw,s,sw,nw,s,nw,nw,sw,nw,sw,sw,nw,sw,se,nw,nw,nw,nw,nw,sw,sw,nw,se,sw,sw,nw,sw,nw,se,sw,nw,nw,nw,n,nw,sw,nw,ne,nw,nw,s,nw,nw,nw,sw,nw,sw,nw,sw,sw,sw,sw,n,sw,nw,s,sw,nw,ne,sw,n,s,sw,sw,sw,sw,ne,sw,sw,sw,nw,sw,s,sw,sw,nw,sw,sw,sw,sw,sw,sw,sw,sw,sw,se,n,sw,s,sw,sw,n,sw,sw,s,s,sw,sw,sw,sw,sw,sw,sw,s,s,sw,sw,sw,ne,sw,sw,s,sw,s,nw,sw,sw,se,se,sw,sw,sw,s,s,s,sw,nw,sw,nw,s,sw,sw,sw,sw,sw,sw,s,sw,s,sw,sw,sw,s,sw,s,sw,s,sw,s,sw,s,sw,s,sw,sw,s,s,s,sw,n,sw,s,n,s,s,s,sw,s,n,s,sw,sw,sw,sw,s,s,s,s,s,s,s,s,sw,sw,s,sw,se,s,s,s,sw,s,s,sw,s,ne,s,s,s,s,s,s,s,s,s,n,se,s,s,nw,s,s,s,se,s,s,s,s,s,s,s,s,s,s,nw,se,s,s,s,sw,s,s,s,s,s,s,s,ne,n,s,se,s,s,s,s,s,s,se,se,nw,nw,s,s,s,s,s,se,sw,s,s,se,s,s,s,s,s,s,s,s,s,n,s,s,s,se,s,s,s,s,s,s,se,s,s,n,s,s,se,s,s,s,se,s,s,nw,s,s,s,n,s,s,se,s,se,s,n,ne,s,s,s,s,se,ne,n,s,se,s,se,s,s,se,s,s,s,s,s,se,s,sw,s,se,se,s,se,s,s,ne,s,ne,ne,se,s,ne,se,s,ne,s,se,ne,se,se,se,sw,s,se,se,se,se,se,n,s,s,se,s,se,s,s,se,se,s,s,se,se,nw,se,ne,s,s,se,se,se,se,se,s,se,ne,se,se,s,ne,se,sw,se,se,se,se,se,nw,se,se,se,se,se,se,se,s,ne,n,sw,ne,se,se,se,se,se,ne,se,se,se,se,se,se,se,se,se,n,se,s,s,se,se,sw,nw,s,se,se,ne,se,se,ne,s,se,se,se,se,se,se,se,se,n,se,se,se,se,se,se,ne,se,ne,nw,se,se,ne,nw,nw,se,se,se,se,sw,se,se,sw,n,se,se,sw,se,se,se,se,nw,se,se,se,se,se,nw,sw,ne,ne,ne,se,ne,ne,nw,se,se,nw,se,se,se,se,se,ne,se,s,se,se,s,ne,se,ne,se,se,se,ne,s,se,ne,ne,ne,ne,se,n,se,se,se,se,ne,se,n,ne,se,se,se,ne,sw,nw,nw,ne,se,ne,se,se,se,n,se,ne,se,se,se,se,ne,se,ne,n,se,se,ne,nw,ne,ne,se,se,n,se,se,s,se,sw,ne,ne,ne,se,se,se,ne,ne,se,n,ne,ne,se,ne,ne,ne,se,ne,sw,n,ne,s,se,se,se,sw,ne,se,se,se,sw,ne,se,ne,ne,ne,sw,ne,ne,s,ne,s,ne,se,ne,ne,se,se,se,nw,ne,ne,ne,se,ne,se,se,ne,ne,ne,se,sw,ne,ne,ne,ne,se,ne,sw,ne,sw,ne,n,nw,sw,ne,ne,ne,sw,ne,se,sw,n,ne,ne,se,n,se,ne,n,ne,se,ne,nw,ne,ne,s,n,se,ne,nw,ne,se,ne,ne,ne,se,ne,se,se,ne,ne,ne,se,se,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,se,se,nw,ne,ne,ne,ne,ne,ne,ne,se,se,ne,ne,ne,se,ne,ne,ne,ne,ne,ne,ne,ne,nw,n,n,ne,nw,ne,ne,sw,se,ne,s,ne,nw,ne,nw,ne,ne,ne,ne,ne,ne,ne,n,n,ne,n,ne,ne,s,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,ne,se,n,n,ne,s,ne,ne,ne,nw,se,ne,nw,nw,ne,n,n,ne,ne,nw,ne,n,ne,sw,n,ne,ne,ne,ne,ne,ne,ne,ne,ne,n,n,ne,ne,ne,ne,ne,n,n,ne,ne,se,sw,ne,n,ne,n,ne,ne,sw,ne,ne,n,ne,ne,se,sw,ne,ne,ne,s,n,n,s,ne,n,se,ne,ne,sw,s,ne,nw,n,n,ne,nw,ne,n,n,s,s,n,ne,ne,n,n,nw,ne,n,ne,ne,ne,ne,n,s,n,se,sw,se,ne,ne,ne,n,ne,ne,ne,n,nw,ne,n,n,ne,nw,ne,ne,sw,n,n,se,ne,n,ne,nw,se,ne,ne,ne,n,ne,ne,n,ne,ne,ne,ne,n,ne,ne,s,n,s,nw,n,n,nw,ne,ne,ne,s,n,s,ne,ne,ne,n,se,ne,ne,nw,n,n,n,ne,s,sw,n,ne,n,n,sw,n,ne,n,ne,n,sw,ne,ne,ne,sw,n,ne,ne,sw,ne,ne,n,n,se,ne,ne,s,ne,ne,n,nw,s,n,n,ne,nw,sw,ne,n,ne,ne,n,ne,n,se,n,ne,sw,sw,n,n,n,ne,ne,n,ne,n,s,ne,se,ne,nw,n,n,n,n,se,nw,ne,n,n,n,n,nw,nw,ne,n,se,ne,n,n,n,ne,n,ne,ne,ne,sw,n,s,n,n,n,ne,se,n,n,ne,ne,ne,sw,ne,ne,n,n,n,n,n,n,ne,ne,n,s,n,n,n,n,ne,ne,ne,n,n,se,nw,n,n,n,n,s,n,ne,n,n,n,n,n,n,n,n,sw,n,n,ne,n,n,n,s,ne,nw,n,nw,sw,n,n,n,n,s,n,n,ne,n,n,n,n,n,n,n,s,s,n,n,n,ne,n,s,ne,n,se,n,n,n,sw,n,n,n,ne,n,n,n,n,sw,se,n,sw,n,n,n,sw,ne,s,n,n,sw,s,se,n,n,n,n,ne,n,n,n,n,se,n,n,n,n,n,n,se,n,sw,n,n,n,nw,s,sw,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,sw,n,n,se,n,nw,se,n,sw,n,n,n,n,n,n,nw,n,n,n,n,n,n,n,n,n,sw,n,nw,ne,n,n,n,n,n,n,n,nw,n,n,sw,n,n,n,n,n,n,n,n,ne,n,s,n,n,n,n,ne,n,s,ne,n,n,n,nw,n,n,n,n,n,se,nw,n,nw,sw,n,n,ne,n,n,n,n,nw,n,n,n,nw,n,n,nw,n,n,n,sw,ne,n,n,s,n,n,n,nw,n,ne,n,nw,n,n,nw,nw,n,nw,nw,n,n,n,nw,sw,n,n,nw,sw,sw,n,nw,n,s,nw,n,s,nw,nw,nw,nw,n,se,n,n,nw,sw,nw,n,nw,n,ne,n,n,nw,nw,n,n,nw,nw,ne,nw,se,nw,nw,sw,n,n,n,sw,sw,n,n,n,n,nw,n,nw,nw,n,nw,n,nw,s,n,n,nw,nw,n,n,nw,n,sw,nw,nw,n,n,nw,nw,s,n,n,n,n,s,nw,n,n,n,n,n,nw,n,n,nw,n,nw,n,n,n,nw,n,n,nw,ne,nw,nw,n,nw,nw,n,n,nw,n,nw,ne,n,nw,n,n,nw,nw,nw,ne,nw,nw,n,n,n,se,ne,n,nw,n,n,n,n,nw,nw,n,nw,n,n,s,n,n,n,n,nw,n,nw,nw,se,n,nw,n,n,nw,nw,nw,nw,n,n,n,s,nw,nw,nw,ne,s,nw,nw,nw,n,nw,nw,sw,nw,n,nw,nw,n,nw,nw,n,nw,sw,n,n,n,n,nw,nw,nw,n,n,n,n,nw,n,n,ne,n,nw,nw,nw,ne,ne,n,n,nw,nw,ne,n,nw,nw,nw,nw,n,n,n,nw,nw,n,n,n,ne,nw,nw,nw,se,n,n,nw,n,nw,n,nw,nw,n,s,nw,n,nw,nw,nw,nw,nw,ne,nw,nw,n,nw,n,n,n,nw,n,n,ne,nw,n,n,nw,sw,nw,nw,nw,nw,nw,nw,nw,nw,s,nw,se,n,nw,nw,nw,nw,nw,nw,s,nw,n,nw,n,nw,nw,nw,nw,nw,se,s,nw,se,nw,n,ne,nw,nw,nw,n,nw,nw,nw,nw,nw,nw,n,ne,nw,se,n,nw,nw,nw,nw,sw,n,nw,nw,n,nw,n,nw,nw,ne,n,nw,n,ne,se,n,sw,nw,ne,nw,sw,nw,ne,n,nw,ne,se,nw,nw,nw,nw,nw,ne,nw,n,nw,s,nw,nw,nw,ne,nw,nw,nw,nw,s,nw,n,sw,s,nw,se,nw,ne,nw,n,nw,sw,nw,n,nw,nw,sw,nw,nw,nw,nw,n,nw,nw,nw,nw,nw,nw,nw,nw,se,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,n,s,s,nw,nw,nw,se,nw,nw,nw,nw,sw,nw,nw,n,nw,n,sw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,nw,n,nw,s,nw,nw,nw,nw,nw,nw,nw,nw,n,nw,sw,nw,nw,nw,nw,nw,s,nw,sw,nw,nw,nw,se,nw,nw,sw,nw,nw,nw,nw,nw,nw,n,nw,nw,nw,n,nw,sw,n,nw,nw,se,nw,sw,se,sw,nw,sw,n,nw,nw,nw,ne,nw,nw,nw,se,nw,ne,ne,nw,nw,nw,nw,nw,s,nw,ne,n,nw,ne,nw,sw,nw,nw,sw,nw,se,nw,nw,ne,nw,ne,nw,sw,sw,nw,nw,nw,nw,nw,sw,nw,nw,nw,se,sw,sw,nw,s,sw,nw,nw,nw,nw,ne,nw,ne,nw,nw,sw,nw,nw,nw,sw,nw,nw,nw,ne,sw,s,nw,ne,nw,nw,se,se,n,nw,nw,sw,nw,nw,nw,nw,nw,sw,nw,nw,nw,nw,n,nw,sw,nw,s,nw,sw,nw,nw,nw,sw,nw,nw,se,nw,n,nw,nw,ne,sw,nw,ne,nw,se,s,nw,nw,n,n,nw,nw,nw,nw,ne,se,sw,nw,nw,nw,nw,se,nw,nw,nw,sw,nw,nw,s,nw,nw,nw,nw,nw,sw,nw,nw,nw,nw,sw,nw,sw,nw,nw,nw,ne,nw,sw,nw,sw,nw,sw,nw,nw,nw,sw,ne,sw,nw,sw,nw,se,nw,se,nw,nw,n,sw,nw,nw,nw,nw,sw,sw,nw,sw,nw,nw,nw,nw,nw,sw,sw,nw,nw,nw,sw,nw,nw,nw,nw,ne,nw,n,nw,sw,sw,nw,nw,nw,nw,nw,sw,nw,sw,nw,sw,sw,nw,nw,sw,sw,nw,nw,nw,ne,sw,ne,sw,sw,nw,nw,nw,nw,nw,nw,nw,sw,se,nw,nw,sw,sw,nw,nw,s,nw,nw,sw,nw,nw,nw,n,sw,nw,sw,sw,n,nw,nw,nw,sw,sw,nw,s,nw,nw,nw,sw,nw,n,nw,nw,nw,nw,n,nw,nw,nw,sw,nw,nw,nw,se,nw,sw,nw,sw,sw,sw,nw,ne,s,sw,ne,nw,nw,s,nw,sw,nw,s,nw,sw,sw,sw,s,nw,se,nw,nw,nw,sw,sw,sw,n,nw,sw,nw,nw,nw,nw,nw,nw,sw,n,nw,nw,nw,s,nw,nw,nw,nw,sw,sw,sw,nw,nw,sw,sw,nw,nw,sw,nw,sw,nw,sw,sw,sw,sw,sw,nw,sw,s,nw,nw,sw,sw,sw,sw,nw,sw,sw,nw,sw,ne,nw,sw,nw,nw,sw,nw,se,nw,n,sw,nw,s,nw,ne,nw,se,sw,sw,sw,n,ne,sw,nw,sw,nw,sw,se,s,sw,sw,nw,sw,sw,nw,nw,sw,nw,sw,nw,sw,nw,sw,sw,sw,ne,sw,nw,sw,nw,nw,sw,nw,n,nw,sw,n,nw,nw,sw,sw,se,nw,nw,sw,nw,sw,sw,sw,sw,nw,se,sw,sw,nw,nw,sw,sw,sw,nw,sw,nw,nw,nw,sw,n,nw,sw,n,nw,s,nw,nw,sw,s,sw,sw,nw,sw,sw,sw,nw,ne,sw,ne,sw,s,sw,sw,nw,sw,nw,sw,sw,nw,nw,nw,sw,sw,ne,sw,n,sw,sw,sw,n,sw,sw,nw,sw,n,n,sw,sw,sw,nw,s,sw,sw,sw,sw,nw,nw,sw,nw,sw,sw,s,sw,nw,sw,sw,sw,s,sw,sw,n,nw,sw,n,sw,sw,s,nw,se,sw,sw,sw,sw,sw,nw,sw,sw,sw,sw,sw,nw,sw,sw,sw,sw,sw,sw,sw,sw,sw,nw,nw,sw,sw,sw,s,ne,se,nw,sw,sw,sw,nw,s,nw,nw,sw,sw,s,sw,sw,se,sw,sw,sw,sw,sw,sw,sw,sw,sw,nw,sw,nw,nw,nw,nw,nw,nw,s,nw,sw,sw,sw,sw,sw,n,sw,se,sw,sw,s,sw,sw,sw,sw,sw,s,s,sw,s,ne,sw,sw,s,sw,sw,nw,sw,sw,sw,sw,ne,se,sw,sw,sw,sw,n,sw,sw,sw,sw,s,nw,se,nw,nw,sw,sw,nw,se,nw,se,sw,sw,sw,nw,s,sw,sw,sw,sw,sw,sw,sw,sw,sw,ne,se,nw,nw,sw,sw,sw,n,s,sw,nw,nw,se,sw,ne,sw,n,s,sw,sw,nw,sw,sw,sw,sw,sw,ne,sw,sw,se,sw,sw,sw,sw,nw,sw,sw,ne,sw,nw,sw,s,sw,sw,sw,sw,sw,sw,sw,sw,se,sw,se,sw,sw,sw,sw,sw,nw,sw,sw,sw,sw,sw,se,sw,sw,se,sw,sw,ne";
