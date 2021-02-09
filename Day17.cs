using System;
using System.Collections.Generic;
using System.IO;

class Day17 {
	//Speedup: Dictionary<Vector3, int> for getting new active squares - each square increments their neighbors (and decrements themselves)

	public static int Solution1() {
		HashSet<Vector3> active = new HashSet<Vector3>();
		char[][] input = Array.ConvertAll(File.ReadAllLines("input17.txt"), line => line.ToCharArray());
		for (int x = 0; x < input[0].Length; x++) {
			for (int y = 0; y < input.Length; y++) {
				if (input[y][x] == '#') {
					active.Add(new Vector3(x, y, 0));
				}
			}
		}

		for (int i = 0; i < 6; i++) {
			HashSet<Vector3> toCheck = new HashSet<Vector3>();
			foreach (Vector3 coord in active) {
				foreach (Vector3 neighbor in coord.GetAllNeighbors()) {
					toCheck.Add(neighbor);
				}
			}

			HashSet<Vector3> newActive = new HashSet<Vector3>();
			foreach (Vector3 coord in toCheck) {
				int count = 0;

				foreach (Vector3 neighbor in coord.GetAllNeighbors()) {
					if (active.Contains(neighbor)) {
						count++;
						if (count > 4) {
							break;
						}
					}
				}

				if (count == 3 || count == 4 && active.Contains(coord)) {
					newActive.Add(coord);
				}
			}

			active = newActive;
		}

		return active.Count;
	}

	public static int Solution2() {
		HashSet<Vector4> active = new HashSet<Vector4>();
		char[][] input = Array.ConvertAll(File.ReadAllLines("input17.txt"), line => line.ToCharArray());
		for (int x = 0; x < input[0].Length; x++) {
			for (int y = 0; y < input.Length; y++) {
				if (input[y][x] == '#') {
					active.Add(new Vector4(x, y, 0, 0));
				}
			}
		}

		for (int i = 0; i < 6; i++) {
			HashSet<Vector4> toCheck = new HashSet<Vector4>();
			foreach (Vector4 coord in active) {
				foreach (Vector4 neighbor in coord.GetAllNeighbors()) {
					toCheck.Add(neighbor);
				}
			}

			HashSet<Vector4> newActive = new HashSet<Vector4>();
			foreach (Vector4 coord in toCheck) {
				int count = 0;

				foreach (Vector4 neighbor in coord.GetAllNeighbors()) {
					if (active.Contains(neighbor)) {
						count++;
						if (count > 4) {
							break;
						}
					}
				}

				if (count == 3 || count == 4 && active.Contains(coord)) {
					newActive.Add(coord);
				}
			}

			active = newActive;
		}

		return active.Count;
	}

	struct Vector3 {
		public readonly int x, y, z;

		public Vector3(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public IEnumerable<Vector3> GetAllNeighbors() {
			int xTo = x + 1;
			int yTo = y + 1;
			int zTo = z + 1;
			for (int x = this.x - 1; x <= xTo; x++) {
				for (int y = this.y - 1; y <= yTo; y++) {
					for (int z = this.z - 1; z <= zTo; z++) {
						yield return new Vector3(x, y, z);
					}
				}
			}
		}

		public override int GetHashCode() => (x << 20) ^ (y << 10) ^ z;
	}

	struct Vector4 {
		public readonly int x, y, z, w;

		public Vector4(int x, int y, int z, int w) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public IEnumerable<Vector4> GetAllNeighbors() {
			int xTo = x + 1;
			int yTo = y + 1;
			int zTo = z + 1;
			int wTo = w + 1;
			for (int x = this.x - 1; x <= xTo; x++) {
				for (int y = this.y - 1; y <= yTo; y++) {
					for (int z = this.z - 1; z <= zTo; z++) {
						for (int w = this.w - 1; w <= wTo; w++) {
							yield return new Vector4(x, y, z, w);
						}
					}
				}
			}
		}

		public override int GetHashCode() => (x << 24) ^ (y << 16) ^ (z << 8) ^ w;
	}
}
