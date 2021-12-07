using System;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day7Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day7_example.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(37, Day7.Solve1(input));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day7.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day7.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day7_example.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(168, Day7.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsIntList("..\\..\\..\\PuzzleInputs\\day7.txt", commaSeparated:true);
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day7.Solve2(input));
		}
	}
}