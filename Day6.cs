using System;
using System.Collections.Generic;
using System.IO;

class Day6 {
	public static int Solution1() => Solution(new HashSet<char>(), (answers, person) => answers.UnionWith(person));

	public static int Solution2() => Solution(new HashSet<char>("abcdefghijklmnopqrstuvwxyz"), (answers, person) => answers.IntersectWith(person));

	static int Solution(HashSet<char> startSet, Action<HashSet<char>, string> setOp) {
		int answer = 0;

		foreach (string group in File.ReadAllText("input6.txt").Split(new[] { "\n\n" }, StringSplitOptions.None)) {
			HashSet<char> answers = new HashSet<char>(startSet);
			foreach (string person in group.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
				setOp(answers, person);
			}
			answer += answers.Count;
		}

		return answer;
	}
}
