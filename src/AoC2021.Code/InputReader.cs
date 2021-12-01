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
	}
}