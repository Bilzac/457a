using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{

    class Node
    {
        //  private variables
        int x; //x coordinate
        int y; // y coordinate
        int _F; // G + H
        int _G; // path from start to current
        int _H; // path from current to final
        Node parent;
        List<Node> children;

        // Public setters and Getters
        public int F
        {
            //set the person name
            set { this._F = value; }
            //get the person name 
            get { return this._F; }
        }
        public int G
        {
            //set the person name
            set { this._G = value; }
            //get the person name 
            get { return this._G; }
        }
        public int H
        {
            //set the person name
            set { this._H = value; }
            //get the person name 
            get { return this._H; }
        }

        //default constructor 
        public Node()
        {
            x = 0;
            y = 0;
            parent = null;
            children = new List<Node>();
        }
        // constructor
        public Node(int x, int y, Node parent)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;


            children = new List<Node>();
        }

        // methods
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public bool AddChild(Node child)
        {
            //checks
            children.Add(child);
            child.parent = this;
            return true;

        }
        public bool RemoveChild(Node child)
        {
            //checks
            children.Remove(child);
            return true;
        }
        public bool SetCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
            return true;
        }
        public Node GetChildAt(int index)
        {
            Node[] children = this.children.ToArray();
            return children[index];
        }
        public void RemoveChildren()
        {
            this.RemoveChildren();
        }
        public List<Node> GetChildren()
        {
            return this.children;
        }
        public Node GetParent()
        {
            return this.parent;
        }
        public bool NodeCompare(Node other_node)
        {
            if (x == other_node.GetX() && y == other_node.GetY())
            {
                return true;
            }
            return false;

        }
        public void calculatePathCost(Node startNode, Node endNode)
        {
            setCurrentNode_G(startNode, this);
            setCurrentNode_H(endNode, this);
            setCurrentNode_F(this);
        }
        public void setParent(Node parent)
        {
            this.parent = parent;
        }

        // private methods
        private void setCurrentNode_G(Node startNode, Node currentNode)
        {
            // distance from the start node to the current node
            int distance = 0;
            Node temp = currentNode;
            while (temp.GetParent() != null)
            {
                temp = temp.GetParent();
                distance++;
            }

            currentNode.G = distance;
        }
        private void setCurrentNode_H(Node endNode, Node currentNode)
        {
            // estimated distance to the end node
            currentNode.H = Math.Abs(currentNode.GetX() - endNode.GetX()) + Math.Abs(currentNode.GetY() - endNode.GetY());
        }
        private void setCurrentNode_F(Node node)
        {
            node.F = node.G + node.H;
        }
    }
}
