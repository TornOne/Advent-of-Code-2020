using System.Collections.Generic;
using System.IO;

class Day2 {
	public static int Solution1() {
		int answer = 0;
		foreach ((int lowBound, int highBound, char c, string pass) in ReadInput()) {
			int count = 0;
			foreach (char letter in pass) {
				if (letter == c) {
					count++;
				}
			}

			if (lowBound <= count && highBound >= count) {
				answer++;
			}
		}

		return answer;
	}

	public static int Solution2() {
		int answer = 0;
		foreach ((int index1, int index2, char c, string pass) in ReadInput()) {
			if (pass[index1 - 1] == c != (pass[index2 - 1] == c)) {
				answer++;
			}
		}

		return answer;
	}

	static IEnumerable<(int, int, char, string)> ReadInput() {
		foreach (string line in File.ReadLines("input2.txt")) {
			string[] parts = line.Split(' ');
			string[] bounds = parts[0].Split('-');
			char c = parts[1][0];

			yield return (int.Parse(bounds[0]), int.Parse(bounds[1]), c, parts[2]);
		}
	}
}
