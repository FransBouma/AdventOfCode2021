using System;
using System.IO;
using AoC2021.Core.Day17;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day17Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		[TestCase("target area: x=20..30, y=-10..-5", 45)]
		public void Puzzle1_ExampleInput(string input, int output)
		{
			Assert.AreEqual(output, Day17.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		[TestCase("target area: x=137..171, y=-98..-73")]
		public void Puzzle1_Solver(string input)
		{
			Console.WriteLine(Day17.Solve1(input));
		}
		

		[Test]
		[TestCase("target area: x=20..30, y=-10..-5", 112)]
		public void Puzzle2_ExampleInput(string input, int output)
		{
			Assert.AreEqual(output, Day17.Solve2(input));
		}
		
		
		[Test]
		[TestCase("target area: x=137..171, y=-98..-73")]
		public void Puzzle2_Solver(string input)
		{
			Console.WriteLine(Day17.Solve2(input));
		}
	}
}