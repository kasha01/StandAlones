using System;
using System.Collections.Generic;

namespace Algorithms.GraphTheory
{
	class HeldKarp_TravellingSalesman
	{
		int[] nodes = null;
		int sz = 0;
		public HeldKarp_TravellingSalesman(int vertexCount)
		{
			sz = vertexCount;
			nodes = new int[vertexCount];
			for (int i = 0; i < vertexCount; i++) { nodes[i] = i; }
		}

		public void HeldKarp(int[,] dist)
		{
			//var listofSubSets = printSubsets2(nodes);
			var listofSubSets = getSubSets(nodes);

			// Dictionary of ((target Node - Subsets)/MinDPRoute that I can go thru to reach target Node) - cost.
			// gives me, cost of the route going to a target via a designated subset (defined in MinDPRoute constructor)
			Dictionary<MinDPRoute, int> dic2 = new Dictionary<MinDPRoute, int>(new FooEqualityComparer());

			// Dictionary of ((target Node - Subsets)/MinDPRoute that I can go thru to reach target Node) - intermediate/parent node right before reaching target.
			Dictionary<MinDPRoute, int> parent = new Dictionary<MinDPRoute, int>(new FooEqualityComparer());

			// skip start node = node 0 - loop thru nodes
			foreach (List<int> subset in listofSubSets)
			{
				// loop thru all target nodes:
				// **Finding min cost to go from source to target node via intermediate node(s) specified in the subset.
				// this is the essence of HeldKarp Algo.
				for (int targetN = 1; targetN < sz; targetN++)
				{
					// I am trying to find the min cost to go from source to target via the nodes in the subset, so if 
					// target exists in subset, skip
					if (subset.Contains(targetN))
						continue;

					int mincost = int.MaxValue;
					int minPreNode = 0;

					List<int> copyset = new List<int>(subset);
					MinDPRoute dp = null;
					// ind = route from source to targetN via this subset
					var ind = MinDPRoute.CreateDP(targetN, subset);

					// for each intermediate node in the subset picked.
					foreach (int prev in subset)
					{
						copyset.Remove(prev);

						dp = MinDPRoute.CreateDP(prev, copyset);
						// cost from source to targetN via intermediate prev = cost[prev,targetN] + cost of route from source
						// to prev via the subsets of {subset nodes - prev} since prev is the intermediate now.
						int cost = dist[prev, targetN] + dic2[dp];
						if (cost < mincost)
						{
							mincost = cost;
							minPreNode = prev;
						}

						copyset.Add(prev);
					}

					//for empty subset
					if (subset.Count == 0)
					{
						dp = new MinDPRoute() { targetNode = targetN, thrusubset = subset };
						mincost = dist[0, targetN]; // cost to reach target node N from 0 via an empty set. so essentially
													// dist[source,target] = dist[0,targetN]
					}

					// save min dist
					dic2.Add(ind, mincost);
					parent.Add(ind, minPreNode);
				}
			}


			// this part is exact same of the above, except here my target node (targetN) is the source (sales man going back
			// to source). Notice here I picked the last subset which has all the nodes (except source of course), so I am doing:
			// mincost = cost[prev,target=0] + cost from source to prev via subset(all nodes - prev).
			// if my subset is {1,2,3} => mincost = cost[3,0]+cost[0 to 3 via {1,2}] and so on for all nodes in the subsets
			int fmincost = int.MaxValue;
			int ftarget = 0;
			List<int> lastsubset = listofSubSets[listofSubSets.Count - 1];
			MinDPRoute fdp = null;
			var find = MinDPRoute.CreateDP(ftarget, lastsubset);

			int fminprevnode = 0;
			var fcopyset = new List<int>(lastsubset);
			foreach (int prev in lastsubset)
			{
				fcopyset.Remove(prev);

				fdp = MinDPRoute.CreateDP(prev, fcopyset);
				int cost = dist[prev, ftarget] + dic2[fdp];
				if (cost < fmincost)
				{
					fmincost = cost;
					fminprevnode = prev;
				}

				fcopyset.Add(prev);
			}

			// save min dist
			dic2.Add(find, fmincost);
			parent.Add(find, fminprevnode);

			Console.WriteLine("Shortest Travel Route Cost: " + fmincost);
			getRoute(parent, lastsubset);
		}

