using System;
using System.Collections.Generic;

namespace AoC2021.Core
{
	public static class Day2
	{
		public static int Solve1(List<string> input)
		{
			int forwardAmount = 0;
			int depthAmount = 0;
			foreach(var commandAsString in input)
			{
				int indexOfSpace = commandAsString.IndexOf(' ');
				if(indexOfSpace < 0)
				{
					continue;
				}

				// 0123456789
				// forward 5
				// indexSpace == 7
				var commandFragment = commandAsString.AsSpan(0, indexOfSpace);
				var amountFragment = commandAsString.AsSpan(indexOfSpace+1, (commandAsString.Length - (indexOfSpace + 1)));

				if(!int.TryParse(amountFragment, out var amount))
				{
					continue;
				}

				switch(commandFragment)
				{
					case var s when s.Equals("forward", StringComparison.Ordinal):
						forwardAmount += amount;
						break;
					case var s when s.Equals("down", StringComparison.Ordinal):
						depthAmount += amount;
						break;
					case var s when s.Equals("up", StringComparison.Ordinal):
						depthAmount -= amount;
						break;
				}
			}

			return forwardAmount * depthAmount;
		}
		
		public static int Solve2(List<string> input)
		{
			int forwardAmount = 0;
			int aimAmount = 0;
			int depthAmount = 0;
			foreach(var commandAsString in input)
			{
				int indexOfSpace = commandAsString.IndexOf(' ');
				if(indexOfSpace < 0)
				{
					continue;
				}

				// 0123456789
				// forward 5
				// indexSpace == 7
				var commandFragment = commandAsString.AsSpan(0, indexOfSpace);
				var amountFragment = commandAsString.AsSpan(indexOfSpace+1, (commandAsString.Length - (indexOfSpace + 1)));

				if(!int.TryParse(amountFragment, out var amount))
				{
					continue;
				}

				switch(commandFragment)
				{
					case var s when s.Equals("forward", StringComparison.Ordinal):
						forwardAmount += amount;
						depthAmount += aimAmount * amount;
						break;
					case var s when s.Equals("down", StringComparison.Ordinal):
						aimAmount += amount;
						break;
					case var s when s.Equals("up", StringComparison.Ordinal):
						aimAmount -= amount;
						break;
				}
			}

			return forwardAmount * depthAmount;
		}
	}
}