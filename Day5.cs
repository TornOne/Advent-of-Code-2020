using System.Collections.Generic;
using System.IO;

class Day5 {
	public static int Solution1() {
		int max = 0;

		foreach (int id in GetIDs()) {
			if (id > max) {
				max = id;
			}
		}

		return max;
	}

	public static int Solution2() {
		int min = int.MaxValue;
		int max = 0;
		int sum = 0;

		foreach (int id in GetIDs()) {
			if (id > max) {
				max = id;
			} else if (id < min) {
				min = id;
			}
			sum += id;
		}

		return (max + min) * (max - min + 1) / 2 - sum;
	}

	static IEnumerable<int> GetIDs() {
		foreach (string seat in File.ReadLines("input5.txt")) {
			int id = 0;

			for (int i = 0; i < 7; i++) {
				if (seat[i] == 'B') {
					id |= 1 << (9 - i);
				}
			}

			for (int i = 7; i < 10; i++) {
				if (seat[i] == 'R') {
					id |= 1 << (9 - i);
				}
			}

			yield return id;
		}
	}
}
