using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day6Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day6_example.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(5934, Day6.Solve1(input));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day6.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day6.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day6_example.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(26984457539, Day6.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day6.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day6.Solve2(input));
		}
	}
}