using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{

    class Node
    {
        //class variables 
        //int distance; //distance from start - tree level
        int x; //x,y coordinates
        int y;

        //parent and children
        Node parent;
        List<Node> children;



        //default constructor 
        public Node()
        {
            x = 0;
            y = 0;
            parent = null;
            children = new List<Node>(); 
        }

        public Node(int x, int y, Node parent)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
            children = new List<Node>(); 
            
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

         public List<Node> GetChildren()
        {
            return this.children; 
        }

         public int GetX()
         {
             return this.x; 
         }

         public int GetY()
         {
             return this.y; 
         }

         public Node GetParent()
         {
             return this.parent; 
         }
        //compares this node with another node
         public bool NodeCompare(Node other_node)
         {
             if (x == other_node.GetX() && y == other_node.GetY())
             {
                 return true;
             }
             return false; 

         }

            

            
    }
}
