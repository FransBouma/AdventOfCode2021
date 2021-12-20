using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2021.Core
{
	public static class Day8
	{ 
		public static long Solve1(List<string> input)
		{
			var lengthsToSearch = new HashSet<int>() { 2, 4, 3, 7 };

			int toReturn = 0;
			foreach(var l in input)
			{
				var fragments = l.Split('|');
				var outputFragments = fragments[1].Split(' ');
				toReturn += outputFragments.Count(f => lengthsToSearch.Contains(f.Length));
			}

			return toReturn;
		}


		public static long Solve2(List<string> input)
		{
			return input.Sum(l => MapAndAdd(l));
		}


		private static int MapAndAdd(string inputLine)
		{
			var fragments = inputLine.Split('|');
			var inputSignals = fragments[0].Split(' ').ToList();
			var displayDigits = fragments[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

			var signalPerDigit = new string[10];

			// first the known digits
			signalPerDigit[1] = inputSignals.First(s => s.Length == 2);
			inputSignals.Remove(signalPerDigit[1]);
			signalPerDigit[7] = inputSignals.First(s => s.Length == 3);
			inputSignals.Remove(signalPerDigit[7]);
			signalPerDigit[4] = inputSignals.First(s => s.Length == 4);
			inputSignals.Remove(signalPerDigit[4]);
			signalPerDigit[8] = inputSignals.First(s => s.Length == 7);
			inputSignals.Remove(signalPerDigit[8]);
			// 9 is the only signal which has 4 and 7 and is of length 6
			signalPerDigit[9] = inputSignals.First(s => s.Length==6 && s.Except(signalPerDigit[4].Union(signalPerDigit[7])).Count()==1);
			inputSignals.Remove(signalPerDigit[9]);
			// 0 is the signal of length 6 which isn't 9 and which contains 1 (or 7). 9 isn't in the input anymore so we don't have test for that.
			signalPerDigit[0] = inputSignals.First(s => s.Length == 6 && s.Except(signalPerDigit[1]).Count() == 4);
			inputSignals.Remove(signalPerDigit[0]);
			// 3 is the signal of length 5 which contains 1
			signalPerDigit[3] = inputSignals.First(s => s.Length == 5 && s.Except(signalPerDigit[1]).Count() == 3);
			inputSignals.Remove(signalPerDigit[3]);
			// 5 is the signal of length 5 which is, unioned with 1, equal to 9
			signalPerDigit[5] = inputSignals.First(s => s.Length == 5 && s.Union(signalPerDigit[1]).SetEqual(signalPerDigit[9]));
			inputSignals.Remove(signalPerDigit[5]);
			// 2 is the only signal of length 5 still left
			signalPerDigit[2] = inputSignals.First(s => s.Length == 5);
			inputSignals.Remove(signalPerDigit[2]);
			// 6 is the only signal left
			signalPerDigit[6] = inputSignals.First();
			
			// decode the output digits
			int toReturn = 0;
			for(int i=0;i<4;i++)
			{
				var digit = FindDigit(displayDigits[i], signalPerDigit);
				if(digit < 0)
				{
					throw new InvalidOperationException($"signal {displayDigits[i]} can't be decoded");
				}

				toReturn += (int)Math.Pow(10, 3-i) * digit;
			}
			return toReturn;
		}


		private static int FindDigit(string signal, string[] signalPerDigit)
		{
			for(int i = 0; i < 10; i++)
			{
				if(signalPerDigit[i].SetEqual(signal))
				{
					return i;
				}
			}
			return -1;
		}
	}
}