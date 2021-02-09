using System;
using System.Collections.Generic;
using System.IO;

class Day1 {
	public static int Solution1() {
		HashSet<int> solutions = new HashSet<int>();

		foreach (int number in Array.ConvertAll(File.ReadAllLines("input1.txt"), int.Parse)) {
			int other = 2020 - number;

			if (solutions.Contains(number)) {
				return other * number;
			}
			solutions.Add(other);
		}

		return -1;
	}

	public static int Solution2() {
		Dictionary<int, int> solutions = new Dictionary<int, int>();
		int[] numbers = Array.ConvertAll(File.ReadAllLines("input1.txt"), int.Parse);

		foreach (int number1 in numbers) {
			if (solutions.TryGetValue(number1, out int product)) {
				return product * number1;
			}

			foreach (int number2 in numbers) {
				if (number1 == number2) {
					continue;
				}

				int sum = number1 + number2;
				if (sum < 2020) {
					solutions[2020 - sum] = number1 * number2;
				}
			}
		}

		return -1;
	}
}
