using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree
{
    class Tree<T> where T : System.IComparable<T>
    {
        private Node<T> root;
        private int fanOut;


        public Tree(int fanOut)
        {
            this.root = null;
            this.fanOut = fanOut;
        }
         
        public void createTree(T[] items)
        {
            this.root = new Node<T>(fanOut, 0);
            Node<T> currentNode = root;
            currentNode.setLeaf();
            for (int i = 0; i < items.Length/(fanOut-1); i++)
            {
                for (int j = 1; j < fanOut-1; j++)
                {
                    currentNode.setNextValue(items[i + j]);
                }
                currentNode = addNode(currentNode.getParent(), items[i+fanOut-1], i+fanOut-1);
            }
            for (int i = 1; i < items.Length%(fanOut-1); i++)
            {
                currentNode.setNextValue(items[i]);
            }
        }

        public int findLeftmostItem(T key)
        {
            int index = -1;
            Node<T> node = root;
            while (!node.getIsLeaf())
            {
                T[] keys = node.getKeys();
                if (key.CompareTo(keys[0]) < 0)
                {
                    node = node.getNodes()[0];
                }
                else if (key.CompareTo(keys[keys.Length - 1]) < 0)
                {
                    for (int i = 0; i < keys.Length - 1; i++)
                    {
                        if (key.CompareTo(keys[i]) >= 0 && key.CompareTo(keys[i + 1]) < 0)
                        {
                            node = node.getNodes()[i + 1];
                        }
                    }
                }
                else
                {
                    node = node.getNodes()[node.getNodes().Length - 1];
                }
            }
            for (int i = 0; i < node.getKeys().Length; i++)
            {
                if (key.CompareTo(node.getKeys()[i]) == 0)
                {
                    index = node.getIndex() + i;
                }
            }
            return index;
        }

        public int findRightmostItem(T key)
        {
            int index = -1;
            Node<T> node = root;
            while (!node.getIsLeaf())
            {
                T[] keys = node.getKeys();
                if (key.CompareTo(keys[keys.Length - 1]) >= 0)
                {
                    node = node.getNodes()[node.getNodes().Length - 1];
                }
                else if (key.CompareTo(keys[0]) >= 0)
                {
                    for (int i = 0; i < keys.Length - 1; i++)
                    {
                        if (key.CompareTo(keys[i]) >= 0 && key.CompareTo(keys[i + 1]) < 0)
                        {
                            node = node.getNodes()[i + 1];
                        }
                    }
                }
                else
                {
                    node = node.getNodes()[0];
                }
            }
            for (int i = node.getKeys().Length - 1; i > 0; i--)
            {
                if (key.CompareTo(node.getKeys()[i]) == 0)
                {
                    index = node.getIndex() + i;
                }
            }
            return index;
        }

        private Node<T> addNode(Node<T> node, T value, int index)
        {
            Node<T> parent = node;
            Node<T> newNode = new Node<T>(fanOut);
            newNode.setLeaf();
            newNode.setParent(parent);
            if(isFull()) {
                moveRoot();
            } 
            addToParent(newNode, value);
            return newNode;
        }

        private void addToParent(Node<T> child, T value) 
        {
            Node<T> parent = child.getParent();
            if(parent.isFull()) {
                Node<T> newParent = new Node<T>(fanOut, -1);
                child.setParent(newParent);
                newParent.setParent(parent.getParent());
                addToParent(newParent, value);
                parent = newParent;
            } else {
                parent.setNextChild(child);
                parent.setNextValue(value);
            }
        }

        private void moveRoot() {
            Node<T> newRoot = new Node<T>(fanOut);
            newRoot.setNextChild(root);
            root = newRoot;
        }

        private bool isFull()
        {
            Node<T> lastNode = root;
            Node<T> rightChild;
            do
            {
                rightChild = lastNode.getNodes()[lastNode.getNodes().Length];
                lastNode = rightChild;
            } while (rightChild != null && !rightChild.getIsLeaf());
            if (rightChild != null && rightChild.getIsLeaf())
            {
                return true;
            }
            return false;
        }
    }
}
