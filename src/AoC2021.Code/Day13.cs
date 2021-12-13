using System;
using System.Linq;
using AoC2021.Core.Day13Classes;

namespace AoC2021.Core.Day13
{
	public static class Day13
	{ 
		public static long Solve1(Paper input, bool verboseOutput=false)
		{
			input.BuildDots();
			if(verboseOutput)
			{
				input.Display();
			}
			// fold just once
			input.Fold();
			if(verboseOutput)
			{
				Console.WriteLine("\nAfter 1 fold:");
				input.Display();
			}
			return input.GetNumberOfDots();
		}


		public static long Solve2(Paper input, bool verboseOutput=false)
		{
			input.BuildDots();
			while(input.CanFold)
			{
				input.Fold();
			}

			if(verboseOutput)
			{
				Console.WriteLine("\nAfter all fold commands:");
				input.Display();
			}
			return input.GetNumberOfDots();
		}
	}
}