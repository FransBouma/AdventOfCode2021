using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Core
{
	public class Day4PuzzleInput
	{
		public Day4PuzzleInput()
		{
			this.BingoCards = new List<BingoCard>();
		}
		
		/// <summary>
		/// Plays the drawn numbers over the cards known and returns the first card that wins
		/// </summary>
		/// <returns></returns>
		public (BingoCard WinningCard, int WinningNumber) PlayBingo()
		{
			foreach(var n in DrawnNumbers)
			{
				foreach(var card in BingoCards)
				{
					card.CheckNumber(n);
					if(card.Wins)
					{
						return (card, n);
					}
				}
			}

			return (null, -1);
		}

		
		/// <summary>
		/// Plays bingo till there's 1 card left. When *that* happens it returns that card and the number drawn to make the semi last card to win.
		/// </summary>
		/// <returns></returns>
		public (BingoCard LosingCard, int WinningNumber) PlayBingoToLose()
		{
			var cardsLeft = this.BingoCards.ToList();
			if(cardsLeft.Count <= 1)
			{
				return (null, -1);
			}
			
			var cardsToRemove = new List<BingoCard>();
			foreach(var n in DrawnNumbers)
			{
				foreach(var c in cardsToRemove)
				{
					cardsLeft.Remove(c);
				}
				cardsToRemove.Clear();
				foreach(var card in cardsLeft)
				{
					card.CheckNumber(n);
					if(card.Wins)
					{
						if(cardsLeft.Count == 1)
						{
							// we've reached the end. return the card left with the number we just drew
							return (card, n);
						}

						cardsToRemove.Add(card);
					}
				}
			}

			return (null, -1);
		}


		public void Sanitize()
		{
			// toss out all bingo cards that are invalid, because e.g. there were trailing empty lines in the input
			var toRemove = this.BingoCards.Where(c => !c.IsValid).ToList();
			foreach(var c in toRemove)
			{
				this.BingoCards.Remove(c);
			}
		}
		
		public List<BingoCard> BingoCards { get; set; }
		public List<int> DrawnNumbers { get; set; }
	}
}