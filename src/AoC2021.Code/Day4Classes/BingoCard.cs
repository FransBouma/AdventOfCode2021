using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Core
{
	public class BingoCard
	{
		private struct BoardPosition
		{
			public int Column;		// 0,0 is top left
			public int Row;
		}
		
		private List<int> _numberOfMarkedNumbersPerRow;		// 1 value per row, starting with 0. If a number is 5, the row wins. Value at offset 0 is first row
		private List<int> _numberOfMarkedNumbersPerColumn;	// 1 value per row, starting with 0. If a number is 5, the row wins. Value at offset 0 is first column
		private HashSet<int> _unmarkedNumbers;
		private Dictionary<int, BoardPosition> _boardPositionPerNumber;


		public BingoCard()
		{
			_numberOfMarkedNumbersPerColumn = new List<int>() {0,0,0,0,0};
			_numberOfMarkedNumbersPerRow = new List<int>() {0,0,0,0,0};
			_unmarkedNumbers = new HashSet<int>();
			_boardPositionPerNumber = new Dictionary<int, BoardPosition>();
			this.Wins = false;
		}


		public void AddRow(string rowValues, int rowNumber)
		{
			if(string.IsNullOrWhiteSpace(rowValues))
			{
				return;
			}
			// numbers are divided by whitespace. So we'll split on space and skip the whitespace fragments
			var numberFragments = rowValues.Split(' ');
			int column = 0;
			foreach(var fragment in numberFragments)
			{
				if(string.IsNullOrWhiteSpace(fragment))
				{
					continue;
				}
				int number = int.Parse(fragment);
				var position = new BoardPosition() { Column = column, Row = rowNumber };
				_boardPositionPerNumber.Add(number, position);
				_unmarkedNumbers.Add(number);
				column++;
			}
		}


		public void CheckNumber(int numberDrawn)
		{
			if(!_boardPositionPerNumber.ContainsKey(numberDrawn))
			{
				// not on the board
				return;
			}

			_unmarkedNumbers.Remove(numberDrawn);
			var position = _boardPositionPerNumber[numberDrawn];
			_numberOfMarkedNumbersPerColumn[position.Column]++;
			_numberOfMarkedNumbersPerRow[position.Row]++;
			if(_numberOfMarkedNumbersPerColumn[position.Column] >= 5 || _numberOfMarkedNumbersPerRow[position.Row] >= 5)
			{
				this.Wins = true;
			}
		}


		public int GetSumOfUnmarkedNumbers()
		{
			return _unmarkedNumbers.Sum();
		}
		
		public bool Wins { get; private set; }
		public int CardNo { get; set; }		// for testing
		public bool IsValid => _boardPositionPerNumber.Count == 25;
	}
}