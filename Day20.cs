using System;
using System.Collections.Generic;
using System.IO;

class Day20 {
	public static int Solution1() {
		(int id, int orientation)[,] tiles = FitTiles();
		Console.WriteLine((long)tiles[0, 0].id * tiles[0, 11].id * tiles[11, 0].id * tiles[11, 11].id);
		return 0;
	}

	public static int Solution2() {
		bool[,] Rotate(bool[,] image, int amount) {
			int height = image.GetLength(0);
			int width = image.GetLength(1);
			bool[,] newImage = new bool[amount == 2 ? height : width, amount == 2 ? width : height];

			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					if (amount == 1) {
						newImage[x, height - y - 1] = image[y, x];
					} else if (amount == 2) {
						newImage[height - y - 1, width - x - 1] = image[y, x];
					} else if (amount == 3) {
						newImage[width - x - 1, y] = image[y, x];
					} else {
						throw new ArgumentException("Wrong rotation", nameof(amount));
					}
				}
			}

			return newImage;
		}

		bool[,] Mirror(bool[,] image) {
			int height = image.GetLength(0);
			int width = image.GetLength(1);
			bool[,] newImage = new bool[height, width];

			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					newImage[y, width - x - 1] = image[y, x];
				}
			}

			return newImage;
		}

		(int id, int orientation)[,] tileIDs = FitTiles();

		Dictionary<int, bool[,]> tiles = new Dictionary<int, bool[,]>();
		foreach (string[] lines in Array.ConvertAll(File.ReadAllText("input20.txt").Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries), tile => tile.Split('\n'))) {
			bool[,] tile = new bool[8, 8];

			for (int y = 0; y < 8; y++) {
				for (int x = 0; x < 8; x++) {
					tile[y, x] = lines[y + 2][x + 1] == '#';
				}
			}

			tiles[int.Parse(lines[0].Substring(5, 4))] = tile;
		}

		bool[,] photo = new bool[8 * 12, 8 * 12];
		for (int tileY = 0; tileY < 12; tileY++) {
			for (int tileX = 0; tileX < 12; tileX++) {
				(int id, int orientation) = tileIDs[tileY, tileX];
				bool[,] tile = tiles[id];
				if (orientation > 3) {
					tile = Mirror(tile);
				}
				if (orientation % 4 != 0) {
					tile = Rotate(tile, orientation % 4);
				}

				for (int y = 0; y < 8; y++) {
					for (int x = 0; x < 8; x++) {
						photo[tileY * 8 + y, tileX * 8 + x] = tile[y, x];
					}
				}
			}
		}

		int FindSubImage(bool[,] subImage) {
			bool[,] newImage = (bool[,])photo.Clone();
			int height = subImage.GetLength(0);
			int width = subImage.GetLength(1);

			for (int y = 0; y < 8 * 12 - height; y++) {
				for (int x = 0; x < 8 * 12 - width; x++) {
					bool isMonster = true;
					for (int yOffset = 0; yOffset < height; yOffset++) {
						for (int xOffset = 0; xOffset < width; xOffset++) {
							if (subImage[yOffset, xOffset] && !photo[y + yOffset, x + xOffset]) {
								isMonster = false;
								break;
							}
						}
						if (!isMonster) {
							break;
						}
					}

					if (isMonster) {
						for (int yOffset = 0; yOffset < height; yOffset++) {
							for (int xOffset = 0; xOffset < width; xOffset++) {
								if (subImage[yOffset, xOffset]) {
									newImage[y + yOffset, x + xOffset] = false;
								}
							}
						}
					}
				}
			}

			int count = 0;
			foreach (bool square in newImage) {
				if (square) {
					count++;
				}
			}

			return count;
		}

		bool[,] monster = new bool[3, 20] {
			{ false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false },
			{ true, false, false, false, false, true, true, false, false, false, false, true, true, false, false, false, false, true, true, true },
			{ false, true, false, false, true, false, false, true, false, false, true, false, false, true, false, false, true, false, false, false }
		};

		return Math.Min(FindSubImage(monster),
			Math.Min(FindSubImage(Rotate(monster, 1)),
			Math.Min(FindSubImage(Rotate(monster, 2)),
			Math.Min(FindSubImage(Rotate(monster, 3)),
			Math.Min(FindSubImage(Mirror(monster)),
			Math.Min(FindSubImage(Rotate(Mirror(monster), 1)),
			Math.Min(FindSubImage(Rotate(Mirror(monster), 2)),
			FindSubImage(Rotate(Mirror(monster), 3)))))))));
	}

	static (int, int)[,] FitTiles() {
		int Mirror(int x) =>
			  ((x & 1) << 9)
			| ((x & 2) << 7)
			| ((x & 4) << 5)
			| ((x & 8) << 3)
			| ((x & 16) << 1)
			| ((x & 32) >> 1)
			| ((x & 64) >> 3)
			| ((x & 128) >> 5)
			| ((x & 256) >> 7)
			| ((x & 512) >> 9);

		List<KeyValuePair<int, int[][]>> tiles = new List<KeyValuePair<int, int[][]>>();
		foreach (string[] lines in Array.ConvertAll(File.ReadAllText("input20.txt").Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries), tile => tile.Split('\n'))) {
			int[][] orientations = new int[8][];

			int[] sides = new int[4];
			for (int i = 0; i < 10; i++) {
				if (lines[1][i] == '#') {
					sides[0] |= 1 << i;
				}
				if (lines[i + 1][9] == '#') {
					sides[1] |= 1 << i;
				}
				if (lines[10][i] == '#') {
					sides[2] |= 512 >> i;
				}
				if (lines[i + 1][0] == '#') {
					sides[3] |= 512 >> i;
				}
			}
			orientations[0] = sides;
			orientations[1] = new[] { sides[3], sides[0], sides[1], sides[2] };
			orientations[2] = new[] { sides[2], sides[3], sides[0], sides[1] };
			orientations[3] = new[] { sides[1], sides[2], sides[3], sides[0] };

			sides = new[] { Mirror(sides[0]), Mirror(sides[3]), Mirror(sides[2]), Mirror(sides[1]) };
			orientations[4] = sides;
			orientations[5] = new[] { sides[3], sides[0], sides[1], sides[2] };
			orientations[6] = new[] { sides[2], sides[3], sides[0], sides[1] };
			orientations[7] = new[] { sides[1], sides[2], sides[3], sides[0] };

			int[] sides2 = Array.ConvertAll(sides, Mirror);

			tiles.Add(new KeyValuePair<int, int[][]>(int.Parse(lines[0].Substring(5, 4)), orientations));
		}

		HashSet<int> placedTiles = new HashSet<int>();
		int[,][] image = new int[12, 12][];
		(int, int)[,] imageTiles = new (int, int)[12, 12];
		bool PlaceTile(int row, int col) {
			if (row == 12) {
				return true;
			}

			foreach (KeyValuePair<int, int[][]> tile in tiles) {
				if (placedTiles.Contains(tile.Key)) {
					continue;
				}

				for (int i = 0; i < 8; i++) {
					int[] orientation = tile.Value[i];
					if (row > 0 && image[row - 1, col][2] != Mirror(orientation[0]) || col > 0 && image[row, col - 1][1] != Mirror(orientation[3])) {
						continue;
					}

					placedTiles.Add(tile.Key);
					image[row, col] = orientation;
					imageTiles[row, col] = (tile.Key, i);
					if (PlaceTile(row + (col == 11 ? 1 : 0), (col + 1) % 12)) {
						return true;
					}
					placedTiles.Remove(tile.Key);
				}
			}

			return false;
		}

		PlaceTile(0, 0);
		return imageTiles;
	}
}
