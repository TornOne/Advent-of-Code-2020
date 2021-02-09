using System.Collections.Generic;
using System.IO;

class Day9 {
	public static int Solution1() {
		StreamReader input = File.OpenText("input9.txt");
		Queue<int> previous = new Queue<int>(25);
		Dictionary<int, int> sums = new Dictionary<int, int>();

		for (int i = 0; i < 25; i++) {
			int next = int.Parse(input.ReadLine());

			foreach (int existing in previous) {
				int sum = existing + next;
				sums[sum] = sums.TryGetValue(sum, out int count) ? count + 1 : 1;
			}

			previous.Enqueue(next);
		}

		while (true) {
			int next = int.Parse(input.ReadLine());
			if (!sums.ContainsKey(next)) {
				input.Close();
				return next;
			}

			int first = previous.Dequeue();
			foreach (int existing in previous) {
				int sum = existing + next;
				sums[sum] = sums.TryGetValue(sum, out int count) ? count + 1 : 1;

				sum = first + existing;
				count = sums[sum];
				if (count > 1) {
					sums[sum] = count - 1;
				} else {
					sums.Remove(sum);
				}
			}
			previous.Enqueue(next);
		}
	}

	public static int Solution2() {
		const int target = 258585477;
		Queue<int> window = new Queue<int>();
		int sum = 0;

		foreach (string line in File.ReadLines("input9.txt")) {
			int n = int.Parse(line);
			window.Enqueue(n);
			sum += n;

			while (sum > target) {
				n = window.Dequeue();
				sum -= n;
			}

			if (sum == target) {
				int smallest = int.MaxValue;
				int largest = int.MinValue;
				foreach (int num in window) {
					if (num > largest) {
						largest = num;
					} else if (num < smallest) {
						smallest = num;
					}
				}
				return smallest + largest;
			}
		}

		return -1;
	}
}
