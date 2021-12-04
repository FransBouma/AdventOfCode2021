using System;
using System.Collections.Generic;

namespace AoC2021.Core
{
	public static class Day4
	{
		public static int Solve1(Day4PuzzleInput input)
		{
			var (winningCard, winningNumber) = input.PlayBingo();
			return winningCard.GetSumOfUnmarkedNumbers() * winningNumber;
		}

		
		public static int Solve2(Day4PuzzleInput input)
		{
			var (losingCard, winningNumber) = input.PlayBingoToLose();
			return losingCard.GetSumOfUnmarkedNumbers() * winningNumber;
		}
	}
}