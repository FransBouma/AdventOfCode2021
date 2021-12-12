using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day12Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day12_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(10, Day12.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day12.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day12.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day12_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(36, Day12.Solve2(input, verboseOutput:true));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day12.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day12.Solve2(input));
		}
	}
}