using System;
using System.IO;

class Day12 {
	public static int Solution1() {
		int x = 0;
		int y = 0;
		int rot = 0;

		foreach (string line in File.ReadLines("input12.txt")) {
			char action = line[0];
			int amount = int.Parse(line.Substring(1));

			if (action == 'N') {
				y += amount;
			} else if (action == 'S') {
				y -= amount;
			} else if (action == 'E') {
				x += amount;
			} else if (action == 'W') {
				x -= amount;
			} else if (action == 'L') {
				rot = (rot - amount / 90 + 4) % 4;
			} else if (action == 'R') {
				rot = (rot + amount / 90) % 4;
			} else if (action == 'F') {
				if (rot == 0) {
					x += amount;
				} else if (rot == 1) {
					y -= amount;
				} else if (rot == 2) {
					x -= amount;
				} else {
					y += amount;
				}
			}
		}

		return Math.Abs(x) + Math.Abs(y);
	}

	public static int Solution2() {
		int x = 10;
		int y = 1;
		int xPos = 0;
		int yPos = 0;

		foreach (string line in File.ReadLines("input12.txt")) {
			char action = line[0];
			int amount = int.Parse(line.Substring(1));

			if (action == 'N') {
				y += amount;
			} else if (action == 'S') {
				y -= amount;
			} else if (action == 'E') {
				x += amount;
			} else if (action == 'W') {
				x -= amount;
			} else if (action == 'L') {
				action = 'R';
				amount = 360 - amount;
			} if (action == 'R') {
				if (amount == 90) {
					int t = x;
					x = y;
					y = -t;
				} else if (amount == 180) {
					x = -x;
					y = -y;
				} else if (amount == 270) {
					int t = x;
					x = -y;
					y = t;
				}
			} else if (action == 'F') {
				xPos += x * amount;
				yPos += y * amount;
			}
		}

		return Math.Abs(xPos) + Math.Abs(yPos);
	}
}
