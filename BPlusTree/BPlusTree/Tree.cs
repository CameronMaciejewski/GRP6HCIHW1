using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree
{
    class Tree<T>
    {
        private Node<T> root;
        private int fanOut;


        public Tree(int fanOut)
        {
            this.root = null;
            this.fanOut = fanOut;
        }

        public Tree(Node<T> root, int fanOut)
        {
            this.root = root;
            this.fanOut = fanOut;
        }

        public void createTree(T[] items)
        {
            int nodeCount = 0;
            // Assumes array is in order
            Node<T> currentNode = root;
            for (int i = 0; i < items.Length; i += fanOut)
            {
                Node<T> newNode = null;
                T[] nodeItems = new T[fanOut];
                // children are all null because it is a leaf node
                Node<T>[] children = new Node<T>[fanOut];
                for (int j = 0; j < fanOut; j++)
                {
                    nodeItems[j] = items[j + i];
                }
                newNode = new Node<T>(nodeItems, children, true);
                currentNode.setNext(newNode);
                currentNode = newNode;
                nodeCount++;
            }
        }
    }
}
