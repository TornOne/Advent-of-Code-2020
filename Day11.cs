using System.Collections.Generic;
using System.IO;

class Day11 {
	public static int Solution1() {
		string[] input = File.ReadAllLines("input11.txt");
		int width = input[0].Length + 2;
		int height = input.Length + 2;
		int[] seats1 = new int[width * height];
		for (int y = 0; y < input.Length; y++) {
			int i = (y + 1) * width + 1;
			for (int x = 0; x < input[0].Length; x++) {
				if (input[y][x] == 'L') {
					seats1[i + x] = 1;
				}
			}
		}
		int[] seats2 = (int[])seats1.Clone();

		int[] offsets = new int[8] { -width - 1, -width, -width + 1, -1, 1, width - 1, width, width + 1 };
		for (int round = 1;; round++) {
			bool change = false;
			int[] from = round % 2 == 0 ? seats1 : seats2;
			int[] to = round % 2 == 0 ? seats2 : seats1;

			for (int i = 0; i < seats1.Length; i++) {
				if (from[i] == 1) {
					int result = 2;
					foreach (int offset in offsets) {
						if (from[i + offset] == 2) {
							result = 1;
							break;
						}
					}
					change = change || result == 2;
					to[i] = result;
				} else if (from[i] == 2) {
					int count = 0;
					foreach (int offset in offsets) {
						if (from[i + offset] == 2) {
							count++;
						}
					}
					change = change || count >= 4;
					to[i] = count >= 4 ? 1 : 2;
				}
			}

			if (!change) {
				int count = 0;
				foreach (int seat in to) {
					if (seat == 2) {
						count++;
					}
				}
				return count;
			}
		}
	}

	public static int Solution2() {
		string[] map = File.ReadAllLines("input11.txt");
		int width = map[0].Length;
		int height = map.Length;

		int count = 0;
		int[,] indexMap = new int[map.Length, map[0].Length];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				indexMap[y, x] = map[y][x] == 'L' ? count++ : -1;
			}
		}
		
		int[][] adjacencyMap = new int[count][];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				if (indexMap[y, x] == -1) {
					continue;
				}
				List<int> adjacent = new List<int>(8);
				//Top left
				for (int y2 = y - 1, x2 = x - 1; y2 >= 0 && x2 >= 0; y2--, x2--) {
					int i = indexMap[y2, x2];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Top
				for (int y2 = y - 1; y2 >= 0; y2--) {
					int i = indexMap[y2, x];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Top right
				for (int y2 = y - 1, x2 = x + 1; y2 >= 0 && x2 < width; y2--, x2++) {
					int i = indexMap[y2, x2];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Left
				for (int x2 = x - 1; x2 >= 0; x2--) {
					int i = indexMap[y, x2];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Right
				for (int x2 = x + 1; x2 < width; x2++) {
					int i = indexMap[y, x2];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Bottom left
				for (int y2 = y + 1, x2 = x - 1; y2 < height && x2 >= 0; y2++, x2--) {
					int i = indexMap[y2, x2];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Bottom
				for (int y2 = y + 1; y2 < height; y2++) {
					int i = indexMap[y2, x];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}
				//Bottom right
				for (int y2 = y + 1, x2 = x + 1; y2 < height && x2 < width; y2++, x2++) {
					int i = indexMap[y2, x2];
					if (i != -1) {
						adjacent.Add(i);
						break;
					}
				}

				adjacencyMap[indexMap[y, x]] = adjacent.ToArray();
			}
		}

		bool[] seats1 = new bool[count];
		bool[] seats2 = new bool[count];

		for (int round = 1; ; round++) {
			bool change = false;
			bool[] from = round % 2 == 0 ? seats1 : seats2;
			bool[] to = round % 2 == 0 ? seats2 : seats1;

			for (int i = 0; i < seats1.Length; i++) {
				if (from[i]) {
					int seen = 0;
					foreach (int adjacent in adjacencyMap[i]) {
						if (from[adjacent]) {
							seen++;
						}
					}
					change = change || seen >= 5;
					to[i] = seen < 5;
				} else {
					bool result = true;
					foreach (int adjacent in adjacencyMap[i]) {
						if (from[adjacent]) {
							result = false;
							break;
						}
					}
					change = change || result;
					to[i] = result;
				}
			}

			if (!change) {
				int total = 0;
				foreach (bool seat in to) {
					if (seat) {
						total++;
					}
				}
				return total;
			}
		}
	}
}
