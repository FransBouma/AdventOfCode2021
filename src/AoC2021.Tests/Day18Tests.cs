using System;
using System.IO;
using AoC2021.Core;
using AoC2021.Core.Day18;
using NUnit.Framework;

namespace AoC2021.Tests
{
	public class Day18Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		[TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
		[TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
		[TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
		[TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
		[TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
		[TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]")]
		[TestCase("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
		[TestCase("[[[[4,0],[5,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]", "[[[[4,0],[5,4]],[[0,[7,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]")]
		public void Puzzle1_SingleExplodeTest(string input, string output)
		{
			Assert.AreEqual(output, Day18.SingleExplodeTest(input));
		}


		[Test]
		[TestCase("[[1,2],[[3,4],5]]", 143)]
		[TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
		[TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
		[TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
		[TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
		[TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
		public void Puzzle1_MagnitudeTest(string input, long output)
		{
			Assert.AreEqual(output, Day18.MagnitudeTest(input)); 
		}


		[Test]
		[TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
		[TestCase("[[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]", "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
		public void Puzzle1_ReduceTest(string input, string output)
		{
			Assert.AreEqual(output, Day18.ReduceTest(input));
		}
		

		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day18_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(4140, Day18.Solve1(input, verboseOutput:true));
		}

		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day18.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day18.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day18_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(3993, Day18.Solve2(input, verboseInput:true));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day18.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day18.Solve2(input));
		}
	}
}