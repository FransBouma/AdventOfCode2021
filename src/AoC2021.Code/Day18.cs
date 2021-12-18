using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2021.Core.Day18
{
	public class SnailfishNumber
	{
		public SnailfishNumber(int value)
		{
			this.Value = value;
			this.LeftOperand = null;
			this.RightOperand = null;
		}


		public SnailfishNumber(SnailfishNumber left, SnailfishNumber right)
		{
			this.Value = -1;
			this.LeftOperand = left;
			this.RightOperand = right;
		}


		public SnailfishNumber Clone()
		{
			return this.IsValueNode ? new SnailfishNumber(this.Value) : new SnailfishNumber(this.LeftOperand.Clone(), this.RightOperand.Clone());
		}


		public SnailfishNumber Add(SnailfishNumber toAdd)
		{
			var toReturn = new SnailfishNumber(this, toAdd);
			toReturn.Reduce();
			return toReturn;
		}
		
		
		public long GetMagnitude() => this.IsValueNode ? this.Value : this.LeftOperand.GetMagnitude() * 3 + this.RightOperand.GetMagnitude() * 2;


		public bool HandleSplit()
		{
			if(this.IsValueNode)
			{
				if(this.Value < 10)
				{
					return false;
				}
				this.LeftOperand = new SnailfishNumber(this.Value / 2);
				this.RightOperand = new SnailfishNumber(this.Value / 2 + (this.Value % 2));
				return true;
			}
			
			return this.LeftOperand.HandleSplit() || this.RightOperand.HandleSplit();
		}


		public (int left, int right, bool actionTaken) HandleExplode(int depth)
		{
			if(this.IsValueNode)
			{
				return (0, 0, false);
			}

			if(depth == 4)
			{
				var leftValue = LeftOperand.Value;
				var rightValue = RightOperand.Value;
				this.Value = 0;
				this.LeftOperand = null;
				this.RightOperand = null;
				return (leftValue, rightValue, true);
			}
			// depth < 4
			(int lReturn, int rReturn, bool aReturn) toReturn = this.LeftOperand.HandleExplode(depth + 1);
			RightOperand.AddToLeft(toReturn.rReturn);
			toReturn.rReturn = 0;
			if(toReturn.aReturn)
			{
				// action taken
				return toReturn;
			}

			var (rl, rr, ra) = RightOperand.HandleExplode(depth + 1);
			LeftOperand.AddToRight(rl);
			toReturn.rReturn += rr;
			toReturn.aReturn |= ra;
			return toReturn;
		}


		private void AddToLeft(int toAdd)
		{
			if(this.IsValueNode)
			{
				this.Value += toAdd;
				return;
			}
			this.LeftOperand.AddToLeft(toAdd);
		}


		private void AddToRight(int toAdd)
		{
			if(this.IsValueNode)
			{
				this.Value += toAdd;
				return;
			}
			this.RightOperand.AddToRight(toAdd);
		}


		public void Reduce(bool verboseOutput=false)
		{
			bool done = false;
			while(!done)
			{
				bool actionTaken = false;
				var (left, right, a) = HandleExplode(0);
				actionTaken |= a;
				string actionDesc = "None";
				if(actionTaken)
				{
					actionDesc = "Explode";
				}
				else
				{
					actionTaken = HandleSplit();
					if(actionTaken)
					{
						actionDesc = "Split";
					}
				}
				if(verboseOutput)
				{
					Console.WriteLine("Action taken: {0}. New number: {1}", actionDesc, this.ToString());
				}
				done = !actionTaken;
			}
		}


		public override string ToString()
		{
			if(this.IsValueNode)
			{
				return this.Value.ToString();
			}
			var sb = new StringBuilder();
			sb.Append("[");
			sb.Append(LeftOperand);
			sb.Append(",");			
			sb.Append(RightOperand);
			sb.Append("]");
			return sb.ToString();
		}

		public bool IsValueNode => this.LeftOperand == null && this.RightOperand == null;
		public int Value { get; set; }
		public SnailfishNumber LeftOperand { get; set; }
		public SnailfishNumber RightOperand { get; set; }
	}


	public class SnailfishNumberParser
	{
		private Stack<object> _shiftReducer;
		private string _toParse;

		public SnailfishNumberParser()
		{
			_shiftReducer = new Stack<object>();
			_toParse = string.Empty;
		}


		public SnailfishNumber Parse(string toParse)
		{
			_shiftReducer.Clear();
			_toParse = toParse;
			for(int i = 0; i < _toParse.Length; i++)
			{
				switch(_toParse[i])
				{
					case '[':
						// shift 
						_shiftReducer.Push(new SnailfishNumber(-1));
						break;
					case ',':
						// nothing to do, there are only single digits
						break;
					case ']':
						// reduce the top of the stack into a single number
						ReduceStack();
						break;
					default:
						// digit, shift
						_shiftReducer.Push(new SnailfishNumber(_toParse[i]-'0'));
						break;
				}
			}
			// at the top of the stack is our number
			return (SnailfishNumber)_shiftReducer.Pop();
		}


		private void ReduceStack()
		{
			// pop the top 2 elements and add them as the operands of the element then on top of the stack which should be a snailfishnumber
			var rightOperand = _shiftReducer.Pop();
			var leftOperand = _shiftReducer.Pop();
			var sfn = (SnailfishNumber)_shiftReducer.Peek();
			sfn.LeftOperand = leftOperand as SnailfishNumber;
			sfn.RightOperand = rightOperand as SnailfishNumber;
		}
	}
	
	
	public static class Day18
	{
		public static string SingleExplodeTest(string input)
		{
			var p = new SnailfishNumberParser();
			var n = p.Parse(input);
			Console.WriteLine(n.ToString());
			n.HandleExplode(0);
			var toReturn = n.ToString();
			Console.WriteLine(toReturn);
			return toReturn;
		}


		public static long MagnitudeTest(string input)
		{
			var p = new SnailfishNumberParser();
			var n = p.Parse(input);
			Console.WriteLine(n.ToString());
			return n.GetMagnitude();
		}


		public static string ReduceTest(string input)
		{
			var p = new SnailfishNumberParser();
			var n = p.Parse(input);
			Console.WriteLine(n.ToString());
			n.Reduce(true);
			var toReturn = n.ToString();
			return toReturn;
		}
		
		
		public static long Solve1(List<string> input, bool verboseOutput=false)
		{
			var p = new SnailfishNumberParser();
			var numbers = input.Select(l => p.Parse(l)).ToList();

			var currentNumber = numbers[0];
			for(int i = 1; i < numbers.Count; i++)
			{
				currentNumber = currentNumber.Add(numbers[i]);
				if(verboseOutput)
				{
					Console.WriteLine(currentNumber.ToString());
				}
			}
			
			return currentNumber.GetMagnitude();
		}


		public static long Solve2(List<string> input, bool verboseInput=false)
		{
			var p = new SnailfishNumberParser();
			var numbers = input.Select(l => p.Parse(l)).ToList();

			long maxMagnitude = 0;
			foreach(var n1 in numbers)
			{
				foreach(var n2 in numbers)
				{
					if(n1 == n2)
					{
						continue;
					}
					var n1Clone = n1.Clone();
					var n2Clone = n2.Clone();
					n2Clone = n2Clone.Add(n1Clone);
					var magnitude = n2Clone.GetMagnitude();
					if(verboseInput)
					{
						Console.WriteLine(" {0}\n+{1}\n={2}\nwith magnitude: {3}", n1, n2, n2Clone, magnitude);
					}
					maxMagnitude = Math.Max(maxMagnitude, magnitude);
				}
			}
			return maxMagnitude;
		}
	}
}