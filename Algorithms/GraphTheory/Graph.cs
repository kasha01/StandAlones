using Algorithms.HelpersDataStructures;
using System;
using System.Collections.Generic;

namespace Algorithms.GraphTheory
{
    public class Graph<T>
    {
        int size = 0;
        Node<T> root = null;
        List<Node<T>> nodes = new List<Node<T>>();

        bool[,] matrix;
        int[,] wMatrix; //Matrix with path weight
        public List<int> ids = new List<int>();
        int[] nodeCost;
        int[] pred;
        bool[] visited;
        List<Edge> minSpanEdge = new List<Edge>();
        List<Edge> edgeList = new List<Edge>();

        public Graph(int size)
        {
            this.size = size;
            matrix = new bool[size, size];
            wMatrix = new int[size, size];
            nodeCost = new int[size];
            pred = new int[size];
            visited = new bool[size];
        }

        public void SetRootNode(Node<T> n)
        {
            this.root = n;
        }

        // Add Node to Graph
        public void addNode(Node<T> n)
        {
            n.index = nodes.Count;
            nodes.Add(n);
        }

        // Connect Nodes - with no weight - used for unweighted graph
        public void connectNode(Node<T> an, Node<T> bn)
        {
            int a = an.index;
            int b = bn.index;
            matrix[a, b] = true;
            matrix[b, a] = true;
        }

        // Coonect Nodes - with weight - used for weighted graph
        public void connectNode(Node<T> an, Node<T> bn, int w)
        {
            int a = an.index;
            int b = bn.index;
            matrix[a, b] = true;
            matrix[b, a] = true;
            wMatrix[a, b] = w;
            wMatrix[b, a] = w;
        }

        // Connect Nodes - knowing nodes index
        public void connectNode(int s, int t, int weight)
        {
            wMatrix[s, t] = weight;
        }

        // used with Min Span Tree - s,t: index of Node<T>
        public void connectNodeAndCreateEdge(int s, int t, int weight)
        {
            edgeList.Add(new Edge(s, t, weight));
        }

        public void connnectNodeWithAdj(Node<T> an, Node<T> bn, int w)
        {
            int a = an.index;
            int b = bn.index;
            wMatrix[a, b] = w;
            wMatrix[b, a] = w;
            an.adjNodes.Add(bn);
            bn.adjNodes.Add(an);
        }

        public void BFS(Node<T> startnode)
        {
            Queue<Node<T>> q = new Queue<Node<T>>();
            q.Enqueue(startnode);
            int[] disto = new int[this.size];
            while (q.Count > 0)
            {
                //get Node<T> from queue
                Node<T> n = q.Dequeue();

                //mark node as visited
                n.visited = true;
                Console.Write(n.data + ", ");
                //get adjacent nodes into q
                Node<T> gn = null;
                while ((gn = getAdjacentNode(n)) != null)
                {
                    disto[gn.index] = disto[n.index] + 1;
                    gn.visited = true;
                    q.Enqueue(gn);
                }
            }
        }

        public void DFS()
        {
            Stack<Node<T>> s = new Stack<Node<T>>();
            this.root.visited = true;
            s.Push(this.root);
            Console.Write(this.root.data + ", ");

            while (s.Count > 0)
            {
                Node<T> child = null;
                if ((child = getAdjacentNode(s.Peek())) != null)
                {
                    Console.Write(child.data + ", ");
                    child.visited = true;
                    s.Push(child);
                }
                else
                {
                    s.Pop();
                }
            }
        }

        // Using Recurssion
        public int myDFS(Node<T> root)
        {
            Node<T> child = null;
            root.visited = true;
            int counter = 0;
            int sum = 0;
            while ((child = getAdjacentNode(root)) != null)
            {
                child.visited = true;
                counter = myDFS(child);
                sum = sum + counter;
                wMatrix[root.index, child.index] = counter;
                ids.Add(counter);
                //if (root.index == 0) { sum = 0; } // Can't remember why I put this line
            }
            Console.WriteLine(String.Format("Node {0} has {1} Child", root.data, sum));
            return sum + 1;
        }

        public int PrintPathFromSToV(int node)
        {
            if (node == 0)
            {
                return 0;
            }
            else if (pred[node] == -1)
            {
                return -1;
            }
            else
            {
                int cost = PrintPathFromSToV(pred[node]);
                if (cost < 0)
                {
                    return -1;
                }
                else
                {
                    return nodeCost[node] + cost;
                }
            }
        }

        public void GetMinSpanTree_Kruskal()
        {
            DisJointSet<T> jointset = new DisJointSet<T>();

            // sort edges ascn
            edgeList.Sort(getAscEdgeComparer());

            // Create sets for each vertex
            for (int i = 0; i < nodes.Count; i++)
            {
                jointset.makeset(nodes[i]);
            }

            // Construct Min Span Tree
            for (int i = 0; i < edgeList.Count; i++)
            {
                Edge edge = edgeList[i];

                if (jointset.union(nodes[edge.e1], nodes[edge.e2]))
                    // add to result
                    minSpanEdge.Add(edge);
            }

            //print result
            foreach (var item in minSpanEdge)
            {
                Console.WriteLine(nodes[item.e1].data + "---" + nodes[item.e2].data);
            }
        }

        // Big Thanks to Tushar Roy - see: https://www.youtube.com/watch?v=z1L3rMzG1_A
        public void GetMinSpanTree_Prim()
        {
            MinPriorityQueue<T> pqueue = new MinPriorityQueue<T>(this.size);
            List<Tuple<T, T>> result = new List<Tuple<T, T>>();

            foreach (Node<T> n in nodes)
            {
                pqueue.add_with_priority(n, int.MaxValue);
                n.parent = null;
            }

            //set root
            pqueue.decrease_priority(nodes[0].data, 0);
            nodes[0].parent = null;

            while (pqueue.heapSize > 0)
            {
                Node<T> cur = pqueue.extract_min_node();

                if (cur.parent != null)
                {
                    result.Add(new Tuple<T, T>(cur.parent.data, cur.data));
                }

                //get adj
                foreach (var adj in cur.adjNodes)
                {
                    int w = wMatrix[cur.index, adj.index];
                    if (adj.priority > w)
                    {
                        adj.parent = cur;
                        pqueue.decrease_priority(adj.data, w);
                    }
                }
            }

            foreach (var t in result)
            {
                Console.WriteLine(t.Item1 + "-->" + t.Item2);
            }
        }

        private IComparer<Edge> getAscEdgeComparer()
        {
            return new EdgeComparer();
        }

        private Node<T> getAdjacentNode(Node<T> n)
        {
            int j = 0;
            int index = nodes.IndexOf(n);
            while (j < nodes.Count)
            {
                if (j != index && nodes[j].visited == false && (matrix[index, j] || matrix[j, index]))
                {
                    return nodes[j];
                }
                j++;
            }
            return null;
        }
    }
}