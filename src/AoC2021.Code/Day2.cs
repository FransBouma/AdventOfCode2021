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
				var elements = commandAsString.Split(' ');
				if(elements.Length != 2)
				{
					continue;
				}

				string command = elements[0];
				int amount = 0;
				if(!int.TryParse(elements[1], out amount))
				{
					continue;
				}

				switch(command)
				{
					case "forward":
						forwardAmount += amount;
						break;
					case "down":
						depthAmount += amount;
						break;
					case "up":
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
				var elements = commandAsString.Split(' ');
				if(elements.Length != 2)
				{
					continue;
				}

				string command = elements[0];
				int amount = 0;
				if(!int.TryParse(elements[1], out amount))
				{
					continue;
				}

				switch(command)
				{
					case "forward":
						forwardAmount += amount;
						depthAmount += aimAmount * amount;
						break;
					case "down":
						aimAmount += amount;
						break;
					case "up":
						aimAmount -= amount;
						break;
				}
			}

			return forwardAmount * depthAmount;
		}
	}
}