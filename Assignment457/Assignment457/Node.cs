using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{

    class Node
    {
        //class variables 
        int distance; //distance from start - tree level
        int x; //x,y coordinates
        int y;

        //parent and children
        Node parent; 




        //default constructor 
        public Node()
        {

        }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;

            
        }

    }
}
