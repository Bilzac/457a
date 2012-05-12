using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    class Tree
    {
        //class variables
        Node root_node;
        Node exit_node;
        int[,] blocked_list; 
        int count; 


        //default constructor
        public Tree(Node root_node)
        {
            count = 1;
            this.root_node = root_node; //set root to starting node
        }

      
        //constructor
        public Tree(int[,] blocked_list, Node root_node)
        {
            count = 1;
            this.root_node = root_node; //set root to starting node
            this.blocked_list = blocked_list; 
        }


        static void test()
        {
            unsafe
            {

            }
        }
    }
}
