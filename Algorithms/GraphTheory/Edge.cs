using System.Collections.Generic;

namespace Algorithms.GraphTheory
{
    public class Edge
    {
        public int e1; public int e2; public int weight;
        public Edge(int from, int to, int w)
        {
            this.e1 = from; this.e2 = to; this.weight = w;
        }
    }

    public class EdgeComparer : IComparer<Edge>
    {
        public int Compare(Edge x, Edge y)
        {
            if (x.weight >= y.weight) { return 1; }
            else { return -1; }
        }
    }
}
