using System.Collections.Generic;

class Day15 {
	static readonly int[] input = new[] { 20, 9, 11, 0, 1, 2 };

	public static int Solution1() => Solution(2020);

	public static int Solution2() => Solution(30000000);

	static int Solution(int n) {
		Dictionary<int, (int, int)> numbers = new Dictionary<int, (int, int)>();
		for (int turn = 0; turn < input.Length;) {
			numbers[input[turn++]] = (turn, turn);
		}
		int last = input[input.Length - 1];
		(int penultimate, int ultimate) = numbers[last];

		for (int turn = input.Length + 1; turn <= n; turn++) {
			last = ultimate - penultimate;
			penultimate = numbers.TryGetValue(last, out (int _, int value) ult) ? ult.value : turn;
			ultimate = turn;
			numbers[last] = (penultimate, ultimate);
		}

		return last;
	}
}
