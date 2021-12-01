using System.Linq;

namespace AoC2021.Core
{
	public class PasswordEntry
	{
		public int Value1 { get; set; }
		public int Value2 { get; set; }
		public char CheckChar { get; set; }
		public string Password { get; set; }


		public bool IsValidForPuzzle1()
		{
			if(string.IsNullOrEmpty(this.Password))
			{
				return false;
			}

			int amount = this.Password.Count(c => c == this.CheckChar);
			return amount >= this.Value1 && amount <= this.Value2;
		}

		
		public bool IsValidForPuzzle2()
		{
			if(string.IsNullOrEmpty(this.Password))
			{
				return false;
			}

			if(this.Value1 < 1 || this.Value1 > this.Password.Length || this.Value2 < 1 || this.Value2 > this.Password.Length)
			{
				return false;
			}

			return (this.Password[this.Value1-1] == this.CheckChar && this.Password[this.Value2-1] != this.CheckChar) ||
				   (this.Password[this.Value1-1] != this.CheckChar && this.Password[this.Value2-1] == this.CheckChar);
		}
	}
}