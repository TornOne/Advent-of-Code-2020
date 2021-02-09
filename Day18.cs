using System;
using System.Collections.Generic;
using System.IO;

class Day18 {
	public static int Solution1() {
		long sum = 0;

		foreach (string line in File.ReadLines("input18.txt")) {
			Stack<long> values = new Stack<long>();
			Stack<char> operators = new Stack<char>();

			foreach (char c in line) {
				switch (c) {
					case ' ':
						break;
					case '(':
						operators.Push(c);
						break;
					case ')':
						char op;
						while ((op = operators.Pop()) != '(') {
							values.Push(Calc(values.Pop(), values.Pop(), op));
						}
						break;
					case '+':
					case '*':
						if (operators.Count > 0 && operators.Peek() != '(') {
							values.Push(Calc(values.Pop(), values.Pop(), operators.Pop()));
						}
						operators.Push(c);
						break;
					default:
						values.Push(c - '0');
						break;
				}
			}

			while (operators.Count > 0) {
				values.Push(Calc(values.Pop(), values.Pop(), operators.Pop()));
			}
			sum += values.Pop();
		}

		Console.WriteLine(sum);
		return 0;
	}

	public static int Solution2() {
		long sum = 0;

		foreach (string line in File.ReadLines("input18.txt")) {
			Stack<long> values = new Stack<long>();
			Stack<char> operators = new Stack<char>();

			foreach (char c in line) {
				switch (c) {
					case ' ':
						break;
					case '(':
						operators.Push(c);
						break;
					case ')':
						char op;
						while ((op = operators.Pop()) != '(') {
							values.Push(Calc(values.Pop(), values.Pop(), op));
						}
						break;
					case '+':
						if (operators.Count > 0 && operators.Peek() == '+') {
							values.Push(Calc(values.Pop(), values.Pop(), operators.Pop()));
						}
						operators.Push(c);
						break;
					case '*':
						while (operators.Count > 0 && operators.Peek() != '(') {
							values.Push(Calc(values.Pop(), values.Pop(), operators.Pop()));
						}
						operators.Push(c);
						break;
					default:
						values.Push(c - '0');
						break;
				}
			}

			while (operators.Count > 0) {
				values.Push(Calc(values.Pop(), values.Pop(), operators.Pop()));
			}
			sum += values.Pop();
		}

		Console.WriteLine(sum);
		return 0;
	}

	static long Calc(long a, long b, char op) => op == '+' ? a + b : a * b;
}
