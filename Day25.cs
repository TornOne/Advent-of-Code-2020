using System;
using System.IO;

class Day25 {
	public static int Solution1() {
		string[] keys = File.ReadAllLines("input25.txt");
		int key1 = int.Parse(keys[0]);
		int key2 = int.Parse(keys[1]);
		int loopSize;

		int value = 1;
		int loops = 0;
		while (true) {
			loops++;
			value = value * 7 % 20201227;
			if (value == key2) {
				loopSize = loops;
				break;
			}
		}

		long keyEnc = 1;
		for (int i = 0; i < loopSize; i++) {
			keyEnc = keyEnc * key1 % 20201227;
		}

		Console.WriteLine(keyEnc);
		return 0;
	}

	public static int Solution2() => -1;
}
