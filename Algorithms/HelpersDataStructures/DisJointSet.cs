using Algorithms.HelpersDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.HelpersDataStructures
{
    class DisJointSet<T>
    {
        Dictionary<T, Node<T>> map = new Dictionary<T, Node<T>>();

        public void makeset(T d)
        {
            map.Add(d, new Node<T>(d));
        }

        public void makeset(Node<T> node)
        {
            map.Add(node.data, node);
        }

        public bool union(Node<T> n1, Node<T> n2)
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

        public void union(T d1, T d2)
        {
            Node<T> n1 = map[d1];
            Node<T> n2 = map[d2];

            union(n1, n2);
        }

        private Node<T> findSet(Node<T> n1)
        {
            if (n1 == n1.parent)
            {
                return n1;
            }
            n1.parent = findSet(n1.parent);
            return n1.parent;
        }

        internal Node<T> findSet(T data)
        {
            return findSet(map[data]);
        }
    }
}
