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
            currentNode.setNextValue(items[0]);
            int index = 0;
            for (int i = 0; i < items.Length/(fanOut-1); i++)
            {
                index = i * (fanOut -1);
                for (int j = 1; j < fanOut-1; j++)
                {
                        currentNode.setValue(items[index + j], j);           
                }
                currentNode = addNode(currentNode, items[index+fanOut-1], index+fanOut-1);
            }
            for (int i = 1; i < items.Length%(fanOut-1); i++)
            {
                currentNode.setValue(items[index + i + fanOut - 1], i);
            }
        }

        public int search(T key)
        {
            return findLeftMostItem(key);
        }

        public int findRightMostItem(T key)
        {
            int index = -1;
            Node<T> node = root;
            while (!node.getIsLeaf())
            {
                T[] keys = node.getKeys();
                int rightMostIndex = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i] != null)
                    {
                        rightMostIndex = i;
                    }
                }
                if (key.CompareTo(keys[rightMostIndex]) >= 0)
                {
                    node = node.getNodes()[rightMostIndex+1];
                }
                else if (key.CompareTo(keys[0]) >= 0)
                {
                    for (int i = 0; i < rightMostIndex; i++)
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
            for (int i = 0; i < node.getKeys().Length; i++)
            {
                if (key.CompareTo(node.getKeys()[i]) == 0)
                {
                    index = node.getIndex() + i;
                }
            }
            return index;
        }

        public int findLeftMostItem(T key)
        {
            int index = -1;
            Node<T> node = root;
            while (!node.getIsLeaf())
            {
                T[] keys = node.getKeys();
                int rightMostIndex = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i] != null)
                    {
                        rightMostIndex = i;
                    }
                }
                if (key.CompareTo(keys[0]) < 0)
                {
                    node = node.getNodes()[0];
                }
                else if (key.CompareTo(keys[rightMostIndex]) < 0)
                {
                    for (int i = rightMostIndex; i > 0; i--)
                    {
                        if (key.CompareTo(keys[i]) <= 0 && key.CompareTo(keys[i - 1]) > 0)
                        {
                            node = node.getNodes()[i];
                        }
                    }
                }
                else
                {
                    node = node.getNodes()[rightMostIndex + 1];
                }
            }
            for (int i = node.getKeys().Length - 1; i >= 0; i--)
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
            Node<T> newNode = new Node<T>(fanOut, index);
            newNode.setLeaf();
           
            if(isFull()) {
                moveRoot();

            } 
            Node<T> parent = node.getParent();
            newNode.setParent(parent);
            addToParent(newNode, value);
            newNode.setValue(value, 0);
            return newNode;
        }

        private void addToParent(Node<T> child, T value) 
        {
            Node<T> parent = child.getParent();
            if(parent != null && parent.isFull()) {
                Node<T> newParent = new Node<T>(fanOut, -1);
                newParent.setNextChild(child);
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
            if (lastNode.getIsLeaf())
            {
                return true;
            }
            do
            {
                rightChild = lastNode.getNodes()[lastNode.getNodes().Length - 1];
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
