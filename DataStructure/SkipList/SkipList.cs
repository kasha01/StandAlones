using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.SkipList
{
    public class SkipList
    {
        public Node positiveInfinity = new Node(int.MaxValue);
        public Node negativeInfinity = new Node(int.MinValue);

        public Random rand = new Random();

        // fixed flipping of coin - for demonstration purposes
        public bool[] arr = { false, false, false, true, false, true, true, false, false, true, false, false, true, false, true, false, false };
        public static int counter = 0;

        public SkipList()
        {
            this.negativeInfinity.Right = this.positiveInfinity;
            this.positiveInfinity.Left = this.negativeInfinity;
        }

        public bool flipCoin()
        {
            //return rand.Next(10) >= 5;

            return arr[counter++];
        }

        public Node search(int value)
        {
            Node current = this.negativeInfinity;

            while (current != null)
            {
                while (current.Right != null && current.Right.data <= value)
                {
                    current = current.Right;
                }

                if (current.data == value)
                {
                    break;
                }

                current = current.Down;
            }

            return current;
        }

        public void insert(int data)
        {
            // find where to insert - no update
            List<Node> nodestoupdate = new List<Node>();

            Node current = this.negativeInfinity;

            while (current != null)
            {
                while (current.Right != null && current.Right.data < data)
                {
                    current = current.Right;
                }

                nodestoupdate.Add(current);
                current = current.Down;
            }

            int level = 0; // always insert at bottom level 0
			//in the while search above, ground level node was inserted last, we searched Top to bottom
			int LowLevelNodeIndex = nodestoupdate.Count - 1; 
			Node newNode = null;

            while (level == 0 || flipCoin())
            {
                if (newNode == null)
                {
                    newNode = new Node(data);
                }
                else
                {
                    // this only happens when I am promoting the newly current node, so makes sense to check if newNode is null
                    newNode = new Node(newNode);
                }

                Node updatenode = null;

                if (nodestoupdate.Count <= level)
                {
                    //need to create new layer for the level I am currently at is the last level
                    //remember nodestoupdate has nodes count as much as the current level of the list
                    CreateLayer();
                    updatenode = this.negativeInfinity; //set updatenode to negative infinity, I am at the new created layer
                }
                else
                {
                    updatenode = nodestoupdate[LowLevelNodeIndex - level];
                }

                //insert
                newNode.Right = updatenode.Right;
                newNode.Left = updatenode;
                updatenode.Right.Left = newNode;
                updatenode.Right = newNode;

                level++;
            }
        }

        public void delete(int data)
        {
            //find the nodes to update
            Node current = this.negativeInfinity; //which is always the most top negative infinity
            List<Node> nodestoUpdateList = new List<Node>();

            while (current != null)
            {
                while (current.Right != null && current.Right.data <= data)
                {
                    current = current.Right;
                }

                if (current.data == data) { nodestoUpdateList.Add(current); }

                current = current.Down;
            }


            //Delete
            bool isOrphan = false;
            Node nodetoChange = null;
            for (int i = 0; i < nodestoUpdateList.Count; i++)
            {
                //delete the top level node
                nodetoChange = nodestoUpdateList[i];

                //Check if this node is an orphan in the level (deleting it will leave an empty level)
                isOrphan = nodetoChange.Left == this.negativeInfinity && nodetoChange.Right == this.positiveInfinity;

                if (isOrphan)
                {
                    // delete level
                    this.negativeInfinity = nodetoChange.Left.Down;
                    this.positiveInfinity = nodetoChange.Right.Down;

                    //nullifying
                    nodetoChange.Left.Dispose();
                    nodetoChange.Right.Dispose();
                    nodetoChange.Dispose();
                }
                else
                {
                    //delete node
                    nodetoChange.Left.Right = nodetoChange.Right;
                    nodetoChange.Right.Left = nodetoChange.Left;
                    nodetoChange.Dispose();
                }
            }
        }

        public void PrintAllLevels()
        {
            Node printHead = this.negativeInfinity;
            Node printTail = this.positiveInfinity;
            while (printHead != null)
            {
                Node current = printHead.Right;
                while (current != null && current != printTail)
                {
                    Console.Write(current.data + " ");
                    current = current.Right;
                }
                Console.WriteLine();
                //go down
                printHead = printHead.Down;
                printTail = printTail.Down;
            }
        }

        public void PrintAllValues()
        {
            Node current = this.negativeInfinity;
            while (current != null && current.Down != null)
            {
                current = current.Down;
            }

            //i am at loweest level
            while (current != null && current.Right != null && current.Right.Right != null)
            {
                Console.Write(current.Right.data + " ");
                current = current.Right;
            }
        }

        private void CreateLayer()
        {
            Node positiveInfinity = new Node(int.MaxValue);
            Node negativeInfinity = new Node(int.MinValue);

            this.negativeInfinity.Up = negativeInfinity;
            this.positiveInfinity.Up = positiveInfinity;

            negativeInfinity.Down = this.negativeInfinity;
            positiveInfinity.Down = this.positiveInfinity;

            negativeInfinity.Right = positiveInfinity;
            positiveInfinity.Left = negativeInfinity;

            this.negativeInfinity = negativeInfinity;
            this.positiveInfinity = positiveInfinity;
        }

        public static void SkipListDriver()
        {
            int[] arr = { 1, 4, 5, 6, 7 };
            SkipList skiplis = new SkipList();
            for (int i = 1; i < 10; i++)
            {
                skiplis.insert(i);
            }

            skiplis.PrintAllLevels();

            skiplis.delete(4);

            Console.WriteLine("after delete");
            skiplis.PrintAllLevels();

            Console.WriteLine("**********Print Values");
            skiplis.PrintAllValues();
            return;
        }
    }

    public class Node : IDisposable
    {
        public int data;
        public Node Right;
        public Node Left;
        public Node Up;
        public Node Down;

        public Node(int v)
        {
            this.data = v;
        }

        public Node(Node l)
        {
            this.data = l.data;
            this.Down = l;
            l.Up = this;
        }

        public void Dispose()
        {
            this.Down = null;
            this.Up = null;
            this.Right = null;
            this.Left = null;
        }
    }
}