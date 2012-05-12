using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    class Assignment1
    {
        static void Main(string[] args)
        {
            while (true)
            {
                
                //console input from user
                //start, end, search type

                //blocked list array
                int[,] blocked_array = new int[2,100]; 

                Node start_node = new Node(); 

                //create a tree
                Tree test_tree = new Tree(blocked_array, start_node ); 

                



            }
        }
    }
}
