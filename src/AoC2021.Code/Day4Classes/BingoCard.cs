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
			_numberOfMarkedNumbersPerColumn = new List<int>();
			_numberOfMarkedNumbersPerRow = new List<int>();
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

			if(_numberOfMarkedNumbersPerColumn.Count <= 0)
			{
				for(int i = 0; i < column; i++)
				{
					_numberOfMarkedNumbersPerColumn.Add(0);
					_numberOfMarkedNumbersPerRow.Add(0);
				}
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
			if(_numberOfMarkedNumbersPerColumn[position.Column] >= _numberOfMarkedNumbersPerColumn.Count || _numberOfMarkedNumbersPerRow[position.Row] >= _numberOfMarkedNumbersPerRow.Count)
			{
				this.Wins = true;
			}
		}


		public long GetSumOfUnmarkedNumbers()
		{
			long toReturn = 0;
			foreach(var n in _unmarkedNumbers)
			{
				toReturn += n;
			}
			//return _unmarkedNumbers.Sum();
			return toReturn;
		}
		
		public bool Wins { get; private set; }
		public int CardNo { get; set; }		// for testing
		public bool IsValid => _boardPositionPerNumber.Count > 0 && _boardPositionPerNumber.Count == _numberOfMarkedNumbersPerColumn.Count * _numberOfMarkedNumbersPerRow.Count;
	}
}