using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Core.Day16
{
	public class Packet
	{
		private readonly List<Packet> _containingPackets;

		public Packet()
		{
			_containingPackets = new List<Packet>();
			this.Version = -1;
			this.TypeID = -1;
			this.PayloadAsHex = string.Empty;
		}


		public void Display(int tabCount=0)
		{
			Console.WriteLine("{0}Packet seen: Version: {1}, TypeID: {2}", new string('\t', tabCount), this.Version, this.TypeID);
			if(!string.IsNullOrWhiteSpace(this.PayloadAsHex))
			{
				Console.WriteLine("{0}Payload as hex: '{1}'", new string('\t', tabCount), this.PayloadAsHex);
			}

			if(this.ContainingPackets.Count > 0)
			{
				Console.WriteLine("{0}Contains # of packets: {0}", new string('\t', tabCount), this.ContainingPackets.Count);
				foreach(var p in this.ContainingPackets)
				{
					p.Display(tabCount+1);
				}
			}
		}


		public long Eval()
		{
			switch(this.TypeID)
			{
				case 0:
					return this.ContainingPackets.Select(p => p.Eval()).Sum();
				case 1:
					return this.ContainingPackets.Aggregate(1L, (current, p) => current * p.Eval());
				case 2:
					return this.ContainingPackets.Select(p => p.Eval()).Min();
				case 3:
					return this.ContainingPackets.Select(p => p.Eval()).Max();
				case 4:
					return Convert.ToInt64(this.PayloadAsHex, 16);
				case 5:
					return this.ContainingPackets[0].Eval() > this.ContainingPackets[1].Eval() ? 1 : 0;
				case 6:
					return this.ContainingPackets[0].Eval() < this.ContainingPackets[1].Eval() ? 1 : 0;
				case 7:
					return this.ContainingPackets[0].Eval() == this.ContainingPackets[1].Eval() ? 1 : 0;
				default:
					return -1;
			}
		}


		public int GetVersionTotal()
		{
			return this.ContainingPackets.Count == 0 ? this.Version : this.Version + this.ContainingPackets.Select(p => p.GetVersionTotal()).Sum();
		}
		

		public string PayloadAsHex { get; set; }
		public List<Packet> ContainingPackets => _containingPackets;
		public int Version { get; set; }
		public int TypeID { get; set; }
	}


	public class HexParser
	{
		private List<char> _bitArray;
		private readonly string _hexSource;
		private Packet _packetSeen;
		private int _currentBit;

		public HexParser(string toParse)
		{
			_bitArray = new List<char>();
			_hexSource = toParse.ToUpperInvariant();
			// convert first to a string of 0 and 1 chars, preserving leading zeros.
			_bitArray = string.Join(string.Empty, _hexSource.Select(c =>Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')))
							  .Select(c=>c).ToList();
		}


		public Packet Parse()
		{
			_packetSeen = null;
			_currentBit = 0;
			_packetSeen = RecognizePacket();
			return _packetSeen;
		}


		public void Display()
		{
			Console.WriteLine("Hex source: '{0}'", _hexSource);
			Console.WriteLine("Binary array:\n{0}", string.Join(string.Empty, _bitArray));
		}


		private Packet RecognizePacket()
		{
			var toReturn = new Packet();

			// packet is 
			// 3 bits version | 3 bits ID | payload
			toReturn.Version = BitsToInt(3);
			_currentBit += 3;
			toReturn.TypeID = BitsToInt(3);
			_currentBit += 3;
			// based on typeid we'll handle the payload
			switch(toReturn.TypeID)
			{
				case 4:
					ParseLiteralPayload(toReturn);
					break;
				default:
					ParseOperatorPayload(toReturn);
					break;
			}
			return toReturn;
		}


		private void ParseOperatorPayload(Packet currentPacket)
		{
			Func<int, bool> stopClause = null;
			if(_bitArray[_currentBit] == '0')
			{
				// length is # of bits
				_currentBit++;
				var bitLength = BitsToInt(15);
				_currentBit += 15;
				var endIndex = _currentBit + bitLength;
				stopClause = (n) => _currentBit >= endIndex;
			}
			else
			{
				// length is # of packets
				_currentBit++;
				var numberOfPackets = BitsToInt(11);
				_currentBit += 11;
				stopClause = (n) => n >= numberOfPackets;
			}
			
			while(!stopClause(currentPacket.ContainingPackets.Count))
			{
				currentPacket.ContainingPackets.Add(RecognizePacket());
			}
		}


		private void ParseLiteralPayload(Packet currentPacket)
		{
			// blocks of 5 bytes, first byte tells if there's more to follow or not.
			bool lastBlockSeen = false;
			while(!lastBlockSeen && _currentBit < _bitArray.Count)
			{
				lastBlockSeen = _bitArray[_currentBit] == '0';
				_currentBit++;
				// append as hex char to currentPacket
				var nibble = BitsToInt(4);
				currentPacket.PayloadAsHex += Convert.ToString(nibble, 16);
				_currentBit += 4;
			}
		}


		private int BitsToInt(int length)
		{
			int toReturn = 0;
			for(int i = 0;i<length;i++)
			{
				if(_bitArray[_currentBit + i] =='1')
				{
					toReturn += 1 << ((length - 1) - i);
				}
			}
			return toReturn;
		}
	}
	
	
	public static class Day16
	{
		public static Packet Parse(string input, bool verboseOutput=false)
		{
			var p = new HexParser(input);
			if(verboseOutput)
			{
				p.Display();
			}

			var packet = p.Parse();
			if(verboseOutput)
			{
				packet.Display();
			}
			return packet;
		}

		
		public static int Solve1(string input, bool verboseOutput = false)
		{
			return Parse(input, verboseOutput).GetVersionTotal();
		}
		
		
		public static long Solve2(string input)
		{
			return Parse(input).Eval();
		}
	}

}