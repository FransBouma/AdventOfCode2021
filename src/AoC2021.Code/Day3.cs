using System;
using System.Collections.Generic;

namespace AoC2021.Core
{
	public static class Day3
	{
		public static int Solve1(List<string> input)
		{
			if(input.Count <= 0)
			{
				return 0;
			}
			
			int gamma = 0;
			int epsilon = 0;
			int amountBits = input[0].Length;
			for(int i = 0; i < input[0].Length; i++)
			{
				int amountOnes = 0;
				int amountZeros = 0;
				foreach(var binaryNumberAsString in input)
				{
					switch(binaryNumberAsString[i])
					{
						case '0':
							amountZeros++;
							break;
						case '1':
							amountOnes++;
							break;
					}
				}

				gamma += amountOnes > amountZeros ? (1 << (amountBits-i)-1) : 0;
				epsilon += amountOnes <= amountZeros ? (1 << (amountBits-i)-1) : 0;
			}
			// I know I can xor epsilon with a series of bits, but I don't know the length of the value, so it's easier to
			// simply calculate epsilon on the fly.
			return gamma * epsilon;
		}


		private class ValueInfo
		{
			public string BinaryValueAsString { get; set; }
			public int BinaryAsInt { get; set; }
		}
		
		
		public static int Solve2(List<string> input)
		{
			if(input.Count <= 0)
			{
				return 0;
			}
			
			int amountBits = input[0].Length;
			var oxygenRatingValues = new List<ValueInfo>();
			var co2scrubberRatingValues = new List<ValueInfo>();
			foreach(var s in input)
			{
				oxygenRatingValues.Add(new ValueInfo() { BinaryValueAsString = s, BinaryAsInt = 0});
				co2scrubberRatingValues.Add(new ValueInfo() { BinaryValueAsString = s, BinaryAsInt = 0});
			}
			
			for(int i = 0; i < input[0].Length; i++)
			{
				oxygenRatingValues = DetermineValuesLeft(oxygenRatingValues, amountBits, i, returnListWithMostCommon:true);
				co2scrubberRatingValues = DetermineValuesLeft(co2scrubberRatingValues, amountBits, i, returnListWithMostCommon:false);
			}
			
			return oxygenRatingValues[0].BinaryAsInt * co2scrubberRatingValues[0].BinaryAsInt;
		}


		private static List<ValueInfo> DetermineValuesLeft(List<ValueInfo> valuesToCheck, int amountBits, int bitIndex, bool returnListWithMostCommon)
		{
			// Always run through the loop, because we're converting the binary string to an int on the fly!
			int amountOnes = 0;
			int amountZeros = 0;
			var valuesWithOne = new List<ValueInfo>();
			var valuesWithZero = new List<ValueInfo>();
			foreach(var valueInfo in valuesToCheck)
			{
				switch(valueInfo.BinaryValueAsString[bitIndex])
				{
					case '0':
						amountZeros++;
						valuesWithZero.Add(valueInfo);
						break;
					case '1':
						amountOnes++;
						valueInfo.BinaryAsInt += 1 << ((amountBits - bitIndex) - 1);
						valuesWithOne.Add(valueInfo);
						break;
				}
			}
			// we don't change the output if the input has just 1 value. We do this here and not at the top because we had to convert the bit to int
			if(valuesToCheck.Count == 1)
			{
				// already found the value we're looking for.
				return valuesToCheck;
			}

			if(returnListWithMostCommon)
			{
				return amountOnes >= amountZeros ? valuesWithOne : valuesWithZero;
			}
			// return least common 
			return amountZeros <= amountOnes ? valuesWithZero : valuesWithOne;
		}
	}
}