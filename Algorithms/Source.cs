using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper = Algorithms.HelpersDataStructures;
using GraphNS = Algorithms.GraphTheory;

namespace Algorithms
{
    class Source
    {
        static void Main(string[] args)
        {
            KruskalDriver();
            Console.ReadLine();
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
            Algorithms.GraphTheory.ShortestPath.PrintPathFromSToV(3);

            Console.WriteLine();
            Console.WriteLine("Now using Priority Queue");

            Algorithms.GraphTheory.ShortestPath.DijkstraWithPriorityQueue(graph, 0, 9);
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
            g.DFS();
            g.BFS(nA);
        }
    }
}
