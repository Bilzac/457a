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
                int search_type = 0; 

                //blocked list array
                int[,] blocked_array = new int[2,100]; 
                
                //unvisited Node queue
                Queue<Node> node_queue = new Queue<Node>(); 

                Node start_node = new Node();
                Node exit_node = new Node(); 
               
                /*
                Console.WriteLine("Enter start node coordinates: x");
                int x = Convert.ToInt32(Console.ReadLine()); 
                Console.WriteLine("Enter start node coordinates: y");
                int y = Convert.ToInt32(Console.ReadLine()); 
                start_node.SetCoordinates(x,y);

                Console.WriteLine("Enter exit node coordinates: x");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter exit node coordinates: y");
                y = Convert.ToInt32(Console.ReadLine());
                exit_node.SetCoordinates(x, y);
                */

                start_node.SetCoordinates(3, 3);
                exit_node.SetCoordinates(4, 3); 


                Console.WriteLine("Search Type:\n[1] Breadth First Search\n[2] Depth First Search\n[3] A* Search");
                search_type = Convert.ToInt32(Console.ReadLine());


                 //create a tree
                //Tree test_tree = new Tree(blocked_array, start_node, exit_node);

                switch (search_type){
                    case 1:
                        //do breadth first search 
                       // BreadthFirstSearch(test_tree);
                        BreadthFirstSearch(start_node, exit_node, blocked_array, 1, node_queue);
                        break; 
                    case 2:
                        //depth first search
                        break; 
                    case 3:
                        //a* search 
                        break; 
                }
                
            }
            //return; 
        }

        static bool BreadthFirstSearch(Node start_node, Node exit_node, int[,] blocked_list, int distance, Queue<Node> node_queue)
        {
            /* implementation
             * start with current node as root            
             * add all 4 children if they are not:
             *      1. the exit node - done
             *      2. out of bounds - done
             *      3. blocked
             *      4. the start node
             * when nodes of 1 level are finished, increase distance
             */ 
            
            Node current_node = start_node;

            node_queue.Enqueue(current_node);

            while (true)
            {
                //check if first node is the solution
                Node next_node = node_queue.Dequeue();
                current_node = next_node;
                if (next_node.NodeCompare(exit_node))
                {                    
                    Console.WriteLine("Found Solution: \n" + current_node.GetX().ToString() + "," + current_node.GetY().ToString());
                    while (current_node.GetParent() != null)
                    {
                        current_node = current_node.GetParent();
                        Console.WriteLine(current_node.GetX().ToString() + "," + current_node.GetY().ToString());
                        distance++;
                    }
                    Console.WriteLine("Distance: " + distance.ToString());
                    return true;
                }
                else //add child nodes to queue
                {
                    //get the current node's coordinates
                    int x = current_node.GetX();
                    int y = current_node.GetY();
                    //create child nodes
                    Node child1 = new Node(++x, y, current_node);
                    //get the current node's coordinates
                    x = current_node.GetX();
                    y = current_node.GetY();
                    Node child2 = new Node(--x, y, current_node);
                    //get the current node's coordinates
                    x = current_node.GetX();
                    y = current_node.GetY();
                    Node child3 = new Node(x, ++y, current_node);
                    //get the current node's coordinates
                    x = current_node.GetX();
                    y = current_node.GetY();
                    Node child4 = new Node(x, --y, current_node);

                    //CHILD1
                    if (!child1.NodeCompare(exit_node) && child1.GetX() >= 0 && child1.GetY() >= 0
                        && child1.GetX() <= 24 && child1.GetY() <= 24)
                    {
                        current_node.AddChild(child1);
                        if (!node_queue.Contains(child1))
                        {
                            node_queue.Enqueue(child1);
                        }
                    }
                    if (!child2.NodeCompare(exit_node) && child2.GetX() >= 0 && child2.GetY() >= 0
                        && child2.GetX() <= 24 && child2.GetY() <= 24)
                    {
                        current_node.AddChild(child2);
                        if (!node_queue.Contains(child2))
                        {
                            node_queue.Enqueue(child2);
                        }
                    }
                    if (!child3.NodeCompare(exit_node) && child3.GetX() >= 0 && child3.GetY() >= 0
                        && child3.GetX() <= 24 && child3.GetY() <= 24)
                    {
                        current_node.AddChild(child3);
                        if (!node_queue.Contains(child3))
                        {
                            node_queue.Enqueue(child3);
                        }
                    }
                    if (!child4.NodeCompare(exit_node) && child4.GetX() >= 0 && child4.GetY() >= 0
                        && child4.GetX() <= 24 && child4.GetY() <= 24)
                    {
                        current_node.AddChild(child4);
                        if (!node_queue.Contains(child4))
                        {
                            node_queue.Enqueue(child4);
                        }
                    }               
                
                }
            }
            return false; 
        }
    }
}
