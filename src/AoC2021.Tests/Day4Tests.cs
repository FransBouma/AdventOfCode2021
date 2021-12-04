using System;
using System.Diagnostics;
using System.IO;
using AoC2021.Core;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day4Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsDay4PuzzleInput("..\\..\\..\\PuzzleInputs\\day4_example.txt");
			Assert.IsTrue(input.DrawnNumbers.Count > 0);
			Assert.IsTrue(input.BingoCards.Count > 0);
			Assert.AreEqual(4512, Day4.Solve1(input));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsDay4PuzzleInput("..\\..\\..\\PuzzleInputs\\day4.txt");
			Assert.IsTrue(input.DrawnNumbers.Count > 0);
			Assert.IsTrue(input.BingoCards.Count > 0);
			Console.WriteLine(Day4.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsDay4PuzzleInput("..\\..\\..\\PuzzleInputs\\day4_example.txt");
			Assert.IsTrue(input.DrawnNumbers.Count > 0);
			Assert.IsTrue(input.BingoCards.Count > 0);
			Assert.AreEqual(1924, Day4.Solve2(input));
		}

		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsDay4PuzzleInput("..\\..\\..\\PuzzleInputs\\day4.txt");
			Assert.IsTrue(input.DrawnNumbers.Count > 0);
			Assert.IsTrue(input.BingoCards.Count > 0);
			Console.WriteLine(Day4.Solve2(input));
		}

		
		[Test]
		public void Puzzle2_Big_Solver()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var input = InputReader.GetInputAsDay4PuzzleInput("..\\..\\..\\PuzzleInputs\\day4_big.txt");
			Assert.IsTrue(input.DrawnNumbers.Count > 0);
			Assert.IsTrue(input.BingoCards.Count > 0);
			// solution should print: 3074334900
			Console.WriteLine(Day4.Solve2(input));
			sw.Stop();
			Console.WriteLine("Test took: {0}ms", sw.ElapsedMilliseconds);
		}
	}
}