using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day20;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day20Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day20_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(35, Day20.Solve1(input, verboseInput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day20.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day20.Solve1(input, verboseInput:true));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day20_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(3351, Day20.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day20.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day20.Solve2(input));
		}
	}
}