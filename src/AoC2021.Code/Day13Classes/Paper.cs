using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AoC2021.Core.Day13Classes
{
	public class Paper
	{
		private bool[,] _dots;
		private List<Point> _inputPoints;
		private Queue<Point> _foldCommands;
		private int _top, _left, _bottom, _right;

		public Paper()
		{
			_inputPoints = new List<Point>();
			_foldCommands = new Queue<Point>();
		}


		public void AddDot(int x, int y)
		{
			_inputPoints.Add(new Point(x, y));
		}


		public void AddFoldCommand(int x, int y)
		{
			_foldCommands.Enqueue(new Point(x, y));
		}


		public void BuildDots()
		{
			_bottom = _inputPoints.Select(p => p.Y).Max();
			_right = _inputPoints.Select(p => p.X).Max();
			_top = 0;
			_left = 0;
			_dots = new bool[_right+1, _bottom+1];
			foreach(var p in _inputPoints)
			{
				_dots[p.X, p.Y] = true;
			}
		}


		public void Fold()
		{
			// process the first command in the queue and dequeue it
			var cmd = _foldCommands.Dequeue();
			if(cmd.X == 0)
			{
				// fold over Y
				for(int lineDelta = 1; lineDelta <= (_bottom-_top)/2; lineDelta++)
				{
					for(int x = _left; x <= _right; x++)
					{
						_dots[x, cmd.Y - lineDelta] |= _dots[x, cmd.Y + lineDelta];
					}
				}

				_bottom = cmd.Y - 1;
			}
			else
			{
				// fold over X
				for(int y = _top; y <= _bottom; y++)
				{
					for(int columnDelta = 1; columnDelta <= (_right-_left)/2; columnDelta++)
					{
						_dots[cmd.X - columnDelta, y] |= _dots[cmd.X + columnDelta, y];
					}
				}

				_right = cmd.X - 1;
			}
		}


		public int GetNumberOfDots()
		{
			int toReturn = 0;
			for(int y = _top; y <= _bottom; y++)
			{
				for(int x = _left; x <= _right; x++)
				{
					toReturn += _dots[x, y] ? 1 : 0;
				}
			}
			return toReturn;
		}


		public void Display()
		{
			Console.WriteLine("Paper ({0} x {1}):", (_right-_left)+1, (_bottom-_top)+1);
			for(int y = _top; y <= _bottom; y++)
			{
				for(int x = _left; x <= _right; x++)
				{
					Console.Write("{0}", _dots[x, y] ? "#" : ".");
				}
				Console.Write("\n");
			}

			Console.WriteLine("\nFold commands:");
			foreach(var p in _foldCommands)
			{
				if(p.X == 0)
				{
					Console.WriteLine("Fold along y={0}", p.Y);
				}
				else
				{
					Console.WriteLine("Fold along x={0}", p.X);
				}
			}

			Console.WriteLine("\nNumber of dots: {0}", GetNumberOfDots());
		}


		public bool IsValid => _inputPoints.Count > 0 && _foldCommands.Count > 0;
		public bool CanFold => _foldCommands.Count > 0;
	}
}