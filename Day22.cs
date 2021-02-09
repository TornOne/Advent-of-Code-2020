using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day22 {
	public static int Solution1() {
		GetDecks(out Queue<int> deck1, out Queue<int> deck2);

		while (deck1.Count > 0 && deck2.Count > 0) {
			int card1 = deck1.Dequeue();
			int card2 = deck2.Dequeue();

			if (card1 > card2) {
				deck1.Enqueue(card1);
				deck1.Enqueue(card2);
			} else {
				deck2.Enqueue(card2);
				deck2.Enqueue(card1);
			}
		}

		int sum = 0;
		Queue<int> winningDeck = deck1.Count > 0 ? deck1 : deck2;
		while (winningDeck.Count > 0) {
			sum += winningDeck.Count * winningDeck.Dequeue();
		}

		return sum;
	}

	public static int Solution2() {
		GetDecks(out Queue<int> deck1, out Queue<int> deck2);
		PlayGame(deck1, deck2);

		int sum = 0;
		Queue<int> winningDeck = deck1.Count > 0 ? deck1 : deck2;
		while (winningDeck.Count > 0) {
			sum += winningDeck.Count * winningDeck.Dequeue();
		}

		return sum;
	}

	static void GetDecks(out Queue<int> deck1, out Queue<int> deck2) {
		string[][] playerInputs = Array.ConvertAll(File.ReadAllText("input22.txt").Split(new[] { "\n\n" }, StringSplitOptions.None), input => input.Substring(10).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
		deck1 = new Queue<int>(Array.ConvertAll(playerInputs[0], int.Parse));
		deck2 = new Queue<int>(Array.ConvertAll(playerInputs[1], int.Parse));
	}

	static bool PlayGame(Queue<int> deck1, Queue<int> deck2) {
		HashSet<GameState> stateMemory = new HashSet<GameState>();

		while (deck1.Count > 0 && deck2.Count > 0) {
			if (!stateMemory.Add(new GameState(deck1.ToArray(), deck2.ToArray()))) {
				return true;
			}

			int card1 = deck1.Dequeue();
			int card2 = deck2.Dequeue();

			bool winnerIs1 = deck1.Count >= card1 && deck2.Count >= card2 ? PlayGame(new Queue<int>(deck1.Take(card1)), new Queue<int>(deck2.Take(card2))) : card1 > card2;

			if (winnerIs1) {
				deck1.Enqueue(card1);
				deck1.Enqueue(card2);
			} else {
				deck2.Enqueue(card2);
				deck2.Enqueue(card1);
			}
		}

		return deck1.Count > 0;
	}

	class GameState : IEquatable<GameState> {
		readonly int[] deck1, deck2;

		public GameState(int[] deck1, int[] deck2) {
			this.deck1 = deck1;
			this.deck2 = deck2;
		}

		public override bool Equals(object other) => other is GameState state && Equals(state);
		public bool Equals(GameState other) => deck1.Length == other.deck1.Length && deck1.SequenceEqual(other.deck1) && deck2.SequenceEqual(other.deck2);

		public override int GetHashCode() {
			int hash = deck1.Length;
			for (int i = 0; i < deck1.Length; i++) {
				hash ^= deck1[i] << (i % 30);
			}
			return hash;
		}
	}
}
