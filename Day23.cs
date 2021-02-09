using System;

class Day23 {
	const string input = "198753462";

	public static int Solution1() {
		int[] cups = Array.ConvertAll(input.ToCharArray(), c => c - '0');
		int[] carry = new int[4];

		for (int i = 0; i < 100; i++) {
			Array.Copy(cups, carry, 4);
			int search = cups[0];
			do {
				search -= 1;
				if (search == 0) {
					search = 9;
				}
			} while (Array.Exists(carry, c => c == search));
			int dst = Array.IndexOf(cups, search);
			int count = dst - 3;
			Array.Copy(cups, 4, cups, 0, count);
			Array.Copy(carry, 1, cups, count, 3);
			Array.Copy(cups, dst + 1, cups, dst, 8 - dst);
			cups[8] = carry[0];
		}

		int answer = 0;
		int start = Array.IndexOf(cups, 1);
		for (int i = 1; i <= 8; i++) {
			answer = answer * 10 + cups[(start + i) % 9];
		}
		return answer;
	}

	public static int Solution2() {
		Node<int>[] nodes = new Node<int>[1000001];
		for (int i = 1; i <= 1000000; i++) {
			nodes[i] = new Node<int>(i);
		}

		Node<int> first = nodes[input[0] - '0'];
		Node<int> last = first;
		for (int i = 1; i < 9; i++) {
			last.next = nodes[input[i] - '0'];
			last = last.next;
		}
		for (int i = 10; i <= 1000000; i++) {
			last.next = nodes[i];
			last = last.next;
		}
		last.next = first;

		for (int i = 0; i < 10000000; i++) {
			Node<int> carryStart = first.next;
			Node<int> carryEnd = carryStart.next.next;

			int dstVal = first.value;
			do {
				dstVal -= 1;
				if (dstVal == 0) {
					dstVal = 1000000;
				}
			} while (carryStart.value == dstVal || carryStart.next.value == dstVal || carryEnd.value == dstVal);
			Node<int> dst = nodes[dstVal];

			Node<int> dstNext = dst.next;
			dst.next = carryStart;
			first.next = carryEnd.next;
			carryEnd.next = dstNext;

			first = first.next;
		}

		Console.WriteLine((long)nodes[1].next.value * nodes[1].next.next.value);
		return 0;
	}

	class Node<T> {
		public readonly T value;
		public Node<T> next;

		public Node(T value) {
			this.value = value;
		}
	}
}
