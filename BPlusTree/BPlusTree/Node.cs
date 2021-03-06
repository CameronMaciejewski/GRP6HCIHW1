﻿using System;
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
        private int index;

        public Node(int fanOut)
        {
            this.keys = new T[fanOut - 1];
            this.nodes = new Node<T>[fanOut];
            this.index = -1;
            this.isLeaf = false;
        }

        public Node(int fanOut, int index)
        {
            this.keys = new T[fanOut-1];
            this.nodes = new Node<T>[fanOut];
            this.index = index;
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
                if (keys[i] == null || keys[i].Equals(default(T)))
                {
                    keys[i] = value;
                    return;
                }
            }
        }

        public int getIndex()
        {
            return this.index;
        }

        public T[] getKeys()
        {
            return this.keys;
        }

        public void setLeaf()
        {
            this.isLeaf = true;
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
        public void setValue(T Value, int index)
        {
            this.keys[index] = Value;
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
