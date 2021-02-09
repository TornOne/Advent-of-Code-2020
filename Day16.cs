using System;
using System.Collections.Generic;
using System.IO;

class Day16 {
	public static int Solution1() {
		string[] input = File.ReadAllText("input16.txt").Split(new[] { "\n\n" }, StringSplitOptions.None);

		bool[] rules = new bool[1000];
		foreach (string[] ranges in Array.ConvertAll(input[0].Split('\n'), rule => rule.Split(new[] { ": " }, StringSplitOptions.None)[1].Split(new[] { " or " }, StringSplitOptions.None))) {
			foreach (string[] range in Array.ConvertAll(ranges, range => range.Split('-'))) {
				int start = int.Parse(range[0]);
				int end = int.Parse(range[1]);
				for (int i = start; i <= end; i++) {
					rules[i] = true;
				}
			}
		}

		int sum = 0;
		foreach (int rule in Array.ConvertAll(input[2].Split(new[] { '\n' }, 2)[1].Split(new[] { '\n', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse)) {
			if (!rules[rule]) {
				sum += rule;
			}
		}

		return sum;
	}

	public static int Solution2() {
		string[] input = File.ReadAllText("input16.txt").Split(new[] { "\n\n" }, StringSplitOptions.None);

		Dictionary<string, bool[]> rules = new Dictionary<string, bool[]>();
		foreach (string[] rule in Array.ConvertAll(input[0].Split('\n'), rule => rule.Split(new[] { ": " }, StringSplitOptions.None))) {
			bool[] validValues = new bool[1000];
			rules[rule[0]] = validValues;

			foreach (string[] range in Array.ConvertAll(rule[1].Split(new[] { " or " }, StringSplitOptions.None), range => range.Split('-'))) {
				int start = int.Parse(range[0]);
				int end = int.Parse(range[1]);
				for (int i = start; i <= end; i++) {
					validValues[i] = true;
				}
			}
		}

		int[] myTicket = Array.ConvertAll(input[1].Split('\n')[1].Split(','), int.Parse);
		List<int[]> validTickets = new List<int[]> { myTicket };
		foreach (int[] ticket in Array.ConvertAll(input[2].Split(new[] { '\n' }, 2)[1].Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries), line => Array.ConvertAll(line.Split(','), int.Parse))) {
			if (Array.TrueForAll(ticket, value => {
				foreach (bool[] validValues in rules.Values) {
					if (validValues[value]) {
						return true;
					}
				}
				return false;
			})) {
				validTickets.Add(ticket);
			}
		}

		HashSet<string>[] possibilities = new HashSet<string>[validTickets[0].Length];
		for (int i = 0; i < possibilities.Length; i++) {
			possibilities[i] = new HashSet<string>(rules.Keys);
		}
		foreach (int[] ticket in validTickets) {
			for (int i = 0; i < ticket.Length; i++) {
				foreach (KeyValuePair<string, bool[]> rule in rules) {
					if (!rule.Value[ticket[i]]) {
						possibilities[i].Remove(rule.Key);
					}
				}
			}
		}

		Dictionary<string, int> fieldNames = new Dictionary<string, int>(20);
		int[] order = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
		Array.Sort(possibilities, order, Comparer<HashSet<string>>.Create((a, b) => a.Count - b.Count));
		for (int i = 0; i < possibilities.Length; i++) {
			HashSet<string>.Enumerator enumerator = possibilities[i].GetEnumerator();
			enumerator.MoveNext();
			string name = enumerator.Current;
			fieldNames[name] = order[i];
			for (int j = i + 1; j < possibilities.Length; j++) {
				possibilities[j].Remove(name);
			}
		}

		long product = 1;
		foreach (string field in new[] { "departure location", "departure station", "departure platform", "departure track", "departure date", "departure time" }) {
			product *= myTicket[fieldNames[field]];
		}

		Console.WriteLine(product);
		return 0;
	}
}
