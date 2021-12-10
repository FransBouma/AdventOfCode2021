using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day10Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(26397, Day10.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day10.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(288957, Day10.Solve2(input, verboseOutput:true));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day10.Solve2(input));
		}
	}
}