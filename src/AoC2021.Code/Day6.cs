using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AoC2021.Core.Day5Classes;

namespace AoC2021.Core
{
	public class FishFlock
	{
		// 3, 4, 3, 1, 2
		// -> 2x3, 1x4, 1x1, 1x2				[0, 1, 1, 2, 1, 0, 0, 0, 0]
		// after one day 2, 3, 2, 0, 1
		// -> 2x2, 1x3, 1x0, 1x1				[1, 1, 2, 1, 0, 0, 0, 0, 0]
		// after one day 1, 2, 1, 6, 0, 8
		// -> 1x0, 2x1, 1x2, 1x6, 1x8			[1, 2, 1, 0, 0, 0, 1, 0, 1]
		// after one day 0, 1, 0, 5, 6, 7, 8
		// -> 2x0, 1x1, 1x5, 1x6, 1x7, 1x8		[2, 1, 0, 0, 0, 1, 1, 1, 1]
		
		private long[] _fishCounters;		// index is internal timer, value at index is # of fish with that internal timer.  

		public FishFlock(List<int> startCounters)
		{
			_fishCounters = new long[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach(int i in startCounters)
			{
				_fishCounters[i]++;
			}
		}

		
		public void Tick(bool spawnNew = true)
		{
			var newFishCounters = new long[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			var valueAt0 = _fishCounters[0];
			for(int i = 0; i < 8; i++)
			{
				newFishCounters[i] = _fishCounters[i + 1];
			}

			if(spawnNew)
			{
				newFishCounters[8] = valueAt0; // each fish with internal counter 0 spawns a new one
				newFishCounters[6] += valueAt0; // fish with internal counter 0 get internal counter reset to 6
			}

			_fishCounters = newFishCounters;
		}


		public override string ToString()
		{
			return string.Join(',', _fishCounters);
		}


		public long FishCount => _fishCounters.Sum();
	}
	
	
	public static class Day6
	{
		private static long GetFishCount(List<int> input, int numberOfDays)
		{
			var flock = new FishFlock(input);
			int day = 0;
			do
			{
				flock.Tick(spawnNew: day > 0);
				day++;
			} while(day < numberOfDays);

			return flock.FishCount;
		}

		public static long Solve1(List<int> input)
		{
			return GetFishCount(input, 80);
		}


		public static long Solve2(List<int> input)
		{
			return GetFishCount(input, 256);
		}
	}
}