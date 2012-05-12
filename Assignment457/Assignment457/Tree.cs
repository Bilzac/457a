using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    class Tree
    {
        //class variables
        Node root_node = null;
        Node exit_node = null;
        int[,] blocked_list = null; 
        int count = 0;
        
        
        //default constructor
        public Tree(Node root_node)
        {
            count = 1;
            this.root_node = root_node; //set root to starting node
        }

      
        //constructor
        public Tree(int[,] blocked_list, Node root_node, Node exit_node)
        {
            count = 1;
            this.root_node = root_node; //set root to starting node
            this.exit_node = exit_node; 
            this.blocked_list = blocked_list; 
        }


        public Node GetStartNode()
        {
            return this.root_node;
        }

        public Node GetExitNode()
        {
            return this.exit_node; 
        }

        public int[,] GetBlockedList()
        {
            return this.blocked_list; 
        }
       
       
    }
}
