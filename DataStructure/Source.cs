using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    class Source
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }

        static void Trie_Driver()
        {
            Trees.TrieTree tree = new Trees.TrieTree();
            tree.insert("abc");
            tree.insert("adk");
            tree.insert("ab");
            tree.insert("abz");

            //tree.insert("ab");
            //tree.insert("a");
            //tree.delete("ab", 0, tree.root);


            Console.WriteLine("abc: " + tree.IsFound("abc"));
            Console.WriteLine("zkf: " + tree.IsFound("zkf"));
            Console.WriteLine("abz: " + tree.IsFound("abz"));
            Console.WriteLine("ab: " + tree.IsFound("ab"));
            Console.WriteLine("kdf: " + tree.IsFound("kdf"));
            Console.WriteLine("adk: " + tree.IsFound("adk"));
            tree.delete("abc", 0, tree.root);
            tree.delete("abc", 0, tree.root);
            Console.WriteLine("abc: " + tree.IsFound("abc"));
            Console.WriteLine("ab: " + tree.IsFound("ab"));
            Console.WriteLine("a: " + tree.IsFound("a"));

        }

        private static void BST_Driver()
        {
            DataStructure.Trees.BinarySearchTree tree = new Trees.BinarySearchTree();

            tree.insert(1);
            DataStructure.Trees.Node root = tree.root;
            root.right = new DataStructure.Trees.Node(3);
            root.left = new DataStructure.Trees.Node(2);
            root.left.right = new DataStructure.Trees.Node(5);
            root.left.left = new DataStructure.Trees.Node(4);

            tree.inorder(tree.root);
            Console.WriteLine();
            tree.preorder(tree.root);
            Console.WriteLine();
            tree.postorder(tree.root);
            Console.WriteLine();
            Console.WriteLine(tree.getDepth(root.left.left));
        }

        private static void AVLDriver()
        {
            DataStructure.Trees.AVL_Tree tree = new DataStructure.Trees.AVL_Tree();
            int[] arr = { 5, 3 };

            foreach (var t in arr)
            {
                tree.insert_AVL(t);
            }

            tree.insert_AVL(2);
        }

        private void SkipListDriver()
        {
            SkipList.SkipList list = new SkipList.SkipList();
            list.insert(5);
            list.insert(10);
            list.insert(9);
            list.insert(8);
            list.insert(12);
            list.insert(1);
            list.insert(50);
            list.insert(60);
            list.insert(70);
            list.insert(90);

            list.PrintAllValues();
            list.delete(10);
            list.delete(1);
            list.PrintAllValues();
        }

        private static void HeapDriver()
        {
            int[] arr = { 10, 5, 2, 20, 15, 6 };
            printArray(arr);
            Console.WriteLine();
            Heap.Heap heap = new Heap.Heap(arr, true);
            heap.printHeap();
            heap.heapsort(true);
        }

        static void printArray(int[] arr)
        {
            foreach (var i in arr)
                Console.Write(i + " ");

        }
    }
}
