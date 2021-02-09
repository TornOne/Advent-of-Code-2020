using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day19 {
	public static int Solution1() => Solution((rules, line) => rules[0].Contains(line));

	public static int Solution2() => Solution((rules, line) => {
		int Count(int rule) {
			int count = 0;

			while (true) {
				bool matched = false;
				foreach (string start in rules[rule]) {
					if (line.StartsWith(start)) {
						line = line.Substring(start.Length);
						count++;
						matched = true;
						break;
					}
				}
				if (!matched) {
					return count;
				}
			}
		}

		int count42 = Count(42);
		if (count42 < 2) {
			return false;
		}

		int count31 = Count(31);
		return count31 >= 1 && count42 > count31 && line.Length == 0;
	});

	static int Solution(Func<Dictionary<int, HashSet<string>>, string, bool> counter) {
		string[] input = File.ReadAllText("input19.txt").Split(new[] { "\n\n" }, StringSplitOptions.None);

		Dictionary<int, int[][]> rules = new Dictionary<int, int[][]>();
		foreach (string[] rule in Array.ConvertAll(input[0].Split('\n'), rule => rule.Split(new[] { ": " }, StringSplitOptions.None))) {
			rules[int.Parse(rule[0])] = rule[1] == "\"a\"" ? new[] { new int[] { -'a' } } : rule[1] == "\"b\"" ? new[] { new int[] { -'b' } } : Array.ConvertAll(rule[1].Split(new[] { " | " }, StringSplitOptions.None), result => Array.ConvertAll(result.Split(' '), int.Parse));
		}

		Dictionary<int, HashSet<string>> ruleResults = new Dictionary<int, HashSet<string>>();
		HashSet<string> GetResult(int rule) {
			if (!ruleResults.TryGetValue(rule, out HashSet<string> result)) {
				result = new HashSet<string>();
				foreach (int[] option in rules[rule]) {
					result.UnionWith(Array.ConvertAll(option, x => x < 0 ? new HashSet<string> { new string((char)-x, 1) } : GetResult(x)).Aggregate(Combine));
				}
				ruleResults[rule] = result;
			}
			return result;
		}

		HashSet<string> Combine(HashSet<string> first, HashSet<string> second) {
			HashSet<string> cartesian = new HashSet<string>();

			foreach (string a in first) {
				foreach (string b in second) {
					cartesian.Add(a + b);
				}
			}

			return cartesian;
		}

		HashSet<string> validWords = GetResult(0);

		int count = 0;
		foreach (string line in input[1].Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
			if (counter(ruleResults, line)) {
				count++;
			}
		}

		return count;
	}
}
