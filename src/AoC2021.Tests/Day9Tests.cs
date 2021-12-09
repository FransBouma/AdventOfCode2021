using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day9Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day9_example.txt");
			Assert.IsTrue(input.Length>0);
			Assert.AreEqual(15, Day9.Solve1(input));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day9.txt");
			Assert.IsTrue(input.Length>0);
			Console.WriteLine(Day9.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day9_example.txt");
			Assert.IsTrue(input.Length>0);
			Assert.AreEqual(1134, Day9.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAs2DIntArrayList("..\\..\\..\\PuzzleInputs\\day9.txt");
			Assert.IsTrue(input.Length>0);
			Console.WriteLine(Day9.Solve2(input));
		}
	}
}