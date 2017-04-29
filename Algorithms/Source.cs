using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper = Algorithms.HelpersDataStructures;
using GraphNS = Algorithms.GraphTheory;
using DP = Algorithms.DynamicProgramming;

namespace Algorithms
{
    class Source
    {
        static void Main(string[] args)
        {
            KnapsackDriver();
            Console.ReadLine();
        }

        static void KnapsackDriver()
        {
            int[] W = { 1,3,4,5 };
            int[] V = { 1,4,5,7 };
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
    }
}