using System.Collections.Generic;
using System.IO;

class Day3 {
	public static int Solution1() => Solution(File.ReadAllLines("input3.txt"), 3);

	public static int Solution2() {
		string[] map = File.ReadAllLines("input3.txt");

		int answer = Solution(map, 1, 1);
		foreach (int increment in new[] { 1, 3, 5, 7 }) {
			answer *= Solution(map, increment);
		}

		return answer;
	}

	static int Solution(IEnumerable<string> map, int increment, int alternate = 0) {
		int pos = 0;
		int answer = 0;

		foreach (string line in map) {
			if (alternate > 0) {
				if (alternate++ % 2 == 0) {
					continue;
				}
			}

			if (line[pos] == '#') {
				answer++;
			}
			pos = (pos + increment) % line.Length;
		}

		return answer;
	}
}
