using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day13;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day13Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsDay13Paper("..\\..\\..\\PuzzleInputs\\day13_example.txt");
			Assert.IsTrue(input.IsValid);
			Assert.AreEqual(17, Day13.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsDay13Paper("..\\..\\..\\PuzzleInputs\\day13.txt");
			Assert.IsTrue(input.IsValid);
			Console.WriteLine(Day13.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsDay13Paper("..\\..\\..\\PuzzleInputs\\day13_example.txt");
			Assert.IsTrue(input.IsValid);
			Assert.AreEqual(195, Day13.Solve2(input, verboseOutput:true));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsDay13Paper("..\\..\\..\\PuzzleInputs\\day13.txt");
			Assert.IsTrue(input.IsValid);
			Console.WriteLine(Day13.Solve2(input, verboseOutput:true));
		}
	}
}