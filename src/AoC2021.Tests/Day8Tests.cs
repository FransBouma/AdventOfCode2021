using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day8Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(26, Day8.Solve1(input));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day8.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(61229, Day8.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day8.Solve2(input));
		}
	}
}