using System.Collections.Generic;
using System.IO;

namespace AoC2021.Core
{
	public static class InputReader
	{
		public static List<int> GetInputAsIntList(string pathFilename)
		{
			var toReturn = new List<int>();
			using(var reader = File.OpenText(pathFilename))
			{
				while(!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if(!string.IsNullOrEmpty(line))
					{
						toReturn.Add(int.Parse(line));
					}
				}
			}
			return toReturn;
		}


		public static List<PasswordEntry> GetInputAsPasswordEntries(string pathFilename)
		{
			var toReturn = new List<PasswordEntry>();
			using(var reader = File.OpenText(pathFilename))
			{
				while(!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if(string.IsNullOrEmpty(line))
					{
						continue;
					}
					var entry = new PasswordEntry();
					toReturn.Add(entry);
					var fragments = line.Split(':');
					entry.Password = fragments[1].Trim();
					var policyFragments = fragments[0].Split(' ');
					entry.CheckChar = policyFragments[1][0];
					var quantityFragments = policyFragments[0].Split('-');
					entry.Value1 = int.Parse(quantityFragments[0]);
					entry.Value2 = int.Parse(quantityFragments[1]);
				}
			}
			return toReturn;
		}
	}
}