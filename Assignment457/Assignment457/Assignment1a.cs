using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Assignment457
{
    class Assignment1a
    {
        public Assignment1a()
        {

        }


        public void PartA()
        {
            // create blocked list array 
            int[,] blocked_array = new int[,] { { 0, 23 }, { 0, 22 }, { 0, 17 }, { 0, 5 }, { 0, 4 },
                                                { 1, 23 }, { 1, 22 }, { 1, 17 }, { 1, 5 }, { 1, 4 },
                                                { 2, 23 }, { 2, 22 }, { 2, 17 }, { 2, 8 }, { 2, 7 }, { 2, 6 }, { 2, 5 }, { 2, 4 },
                                                { 3, 23 }, { 3, 22 }, { 3, 21 }, { 3, 20 }, { 3, 17 }, { 3, 8 }, { 3, 7 },
                                                { 4, 24 }, { 4, 23 }, { 4, 22 }, { 4, 21 }, { 4, 20 }, { 4, 17 }, { 4, 11 }, { 4, 8 }, { 4, 7 },
                                                { 5, 24 }, { 5, 23 }, { 5, 21 }, { 5, 20 }, { 5, 17 },
                                                { 6, 23 }, { 6, 17 }, { 6, 7 }, { 6, 6 }, { 6, 5 }, { 6, 4 }, { 6, 3 },
                                                { 7, 23 }, { 7, 7 }, { 7, 6 }, { 7, 5 }, { 7, 4 }, { 7, 3 },
                                                { 8, 23 }, { 8, 22 }, { 8, 21 }, { 8, 20 }, { 8, 19 }, { 8, 18 }, { 8, 17 }, { 8, 14 },
                                                { 9, 23 }, { 9, 20 }, { 9, 19 }, { 9, 18 }, { 9, 14 },
                                                { 10, 20 }, { 10, 19 }, { 10, 18 }, { 10, 16 }, { 10, 15 }, { 10, 14 }, { 10, 13 }, { 10, 12 },  { 10, 7 }, { 10, 6 }, { 10, 5 }, { 10, 4 }, { 10, 3 }, { 10, 2 },
                                                { 11, 14 }, { 11, 7 }, { 11, 6 }, { 11, 5 }, { 11, 4 }, { 11, 3}, { 11, 2 },
                                                { 12, 14 }, { 12, 7 }, { 12, 6 }, { 12, 5 }, { 12, 4 }, { 12, 3 }, { 12, 2 },
                                                { 14, 22 }, { 14, 21 }, { 14, 20 }, { 14, 18 },
                                                { 15, 22 }, { 15, 21 }, { 15, 20 }, { 15, 18 }, { 15, 14 }, { 15, 13 }, { 15, 12 }, { 15, 11 }, { 15, 10 }, 
                                                { 16, 22 }, { 16, 21 }, { 16, 20 }, { 16, 18 }, { 16, 14 }, { 16, 13 }, { 16, 12 }, { 16, 11 }, { 16, 10 },
                                                { 17, 24 }, { 17, 22 }, { 17, 21 }, { 17, 20 }, { 17, 18 },
                                                { 18, 24 }, { 18, 22 }, { 18, 21 }, { 18, 20 }, { 18, 18 }, { 18, 12 }, { 18, 11 }, { 18, 10 }, { 18, 9 }, { 18, 8 }, { 18, 7 }, { 18, 6 }, { 18, 5 }, { 18, 4 }, { 18, 3 }, { 18, 2 }, { 18, 1 },
                                                { 19, 22 }, { 19, 21 }, { 19, 20 }, { 19, 18 }, { 19, 6 }, { 19, 5 }, { 19, 4 }, { 19, 3 }, { 19, 2 }, { 19, 1 },
                                                { 20, 22 }, { 20, 21 }, { 20, 20 }, { 20, 18 }, { 20, 6 }, { 20, 5 },
                                                { 21, 13 }, { 21, 12 }, { 21, 11 }, { 21, 10 }, { 21, 9 }, { 21, 8 }, { 21, 6 }, { 21, 5 },
                                                { 22, 14 }, { 22, 13 }, { 22, 6 },
                                                { 23, 15 }, { 23, 14 }, { 23, 13 }, { 23, 6 },
                                                { 24, 16 }, { 24, 15 }, { 24, 14 }, { 24, 13 }, { 24, 12 } };

            // while (true)
            // {
            /*
            Console.WriteLine("Enter start node coordinates: x");
            int x = Convert.ToInt32(Console.ReadLine()); 
            Console.WriteLine("Enter start node coordinates: y");
            int y = Convert.ToInt32(Console.ReadLine()); 
            start_node.SetCoordinates(x,y);

                
            */


            // start and end nodes
            Node start_node = new Node();
            Node exit_node = new Node();

            //TEST 1 [E1]
            //start_node.SetCoordinates(2, 11);
            //exit_node.SetCoordinates(23, 19);

            //TEST 2 [E2]
            //start_node.SetCoordinates(2, 11);
            //exit_node.SetCoordinates(2, 21);

            //TEST 3 [E3]
            //start_node.SetCoordinates(0,0);
            //exit_node.SetCoordinates(24, 24);

            //TEST FAIL
            //start_node.SetCoordinates(0, 24);
            //exit_node.SetCoordinates(24, 24);


            /* * List of All Nodes and their status
               * 0 - > Not Visited and Not Blocked
               * 1 - > Blocked
               * 2 - > Exit Node
               * 3 - > Visited nodes
               * 4 - > Nodes part of solution path
               * 5 - > Start Node (Not used yet, except by printSearchInformation method)
               * 6 - > Node that is explored
               * 7 - > Node that is to be explored
            */
            // set node statuses
            byte[,] node_list = new byte[25, 25];
            node_list[exit_node.GetX(), exit_node.GetY()] = 2; // set exit node
            for (int i = 0; i < blocked_array.GetLength(0); i++)
            {
                node_list[blocked_array[i, 0], blocked_array[i, 1]] = 1;  //assumed that by default all array values are false, so make blocked true;
            }


            // variables required for search
            int steps = 1;
            bool searchResult = false;
            Stopwatch stopWatch = new Stopwatch();

            // get user input
            Console.WriteLine("Search Type:\n[1] Breadth First Search\n[2] Depth First Search\n[3] Optimized Depth First Search\n[4] A* Search");
            int search_type = Convert.ToInt32(Console.ReadLine());

            // perform search algorithm
            switch (search_type)
            {
                case 1:
                    //do breadth first search     
                    stopWatch.Start();
                    searchResult = BreadthFirstSearch(start_node, exit_node, ref node_list, ref steps);
                    stopWatch.Stop();
                    if (searchResult)
                    {
                        printSearchInformation(start_node, steps, node_list);
                    }
                    else
                    {
                        Console.WriteLine("Search Failed!");
                    }
                    Console.WriteLine("Total Search Time: " + stopWatch.Elapsed.ToString());
                    stopWatch.Reset();
                    break;
                case 2:
                    stopWatch.Start();
                    searchResult = DepthFirstSearch(start_node, ref steps, ref node_list);
                    stopWatch.Stop();
                    if (searchResult)
                    {
                        printSearchInformation(start_node, steps, node_list);
                    }
                    else
                    {
                        Console.WriteLine("Search Failed!");
                    }
                    Console.WriteLine("Total Search Time: " + stopWatch.Elapsed.ToString());
                    stopWatch.Reset();
                    break;
                case 3:
                    stopWatch.Start();
                    steps = OptDepthFirstSearch(start_node, ref node_list);
                    stopWatch.Stop();
                    searchResult = (steps == -1) ? false : true;
                    if (searchResult)
                    {
                        printSearchInformation(start_node, steps, node_list);
                    }
                    else
                    {
                        Console.WriteLine("Search Failed!");
                    }
                    Console.WriteLine("Total Search Time: " + stopWatch.Elapsed.ToString());
                    stopWatch.Reset();
                    break;
                case 4: // A* search
                    start_node.calculatePathCost(start_node, exit_node);
                    node_list[exit_node.GetX(), exit_node.GetY()] = 0; // set exit node

                    stopWatch.Start();
                    searchResult = AStarSearch(start_node, exit_node, node_list, ref steps);
                    stopWatch.Stop();
                    if (searchResult)
                    {
                        printSearchInformation(start_node, steps, node_list);
                    }
                    else
                    {
                        Console.WriteLine("Search Failed!");
                    }
                    Console.WriteLine("Total Search Time: " + stopWatch.Elapsed.ToString());
                    stopWatch.Reset();
                    break;
            }

            Console.WriteLine();
            //}
        }

        static bool BreadthFirstSearch(Node start_node, Node exit_node, ref byte[,] node_list, ref int steps)
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
            Queue<Node> node_queue = new Queue<Node>();  // unvisited queue node
            Node current_node = start_node; //start with starting node as root of tree

            List<string> duplicate_list = new List<string>();  //tracks duplicate nodes in list

            node_queue.Enqueue(current_node); //add current node to queue
            duplicate_list.Add(current_node.GetX().ToString() + "," + current_node.GetY().ToString()); //add current node to list

            while (true)
            {
                Node next_node = null;

                if (node_queue.Count > 0) //make sure that queue is not empty
                {
                    //check if first node is the solution
                    next_node = node_queue.Dequeue();
                }
                else
                {
                    break; //break out of while loop
                }
                current_node = next_node;
                if (next_node.NodeCompare(exit_node)) //check if the node is the exit node
                {
                    //print out solution
                    Console.WriteLine("Found Solution: \n" + current_node.GetX().ToString() + "," + current_node.GetY().ToString());
                    while (current_node.GetParent() != null)
                    {
                        current_node = current_node.GetParent();
                        //Console.WriteLine(current_node.GetX().ToString() + "," + current_node.GetY().ToString());
                        node_list[current_node.GetX(), current_node.GetY()] = 4;
                        steps++;
                    }
                    Console.WriteLine("Distance: " + steps.ToString());
                    return true;
                }
                else //add child nodes of the queue's top node to queue
                {
                    //mark current node as visited
                    node_list[current_node.GetX(), current_node.GetY()] = 3;

                    for (int i = 0; i < 4; i++)
                    {
                        //get current node's position
                        int x = current_node.GetX();
                        int y = current_node.GetY();
                        Node child = null; //child node 

                        //create child nodes
                        switch (i)
                        {
                            case 0:
                                child = new Node(++x, y, current_node);
                                break;
                            case 1:
                                child = new Node(--x, y, current_node);
                                break;
                            case 2:
                                child = new Node(x, ++y, current_node);
                                break;
                            case 3:
                                child = new Node(x, --y, current_node);
                                break;
                        }
                        //check that current node is not out of bounds
                        //or not blocked node
                        if (child.GetX() >= 0 && child.GetY() >= 0
                            && child.GetX() <= 24 && child.GetY() <= 24
                            && node_list[child.GetX(), child.GetY()] != 1)
                        {
                            current_node.AddChild(child);
                            if (!duplicate_list.Contains(child.GetX().ToString() + "," + child.GetY().ToString()))
                            {
                                node_queue.Enqueue(child);
                                duplicate_list.Add(child.GetX().ToString() + "," + child.GetY().ToString());
                            }
                        }
                    }
                }
            }
            return false;
        }

        static bool DepthFirstSearch(Node node, ref int count, ref byte[,] node_list)
        {
            int x, y;
            x = node.GetX();
            y = node.GetY();

            // check if reached endpoint
            if (node_list[x, y] == 2)
            {
                return true;
            }

            node_list[x, y] = 3; // mark as visited

            //Go through children, create tree and conduct depth first search
            for (int i = 0; i < 4; i++)
            {
                Node child_node = null;
                switch (i)
                {
                    case 0:
                        if (x < 24 && node_list[x + 1, y] != 1)
                        {
                            child_node = new Node(x + 1, y, node);
                            node.AddChild(child_node);
                        }
                        break;
                    case 1:
                        if (x > 0 && node_list[x - 1, y] != 1)
                        {
                            child_node = new Node(x - 1, y, node);
                            node.AddChild(child_node);
                        }
                        break;
                    case 2:
                        if (y < 24 && node_list[x, y + 1] != 1)
                        {
                            child_node = new Node(x, y + 1, node);
                            node.AddChild(child_node);
                        }
                        break;
                    case 3:
                        if (y > 0 && node_list[x, y - 1] != 1)
                        {
                            child_node = new Node(x, y - 1, node);
                            node.AddChild(child_node);
                        }
                        break;
                }

                //only perform DFS on child node, if it has been added
                if (child_node != null)
                {
                    if (node_list[child_node.GetX(), child_node.GetY()] != 3)
                    {
                        if (DepthFirstSearch(child_node, ref count, ref node_list))
                        {
                            node_list[node.GetX(), node.GetY()] = 4; // mark path as part of solution
                            count++;
                            return true;
                        }
                        else
                        {
                            node.RemoveChild(child_node);
                        }
                    }
                    else
                    {
                        node.RemoveChild(child_node);
                    }
                }
            }
            return false;
        }

        static int OptDepthFirstSearch(Node node, ref byte[,] node_list)
        {
            int x, y;
            x = node.GetX();
            y = node.GetY();

            // check if reached endpoint
            if (node_list[x, y] == 2)
            {
                return 1;
            }

            node_list[x, y] = 3; // mark as visited
            int size = 10000000;
            Node chosen_child = null;
            //Go through children, create tree and conduct depth first search
            for (int i = 0; i < 4; i++)
            {
                Node child_node = null;
                switch (i)
                {
                    case 0:
                        if (x < 24 && node_list[x + 1, y] != 1)
                        {
                            child_node = new Node(x + 1, y, node);
                        }
                        break;
                    case 1:
                        if (x > 0 && node_list[x - 1, y] != 1)
                        {
                            child_node = new Node(x - 1, y, node);
                        }
                        break;
                    case 2:
                        if (y < 24 && node_list[x, y + 1] != 1)
                        {
                            child_node = new Node(x, y + 1, node);
                        }
                        break;
                    case 3:
                        if (y > 0 && node_list[x, y - 1] != 1)
                        {
                            child_node = new Node(x, y - 1, node);
                        }
                        break;
                }

                //only perform DFS on child node, if it has been added
                if (child_node != null)
                {
                    if (node_list[child_node.GetX(), child_node.GetY()] != 3)
                    {
                        int count;
                        if ((count = OptDepthFirstSearch(child_node, ref node_list)) != -1)
                        {
                            if (count < size)
                            {
                                size = count;
                                chosen_child = child_node;
                            }
                            else if (count == size)
                            {
                                node_list[child_node.GetX(), child_node.GetY()] = 8; // Mark as visted, but potential solution
                            }
                        }
                    }
                }
            }

            if (chosen_child != null)
            {
                node_list[node.GetX(), node.GetY()] = 4; // mark path as part of solution
                node.AddChild(chosen_child);
                return size + 1;
            }
            return -1;
        }

        //Hacky Method to print out information and graph.
        static void printSearchInformation(Node start_node, int steps, byte[,] node_list)
        {
            int nodes_not_visited = 0;
            node_list[start_node.GetX(), start_node.GetY()] = 5;
            for (int l = node_list.GetLength(1) - 1; l >= 0; l--)
            {
                for (int k = 0; k < node_list.GetLength(0); k++)
                {
                    if (node_list[k, l] == 0)
                    {
                        Console.Write("-");
                        nodes_not_visited++;
                    }
                    else if (node_list[k, l] == 1)
                    {
                        Console.Write("#");
                        nodes_not_visited++; // blocked nodes can never been visited...
                    }
                    else if (node_list[k, l] == 3 || node_list[k, l] == 6 || node_list[k, l] == 7)
                    {
                        Console.Write("-");
                    }
                    else if (node_list[k, l] == 4)
                    {
                        Console.Write("*");
                    }
                    else if (node_list[k, l] == 2)
                    {
                        Console.Write("E");
                    }
                    else if (node_list[k, l] == 5)
                    {
                        Console.Write("S");
                    }
                    else
                    {
                        Console.Write("-");
                    }

                }
                Console.WriteLine("");
            }
            Console.WriteLine("Solution Cost: " + steps);
            Console.WriteLine("Total Visited Nodes: " + ((25 * 25) - nodes_not_visited));
            Console.WriteLine("");
        }

        // A Star Search Method 
        static bool AStarSearch(Node startNode, Node endNode, byte[,] nodeMap, ref int steps)
        {
            int nodesExplored = 1; // the start node
            Node currentNode = startNode;
            List<Node> openList = new List<Node>(); // this is a list of nodes to be explored
            openList.Add(currentNode); // Add start node to list
            while (openList.Count != 0) // if nothing in list, no solution
            {
                // remove first node from the list to explore
                currentNode = openList.ElementAt(0);
                openList.RemoveAt(0);
                nodeMap[currentNode.GetX(), currentNode.GetY()] = 6; // the node will be explored
                if (currentNode.NodeCompare(endNode)) // if solution is found, display results
                {
                    steps = currentNode.F + 1; // + 1 to also include start node
                    markSolutionPath(currentNode, nodesExplored, nodeMap);
                    nodeMap[endNode.GetX(), endNode.GetY()] = 2;
                    return true;
                }
                else //add child nodes to list
                {
                    //get the current node's coordinates
                    int x = currentNode.GetX();
                    int y = currentNode.GetY();

                    //create child nodes
                    Node child1 = new Node(x + 1, y, currentNode);
                    Node child2 = new Node(x - 1, y, currentNode);
                    Node child3 = new Node(x, y + 1, currentNode);
                    Node child4 = new Node(x, y - 1, currentNode);

                    //calculate path costs of all nodes
                    child1.calculatePathCost(startNode, endNode);
                    child2.calculatePathCost(startNode, endNode);
                    child3.calculatePathCost(startNode, endNode);
                    child4.calculatePathCost(startNode, endNode);

                    // add nodes that are to be explored
                    addToOpenList(currentNode, child1, openList, nodeMap, ref nodesExplored);
                    addToOpenList(currentNode, child2, openList, nodeMap, ref nodesExplored);
                    addToOpenList(currentNode, child3, openList, nodeMap, ref nodesExplored);
                    addToOpenList(currentNode, child4, openList, nodeMap, ref nodesExplored);

                    // order open list by lowest cost (F)
                    var query = from node in openList
                                orderby node.F ascending
                                select node;
                    openList = query.ToList();
                } //fi
            } //elihw 

            return false; //path has not been found
        }// end method

        public static void addToOpenList(Node currentNode, Node childNode, List<Node> openList, byte[,] nodeMap, ref int nodesExplored)
        {
            int x = childNode.GetX();
            int y = childNode.GetY();

            // ignore nodes that are on the closed list or blocked or out of bounds
            if (!(x >= 0 && x <= 24 && y >= 0 && y <= 24) ||
                nodeMap[x, y] == 1 || nodeMap[x, y] == 6)
                return;

            // if adjacent square is already in the open list, update if necessary
            if (nodeMap[x, y] == 7)
            {
                // path cost if you go vertically or horizontally is the same (set to one in our case)
                int newGScore = currentNode.G + 1;
                // if newGScore is lower, then update child node
                if (newGScore < childNode.G)
                    updateNodeInList(openList, childNode, currentNode, newGScore);
                // else ignore
            }
            else // if adjacent square is not in the open list, add to list to be explored
            {
                openList.Add(childNode);
                nodeMap[childNode.GetX(), childNode.GetY()] = 7; // node is in open list to be explored
                nodesExplored++;
            }
        }

        public static void updateNodeInList(List<Node> list, Node childNode, Node currentNode, int newGScore)
        {
            // get and remove node from list
            IEnumerable<Node> query = from node in list
                                      where node.GetX() == childNode.GetX() && node.GetY() == childNode.GetY()
                                      select node;
            IEnumerator<Node> e = query.GetEnumerator();
            e.MoveNext();
            Node updatedNode = e.Current;
            list.Remove(e.Current);

            // update G and F scores, and change parent to current node
            updatedNode.G = newGScore;
            updatedNode.F = updatedNode.G + updatedNode.H;
            updatedNode.setParent(currentNode);

            // add updated node to list
            list.Add(updatedNode);
        }

        public static void markSolutionPath(Node currentNode, int nodesExplored, byte[,] nodeList)
        {
            // get root node of current Node
            Node parentNode = currentNode;
            while (parentNode != null)
            {
                // mark nodes that are on the solution path
                nodeList[parentNode.GetX(), parentNode.GetY()] = 4;
                parentNode = parentNode.GetParent();
            }
        }
    }
}
