using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2021.Core.Day14Classes
{
	public class PolymerProducer
	{
		private string _template;
		private Dictionary<string, long> _occurrenceCountPerPair;
		private Dictionary<char, long> _occurrenceCountPerChar;
		private Dictionary<string, char> _pairInsertionRules;

		public PolymerProducer()
		{
			_template = string.Empty;
			_occurrenceCountPerPair = new Dictionary<string, long>();
			_pairInsertionRules = new Dictionary<string, char>();
			_occurrenceCountPerChar = new Dictionary<char, long>();
		}

		public void SetTemplate(string template)
		{
			_template = template;
			for(int i = 0; i < _template.Length - 1; i++)
			{
				CountPair(_occurrenceCountPerPair, _template.Substring(i, 2));
			}
		}


		public void AddPairInsertionRule(string pair, char toInsert)
		{
			_pairInsertionRules[pair] = toInsert;
		}


		public void Step()
		{
			var newOccurrenceCountPerPair = new Dictionary<string, long>();
			var newOccurrenceCountPerChar = GetCounterDictionary();
			var newPair = new char[2];
			foreach(var kvp in _occurrenceCountPerPair)
			{
				var pair = kvp.Key;
				var amount = kvp.Value;
				var toInsert = _pairInsertionRules[pair];
				newPair[0] = pair[0];
				newPair[1] = toInsert;
				CountPair(newOccurrenceCountPerPair, new string(newPair), amount);
				newPair[0] = toInsert;
				newPair[1] = pair[1];
				CountPair(newOccurrenceCountPerPair, new string(newPair), amount);
				newOccurrenceCountPerChar[toInsert]+=amount;
				newOccurrenceCountPerChar[pair[1]]+=amount;
			}
			newOccurrenceCountPerChar[_template[0]]++;		// we skipped all first characters, but the first pair has its first character inserted
			_occurrenceCountPerPair.Clear();
			foreach(var kvp in newOccurrenceCountPerPair)
			{
				_occurrenceCountPerPair[kvp.Key] = kvp.Value;
			}
			_occurrenceCountPerChar.Clear();
			foreach(var kvp in newOccurrenceCountPerChar)
			{
				_occurrenceCountPerChar[kvp.Key] = kvp.Value;
			}
			
		}


		private Dictionary<char, long> GetCounterDictionary()
		{
			var toReturn = new Dictionary<char, long>();
			foreach(var c in _pairInsertionRules.Values.Distinct())
			{
				toReturn[c] = 0;
			}
			return toReturn;
		}


		public void Display(bool displayRules)
		{
			Console.WriteLine("Template: {0}", _template);
			if(displayRules)
			{
				Console.WriteLine("Rules:");
				foreach(var kvp in _pairInsertionRules)
				{
					Console.WriteLine("{0} -> {1}", kvp.Key, kvp.Value);
				}
			}

			if(_occurrenceCountPerPair.Count > 0)
			{
				Console.WriteLine("Counters: ");
				foreach(var kvp in _occurrenceCountPerPair)
				{
					Console.WriteLine("Pair: {0} occured {1} times in the polymer", kvp.Key, kvp.Value);
				}
			}
		}


		private void CountPair(Dictionary<string, long> counters, string pairToCount, long amount = 1)
		{
			if(!counters.ContainsKey(pairToCount))
			{
				counters[pairToCount] = amount;
				return;
			}
			counters[pairToCount]+=amount;
		}
		

		public bool IsValid => !string.IsNullOrWhiteSpace(_template) && _pairInsertionRules.Count > 0;
		public Dictionary<char, long> OccurrenceCountPerChar => _occurrenceCountPerChar;
	}
}