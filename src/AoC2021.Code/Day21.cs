using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2021.Core.Day21
{
	public static class Day21
	{
		// use struct for copy characteristics for puzzle 2
		public struct Die
		{
			private int _numberOfRolls;
			private int _currentValue;
			private int _maxValue;

			public Die(int maxValue)
			{
				_numberOfRolls = 0;
				_currentValue = 0;
				_maxValue = maxValue;
			}


			public int Roll()
			{
				_numberOfRolls++;
				_currentValue++;
				var toReturn = _currentValue;
				_currentValue = _currentValue % _maxValue;
				return toReturn;
			}

			public int LastRolledValue => _currentValue+1;
			public int NumberOfRolls => _numberOfRolls;
		}


		public struct Board
		{
			public Board(int startPositionP1, int startPositionP2)
			{
				this.Player1Pos = startPositionP1;
				this.Player2Pos = startPositionP2;
				this.Player1Score = 0;
				this.Player2Score = 0;
			}
			
			public void MovePlayer(int positions, bool player1)
			{
				if(player1)
				{
					this.Player1Pos += positions;
					this.Player1Pos = ((this.Player1Pos - 1) % 10) + 1;
					this.Player1Score += this.Player1Pos;
				}
				else
				{
					this.Player2Pos += positions;
					this.Player2Pos = ((this.Player2Pos - 1) % 10) + 1;
					this.Player2Score += this.Player2Pos;
				}
			}
			
			public int Player1Pos { get; set; }
			public int Player2Pos { get; set; }
			public int Player1Score { get; set; }
			public int Player2Score { get; set; }
		}


		public static (long p1Won, long p2Won) PlayDirac(Board board, Die die, int depth = 0)
		{
			// as we're calling ourselves every roll, we have to check if the turn is over before we can check for scores.
			// A turn is implemented through recursion so after 6 recursive calls a full turn has been completed along the current call path
			// along the way we've created many copies, so we calculate after these 6 calls what the state is so we can calculate a score
			// After 3 calls the turn for player 1 is over, so we can calculate the score for player 1 after 3 recursive calls. 
			if(die.NumberOfRolls > 0 && die.NumberOfRolls % 6 == 0)
			{
				if(board.Player2Score >= 5)
				{
					return (0, 1);
				}
			}
			else
			{
				if(die.NumberOfRolls > 0 && die.NumberOfRolls % 3 == 0)
				{
					if(board.Player1Score >= 5)
					{
						return (1, 0);
					}
				}
			}
			
			Console.WriteLine("# of rolls: {0}. Score p1: {1}, depth: {2}", die.NumberOfRolls, board.Player1Score, depth);
			(long numberOfP1Wins, long numberOfP2Wins) toReturn = (0,0);
			for(int i = 1; i <= 3; i++)
			{
				Board newState = board;
				var dieValue = die.Roll();
				if(die.NumberOfRolls % 6 > 0 && die.NumberOfRolls % 6 < 4)
				{
					newState.MovePlayer(dieValue, true);
				}
				else
				{
					newState.MovePlayer(dieValue, false);
				}
				var (a,b) = PlayDirac(newState, die, depth+1);
				toReturn.numberOfP1Wins += a;
				toReturn.numberOfP2Wins += b;
			}
			return toReturn;
		}

		
		public static long Solve1(int pos1, int pos2, bool verboseOutput=false)
		{
			var board = new Board(pos1, pos2);
			var die = new Die(100);
			do
			{
				board.MovePlayer(die.Roll() + die.Roll() + die.Roll(), true);
				if(board.Player1Score >= 1000)
				{
					break;
				}
				board.MovePlayer(die.Roll() + die.Roll() + die.Roll(), false);
			} while(board.Player1Score < 1000 && board.Player2Score < 1000);

			return die.NumberOfRolls * Math.Min(board.Player1Score, board.Player2Score);
		}


		public static long Solve2(int pos1, int pos2, bool verboseInput=false)
		{
			var board = new Board(pos1, pos2);
			var die = new Die(3);

			var (a, b) = PlayDirac(board, die);
			return Math.Max(a, b);
		}
	}
}