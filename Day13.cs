using System;
using System.Collections.Generic;
using System.IO;

class Day13 {
	public static int Solution1() {
		string[] input = File.ReadAllLines("input13.txt");
		int target = int.Parse(input[0]);
		int earliest = int.MaxValue;
		int earliestId = -1;

		foreach (string str in input[1].Split(',')) {
			if (int.TryParse(str, out int id)) {
				int time = (target / id + (target % id == 0 ? 0 : 1)) * id;
				if (time < earliest) {
					earliest = time;
					earliestId = id;
				}
			}
		}

		return (earliest - target) * earliestId;
	}

	public static int Solution2() {
		string[] input = File.ReadAllLines("input13.txt")[1].Split(',');
		Dictionary<int, int> ids = new Dictionary<int, int>();
		for (int i = 0; i < input.Length; i++) {
			if (int.TryParse(input[i], out int id)) {
				ids[id] = i % id;
			}
		}

		long answer = 0;
		long mult = 1;
		foreach (KeyValuePair<int, int> i in ids) {
			int id = i.Key;
			int offset = i.Value;

			for (long x = answer + offset + mult; ; x += mult) {
				if (x % id == 0) {
					answer = x - offset;
					break;
				}
			}

			mult *= id;
		}

		Console.WriteLine(answer);
		return 0;
	}
}
