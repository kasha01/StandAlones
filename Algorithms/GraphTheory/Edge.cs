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
            /*
             * if -1 is returned => This means => x < y  => List.sort will put x before y
             * if  0 is returned => This means => x = y  => List.sort will keep x and y as is
             * if  1 is returned => This means => x > y  => List.sort will put x after y
             */
            if (x.weight >= y.weight) { return 1; }
            else { return -1; }
        }
    }
}
