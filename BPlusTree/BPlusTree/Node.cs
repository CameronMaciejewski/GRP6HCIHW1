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
        private Node<T> parent;
        private Node<T>[] nodes;
        private bool isLeaf;

        public Node(int fanOut)
        {
            this.keys = new T[fanOut-1];
            this.nodes = new Node<T>[fanOut];
            this.isLeaf = false;
        }

        public void setNextChild(Node<T> node)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] == null)
                {
                    nodes[i] = node;
                    node.setParent(this);
                    return;
                }
            }
        }

        public void setNextValue(T value)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == null)
                {
                    keys[i] = value;
                    return;
                }
            }
        }

        public Node<T>[] getNodes()
        {
            return nodes;
        }

        public bool getIsLeaf()
        {
            return isLeaf;
        }

        public Node<T> getParent()
        {
            return this.parent;
        }

        public void setParent(Node<T> parent) 
        {
            this.parent = parent;
        }

        public bool isFull()
        {
            if (nodes[nodes.Length - 1] == null)
            {
                return false;
            }
            return true;
        }
    }
}
