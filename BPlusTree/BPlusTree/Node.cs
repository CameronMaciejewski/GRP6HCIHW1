using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree
{
    class Node<T>
    {
        private T[] keys;
        private Node<T>[] nodes;
        private bool isLeaf;

        public Node(int fanOut)
        {
            this.keys = new T[fanOut];
            this.nodes = new Node<T>[fanOut];
            this.isLeaf = false;
        }

        public Node(T[] keys, Node<T>[] nodes, bool isLeaf)
        {
            this.keys = keys;
            this.nodes = nodes;
            this.isLeaf = isLeaf;
        }

        public T getNext(int index)
        {
            return keys[index];
        }

        public void setNext(Node<T> node)
        {
            this.nodes[0] = node;
        }

        public void setPointer(Node<T> node, int index)
        {
            this.nodes[index] = node;
        }

        public Node<T>[] getNodes()
        {
            return nodes;
        }

        public bool isLeaf()
        {
            return isLeaf;
        }
    }
}
