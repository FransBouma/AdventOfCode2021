using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day2Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(150, Day2.Solve1(input));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day2.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(900, Day2.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day2.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day2.Solve2(input));
		}
	}
}