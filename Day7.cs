using System;
using System.Collections.Generic;
using System.IO;

class Day7 {
	public static int Solution1() {
		Dictionary<string, (string, int)[]> rules = GetRules();
		Dictionary<string, List<string>> reverseRules = new Dictionary<string, List<string>>();

		foreach (KeyValuePair<string, (string, int)[]> rule in rules) {
			foreach ((string content, _) in rule.Value) {
				if (reverseRules.TryGetValue(content, out List<string> containers)) {
					containers.Add(rule.Key);
				} else {
					reverseRules[content] = new List<string> { rule.Key };
				}
			}
		}

		HashSet<string> potentialContainers = new HashSet<string>();
		Stack<string> checkStack = new Stack<string>();
		checkStack.Push("shiny gold");

		while (checkStack.Count > 0) {
			string bag = checkStack.Pop();
			foreach (string container in reverseRules.TryGetValue(bag, out List<string> containers) ? containers : (IEnumerable<string>)new string[0]) {
				if (potentialContainers.Add(container)) {
					checkStack.Push(container);
				}
			}
		}

		return potentialContainers.Count;
	}

	public static int Solution2() {
		Dictionary<string, (string, int)[]> rules = GetRules();

		int answer = 0;

		Dictionary<string, int> toCheck = new Dictionary<string, int> { { "shiny gold", 1 } };
		while (toCheck.Count > 0) {
			Dictionary<string, int>.Enumerator enumerator = toCheck.GetEnumerator();
			enumerator.MoveNext();
			string container = enumerator.Current.Key;
			int containerCount = enumerator.Current.Value;
			toCheck.Remove(container);

			foreach ((string bag, int count) in rules[container]) {
				int additionalCount = count * containerCount;
				toCheck[bag] = additionalCount + (toCheck.TryGetValue(bag, out int currentCount) ? currentCount : 0);
				answer += additionalCount;
			}
		}

		return answer;
	}

	static Dictionary<string, (string, int)[]> GetRules() {
		Dictionary<string, (string, int)[]> rules = new Dictionary<string, (string, int)[]>();

		foreach (string line in File.ReadLines("input7.txt")) {
			string[] rule = line.Split(new[] { " bags contain " }, StringSplitOptions.None);

			rules[rule[0]] = rule[1] == "no other bags." ? new (string, int)[0] : Array.ConvertAll(rule[1].Split(new[] { ", " }, StringSplitOptions.None), bag => {
				string[] pieces = bag.Split(' ');
				return ($"{pieces[1]} {pieces[2]}", int.Parse(pieces[0]));
			});
		}

		return rules;
	}
}
