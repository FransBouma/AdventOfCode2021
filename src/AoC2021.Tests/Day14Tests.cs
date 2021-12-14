using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day14;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day14Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsDay14PolymerProducer("..\\..\\..\\PuzzleInputs\\day14_example.txt");
			Assert.IsTrue(input.IsValid);
			Assert.AreEqual(1588, Day14.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsDay14PolymerProducer("..\\..\\..\\PuzzleInputs\\day14.txt");
			Assert.IsTrue(input.IsValid);
			Console.WriteLine(Day14.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsDay14PolymerProducer("..\\..\\..\\PuzzleInputs\\day14_example.txt");
			Assert.IsTrue(input.IsValid);
			Assert.AreEqual(2188189693529, Day14.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsDay14PolymerProducer("..\\..\\..\\PuzzleInputs\\day14.txt");
			Assert.IsTrue(input.IsValid);
			Console.WriteLine(Day14.Solve2(input));
		}
	}
}