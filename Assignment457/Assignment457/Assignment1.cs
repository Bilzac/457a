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
                int[,] blocked_array = new int[,] { { 0, 1 }, { 0, 2 }, { 0, 7 }, { 0, 19 }, { 0, 20 },
                                                    { 1, 1 }, { 1, 2 }, { 1, 7 }, { 1, 19 }, { 1, 20 },
                                                    { 2, 1 }, { 2, 2 }, { 2, 7 }, { 2, 16 }, { 2, 17 }, { 2, 18 }, { 2, 19 }, { 2, 20 },
                                                    { 3, 1 }, { 3, 2 }, { 3, 3 }, { 3, 4 }, { 3, 7 }, { 3, 16 }, { 3, 17 },
                                                    { 4, 0 }, { 4, 1 }, { 4, 2 }, { 4, 3 }, { 4, 4 }, { 4, 7 }, { 4, 13 }, { 4, 16 }, { 4, 17 },
                                                    { 5, 0 }, { 5, 1 }, { 5, 3 }, { 5, 4 }, { 5, 7 },
                                                    { 6, 1 }, { 6, 7 }, { 6, 17 }, { 6, 18 }, { 6, 19 }, { 6, 20 }, { 6, 21 },
                                                    { 7, 1 }, { 7, 17 }, { 7, 18 }, { 7, 19 }, { 7, 20 }, { 7, 21 },
                                                    { 8, 1 }, { 8, 2 }, { 8, 3 }, { 8, 4 }, { 8, 5 }, { 8, 6 }, { 8, 7 }, { 8, 10 },
                                                    { 9, 1 }, { 9, 4 }, { 9, 5 }, { 9, 6 }, { 9, 10 },
                                                    { 10, 4 }, { 10, 5 }, { 10, 6 }, { 10, 8 }, { 10, 9 }, { 10, 10 }, { 10, 11 }, { 10, 12 },  { 10, 17 }, { 10, 18 }, { 10, 19 }, { 10, 20 }, { 10, 21 }, { 10, 22 },
                                                    { 11, 10 }, { 11, 17 }, { 11, 18 }, { 11, 19 }, { 11, 20 }, { 11, 21 }, { 11, 22 },
                                                    { 12, 10 }, { 12, 17 }, { 12, 18 }, { 12, 19 }, { 12, 20 }, { 12, 21 }, { 12, 22 },
                                                    { 14, 2 }, { 14, 3 }, { 14, 4 }, { 14, 6 },
                                                    { 15, 2 }, { 15, 3 }, { 15, 4 }, { 15, 6 }, { 15, 10 }, { 15, 11 }, { 15, 12 }, { 15, 13 }, { 15, 14 }, 
                                                    { 16, 2 }, { 16, 3 }, { 16, 4 }, { 16, 6 }, { 16, 10 }, { 16, 11 }, { 16, 12 }, { 16, 13 }, { 16, 14 },
                                                    { 17, 0 }, { 17, 2 }, { 17, 3 }, { 17, 4 }, { 17, 6 },
                                                    { 18, 0 }, { 18, 2 }, { 18, 3 }, { 18, 4 }, { 18, 6 }, { 18, 12 }, { 18, 13 }, { 18, 14 }, { 18, 15 }, { 18, 16 }, { 18, 17 }, { 18, 18 }, { 18, 19 }, { 18, 20 }, { 18, 21 }, { 18, 22 }, { 18, 23 },
                                                    { 19, 2 }, { 19, 3 }, { 19, 4 }, { 19, 6 }, { 19, 18 }, { 19, 19 }, { 19, 20 }, { 19, 21 }, { 19, 22 }, { 19, 23 },
                                                    { 20, 2 }, { 20, 3 }, { 20, 4 }, { 20, 6 }, { 20, 18 }, { 20, 19 },
                                                    { 21, 11 }, { 21, 12 }, { 21, 13 }, { 21, 14 }, { 21, 15 }, { 21, 16 }, { 21, 18 }, { 21, 19 },
                                                    { 22, 10 }, { 22, 11 }, { 22, 18 },
                                                    { 23, 9 }, { 23, 10 }, { 23, 11 }, { 23, 18 },
                                                    { 24, 8 }, { 24, 9 }, { 24, 10 }, { 24, 11 }, { 24, 12 } };
                
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

                start_node.SetCoordinates(0,0);
                exit_node.SetCoordinates(10, 7); 


                Console.WriteLine("Search Type:\n[1] Breadth First Search\n[2] Depth First Search\n[3] A* Search");
                search_type = Convert.ToInt32(Console.ReadLine());


                 //create a tree
                //Tree test_tree = new Tree(blocked_array, start_node, exit_node);

                switch (search_type){
                    case 1:
                        //do breadth first search 
                       // BreadthFirstSearch(test_tree);
                        BreadthFirstSearch(start_node, exit_node, blocked_array, 0, node_queue);
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
             * DUPLICATE LIST - tracks nodes that are already in the tree
             * NODE_QUEUE - nodes that have not been checked for if they are the exit node
             * 
             * 1. start with the starting node as the current node
             * 2. check if the node at top of queue is the exit node
             *  Y - print out the steps 
             *  N - add all children current node to queue 
             * 3. repeat step 2
             */ 
            
            Node current_node = start_node; //start with starting node as root of tree
            
            List<string> duplicate_list = new List<string>();  //tracks duplicate nodes in list
            
            node_queue.Enqueue(current_node); //add current node to queue
            duplicate_list.Add(current_node.GetX().ToString() + "," + current_node.GetY().ToString()); //add current node to list

            while (true)
            {
                //check if first node is the solution
                Node next_node = node_queue.Dequeue();
                current_node = next_node;
                if (next_node.NodeCompare(exit_node)) //check if the node is the exit node
                {                    
                    //print out solution
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
                else //add child nodes of the queue's top node to queue
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

                    //CHILDREN 1,2,3,4
                    if ( child1.GetX() >= 0 && child1.GetY() >= 0
                        && child1.GetX() <= 24 && child1.GetY() <= 24)
                    {
                        current_node.AddChild(child1);
                        if (!duplicate_list.Contains(child1.GetX().ToString() + "," + child1.GetY().ToString()))
                        {
                            node_queue.Enqueue(child1);
                            duplicate_list.Add(child1.GetX().ToString() + "," + child1.GetY().ToString()); 
                        }
                    }
                    if (child2.GetX() >= 0 && child2.GetY() >= 0
                        && child2.GetX() <= 24 && child2.GetY() <= 24)
                    {
                        current_node.AddChild(child2);
                        if (!duplicate_list.Contains(child2.GetX().ToString() + "," + child2.GetY().ToString()))
                        {
                            node_queue.Enqueue(child2);
                            duplicate_list.Add(child2.GetX().ToString() + "," + child2.GetY().ToString());
                        }
                    }
                    if (child3.GetX() >= 0 && child3.GetY() >= 0
                        && child3.GetX() <= 24 && child3.GetY() <= 24)
                    {
                        current_node.AddChild(child3);
                        if (!duplicate_list.Contains(child3.GetX().ToString() + "," + child3.GetY().ToString()))
                        {
                            node_queue.Enqueue(child3);
                            duplicate_list.Add(child3.GetX().ToString() + "," + child3.GetY().ToString());
                        }
                    }
                    if (child4.GetX() >= 0 && child4.GetY() >= 0
                        && child4.GetX() <= 24 && child4.GetY() <= 24)
                    {
                        current_node.AddChild(child4);
                        if (!duplicate_list.Contains(child4.GetX().ToString() + "," + child4.GetY().ToString()))
                        {
                            node_queue.Enqueue(child4);
                            duplicate_list.Add(child4.GetX().ToString() + "," + child4.GetY().ToString());
                        }
                    }               
                
                }

                if (node_queue.Count > 1000) //random break so that we don't have infinite loop
                {
                    break; 
                }

            }
            return false; 
        }
    }
}
