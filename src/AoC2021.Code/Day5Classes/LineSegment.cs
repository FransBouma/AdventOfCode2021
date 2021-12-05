using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AoC2021.Core.Day5Classes
{
	public class LineSegment
	{
		public LineSegment(string segmentData)
		{
			var fragments = segmentData.Split("->");
			this.Start = ConvertToPoint(fragments[0]);
			this.End = ConvertToPoint(fragments[1]);
		}


		private Point ConvertToPoint(string pointData)
		{
			var fragments = pointData.Split(',');
			return new Point(Convert.ToInt32(fragments[0]), Convert.ToInt32(fragments[1]));
		}


		public void CheckCoverage(Dictionary<Point, int> coveredPoints, bool onlyHorizontalVerticalLines)
		{
			// Start could be 'larger' than 'End'. If so we swap them for this function.
			var startToUse = this.Start;
			var endToUse = this.End;

			if(Start.X == End.X)
			{
				if(Start.Y > End.Y)
				{
					startToUse = End;
					endToUse = Start;
				}
				PlotLine(startToUse, endToUse, 0, 1, coveredPoints);
			}
			else
			{
				if(Start.Y == End.Y)
				{
					if(Start.X > End.X)
					{
						startToUse = this.End;
						endToUse = this.Start;
					}
					PlotLine(startToUse, endToUse, 1, 0, coveredPoints);
				}
				else
				{
					if(onlyHorizontalVerticalLines)
					{
						// ignore
						return;
					}
					// diagonal line.
					int xIncrement = Start.X > End.X ? -1 : 1;
					int yIncrement = Start.Y > End.Y ? -1 : 1;
					PlotLine(startToUse, endToUse, xIncrement, yIncrement, coveredPoints);
				}
			}
		}


		private static void PlotLine(Point startToUse, Point endToUse, int xIncrement, int yIncrement, Dictionary<Point, int> coveredPoints)
		{
			Point current = startToUse;
			do
			{
				AddPointToCoverage(current, coveredPoints);
				current.X += xIncrement;
				current.Y += yIncrement;
			} while(!current.Equals(endToUse));

			// add the end point, as that's not done in the while loop above
			AddPointToCoverage(endToUse, coveredPoints);
		}


		public override string ToString()
		{
			return string.Format("{0} -> {1}", this.Start, this.End);
		}


		private static void AddPointToCoverage(Point p, Dictionary<Point, int> toAddTo)
		{
			if(toAddTo.ContainsKey(p))
			{
				var currentCount = toAddTo[p];
				toAddTo[p] = currentCount + 1;
			}
			else
			{
				toAddTo[p] = 1;
			}
		}
		
		
		public Point Start { get; set; }
		public Point End { get; set; }
	}
}
