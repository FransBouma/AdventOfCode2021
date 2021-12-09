using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoC2021.Core.Day5Classes;

namespace AoC2021.Core
{
	public static class InputReader
	{
		public static List<int> GetInputAsIntList(string pathFilename, bool commaSeparated = false)
		{
			if(commaSeparated)
			{
				var lines = GetInputAsStringList(pathFilename);
				var toReturn = new List<int>();
				foreach(var l in lines)
				{
					if(string.IsNullOrWhiteSpace(l))
					{
						continue;
					}
					toReturn.AddRange(l.Split(',').Select(f=>int.Parse(f)));
				}

				return toReturn;
			}
			return File.ReadLines(pathFilename).Select(l => int.Parse(l)).ToList();
		}

		
		public static List<string> GetInputAsStringList(string pathFilename)
		{
			return new List<string>(File.ReadLines(pathFilename));
		}


		public static List<LineSegment> GetInputAsLineSegments(string pathFilename)
		{
			return GetInputAsStringList(pathFilename).Select(s=>new LineSegment(s)).ToList();
		}


		public static Day4PuzzleInput GetInputAsDay4PuzzleInput(string pathFilename)
		{
			var toReturn = new Day4PuzzleInput();

			var allLines = GetInputAsStringList(pathFilename);
			// first line is the drawn number list, then a blank then the 5 rows of card 1 then a blank then a new card and so on.

			toReturn.DrawnNumbers = allLines[0].Split(',').Select(s => int.Parse(s)).ToList();

			int row = 0;
			BingoCard currentCard = null;
			int cardNo = 1;
			for(int i = 1; i < allLines.Count; i++)
			{
				if(string.IsNullOrWhiteSpace(allLines[i]))
				{
					currentCard = new BingoCard() { CardNo = cardNo};
					toReturn.BingoCards.Add(currentCard);
					row = 0;
					cardNo++;
					continue;
				}
				if(currentCard == null)
				{
					continue;
				}
				currentCard.AddRow(allLines[i], row);
				row++;
			}
			// toss out any invalid bingo cards
			toReturn.Sanitize();
			
			return toReturn;
		}


		public static int[,] GetInputAs2DIntArrayList(string pathFilename)
		{
			var lines = GetInputAsStringList(pathFilename);
			int[,] toReturn = new int[lines[0].Length, lines.Count];
			for(var y = 0; y < lines.Count; y++)
			{
				for(var x = 0; x < lines[y].Length; x++)
				{
					toReturn[x, y] = Convert.ToInt32(lines[y][x]) - 48;		// 48 is Ascii of 0 
				}
			}
			return toReturn;
		}
	}
}