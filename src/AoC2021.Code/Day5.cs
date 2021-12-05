using System;
using System.Collections.Generic;
using System.Drawing;
using AoC2021.Core.Day5Classes;

namespace AoC2021.Core
{
	public static class Day5
	{
		public static int Solve1(List<LineSegment> input)
		{
			var coveredPoints = new Dictionary<Point, int>();
			foreach(var lineSegment in input)
			{
				lineSegment.CheckCoverage(coveredPoints, onlyHorizontalVerticalLines:true);
			}

			int toReturn = 0;
			foreach(var kvp in coveredPoints)
			{
				if(kvp.Value >= 2)
				{
					toReturn++;
				}
			}

			return toReturn;
		}
		
		
		public static int Solve2(List<LineSegment> input)
		{
			var coveredPoints = new Dictionary<Point, int>();
			foreach(var lineSegment in input)
			{
				lineSegment.CheckCoverage(coveredPoints, onlyHorizontalVerticalLines:false);
			}

			int toReturn = 0;
			foreach(var kvp in coveredPoints)
			{
				if(kvp.Value >= 2)
				{
					toReturn++;
				}
			}

			return toReturn;
		}
	}
}