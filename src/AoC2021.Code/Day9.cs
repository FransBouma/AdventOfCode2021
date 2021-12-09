using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AoC2021.Core.Day5Classes;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2021.Core
{
	public static class Day9
	{ 
		public static long Solve1(int[,] input)
		{
			int currentRiskLevelSum = 0;
			var mapMaxY = input.GetLength(1);
			var mapMaxX = input.GetLength(0);
			for(int y = 0; y < mapMaxY; y++)
			{
				for(int x = 0; x < mapMaxX; x++)
				{
					 var height = input[x, y];
					 var upIsLower = (y > 0) ? height < input[x, y - 1] : true;
					 var rightIsLower = (x < mapMaxX - 1) ? height < input[x + 1, y] : true;
					 var downIsLower = (y < mapMaxY - 1) ? height < input[x, y + 1] : true;
					 var leftIsLower = (x > 0) ? height < input[x - 1, y] : true;
					 if(upIsLower && rightIsLower && downIsLower && leftIsLower)
					 {
						 currentRiskLevelSum += (height + 1);
					 }
				}
			}
			return currentRiskLevelSum;
		}


		public static long Solve2(int[,] input)
		{
			var mapMaxY = input.GetLength(1);
			var mapMaxX = input.GetLength(0);
			bool[,] markedBasinCells = new bool[mapMaxX, mapMaxY];
			var basinSizes = new List<int>(); 
			for(int y = 0; y < mapMaxY; y++)
			{
				for(int x = 0; x < mapMaxX; x++)
				{
					if(markedBasinCells[x, y])
					{
						// already seen
						continue;
					}
					var height = input[x, y];
					if(height == 9)
					{
						// top height
						continue;
					}
					// not a top height, so likely part of a basin. We'll now try to find the basin using recursion.
					int numberOfCellsInBasin = 0;
					MarkBasinMembers(input, x, y, markedBasinCells, ref numberOfCellsInBasin);
					basinSizes.Add(numberOfCellsInBasin);
				}
			}

			var top3 = basinSizes.OrderByDescending(v => v).Take(3).ToList();
			return top3[0] * top3[1] * top3[2];
		}


		private static void MarkBasinMembers(int[,] input, int x, int y, bool[,] markedBasinCells, ref int numberOfCellsInBasin)
		{
			if(x < 0 || x >= input.GetLength(0) || y < 0 || y >= input.GetLength(1))
			{
				// out of bounds, cell doesn't exit
				return;
			}

			if(markedBasinCells[x, y])
			{
				// already marked/seen
				return;
			}

			if(input[x, y] == 9)
			{
				// top height, not part of a basin
				return;
			}
			// mark it and then check if its top/right/bottom/left cells are also part of it
			markedBasinCells[x, y] = true;
			numberOfCellsInBasin++;
			MarkBasinMembers(input, x, y-1, markedBasinCells, ref numberOfCellsInBasin);	// top 
			MarkBasinMembers(input, x+1, y, markedBasinCells, ref numberOfCellsInBasin);	// right
			MarkBasinMembers(input, x, y+1, markedBasinCells, ref numberOfCellsInBasin);	// bottom
			MarkBasinMembers(input, x-1, y, markedBasinCells, ref numberOfCellsInBasin);	// left 
		}
	}
}