		private void getRoute(Dictionary<MinDPRoute, int> parent, List<int> thrusubset)
		{
			int target = 0;
			Stack<int> st = new Stack<int>();
			st.Push(target);
			while (true)
			{
				// get the intermediate/parent node (right before target) to reach target when passing from source via
				// subset of nodes{1,2,3}. just similar to floyedWarshall, this intermediate becomes my new targets, next 
				// while tick is to find the new intermidiate to reach "target" from source and so on until thrusubset is
				// empty which means (going from 0 to 0) in other words source==targets and salesman reach home again.
				target = parent[MinDPRoute.CreateDP(target, thrusubset)];
				st.Push(target);
				if (thrusubset.Count == 0)
					break;
				thrusubset.Remove(target);
			}

			while (st.Count > 1)
				Console.Write(st.Pop() + " -> ");

			Console.Write(st.Pop());

		}

		private List<MinDPRoute> printSubsets2(int[] set)
		{
			// in short the key is generate all binary numbers from (0) to (n-1), then loop through the bits of each number to see what character repsentation it
			// contains e.g. 100 - has the 3rd character in the set, 011, has the 1st and 2nd character in the set and so on
			int n = set.Length;
			//List<List<int>> result = new List<List<int>>();
			List<MinDPRoute> result = new List<MinDPRoute>();
			for (int i = 0; i < (1 << n); i++)
			{
				// i = index no of the set
				MinDPRoute setC = new MinDPRoute();

				for (int j = 0; j < n; j++)
					if ((i & (1 << j)) > 0)
						setC.thrusubset.Add(set[j]);

				result.Add(setC);
			}
			return result;
		}

		private List<List<int>> getSubSets(int[] set)
		{
			// in short the key is generate all binary numbers from (0) to (2^n -1), then loop through the bits of each number to see what character repsentation it
			// contains e.g. 100 - has the 3rd character in the set, 011, has the 1st and 2nd character in the set and so on
			int n = set.Length - 1;
			List<List<int>> result = new List<List<int>>();
			for (int i = 0; i < (1 << n); i++)
			{
				var l = new List<int>();

				for (int j = 0; j < n; j++)
					if ((i & (1 << j)) > 0)
						l.Add(set[j + 1]);  // I wanted to skip the source which is set[0]

				result.Add(l);
			}
			return result;
		}
	}

	internal class FooEqualityComparer : EqualityComparer<MinDPRoute>
	{
		public override bool Equals(MinDPRoute x, MinDPRoute y)
		{
			if (x.targetNode == y.targetNode && x.GetHashCode() == y.GetHashCode())
				return true;
			else
				return false;
		}

		public override int GetHashCode(MinDPRoute obj)
		{
			int tt = 0;
			foreach (var t in obj.thrusubset)
			{
				tt = t ^ tt;
			}
			int result = 31 * obj.targetNode + tt;
			return result;
		}
	}

	class MinDPRoute
	{
		// via zero by def
		public int targetNode;
		public List<int> thrusubset { get; set; }

		public MinDPRoute()
		{
			thrusubset = new List<int>();
		}
		public static MinDPRoute CreateDP(int t, List<int> l)
		{
			MinDPRoute i = new MinDPRoute();
			i.targetNode = t;
			i.thrusubset = l;
			return i;
		}

		public override int GetHashCode()
		{
			int tt = 0;
			foreach (var t in this.thrusubset)
			{
				tt = t ^ tt;
			}
			int result = 31 * this.targetNode + tt;
			return result;
		}
	}
}
