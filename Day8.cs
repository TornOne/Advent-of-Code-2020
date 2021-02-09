using System;
using System.IO;

class Day8 {
	enum OpCode {
		nop,
		acc,
		jmp
	}

	public static int Solution1() {
		LoadProgram(out OpCode[] opCodes, out int[] opValues);
		return -Run(0, 0, opCodes, opValues, new bool[opCodes.Length], -1);
	}

	public static int Solution2() {
		int acc = 0;
		int ptr = 0;
		LoadProgram(out OpCode[] opCodes, out int[] opValues);
		bool[] visited = new bool[opCodes.Length];
		
		while (true) {
			if (opCodes[ptr] == OpCode.nop || opCodes[ptr] == OpCode.jmp) {
				int result = Run(acc, ptr, opCodes, opValues, (bool[])visited.Clone(), ptr);
				if (result >= 0) {
					return result;
				}
			}

			visited[ptr] = true;
			switch (opCodes[ptr]) {
				case OpCode.nop:
					ptr++;
					break;
				case OpCode.acc:
					acc += opValues[ptr];
					ptr++;
					break;
				case OpCode.jmp:
					ptr += opValues[ptr];
					break;
			}
		}
	}

	static void LoadProgram(out OpCode[] opCodes, out int[] opValues) {
		string[] input = File.ReadAllLines("input8.txt");
		opCodes = new OpCode[input.Length];
		opValues = new int[input.Length];

		for (int i = 0; i < input.Length; i++) {
			string[] command = input[i].Split(' ');
			Enum.TryParse(command[0], out opCodes[i]);
			opValues[i] = int.Parse(command[1]);
		}
	}

	static int Run(int acc, int ptr, OpCode[] opCodes, int[] opValues, bool[] visited, int opSwap) {
		if (opSwap >= 0) {
			opCodes[opSwap] = 2 - opCodes[opSwap];
		}

		while (ptr < opCodes.Length) {
			if (visited[ptr]) {
				if (opSwap >= 0) {
					opCodes[opSwap] = 2 - opCodes[opSwap];
				}
				return -acc;
			}
			visited[ptr] = true;

			switch (opCodes[ptr]) {
				case OpCode.nop:
					ptr++;
					break;
				case OpCode.acc:
					acc += opValues[ptr];
					ptr++;
					break;
				case OpCode.jmp:
					ptr += opValues[ptr];
					break;
			}
		}

		return acc;
	}
}
