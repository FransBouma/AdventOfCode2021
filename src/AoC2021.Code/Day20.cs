using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2021.Core.Day20
{
	public class PixelGrid
	{
		private char[] _algo;
		private char[,] _pixels;
		private int _enhanceCounter;

		public PixelGrid(List<string> input)
		{
			_enhanceCounter = 0;
			_algo = input[0].ToArray();
			
			int width = input[2].Length;
			int height = input.Count - 2;
			_pixels = new char[width, height];
			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					_pixels[x, y] = input[y+2][x] == '#' ? '1' : '0';
				}
			}
		}

		
		public void Display()
		{
			for(int y = 0; y < _pixels.GetLength(1); y++)
			{
				for(int x = 0; x < _pixels.GetLength(0); x++)
				{
					Console.Write(_pixels[x, y]);
				}
				Console.Write('\n');
			}

			Console.WriteLine("\nNumber of pixels set: {0}\n", this.NumberOfPixelsSet);
		}


		public void Enhance()
		{
			// first expand our source
			Expand();
			// then convolute our source
			var newPixels = new char[_pixels.GetLength(0), _pixels.GetLength(1)];
			for(int y = 0; y < _pixels.GetLength(1); y++)
			{
				for(int x = 0; x < _pixels.GetLength(0); x++)
				{
					newPixels[x, y] = _algo[Convolute(x, y)] == '#' ? '1' : '0';
				}
			}
			_pixels = newPixels;
			_enhanceCounter++;
		}


		public void Expand()
		{
			// expand the image in both directions with a 2x2 1 pixel edge with 0's so our kernel's output will bleed into this edge
			var newPixels = new char[_pixels.GetLength(0) + 4, _pixels.GetLength(1) + 4];
			InitPixelArray(newPixels, _enhanceCounter==0 ? '0' : _pixels[0,0]);
			for(int y = 0; y < _pixels.GetLength(1); y++)
			{
				for(int x = 0; x < _pixels.GetLength(0); x++)
				{
					newPixels[x+2, y+2] = _pixels[x, y];
				}
			}
			_pixels = newPixels;
		}


		private int Convolute(int x, int y)
		{
			var sb = new StringBuilder(9);
			sb.Append(GetPixel(x - 1, y - 1));
			sb.Append(GetPixel(x, y - 1));
			sb.Append(GetPixel(x + 1, y - 1));
			sb.Append(GetPixel(x - 1, y));
			sb.Append(GetPixel(x, y));
			sb.Append(GetPixel(x + 1, y));
			sb.Append(GetPixel(x - 1, y + 1));
			sb.Append(GetPixel(x, y + 1));
			sb.Append(GetPixel(x + 1, y + 1));
			return Convert.ToInt32(sb.ToString(), 2);
		}


		private char GetPixel(int x, int y)
		{
			if(x < 0 || x >= _pixels.GetLength(0) || y < 0 || y >= _pixels.GetLength(1))
			{
				if(_enhanceCounter == 0)
				{
					return '0';
				}
				// we've enhanced already so the infinite pixels might be non-zero, so simply return 0,0 as that was an outofbound pixel of the previous enhance.
				return _pixels[0, 0];
			}

			return _pixels[x, y];
		}


		private static void InitPixelArray(char[,] toInit, char initialValue='0')
		{
			for(int y = 0; y < toInit.GetLength(1); y++)
			{
				for(int x = 0; x < toInit.GetLength(0); x++)
				{
					toInit[x, y] = initialValue;
				}
			}
		}
		

		public int NumberOfPixelsSet => _pixels.Cast<char>().Count(c => c == '1');
	}
	
	
	public static class Day20
	{
		public static long Solve1(List<string> input, bool verboseInput=false)
		{
			var pg = new PixelGrid(input);
			if(verboseInput)
			{
				pg.Display();
			}

			pg.Enhance();
			if(verboseInput)
			{
				pg.Display();
			}

			pg.Enhance();
			if(verboseInput)
			{
				pg.Display();
			}
		
			return pg.NumberOfPixelsSet;
		}


		public static long Solve2(List<string> input)
		{
			var pg = new PixelGrid(input);
			for(int i = 0; i < 50; i++)
			{
				pg.Enhance();
			}
			return pg.NumberOfPixelsSet;
		}
	}
}