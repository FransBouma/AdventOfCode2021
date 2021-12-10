using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Core
{
	public class ParseError
	{
		protected ParseError(string message) => this.Message = message;

		public string Message { get; }
	}


	public class AllOk : ParseError
	{
		public AllOk() : base("Nothing to report") { }
	}
	
	public class IncompleteLineError : ParseError
	{
		public IncompleteLineError(string message, Stack<char> openCharsInFlight, Dictionary<char, char> openClosePairs) : base(message)  
		{
			if(openCharsInFlight.Count <= 0)
			{
				return;
			}
			this.CharsMissing = openCharsInFlight.Select(c => openClosePairs[c]).ToList();
		}

		public List<char> CharsMissing { get; }
	}
	
	
	public class IllegalCharacterError : ParseError
	{
		public IllegalCharacterError(string message, char illegalCharacter) : base(message)
		{
			this.IllegalCharacter = illegalCharacter;
		}

		public char IllegalCharacter { get; set; }
	}

	
	public class Parser
	{
		private Stack<char> _openCharsInFlight;
		private Dictionary<char, char> _openClosePairs;

		public Parser()
		{
			_openCharsInFlight = new Stack<char>();
			_openClosePairs = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '<', '>' }, { '{', '}' } };
		}


		public ParseError ParseLine(string line)
		{
			ParseError toReturn = null;
			_openCharsInFlight.Clear();
			for(int i = 0; i < line.Length; i++)
			{
				if(_openClosePairs.ContainsKey(line[i]))
				{
					// open char for chunk
					_openCharsInFlight.Push(line[i]);
				}
				else
				{
					// check if it's the closing char for the top of the stack
					if(_openCharsInFlight.Count <= 0)
					{
						// Corrupted
						toReturn = new IllegalCharacterError($"Corrupted line '{line}'. At offset '{i}', expected '{string.Join(", ", _openClosePairs.Keys.ToArray())}', but got '{line[i]}'", line[i]);												  
						break;
					}

					var closingCharForTopOfStack = _openClosePairs[_openCharsInFlight.Peek()];
					if(closingCharForTopOfStack != line[i])
					{
						// Corrupted
						toReturn = new IllegalCharacterError($"Corrupted line '{line}'. At offset '{i}', expected '{closingCharForTopOfStack}', but got '{line[i]}'", line[i]);
						break;
					}
					// closing char for top of stack
					_openCharsInFlight.Pop();
				}
			}
			if(toReturn != null)
			{
				return toReturn;
			}
			if(_openCharsInFlight.Count > 0)
			{
				toReturn = new IncompleteLineError($"Line '{line}' is incomplete.", _openCharsInFlight, _openClosePairs);
			}
			else
			{
				// all ok
				return new AllOk();
			}
			return toReturn;
		}
	}
	
	
	public static class Day10
	{
		public static long Solve1(List<string> input, bool verboseOutput = false)
		{
			var illegalCharScores = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
			
			var parser = new Parser();
			int currentScore = 0;
			foreach(var parseError in input.Select(line => parser.ParseLine(line)))
			{
				if(verboseOutput)
				{
					Console.WriteLine(parseError.Message);
				}
				if(parseError is IllegalCharacterError illegalCharacterError)
				{
					currentScore += illegalCharScores[illegalCharacterError.IllegalCharacter];
				}
			}
			return currentScore;
		}


		public static long Solve2(List<string> input, bool verboseOutput = false)
		{
			var parser = new Parser();
			List<long> scores = new List<long>();
			foreach(var parseError in input.Select(line => parser.ParseLine(line)))
			{
				if(verboseOutput)
				{
					Console.WriteLine(parseError.Message);
				}

				if(parseError is IncompleteLineError incompleteLineError)
				{
					scores.Add(CalculateIncompleteScore(incompleteLineError));
				}
			}
			scores.Sort();
			return scores[scores.Count/2];
		}


		private static long CalculateIncompleteScore(IncompleteLineError incompleteLineError)
		{
			var missingCharScores = new Dictionary<char, int>() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4} };
			long currentScore = 0;
			foreach(var c in incompleteLineError.CharsMissing)
			{
				currentScore = currentScore * 5 + missingCharScores[c];
			}
			return currentScore;
		}
	}
}