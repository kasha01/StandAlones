using System.Collections.Generic;

// THANKS TO TUSHAR ROY https://www.youtube.com/watch?v=ID00PMy0-vE
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
        Dictionary<char, Node> map = new Dictionary<char, Node>();

        public void makeset(char d)
        {
            map.Add(d, new Node(d));
        }

        public void makeset(Node node)
        {
            map.Add(node.data, node);
        }

		/* The benefits of using ranks is, it always attaches the shorter tree to the root of the taller tree.
		 * Thus, the resulting tree is no taller than the originals unless they were of equal height, 
		 * in which case the resulting tree is taller by one node.
		 * Initially a set has one element and a rank of zero. If two sets are unioned and have the same rank, 
		 * the resulting set's rank is one larger; otherwise, if two sets are unioned and have different ranks,
		 * the resulting set's rank is the larger of the two (no increment). Ranks are used instead of height or depth because
		 * path compression will change the trees' heights over time.
		 * Notice a--b union d--c ==> a becomes the parent of both b and of d-c, so it rank/depth is +1. 		 
		 * think of ranks as depth/height of the tree
		 */
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

        public void union(char d1, char d2)
        {
            Node n1 = map[d1];
            Node n2 = map[d2];

            union(n1, n2);
        }

		// this is creating a shortcut, so the "grand"child node, after being found will now be pointing directly towards
		// the parent of the set, instead of its local/immediate parent
        private Node findSet(Node n1)
        {
            if (n1 == n1.parent)
            {
                return n1;
            }
            n1.parent = findSet(n1.parent);
            return n1.parent;
        }

        internal Node findSet(char data)
        {
            return findSet(map[data]);
        }
    }
}
