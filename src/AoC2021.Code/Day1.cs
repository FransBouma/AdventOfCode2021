using System;
using System.Collections.Generic;

namespace AoC2021.Core
{
	public static class Day1
	{
		public static int Solve1(List<int> input)
		{
			int numberOfIncreases = 0;
			for(int i=1;i<input.Count;i++)
			{
				numberOfIncreases += input[i - 1] < input[i] ? 1 : 0;
			}
			return numberOfIncreases;
		}

		public static int Solve2(List<int> input)
		{
			int numberOfIncreases = 0;
			for(int i=1;i<input.Count-2;i++)
			{
				var previousSlidingWindowValue = input[i - 1] + input[i] + input[i + 1];
				var currentSlidingWindowValue = input[i] + input[i + 1] + input[i + 2];
				numberOfIncreases += previousSlidingWindowValue < currentSlidingWindowValue ? 1 : 0;
			}
			return numberOfIncreases;
		}
	}
}