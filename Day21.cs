using System;
using System.Collections.Generic;
using System.IO;

class Day21 {
	public static int Solution1() {
		Dictionary<string, int> allIngredients = new Dictionary<string, int>();
		Dictionary<string, HashSet<string>> allergenMap = new Dictionary<string, HashSet<string>>();

		foreach (string line in File.ReadLines("input21.txt")) {
			int allergenIndex = line.IndexOf('(');
			string[] allergens = line.Substring(allergenIndex + 10, line.Length - allergenIndex - 11).Split(new[] { ", " }, StringSplitOptions.None);
			string[] ingredients = line.Substring(0, allergenIndex - 1).Split(' ');

			foreach (string ingredient in ingredients) {
				allIngredients[ingredient] = allIngredients.TryGetValue(ingredient, out int count) ? count + 1 : 1;
			}

			foreach (string allergen in allergens) {
				if (allergenMap.TryGetValue(allergen, out HashSet<string> possibilities)) {
					possibilities.IntersectWith(ingredients);
				} else {
					allergenMap[allergen] = new HashSet<string>(ingredients);
				}
			}
		}

		foreach (HashSet<string> possibilities in allergenMap.Values) {
			foreach (string possibility in possibilities) {
				allIngredients.Remove(possibility);
			}
		}

		int total = 0;
		foreach (int amount in allIngredients.Values) {
			total += amount;
		}

		return total;
	}

	public static int Solution2() {
		Dictionary<string, HashSet<string>> allergenMap = new Dictionary<string, HashSet<string>>();

		foreach (string line in File.ReadLines("input21.txt")) {
			int allergenIndex = line.IndexOf('(');
			string[] allergens = line.Substring(allergenIndex + 10, line.Length - allergenIndex - 11).Split(new[] { ", " }, StringSplitOptions.None);
			string[] ingredients = line.Substring(0, allergenIndex - 1).Split(' ');

			foreach (string allergen in allergens) {
				if (allergenMap.TryGetValue(allergen, out HashSet<string> possibilities)) {
					possibilities.IntersectWith(ingredients);
				} else {
					allergenMap[allergen] = new HashSet<string>(ingredients);
				}
			}
		}

		while (true) {
			bool changed = false;
			foreach (HashSet<string> possibilities in allergenMap.Values) {
				if (possibilities.Count == 1) {
					foreach (HashSet<string> other in allergenMap.Values) {
						if (other.Count != 1) {
							other.ExceptWith(possibilities);
							changed = true;
						}
					}
				}
			}

			if (!changed) {
				break;
			}
		}

		string[] finalAllergens = new string[allergenMap.Count];
		allergenMap.Keys.CopyTo(finalAllergens, 0);
		string[] finalIngredients = new string[allergenMap.Count];
		for (int i = 0; i < finalAllergens.Length; i++) {
			HashSet<string>.Enumerator ingredient = allergenMap[finalAllergens[i]].GetEnumerator();
			ingredient.MoveNext();
			finalIngredients[i] = ingredient.Current;
		}

		Array.Sort(finalAllergens, finalIngredients);
		Console.WriteLine(string.Join(",", finalIngredients));

		return 0;
	}
}
