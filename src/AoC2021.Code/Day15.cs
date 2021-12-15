using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace AoC2021.Core.Day15
{
	public class Grid
	{
		private int[,] _riskLevels;
		private int[,] _totalRiskLevels;		// the value in [x,y] is the lowest risk known till then when reaching that cell. 
		private int _width, _height;

		public Grid(int[,] input)
		{
			_riskLevels = input;
			_width = _riskLevels.GetLength(0);
			_height = _riskLevels.GetLength(1);
		}


		public void Solve(int timesBigger=1, bool verboseOutput=false)
		{
			_width *= timesBigger;
			_height *= timesBigger;

			if(verboseOutput)
			{
				DisplayGrid();
			}
			
			_totalRiskLevels = new int[_width, _height];
			
			var toProcess = new Queue<Point>();
			toProcess.Enqueue(new Point(0, 0));
			while(toProcess.Count > 0)
			{
				var p = toProcess.Dequeue();
				var currentRisk = _totalRiskLevels[p.X, p.Y];		
				// now move to all locations reachable from this point, update the total risk of that location if it becomes lower, and enqueue the
				// locations in the queue if the risk of that cell was updated. Process stops as when we reached the last spot we don't enqueue new points anymore
				// top
				if(p.Y > 0)
				{
					CalculateRisks(currentRisk, new Point(p.X, p.Y - 1), toProcess);
				}
				// right
				if(p.X < _width - 1)
				{
					CalculateRisks(currentRisk, new Point(p.X+1, p.Y), toProcess);
				}
				// bottom
				if(p.Y < _height - 1)
				{
					CalculateRisks(currentRisk, new Point(p.X, p.Y + 1), toProcess);
				}
				// left
				if(p.X > 0)
				{
					CalculateRisks(currentRisk, new Point(p.X-1, p.Y), toProcess);
				}
			}
		}


		private int GetOriginalRisk(Point p)
		{
			var originalWidth = _riskLevels.GetLength(0);
			var originalHeight = _riskLevels.GetLength(1);
			var tile = (p.X / originalWidth) + (p.Y / originalHeight);
			var toReturn = _riskLevels[p.X % originalWidth, p.Y % originalHeight] + tile;
			if(toReturn > 9)
			{
				// wrap around
				return (toReturn % 9);
			}

			return toReturn;
		}
		

		private void CalculateRisks(int currentRisk, Point newPoint, Queue<Point> toProcess)
		{
			var newRisk = currentRisk + GetOriginalRisk(newPoint);
			var currentTotalRiskAtPoint = _totalRiskLevels[newPoint.X, newPoint.Y];
			if(currentTotalRiskAtPoint==0 || newRisk < currentTotalRiskAtPoint)
			{
				_totalRiskLevels[newPoint.X, newPoint.Y] = newRisk;
				toProcess.Enqueue(newPoint);
			}
		}


		private void DisplayGrid()
		{
			for(int y = 0; y < _height; y++)
			{
				for(int x = 0; x < _width; x++)
				{
					Console.Write(GetOriginalRisk(new Point(x, y)));
				}
				Console.Write("\n");
			}
		}


		public int TotalMinimalRisk => _totalRiskLevels[_width - 1, _height - 1];
	}
	
	
	public static class Day15
	{ 
		public static long Solve1(int[,] input, bool verboseOutput=false)
		{
			var grid = new Grid(input);
			grid.Solve(1, verboseOutput);
			return grid.TotalMinimalRisk;
		}


		public static long Solve2(int[,] input, bool verboseOutput=false)
		{
			var grid = new Grid(input);
			grid.Solve(5, verboseOutput);
			return grid.TotalMinimalRisk;
		}
	}
}