using System.Collections.Generic;

namespace DataStructure.Trees
{
	class TrieTree
	{
		public TrieNode root;

		public TrieTree()
		{
			root = new TrieNode();
		}

		public void insert(string word)
		{
			TrieNode current = root;

			foreach (char c in word)
			{
				if (!current.children.ContainsKey(c))
				{
					current.children.Add(c, new TrieNode());
				}
				current = current.children[c];
			}
			current.isCompleteWord = true;
		}

		public bool IsFound(string word)
		{
			var current = this.root;

			foreach (char c in word)
			{
				if (current != null && current.children.ContainsKey(c))
				{
					current = current.children[c];
				}
				else { return false; }
			}
			return current != null && current.isCompleteWord;
		}

		public bool delete(string word, int index, TrieNode node)
		{
			if (node == null || node.children == null) { return false; }
			if (word.Length == index)
			{
				if (node.children.Count == 0) { return true; }
				else { node.isCompleteWord = false; return false; }
			}

			char c = word[index];
			if (node.children.ContainsKey(c) && delete(word, index + 1, node.children[c]))
			{
				node.children[c] = null;
				node.children.Remove(c);

				if (node.children.Count > 0 || node.isCompleteWord)
					return false;
				else
					return true;
			}

			return false;
		}

		public bool delete2(string word, int index, TrieNode current)
		{
			// End of word
			if (index == word.Length)
			{
				if (!current.isCompleteWord)
				{
					// nothing to delete
					return false;
				}
				current.isCompleteWord = false;
				return current.children.Count == 0;
			}

			char ch = word[index];
			TrieNode node = current.children[ch];
			if (node == null)
				return false; // word does not exist - nothing to delete

			bool shouldDeleteCurrrentNode = delete2(word, index + 1, node);

			if (shouldDeleteCurrrentNode)
			{
				// delete node
				current.children.Remove(ch);
				return current.children.Count == 0;
			}

			return false;
		}
	}

	class TrieNode
	{
		public Dictionary<char, TrieNode> children;
		public bool isCompleteWord;

		public TrieNode()
		{
			isCompleteWord = false;
			children = new Dictionary<char, TrieNode>();
		}
	}
}
