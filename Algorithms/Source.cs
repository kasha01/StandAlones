using System;
using System.Collections.Generic;
using Helper = Algorithms.HelpersDataStructures;
using GraphNS = Algorithms.GraphTheory;
using DP = Algorithms.DynamicProgramming;

namespace Algorithms
{
	class Source
	{
		static void Main(string[] args)
		{
			DP.LongestCommonSubSequence s = new DP.LongestCommonSubSequence("OldSite:GeeksforGeeks.org", "NewSite:GeeksQuiz.com");
			s.getCountOfLCSubstring();

			Console.ReadLine();
		}

		static int findoptimal(int N)
		{
			// The optimal string length is N when N is smaller than 7
			if (N <= 6)
				return N;

			// Initialize result
			int max = 0;

			// TRY ALL POSSIBLE BREAK-POINTS
			// For any keystroke N, we need to loop from N-3 keystrokes
			// back to 1 keystroke to find a breakpoint 'b' after which we
			// will have Ctrl-A, Ctrl-C and then only Ctrl-V all the way.
			int b;
			for (b = N - 3; b >= 1; b--)
			{
				// If the breakpoint is s at b'th keystroke then
				// the optimal string would have length
				// (n-b-1)*screen[b-1];
				int curr = (N - b - 1) * findoptimal(b);
				if (curr > max)
					max = curr;
			}
			return max;
		}

		static void HanoiTowerDriver()
		{
			var st = new Stack<int>();
			//st.Push(5); st.Push(4); st.Push(3); st.Push(2); st.Push(1);
			st.Push(5); st.Push(1);
			DP.HanoiTower t = new DP.HanoiTower(st);
			t.buildTower(st.Count);
		}

		static void KnapsackDriver()
		{
			int[] W = { 1, 3, 4, 5 };
			int[] V = { 1, 4, 5, 7 };
			int M = 7;

			DP.Knapsack sack = new DP.Knapsack(W, V, M);
			int result = sack.getMax_01Knapsack();
			Console.WriteLine("Max Value that I can fit in this Bag is: " + result);
		}

		static void KruskalDriver()
		{
			Algorithms.HelpersDataStructures.Node<int> n1 = new Algorithms.HelpersDataStructures.Node<int>(1);
			Algorithms.HelpersDataStructures.Node<int> n2 = new Algorithms.HelpersDataStructures.Node<int>(2);
			Algorithms.HelpersDataStructures.Node<int> n3 = new Algorithms.HelpersDataStructures.Node<int>(3);
			Algorithms.HelpersDataStructures.Node<int> n4 = new Algorithms.HelpersDataStructures.Node<int>(4);
			Algorithms.HelpersDataStructures.Node<int> n5 = new Algorithms.HelpersDataStructures.Node<int>(5);
			Algorithms.HelpersDataStructures.Node<int> n6 = new Algorithms.HelpersDataStructures.Node<int>(6);

			Algorithms.GraphTheory.Graph<int> graph = new Algorithms.GraphTheory.Graph<int>(6);

			graph.addNode(n1);
			graph.addNode(n2);
			graph.addNode(n3);
			graph.addNode(n4);
			graph.addNode(n5);
			graph.addNode(n6);

			graph.connectNodeAndCreateEdge(n1.index, n2.index, 3);
			graph.connectNodeAndCreateEdge(n1.index, n4.index, 1);
			graph.connectNodeAndCreateEdge(n2.index, n4.index, 3);
			graph.connectNodeAndCreateEdge(n2.index, n3.index, 1);
			graph.connectNodeAndCreateEdge(n3.index, n4.index, 1);
			graph.connectNodeAndCreateEdge(n3.index, n5.index, 5);
			graph.connectNodeAndCreateEdge(n3.index, n6.index, 4);
			graph.connectNodeAndCreateEdge(n4.index, n5.index, 6);
			graph.connectNodeAndCreateEdge(n5.index, n6.index, 2);

			graph.GetMinSpanTree_Kruskal();

		}

		static void Driver_PrimAlgo()
		{
			Algorithms.GraphTheory.Graph<char> g = new Algorithms.GraphTheory.Graph<char>(6);
			Helper.Node<char> na = new Helper.Node<char>('A');
			Helper.Node<char> nb = new Helper.Node<char>('B');
			Helper.Node<char> nc = new Helper.Node<char>('C');
			Helper.Node<char> nd = new Helper.Node<char>('D');
			Helper.Node<char> ne = new Helper.Node<char>('E');
			Helper.Node<char> nf = new Helper.Node<char>('F');

			g.addNode(na);
			g.addNode(nb);
			g.addNode(nc);
			g.addNode(nd);
			g.addNode(ne);
			g.addNode(nf);

			g.connnectNodeWithAdj(na, nd, 1);
			g.connnectNodeWithAdj(na, nb, 3);
			g.connnectNodeWithAdj(nb, nd, 3);
			g.connnectNodeWithAdj(nb, nc, 1);
			g.connnectNodeWithAdj(nc, nd, 1);
			g.connnectNodeWithAdj(nc, nf, 4);
			g.connnectNodeWithAdj(nc, ne, 5);
			g.connnectNodeWithAdj(nd, ne, 6);
			g.connnectNodeWithAdj(ne, nf, 2);

			g.GetMinSpanTree_Prim();

		}

