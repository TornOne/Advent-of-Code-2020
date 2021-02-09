using System;
using System.Collections.Generic;
using System.IO;

class Day4 {
	static readonly Dictionary<string, Func<string, bool>> requiredFields = new Dictionary<string, Func<string, bool>> {
		{ "byr", value => int.TryParse(value, out int year) && year >= 1920 && year <= 2002 },
		{ "iyr", value => int.TryParse(value, out int year) && year >= 2010 && year <= 2020 },
		{ "eyr", value => int.TryParse(value, out int year) && year >= 2020 && year <= 2030 },
		{ "hgt", height => height.EndsWith("cm") && int.TryParse(height.Substring(0, height.Length - 2), out int value) && value >= 150 && value <= 193 || height.EndsWith("in") && int.TryParse(height.Substring(0, height.Length - 2), out value) && value >= 59 && value <= 76 },
		{ "hcl", color => color.Length == 7 && color.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f') == "#" },
		{ "ecl", color => color == "amb" || color == "blu" || color == "brn" || color == "gry" || color == "grn" || color == "hzl" || color == "oth" },
		{ "pid", id => id.Length == 9 && int.TryParse(id, out _) }
	};

	public static int Solution1() => Solution((passport, fieldName) => passport.ContainsKey(fieldName));

	public static int Solution2() => Solution((passport, fieldName) => passport.ContainsKey(fieldName) && requiredFields[fieldName](passport[fieldName]));

	static int Solution(Func<Dictionary<string, string>, string, bool> validate) {
		int answer = 0;

		foreach (string passport in File.ReadAllText("input4.txt").Split(new[] { "\n\n" }, StringSplitOptions.None)) {
			Dictionary<string, string> fields = new Dictionary<string, string>();
			foreach (string field in passport.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
				string[] keyValue = field.Split(':');
				fields[keyValue[0]] = keyValue[1];
			}

			bool isValid = true;
			foreach (string fieldName in requiredFields.Keys) {
				if (!validate(fields, fieldName)) {
					isValid = false;
					break;
				}
			}

			if (isValid) {
				answer++;
			}
		}

		return answer;
	}
}
