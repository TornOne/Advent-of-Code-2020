using System.Collections.Generic;
using System.IO;

class Day24 {
	public static int Solution1() => GetTiles().Count;

	public static int Solution2() {
		IEnumerable<(int, int)> AdjacentTiles(int x, int y) {
			yield return (x - 1, y);
			yield return (x + 1, y);
			yield return (x, y - 1);
			yield return (x + 1, y - 1);
			yield return (x - 1, y + 1);
			yield return (x, y + 1);
		}

		HashSet<(int, int)> tiles = GetTiles();

		for (int i = 0; i < 100; i++) {
			HashSet<(int, int)> tilesToCheck = new HashSet<(int, int)>();
			foreach ((int x, int y) in tiles) {
				foreach ((int, int) tile in AdjacentTiles(x, y)) {
					tilesToCheck.Add(tile);
				}
			}

			HashSet<(int, int)> newTiles = new HashSet<(int, int)>();
			foreach ((int x, int y) tile in tilesToCheck) {
				int adjacentBlacks = 0;
				foreach ((int, int) adjacent in AdjacentTiles(tile.x, tile.y)) {
					if (tiles.Contains(adjacent)) {
						adjacentBlacks++;
					}
				}
				if (adjacentBlacks == 2 || tiles.Contains(tile) && adjacentBlacks == 1) {
					newTiles.Add(tile);
				}
			}
			tiles = newTiles;
		}

		return tiles.Count;
	}

	static HashSet<(int, int)> GetTiles() {
		Dictionary<(int, int), bool> tiles = new Dictionary<(int, int), bool>();

		foreach (string line in File.ReadLines("input24.txt")) {
			int x = 0;
			int y = 0;
			for (int i = 0; i < line.Length; i++) {
				if (line[i] == 'e') {
					x++;
				} else if (line[i] == 'w') {
					x--;
				} else if (line[i] == 'n') {
					y++;
					if (line[++i] == 'w') {
						x--;
					}
				} else {
					y--;
					if (line[++i] == 'e') {
						x++;
					}
				}
			}

			tiles[(x, y)] = !(tiles.TryGetValue((x, y), out bool isBlack) && isBlack);
		}

		HashSet<(int, int)> blackTiles = new HashSet<(int, int)>();
		foreach (KeyValuePair<(int, int), bool> tile in tiles) {
			if (tile.Value) {
				blackTiles.Add(tile.Key);
			}
		}

		return blackTiles;
	}
}
