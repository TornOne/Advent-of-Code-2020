using System;
using System.IO;

class Day10 {
	public static int Solution1() {
		int[] adapters = Array.ConvertAll(File.ReadAllLines("input10.txt"), int.Parse);
		Array.Sort(adapters);

		int diff1 = adapters[0] == 1 ? 1 : 0;
		int diff3 = adapters[0] == 3 ? 2 : 1;
		for (int i = adapters.Length - 1; i > 0;) {
			int diff = adapters[i--] - adapters[i];
			if (diff == 1) {
				diff1++;
			} else if (diff == 3) {
				diff3++;
			}
		}

		return diff1 * diff3;
	}

	public static int Solution2() {
		int[] adapters = Array.ConvertAll(File.ReadAllLines("input10.txt"), int.Parse);
		Array.Sort(adapters);
		long[] arrangements = new long[adapters.Length];

		//Count the ways from each adapter to your phone
		arrangements[arrangements.Length - 1] = 1;
		arrangements[arrangements.Length - 2] = 1;
		arrangements[arrangements.Length - 3] = 2;
		for (int i = adapters.Length - 4; i >= 0; i--) {
			arrangements[i] = arrangements[i + 1];

			if (adapters[i + 2] - adapters[i] <= 3) {
				arrangements[i] += arrangements[i + 2];
				if (adapters[i + 3] - adapters[i] <= 3) {
					arrangements[i] += arrangements[i + 3];
				}
			}
		}

		Console.WriteLine(arrangements[0] + arrangements[1] + arrangements[2]);
		return 0;
	}
}
