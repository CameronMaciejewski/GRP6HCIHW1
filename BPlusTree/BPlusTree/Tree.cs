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

        //public void createTree(T[] items)
        //{
        //    int nodeCount = 0;
        //    // Assumes array is in order
        //    Node<T> currentNode = root;
        //    for (int i = 0; i < items.Length; i += fanOut)
        //    {
        //        Node<T> newNode = null;
        //        T[] nodeItems = new T[fanOut];
        //        // children are all null because it is a leaf node
        //        Node<T>[] children = new Node<T>[fanOut];
        //        for (int j = 0; j < fanOut; j++)
        //        {
        //            nodeItems[j] = items[j + i];
        //        }
        //        newNode = new Node<T>(nodeItems, children, true);
        //        currentNode.setNext(newNode);
        //        currentNode = newNode;
        //        nodeCount++;
        //    }
        //    createInnerNodes(nodeCount);
        //}

        private void createInnerNodes(int nodeCount)
        {
            if (nodeCount <= fanOut)
            {
            }
            else
            {
                Node<T> subNode = root;
                for (int i = 0; i < nodeCount / fanOut; i++)
                {
                    Node<T> node;
                    T[] keys = new T[fanOut];
                    Node<T>[] children = new Node<T>[fanOut];
                    for (int j = 0; j < fanOut; j++) {
                        children[j] = subNode;
                    }
                    node = new Node<T>(keys, children, false);
                }
                    createInnerNodes(nodeCount / fanOut);
            }
        }
         
        public void createTree(T[] items)
        {
            // fanout ^ height of tree = max num in tree
            int treeHeight = (int) Math.Ceiling(Math.Log(items.Length, fanOut));
            this.root = new Node<T>(fanOut);

        }

        private void createInnerNode(int height)
        {
            if (height == 1)
            {

            }
            else
            {

            }
        }

        private void addNode(Node<T> node)
        {
            
        }

        private Node<T> nextAvailableNode()
        {
            Node<T> node = root;
            if (hasOpenPointer(node))
            {

            }
            return node;
        }

        private bool hasOpenPointer(Node<T> node) 
        {
            if (node.isLeaf())
            {
                return false;
            }
            for (int i = 0; i <= fanOut; i++) {
                if (node.getNodes()[i] == null)
                {
                    return true;
                }
                else
                {
                    hasOpenPointer(node.getNodes()[i]);
                }
                newNode = new Node<T>(nodeItems, children, true);
                currentNode.setNext(newNode);
                currentNode = newNode;
                nodeCount++;
            }
            return false;
        }
    }
}
