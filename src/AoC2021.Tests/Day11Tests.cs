using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day11;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day11Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day11_example.txt");
			Assert.IsTrue(input.Length>0);
			Assert.AreEqual(1656, Day11.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day11.txt");
			Assert.IsTrue(input.Length>0);
			Console.WriteLine(Day11.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day11_example.txt");
			Assert.IsTrue(input.Length>0);
			Assert.AreEqual(195, Day11.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day11.txt");
			Assert.IsTrue(input.Length>0);
			Console.WriteLine(Day11.Solve2(input));
		}
	}
}