		private static void DijkstraDriver()
		{
			int[,] graph = {
				{ 0, 4, 0, 0, 0, 0, 0, 8, 0 },
				{ 4, 0, 8, 0, 0, 0, 0, 11, 0 },
				{ 0, 8, 0, 7, 0, 4, 0, 0, 2 },
				{ 0, 0, 7, 0, 9, 14, 0, 0, 0 },
				{ 0, 0, 0, 9, 0, 10, 0, 0, 0 },
				{ 0, 0, 4, 0, 10, 0, 2, 0, 0 },
				{ 0, 0, 0, 14, 0, 2, 0, 1, 6 },
				{ 8, 11, 0, 0, 0, 0, 1, 0, 7 },
				{ 0, 0, 2, 0, 0, 0, 6, 7, 0 }
			};

			Algorithms.GraphTheory.ShortestPath.Dijkstra(graph, 0, 9);
			Console.WriteLine();
			Console.WriteLine("Shortest Path from 0 to 3 is:");
			Algorithms.GraphTheory.ShortestPath.PrintPathFromSToV(3);

			Console.WriteLine();
			Console.WriteLine("Now using Priority Queue");

			Algorithms.GraphTheory.ShortestPath.DijkstraWithPriorityQueue(graph, 0, 9);
			Console.WriteLine();
			Console.WriteLine("Shortest Path from 0 to 3 is:");
			Algorithms.GraphTheory.ShortestPath.PrintPathFromSToV(3);
		}

		static void Graph_DFS_BFS_Driver()
		{
			//Lets create nodes as given as an example in the article
			Helper.Node<char> nA = new Helper.Node<char>('A');
			Helper.Node<char> nB = new Helper.Node<char>('B');
			Helper.Node<char> nC = new Helper.Node<char>('C');
			Helper.Node<char> nD = new Helper.Node<char>('D');
			Helper.Node<char> nE = new Helper.Node<char>('E');
			Helper.Node<char> nF = new Helper.Node<char>('F');

			//Create the graph, add nodes, create edges between nodes
			GraphNS.Graph<char> g = new GraphNS.Graph<char>(6);
			g.addNode(nA);
			g.addNode(nB);
			g.addNode(nC);
			g.addNode(nD);
			g.addNode(nE);
			g.addNode(nF);
			g.SetRootNode(nA);

			g.connectNode(nA, nB);
			g.connectNode(nA, nC);
			g.connectNode(nA, nD);

			g.connectNode(nB, nE);
			g.connectNode(nB, nF);
			g.connectNode(nC, nF);

			//Perform the traversal of the graph

			//Console.WriteLine("DFS Traversal is: ");
			//g.DFS();

			//Console.WriteLine("DFS Traversal To find Count of Children for each Node: ");
			//g.myDFS(nA);

			Console.WriteLine("BFS Traversal is: ");
			g.BFS(nA);
		}

		static void Driver_HeldKarp()
		{
			var sk = new GraphNS.HeldKarp_TravellingSalesman(4);
			int[,] d = {
				{ 0, 1, 15, 6},
				{ 2,0,7,3 },
				{ 9,6,0,12 },
				{ 10,4,8,0 }
			};
			sk.HeldKarp(d);
		}

		#region Dyanmic Programming Drivers

		static void Driver_LongestIncreasingSubsequence()
		{
			// int[] arr = { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };
			// {{8,1},{1,2}, {4,3}, {3,4}, {5,5}, {2,6}, {6,7}, {7,8}}
			int[] arr = { 8, 1, 4, 3, 5, 2, 6, 7 };
			int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8 };
			DP.LongestIncreasingSubSequence lis = new DynamicProgramming.LongestIncreasingSubSequence(arr);
			lis.riverCrossingBridges(arr, arr2, arr.Length);
		}

		static void Driver_LongestCommonSubsequence()
		{
			DP.LongestCommonSubSequence lcs = new DynamicProgramming.LongestCommonSubSequence("ABCDGH", "AEDFHR");
			lcs.getCountOfLCSubsequence();
		}

		static void Driver_LevenshteinDistance()
		{
			DP.LevenshteinDistance lev = new DynamicProgramming.LevenshteinDistance("sunday", "saturday");
			lev.getLevenshteinDistance();
		}

		// Egg Dropping
		static void Driver_MinNoOfJumps()
		{
			int[] arr = { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 };
			DP.MinimumNumberOfJumpsToReachEndOfArray minjumps = new DynamicProgramming.MinimumNumberOfJumpsToReachEndOfArray(arr);
			minjumps.getMinNumberOfJumps();
		}

		// Longest Palindormic Subsequence
		static void Driver_LongestPalindormicSubsequence()
		{
			string s = "GEEKSFORGEEKS";
			DP.LongestPalindromicSubsequence lps = new DynamicProgramming.LongestPalindromicSubsequence(s);
			int res = lps.getLPS_TopDown(0, s.Length - 1, s);
			Console.WriteLine(res);
		}

		static void Driver_RodCutting()
		{
			int[] val = { 4, 10, 3, 7, 1 };
			DP.CuttingRod rod = new DynamicProgramming.CuttingRod(val.Length);
			int res = rod.getMaxValue_bottomUp2(val.Length, val);
			Console.WriteLine(res);
		}

		static void Driver_MaxSumOfIncreasingSubSequence()
		{
			int[] s = { 100, 2, 3, 5, 101 };
			DP.MaximumIncreasingSubsequence ma = new DynamicProgramming.MaximumIncreasingSubsequence();
			int res = ma.getMaxSum(s);
			Console.WriteLine(res);
		}

		static void Driver_LongestBitonic()
		{
			int[] s = { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };
			DP.LongestBitonicSubsequence bitonic = new DP.LongestBitonicSubsequence();
			bitonic.getLongestBitonic(s);
		}
		#endregion
	}
}