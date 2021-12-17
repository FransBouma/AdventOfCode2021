using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AoC2021.Core.Day17
{
	public static class Day17
	{
		public class Trajectory : List<Point>
		{
			public int GetMaxY()
			{
				return this.Select(p => p.Y).Max();
			}
			
			public Point StartSpeeds { get; set; }
		}
		
		
		public class Probe
		{
			private int _x, _y, _speedX, _speedY;

			public Probe(int speedX, int speedY)
			{
				_x = 0;
				_y = 0;
				_speedX = speedX;
				_speedY = speedY;
			}


			public void Step()
			{
				_x += _speedX;
				_y += _speedY;
				if(_speedX != 0)
				{
					_speedX -= Math.Sign(_speedX);
				}
				_speedY -= 1;
			}


			public int PositionX => _x;
			public int PositionY => _y;
		}
		
		
		public class TargetArea
		{
			private int _xFrom, _xTo, _yFrom, _yTo;


			public TargetArea(string input)
			{
				var fragments = input.Substring(13).Split(", ");
				var xFragments = fragments[0].Substring(2).Split("..");
				var yFragments = fragments[1].Substring(2).Split("..");
				_xFrom = int.Parse(xFragments[0]);
				_xTo = int.Parse(xFragments[1]);
				_yFrom = int.Parse(yFragments[0]);
				_yTo = int.Parse(yFragments[1]);
				if(_xFrom > _xTo)
				{
					(_xFrom, _xTo) = (_xTo, _xFrom);
				}
				if(_yFrom < _yTo)
				{
					(_yFrom, _yTo) = (_yTo, _yFrom);
				}
			}


			public bool HitTest(int x, int y)
			{
				return x >= _xFrom && x <= _xTo && 
					   (y > 0 ? y >= _yFrom : y <= _yFrom) && 
					   (y > 0 ? y <= _yTo : y>=_yTo);
			}


			public void Display()
			{
				Console.WriteLine("X From:{0}, X To: {1}, Y From:{2}, Y To:{3}", _xFrom, _xTo, _yFrom, _yTo);
			}


			public int XFrom => _xFrom;
			public int XTo => _xTo;
			public int YFrom => _yFrom;
			public int YTo => _yTo;
		}
		
		
		public static int Solve1(string input, bool verboseOutput = false)
		{
			var t = new TargetArea(input);
			if(verboseOutput)
			{
				t.Display();
			}

			return GetTrajectories(verboseOutput, t).Select(t=>t.GetMaxY()).Max();
		}


		public static long Solve2(string input)
		{
			var t = new TargetArea(input);
			return GetTrajectories(false, t).Select(tr => tr.StartSpeeds).Count();
		}


		private static List<Trajectory> GetTrajectories(bool verboseOutput, TargetArea t)
		{
			var trajectories = new List<Trajectory>();
			for(int speedX = GetMinimalSpeedToReachX(t.XFrom); speedX <= t.XTo; speedX++)
			{
				for(int speedY = t.YTo; speedY < Math.Abs(t.YTo); speedY++)
				{
					var trajectory = new Trajectory();
					var probe = new Probe(speedX, speedY);
					trajectory.StartSpeeds = new Point(speedX, speedY);
					bool trajectoryHit = false;
					do
					{
						probe.Step();
						trajectory.Add(new Point(probe.PositionX, probe.PositionY));
						if(t.HitTest(probe.PositionX, probe.PositionY))
						{
							// we hit the target area
							if(verboseOutput)
							{
								Console.WriteLine("Target hit using initial speed: ({0}, {1})", speedX, speedY);
							}

							trajectoryHit = true;
							break;
						}
					} while(probe.PositionX <= t.XTo && (t.YTo < 0 ? probe.PositionY >= t.YTo : probe.PositionY <= t.YTo));

					if(trajectoryHit)
					{
						trajectories.Add(trajectory);
					}
				}
			}

			return trajectories;
		}


		private static int GetMinimalSpeedToReachX(int x)
		{
			for(int i = 1; i <= x; i++)
			{
				if((i * (i + 1)) / 2 < x)
				{
					continue;
				}
				return i;
			}
			return -1;
		}
	}
}