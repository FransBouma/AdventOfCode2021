using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day21;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day21Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			Assert.AreEqual(739785, Day21.Solve1(4, 8, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			Console.WriteLine(Day21.Solve1(10, 3, verboseOutput:true));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			Assert.AreEqual(444356092776315, Day21.Solve2(4, 8));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			Console.WriteLine(Day21.Solve2(10, 3));
		}
	}
}