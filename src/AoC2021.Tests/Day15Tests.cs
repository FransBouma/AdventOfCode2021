using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day15;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day15Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day15_example.txt");
			Assert.IsTrue(input.Length>0);
			Assert.AreEqual(40, Day15.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day15.txt");
			Assert.IsTrue(input.Length>0);
			Console.WriteLine(Day15.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day15_example.txt");
			Assert.IsTrue(input.Length>0);
			Assert.AreEqual(315, Day15.Solve2(input, verboseOutput: true));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day15.txt");
			Assert.IsTrue(input.Length>0);
			Console.WriteLine(Day15.Solve2(input));
		}
	}
}