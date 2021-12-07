using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AoC2021.Core.Day5Classes;

namespace AoC2021.Core
{
	public static class Day7
	{
		private static int GetPositionToMoveTo(List<int> input, List<int> rangeToConsider, Func<int, int, int> rangeCalculatorFunc)
		{
			var minimalDistancesPerPosition = new Dictionary<int, int>();
			foreach(var position in rangeToConsider.Where(position => !minimalDistancesPerPosition.ContainsKey(position)))
			{
				minimalDistancesPerPosition[position] = input.Sum(p => rangeCalculatorFunc(position, p));
			}
			return minimalDistancesPerPosition.Select(kvp => kvp.Value).Prepend(int.MaxValue).Min();
		}

		
		public static long Solve1(List<int> input)
		{
			var rangeToConsider = Enumerable.Range(input.Min(), input.Max()).ToList();
			return GetPositionToMoveTo(input, rangeToConsider, (position, value) => Math.Abs(position - value));
		}


		public static long Solve2(List<int> input)
		{
			var rangeToConsider = Enumerable.Range(input.Min(), input.Max()).ToList();
			return GetPositionToMoveTo(input, rangeToConsider, (position, value) => Enumerable.Range(1, Math.Abs(position - value)).Sum());
		}
	}
}