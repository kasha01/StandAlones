using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.HelpersDataStructures
{
    public class Node<T>
    {
        public T data;
        public int rank;
        public int priority;
        public Node<T> parent;
        public bool visited = false;
        public int index = 0;   //node index

        public List<Node<T>> adjNodes = new List<Node<T>>();


        public static bool operator <(Node<T> n1, Node<T> n2) { return n1.priority < n2.priority; }
        public static bool operator >(Node<T> n1, Node<T> n2) { return n1.priority > n2.priority; }
        public static bool operator <=(Node<T> n1, Node<T> n2) { return n1.priority <= n2.priority; }
        public static bool operator >=(Node<T> n1, Node<T> n2) { return n1.priority >= n2.priority; }

        public Node(T d)
        {
            this.parent = this;
            this.data = d;
            this.rank = 0;
        }

        public Node(T d, int p)
        {
            this.data = d; this.priority = p;
        }
    }
}
