using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.DisJointSet
{
    public class Node
    {
        public char data;
        public int rank;
        public Node parent;

        public Node(char d)
        {
            this.parent = this;
            this.data = d;
            this.rank = 0;
        }
    }

    public class DisJointSet
    {
        Dictionary<int, Node> map = new Dictionary<int, Node>();

        public void makeset(char d)
        {
            map.Add(d, new Node(d));
        }

        public void makeset(Node node)
        {
            map.Add(node.data, node);
        }

        public bool union(Node n1, Node n2)
        {
            if (n1 == null || n2 == null) { return false; }

            var p1 = findSet(n1);
            var p2 = findSet(n2);

            if (p1.parent == p2.parent) { /*same set*/ return false; }

            if (p1.rank >= p2.rank)
            {
                p1.rank = p1.rank == p2.rank ? p1.rank + 1 : p1.rank;
                p2.parent = p1;
            }
            else
            {
                p1.parent = p2;
            }
            return true;
        }

        public void union(int d1, int d2)
        {
            Node n1 = map[d1];
            Node n2 = map[d2];

            union(n1, n2);
        }

        private Node findSet(Node n1)
        {
            if (n1 == n1.parent)
            {
                return n1;
            }
            n1.parent = findSet(n1.parent);
            return n1.parent;
        }

        internal Node findSet(int data)
        {
            return findSet(map[data]);
        }
    }
}
