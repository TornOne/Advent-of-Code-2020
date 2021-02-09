using System;
using System.Collections.Generic;
using System.IO;

class Day14 {
	public static int Solution1() {
		Dictionary<int, long> memory = new Dictionary<int, long>();
		string mask = "";

		foreach (string line in File.ReadLines("input14.txt")) {
			string[] instruction = line.Split(new[] { " = " }, StringSplitOptions.None);
			if (instruction[0] == "mask") {
				mask = instruction[1];
			} else {
				long value = long.Parse(instruction[1]);
				for (int i = 0; i < 36; i++) {
					char bit = mask[i];
					if (bit == '0') {
						value &= 262144L * 262144L - 1 - (1L << (35 - i));
					} else if (bit == '1') {
						value |= 1L << (35 - i);
					}
				}
				memory[int.Parse(instruction[0].Substring(4, instruction[0].Length - 5))] = value;
			}
		}

		long sum = 0;
		foreach (long value in memory.Values) {
			sum += value;
		}

		Console.WriteLine(sum);
		return 0;
	}

	public static int Solution2() {
		Dictionary<long, int> memory = new Dictionary<long, int>();
		string mask = "";

		foreach (string line in File.ReadLines("input14.txt")) {
			string[] instruction = line.Split(new[] { " = " }, StringSplitOptions.None);
			if (instruction[0] == "mask") {
				mask = instruction[1];
			} else {
				int value = int.Parse(instruction[1]);
				int baseAddress = int.Parse(instruction[0].Substring(4, instruction[0].Length - 5));

				void WriteToMemory(char[] address, int i) {
					if (i == 36) {
						memory[Convert.ToInt64(new string(address), 2)] = value;
						return;
					}

					if (address[i] == 'X') {
						address[i] = '0';
						WriteToMemory((char[])address.Clone(), i + 1);
						address[i] = '1';
					}
					WriteToMemory(address, i + 1);
				}

				char[] masterAddress = mask.ToCharArray();
				for (int i = 35; i >= 20; i--) {
					if (masterAddress[i] == '0') {
						masterAddress[i] = (char)(((baseAddress >> (35 - i)) & 1) + '0');
					}
				}

				WriteToMemory(masterAddress, 0);
			}
		}

		long sum = 0;
		foreach (int value in memory.Values) {
			sum += value;
		}

		Console.WriteLine(sum);
		return 0;
	}
}
