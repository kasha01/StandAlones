using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Dictionary<MinDPRoute, int> dic2 = new Dictionary<MinDPRoute, int>(new FooEqualityComparer());
            Dictionary<MinDPRoute, int> parent = new Dictionary<MinDPRoute, int>(new FooEqualityComparer());
            // Dictionary of ((target Node - Subsets)/MinDPRoute that I can go thru to reach target Node) - cost

            // skip start node = node 0 - loop thru nodes
            foreach (List<int> subset in listofSubSets)
            {
                // loop thru all nodes
                for (int targetN = 1; targetN < sz; targetN++)
                {
                    if (subset.Contains(targetN))
                        continue;

                    int mincost = int.MaxValue;
                    int minPreNode = 0;

                    List<int> copyset = new List<int>(subset);
                    MinDPRoute dp = null;
                    var ind = MinDPRoute.CreateDP(targetN, subset);

                    foreach (int prev in subset)
                    {
                        copyset.Remove(prev);

                        dp = MinDPRoute.CreateDP(prev, copyset);
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
                        mincost = dist[0, targetN];
                        // min prev node is zero since this is my start point
                    }

                    // save min dist
                    dic2.Add(ind, mincost);
                    parent.Add(ind, minPreNode);
                }
            }



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

            getRoute(parent, lastsubset);
        }

        private void getRoute(Dictionary<MinDPRoute, int> parent, List<int> thrusubset)
        {
            int target = 0;
            Stack<int> st = new Stack<int>();
            st.Push(target);
            while (true)
            {
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
            // in short the key is generate all binary numbers from (0) to (n-1), then loop through the bits of each number to see what character repsentation it
            // contains e.g. 100 - has the 3rd character in the set, 011, has the 1st and 2nd character in the set and so on
            int n = set.Length - 1;
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < (1 << n); i++)
            {
                var l = new List<int>();

                for (int j = 0; j < n; j++)
                    if ((i & (1 << j)) > 0)
                        l.Add(set[j + 1]);

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
