using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Core
{
	public static class InputReader
	{
		public static List<int> GetInputAsIntList(string pathFilename)
		{
			return File.ReadLines(pathFilename).Select(l => int.Parse(l)).ToList();
		}

		
		public static List<string> GetInputAsStringList(string pathFilename)
		{
			return new List<string>(File.ReadLines(pathFilename));
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
			
			return toReturn;
		}
	}
}