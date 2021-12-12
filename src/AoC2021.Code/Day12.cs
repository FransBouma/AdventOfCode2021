using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2021.Core
{
	public enum VertexType
	{
		Undefined,
		StartNode,
		EndNode,
		NormalNode
	}

	public class Vertex
	{
		public Vertex(string vertexText, bool isLowerCase)
		{
			this.VertexText = vertexText;
			this.IsLowerCase = isLowerCase;
		}


		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
			{
				return false;
			}
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			if(obj.GetType() != this.GetType())
			{
				return false;
			}

			return this.VertexText == ((Vertex)obj).VertexText;
		}

		public override int GetHashCode()
		{
			return (this.VertexText != null ? this.VertexText.GetHashCode() : 0);
		}


		public override string ToString()
		{
			return this.VertexText;
		}


		public string VertexText { get; set; }
		public VertexType VertexType { get; set; }
		public bool IsLowerCase { get; set; }
	}

	
	public class Graph
	{
		private MultiValueDictionary<Vertex, Vertex> _adjacentVerticesPerVertex;
		private Dictionary<string, Vertex> _nodeTextToVertex;
		private Vertex _start, _end;

		public Graph()
		{
			_adjacentVerticesPerVertex = new MultiValueDictionary<Vertex, Vertex>();
			_nodeTextToVertex = new Dictionary<string, Vertex>();
			_start = null;
			_end = null;
		}


		public void AddEdge(string edgeSpecification)
		{
			var fragments = edgeSpecification.Split("-");
			var startVertex = GetOrAddVertex(fragments[0]);
			var endVertex = GetOrAddVertex(fragments[1]);
			// now add the edge for both as we don't know if the edge is part of a cycle so cutting an edge prematurely isn't correct
			_adjacentVerticesPerVertex.Add(startVertex, endVertex);
			_adjacentVerticesPerVertex.Add(endVertex, startVertex);
		}


		public int FindAllPathsFor1(bool verboseOutput=false)
		{
			var paths = new List<List<Vertex>>() { new() {_start}};		// every entry is a list of vertices we've collected till then
			VisitVertices(paths, _start);
			if(verboseOutput)
			{
				foreach(var p in paths)
				{
					Console.WriteLine(string.Join(", ", p.Select(v => v.VertexText)));
				}
			}

			return paths.Count;
		}


		public int FindAllPathsFor2(bool verboseOutput=false)
		{
			var paths = new List<List<Vertex>>() { new() {_start}};		// every entry is a list of vertices we've collected till then
			VisitVertices(paths, _start, canVisitOneSmallCaveOnce:true);
			if(verboseOutput)
			{
				foreach(var p in paths)
				{
					Console.WriteLine(string.Join(", ", p.Select(v => v.VertexText)));
				}
			}

			return paths.Count;
		}


		private void VisitVertices(List<List<Vertex>> paths, Vertex visiting, bool canVisitOneSmallCaveOnce=false)
		{
			if(visiting.VertexType == VertexType.EndNode)
			{
				// done
				return;
			}
			// toVisit is added to all paths we have till now. For all adjacent vertices we create new path, by copying each path we have and add the adjacent node to it.
			// copy paths if it's still valid
			var pathsResult = new List<List<Vertex>>();
			foreach(var av in _adjacentVerticesPerVertex.GetValues(visiting, true))
			{
				var newPaths = new List<List<Vertex>>();
				foreach(var p in paths)
				{
					var pCopy = new List<Vertex>(p);
					if(av.IsLowerCase && p.Contains(av))
					{
						if(av.VertexType != VertexType.NormalNode || !canVisitOneSmallCaveOnce)
						{
							// never visit start nor end twice, or already a small cave and not start/end in the path, not allowed
							continue;
						}
						var q = from v in p.Where(v => v.VertexType == VertexType.NormalNode && v.IsLowerCase)
								group v by v.VertexText
								into g
								select new { g.Key, Amount= g.Count() };
						if(q.Any(g => g.Amount == 2))
						{
							continue;
						}
					}
					pCopy.Add(av);
					newPaths.Add(pCopy);
				}
				if(newPaths.Count > 0)
				{
					VisitVertices(newPaths, av, canVisitOneSmallCaveOnce);
					pathsResult.AddRange(newPaths);
				}
			}
			paths.Clear();
			paths.AddRange(pathsResult);
		}


		private Vertex GetOrAddVertex(string vertexText)
		{
			var toReturn = _nodeTextToVertex.GetValue(vertexText);
			if(toReturn is object)
			{
				return toReturn;
			}

			toReturn = new Vertex(vertexText, vertexText == vertexText.ToLowerInvariant());
			_nodeTextToVertex.Add(vertexText, toReturn);
			var vertexType = VertexType.Undefined;
		
			switch(vertexText)
			{
				case "start":
					vertexType = VertexType.StartNode;
					_start = toReturn;
					break;
				case "end":
					vertexType = VertexType.EndNode;
					_end = toReturn;
					break;
				default:
					vertexType = VertexType.NormalNode;
					break;
			}

			toReturn.VertexType = vertexType;
			return toReturn;
		}


		public void Display()
		{
			Console.WriteLine("Start: {0}. End: {1}", _start.VertexText, _end.VertexText);
			foreach(var v in _nodeTextToVertex.Values)
			{
				var edges = _adjacentVerticesPerVertex.GetValues(v, true);
				Console.WriteLine("Vertex: {0}", v.VertexText);
				foreach(var av in edges)
				{
					Console.WriteLine("\t{0}", av.VertexText);
				}
			}
		}
	}


	public static class Day12
	{
		public static long Solve1(List<string> input, bool verboseOutput = false)
		{
			var g = new Graph();
			foreach(var e in input)
			{
				g.AddEdge(e);
			}

			if(verboseOutput)
			{
				g.Display();
			}

			return g.FindAllPathsFor1(verboseOutput);
		}


		public static long Solve2(List<string> input, bool verboseOutput = false)
		{
			var g = new Graph();
			foreach(var e in input)
			{
				g.AddEdge(e);
			}
			return g.FindAllPathsFor2(verboseOutput);
		}
	}
}