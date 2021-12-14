using System;
using System.Linq;
using AoC2021.Core.Day14Classes;

namespace AoC2021.Core.Day14
{
	public static class Day14
	{ 
		public static long Solve1(PolymerProducer input, bool verboseOutput=false)
		{
			if(verboseOutput)
			{
				input.Display(displayRules:true);
			}

			for(int i = 1; i <= 10; i++)
			{
				input.Step();
				if(verboseOutput)
				{
					Console.WriteLine("After step: {0}", i);
					input.Display(displayRules:false);
				}
			}
			var counters = input.OccurrenceCountPerChar;
			return counters.Values.Max() - counters.Values.Min();
		}


		public static long Solve2(PolymerProducer input)
		{
			for(int i = 1; i <= 40; i++)
			{
				input.Step();
			}
			var counters = input.OccurrenceCountPerChar;
			return counters.Values.Max() - counters.Values.Min();
		}
	}
}