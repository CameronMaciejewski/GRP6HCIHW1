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
         
        public void createTree(T[] items)
        {
            this.root = new Node<T>(fanOut);
            Node<T> currentNode = root;
            for (int i = 0; i < items.Length/(fanOut-1); i++)
            {
                for (int j = 1; j < fanOut-1; j++)
                {
                    currentNode.setNextValue(items[i + j]);
                }
                currentNode = addNode(currentNode.getParent(), items[i+fanOut-1]);
            }
            for (int i = 1; i < items.Length%(fanOut-1); i++)
            {
                currentNode.setNextValue(items[i]);
            }
        }

        private Node<T> addNode(Node<T> node, T value)
        {
            Node<T> parent = node;
            Node<T> newNode = new Node<T>(fanOut);
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
                Node<T> newParent = new Node<T>(fanOut);
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
