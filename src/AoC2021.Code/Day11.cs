using System;
using System.Linq;

namespace AoC2021.Core.Day11
{
	public class Grid
	{
		private int[,] _energyLevels;
		private int _width, _height;

		public Grid(int[,] input)
		{
			_energyLevels = input;
			_width = _energyLevels.GetLength(0);
			_height = _energyLevels.GetLength(1);
			this.NumberOfFlashes = 0;
		}


		public void Step()
		{
			for(int y = 0; y < _height; y++)
			{
				for(int x = 0; x < _width; x++)
				{
					_energyLevels[x, y]++;
				}
			}
			
			// now flash the ones which are > 9. All cells which have 0 are left alone
			for(int y = 0; y < _height; y++)
			{
				for(int x = 0; x < _width; x++)
				{
					CheckFlash(x, y);
				}
			}
		}


		private void CheckFlash(int x, int y)
		{
			if(_energyLevels[x, y] <= 9)
			{
				// not flashing
				return;
			}

			_energyLevels[x, y] = 0;
			this.NumberOfFlashes++;
			// now spread the energy to neighbors if they're not 0
			AddFlashEnergy(x - 1, y - 1);
			AddFlashEnergy(x, y - 1);
			AddFlashEnergy(x + 1, y - 1);
			AddFlashEnergy(x - 1, y);
			AddFlashEnergy(x + 1, y);
			AddFlashEnergy(x - 1, y + 1);
			AddFlashEnergy(x, y + 1);
			AddFlashEnergy(x + 1, y + 1);
		}


		private void AddFlashEnergy(int x, int y)
		{
			if(x < 0 || x >= _width || y < 0 || y >= _height)
			{
				// out of bounds
				return;
			}

			if(_energyLevels[x, y] == 0)
			{
				// has already flashed, ignore
				return;
			}

			_energyLevels[x, y]++;
			CheckFlash(x, y);
		}


		public void DisplayGrid()
		{
			for(int y = 0; y < _height; y++)
			{
				for(int x = 0; x < _width; x++)
				{
					Console.Write(_energyLevels[x, y]);
				}
				Console.Write("\n");
			}
		}


		public bool AllFlashed()
		{
			for(int y = 0; y < _height; y++)
			{
				for(int x = 0; x < _width; x++)
				{
					if(_energyLevels[x, y] > 0)
					{
						return false;
					}
				}
			}
			return true;
		}
		
		public int NumberOfFlashes { get; set; }
	}
	
	
	public static class Day11
	{ 
		public static long Solve1(int[,] input, bool verboseOutput=false)
		{
			var grid = new Grid(input);
			for(int i = 0; i < 100; i++)
			{
				if(verboseOutput)
				{
					Console.WriteLine("Grid at the start of step {0}", i);
					grid.DisplayGrid();
				}
				grid.Step();
				
			}

			return grid.NumberOfFlashes;
		}


		public static long Solve2(int[,] input)
		{
			// eh, who needs analysis, let's brute force the thing... 
			var grid = new Grid(input);
			var stepCounter = 0;
			do
			{
				grid.Step();
				stepCounter++;
			} while(!grid.AllFlashed());

			return stepCounter;
		}
	}
}