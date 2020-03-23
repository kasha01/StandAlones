using System;

namespace DataStructure
{
	class Source
	{
		static void Main(string[] args)
		{
			Max_Priority_Queue_with_map_Driver ();
		}

		static void Max_Priority_Queue_with_map_Driver()
		{
			MinPriorityQueue.MaximumPriorityQueue_with_mapping<string> maxQueue = new MinPriorityQueue.MaximumPriorityQueue_with_mapping<string> (10);

			maxQueue.add_with_priority ("alpha", 10);
			maxQueue.add_with_priority ("beta", 20);
			maxQueue.add_with_priority ("charlie", 30);
			maxQueue.add_with_priority ("delta", 50);

			maxQueue.update_priority ("beta", 120);
			maxQueue.increase_priority ("charlie", 500);

			string s = maxQueue.extract_max ();
			Console.WriteLine (s);

			string s1 = maxQueue.extract_max ();
			Console.WriteLine (s1);
		}

		static void Min_Priority_Queue_with_map_Driver()
		{
			MinPriorityQueue.MinimumPriorityQueue_with_mapping<string> minQueue = new MinPriorityQueue.MinimumPriorityQueue_with_mapping<string> (10);

			minQueue.add_with_priority ("alpha", 10);
			minQueue.add_with_priority ("beta", 20);
			minQueue.add_with_priority ("charlie", 30);
			minQueue.add_with_priority ("delta", 50);

			minQueue.update_priority ("beta", 120);
			minQueue.decrease_priority ("delta", 5);

			string s = minQueue.extract_min ();
			Console.WriteLine (s);

			string s1 = minQueue.extract_min ();
			Console.WriteLine (s1);
		}

		static void Min_Priority_Queue_Driver()
		{
			MinPriorityQueue.MinPriorityQueue_of_nodes<string> minQueue = new MinPriorityQueue.MinPriorityQueue_of_nodes<string> (10);

			minQueue.add_with_priority ("alpha", 1);
			minQueue.add_with_priority ("beta", 2);
			minQueue.add_with_priority ("charlie", 3);
			minQueue.add_with_priority ("delta", 5);

			minQueue.decrease_priority ("delta", 0);

			string s = minQueue.extract_min ();

			Console.WriteLine (s);
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
			root.left.left = new DataStructure.Trees.Node(4);
			root.left.left.left = new DataStructure.Trees.Node(6);
			root.left.right = new DataStructure.Trees.Node(5);
			root.left.right.right = new DataStructure.Trees.Node(7);

			tree.postorder(root);
			Console.WriteLine();
			tree.morrisPostorderTraversal(root);

			DataStructure.Trees.Node common_ancestor = null;
			tree.LCA(tree.root, 4, 4, ref common_ancestor);

			tree.printTreeByLevels(root);
			Console.WriteLine();
			tree.inorder(tree.root);
			Console.WriteLine();
			tree.preorder(tree.root);
			Console.WriteLine();
			tree.postorder(tree.root);
			Console.WriteLine();
			Console.WriteLine(tree.getDepth(root.left.left));
			int tt = tree.traverse_in_postorder(tree.root);
			Console.WriteLine(tt);
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