﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    class Assignment1c
    {
        private const int MOVE_FREQUENCY_LIMIT = 50;

        public Assignment1c() {   }

        public void RunPartC()
        {
            Console.WriteLine();
            Console.WriteLine("Search Agent - Tabu search");

            int[,] distanceMatrix = new int[21,21] 
            {  
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 1, 2, 3, 4, 1, 2, 3, 4, 5, 2, 3, 4, 5, 6, 3, 4, 5, 6, 7},
                {0, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 3, 4, 5, 6},
                {0, 2, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 3, 4, 5},
                {0, 3, 2, 1, 0, 1, 4, 3, 2, 1, 2, 5, 4, 3, 2, 3, 6, 5, 4, 3, 4},
                {0, 4, 3, 2, 1, 0, 5, 4, 3, 2, 1, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3},
                {0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 1, 2, 3, 4, 5, 2, 3, 4, 5, 6},
                {0, 2, 1, 2, 3, 4, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4, 3, 2, 3, 4, 5},
                {0, 3, 2, 1, 2, 3, 2, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4, 3, 2, 3, 4},
                {0, 4, 3, 2, 1, 2, 3, 2, 1, 0, 1, 4, 3, 2, 1, 2, 5, 4, 3, 2, 3},
                {0, 5, 4, 3, 2, 1, 4, 3, 2, 1, 0, 5, 4, 3, 2, 1, 6, 5, 4, 3, 2},
                {0, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 1, 2, 3, 4, 5},
                {0, 3, 2, 3, 4, 5, 2, 1, 2, 3, 4, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4},
                {0, 4, 3, 2, 3, 4, 3, 2, 1, 2, 3, 2, 1, 0, 1, 2, 3, 2, 1, 2, 3},
                {0, 5, 4, 3, 2, 3, 4, 3, 2, 1, 2, 3, 2, 1, 0, 1, 4, 3, 2, 1, 2},
                {0, 6, 5, 4, 3, 2, 5, 4, 3, 2, 1, 4, 3, 2, 1, 0, 5, 4, 3, 2, 1},
                {0, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4},
                {0, 4, 3, 4, 5, 6, 3, 2, 3, 4, 5, 2, 1, 2, 3, 4, 1, 0, 1, 2, 3},
                {0, 5, 4, 3, 4, 5, 4, 3, 2, 3, 4, 3, 2, 1, 2, 3, 2, 1, 0, 1, 2},
                {0, 6, 5, 4, 3, 4, 5, 4, 3, 2, 3, 4, 3, 2, 1, 2, 3, 2, 1, 0, 1},
                {0, 7, 6, 5, 4, 3, 6, 5, 4, 3, 2, 5, 4, 3, 2, 1, 4, 3, 2, 1, 0}
            };

            int[,] flowMatrix = new int[21, 21]
            {
                {0,  0,  0, 0,  0, 0,  0,  0,  0, 0, 0,  0,  0,  0,  0,  0,  0, 0, 0,  0,  0},
                {0,  0,  0, 5,  0, 5,  2, 10,  3, 1, 5,  5,  5,  0,  0,  5,  4, 4, 0,  0,  1},
                {0,  0,  0, 3, 10, 5,  1,  5,  1, 2, 4,  2,  5,  0, 10, 10,  3, 0, 5, 10,  5},
                {0,  5,  3, 0,  2, 0,  5,  2,  4, 4, 5,  0,  0,  0,  5,  1,  0, 0, 5,  0,  0},
                {0,  0, 10, 2,  0, 1,  0,  5,  2, 1, 0, 10,  2,  2,  0,  2,  1, 5, 2,  5,  5},
                {0,  5,  5, 0,  1, 0,  5,  6,  5, 2, 5,  2,  0,  5,  1,  1,  1, 5, 2,  5,  1},
                {0,  2,  1, 5,  0, 5,  0,  5,  2, 1, 6,  0,  0, 10,  0,  2,  0, 1, 0,  1,  5},
                {0, 10,  5, 2,  5, 6,  5,  0,  0, 0, 0,  5, 10,  2,  2,  5,  1, 2, 1,  0, 10},
                {0,  3,  1, 4,  2, 5,  2,  0,  0, 1, 1, 10, 10,  2,  0, 10,  2, 5, 2,  2, 10},
                {0,  1,  2, 4,  1, 2,  1,  0,  1, 0, 2,  0,  3,  5,  5,  0,  5, 0, 0,  0,  2},
                {0,  5,  4, 5,  0, 5,  6,  0,  1, 2, 0,  5,  5,  0,  5,  1,  0, 0, 5,  5,  2},
                {0,  5,  2, 0, 10, 2,  0,  5, 10, 0, 5,  0,  5,  2,  5,  1, 10, 0, 2,  2,  5},
                {0,  5,  5, 0,  2, 0,  0, 10, 10, 3, 5,  5,  0,  2, 10,  5,  0, 1, 1,  2,  5},
                {0,  0,  0, 0,  2, 5, 10,  2,  2, 5, 0,  2,  2,  0,  2,  2,  1, 0, 0,  0,  5},
                {0,  0, 10, 5,  0, 1,  0,  2,  0, 5, 5,  5, 10,  2,  0,  5,  5, 1, 5,  5,  0},
                {0,  5, 10, 1,  2, 1,  2,  5, 10, 0, 1,  1,  5,  2,  5,  0,  3, 0, 5, 10, 10},
                {0,  4,  3, 0,  1, 1,  0,  1,  2, 5, 0, 10,  0,  1,  5,  3,  0, 0, 0,  2,  0},
                {0,  4,  0, 0,  5, 5,  1,  2,  5, 0, 0,  0,  1,  0,  1,  0,  0, 0, 5,  2,  0},
                {0,  0,  5, 5,  2, 2,  0,  1,  2, 0, 5,  2,  1,  0,  5,  5,  0, 5, 0,  1,  1},
                {0,  0, 10, 0,  5, 5,  1,  0,  2, 0, 5,  2,  2,  0,  5, 10,  2, 2, 1,  0,  6},
                {0,  1,  5, 0,  5, 1,  5, 10, 10, 2, 2,  5,  5,  5,  0, 10,  0, 0, 1,  6,  0}
            };

            doTabuSearch(distanceMatrix, flowMatrix);

            return; 
        }

        public void doTabuSearch(int [,] distanceMatrix, int[,] flowMatrix)
        {
            // department[i] = department, i = location
            // Initial solution
            int[] departments = new int[21]
                {  
                    0,  // ignore
                    1,  2,  3,  4,  5,  6,  7,  8,  9, 10,
                   11, 12, 13, 14, 15, 16, 17, 18, 19, 20 
                };

            // get initial cost 
            int currentCost = computeCost(departments, distanceMatrix, flowMatrix);
            int bestCost = currentCost;
            Console.WriteLine("Cost: " + currentCost + ", iter: 0");

            // create tabu list
            int[,] tabuList = new int[21, 21];

            // create list to store the frequency of the moves
            int[,] frequencyList = new int[21, 21];

            // find optimal cost
            int counter = 0;
            while (counter < 1000 && bestCost > 2570) // stop after 2000 iterations or if the optimal cost is found
            {
                counter++;

                // reduce tenure value
                resetTabuList(tabuList);

                // Get sorted candidate list
                List<Pair> candidatePairs = getAllCandidatesFromNeighbourhood(tabuList, frequencyList, departments, bestCost, distanceMatrix, flowMatrix);

                // make sure that the the candidates list is not empty
                if (candidatePairs.Count > 0)
                {
                    // get the candidate that gives the best solution
                    Pair bestCandidate = candidatePairs.ElementAt(0);

                    // if the swap improves the cost, save it as best cost
                    if (bestCandidate.cost < bestCost)
                    {
                        bestCost = bestCandidate.cost;
                        Console.WriteLine("Cost: " + bestCandidate.cost + ", iter: " + counter + ", swapped (" + bestCandidate.x + "," + bestCandidate.y +")" );
                    }

                    // save solution
                    departments = swapValues(departments, bestCandidate.x, bestCandidate.y);
                    currentCost = bestCandidate.cost;

                    int siteA = bestCandidate.x;
                    int siteB = bestCandidate.y;
                    int depA = departments[bestCandidate.y];
                    int depB = departments[bestCandidate.x];

                    // set tenure
                    tabuList[depA, siteA] = 9; 
                    tabuList[depB, siteB] = 9;

                    // set dynamic tabu list
                    //setTenure(tabuList, siteA, siteB, depA, depB);

                    // increase frequency of move
                    //frequencyList[depA, siteA] = frequencyList[depA, siteA] + 1;
                    //frequencyList[depB, siteB] = frequencyList[depB, siteB] + 1;
                }
            }

            Console.WriteLine("The best cost is " + bestCost + ".");
            Console.WriteLine("Solution: " + convToString(departments[1]) + "  " + convToString(departments[2]) + "  "
                                           + convToString(departments[3]) + "  " + convToString(departments[4]));
            Console.WriteLine("          " + convToString(departments[5]) + "  " + convToString(departments[6]) + "  "
                                           + convToString(departments[7]) + "  " + convToString(departments[8]));
            Console.WriteLine("          " + convToString(departments[9]) + "  " + convToString(departments[10]) + "  "
                                           + convToString(departments[11]) + "  " + convToString(departments[12]));
            Console.WriteLine("          " + convToString(departments[13]) + "  " + convToString(departments[14]) + "  "
                                           + convToString(departments[15]) + "  " + convToString(departments[16]));
            Console.WriteLine("          " + convToString(departments[17]) + "  " + convToString(departments[18]) + "  "
                                           + convToString(departments[19]) + "  " + convToString(departments[20]));
            Console.WriteLine();
        }

        public string convToString(int i)
        {
            if ((i / 10) > 0)
                return i.ToString();
            else
                return " " + i;
        }

        public void resetTabuList(int[,] tabuList)
        {
            for (int i = 1; i < tabuList.GetLength(0); i++)
            {
                for (int j = 1; j < tabuList.GetLength(0); j++)
                {
                    if (tabuList[i, j] > 0)
                        tabuList[i, j] = tabuList[i, j] - 1;
                }
            }
        }

        // Makes tabu list size dynamic
        public void setTenure(int [,] tabuList, int siteA, int siteB, int depA, int depB)
        {
            Random random = new Random();
            int randomNumber = random.Next(2, 8);

            tabuList[depB, siteB] = randomNumber;
            randomNumber = random.Next(0, 20);
            tabuList[depA, siteA] = randomNumber;
        }

        // Use less than the whole neighbourhood to select the next solution
        public List<Pair> getSomeCandidatesFromNeighbourhood(
            int[,] tabuList, int[,] frequencyList,
            int[] departments, int bestCost, 
            int[,] distanceMatrix, int[,] flowMatrix)
        {
            Random random = new Random();
            int iRandom = random.Next(1, 20);
            int jRandom = random.Next(1, 20);

            // check to make sure that the moves that valid
            while (iRandom == jRandom || iRandom > jRandom)
            {
                iRandom = random.Next(1, 20);
                jRandom = random.Next(1, 20);
            }

            List<Pair> list = new List<Pair>();
            for (int i = iRandom; i < departments.Length; i++)
            {
                for (int j = jRandom; j < departments.Length; j++)
                {
                    if (!usedMoveToManyTimes(frequencyList, i, j, departments[i], departments[j]))
                    {
                        int[] newDepartments = swapValues(departments, i, j);
                        int cost = computeCost(newDepartments, distanceMatrix, flowMatrix);
                        Pair pair = new Pair(i, j, cost);

                        if (isInTabuList(tabuList, i, j, departments[i], departments[j]))
                        {
                            // Aspiration Condition - A tabu move will be considered only if it 
                            // provides a better solution than the best solution previously computed 
                            if (cost < bestCost)
                                list.Add(pair);
                        }
                        else
                            list.Add(pair);
                    }
                }
            }

            // Aspiration Criteria - Order list with the best solution first. This
            // means that the best solution from the neighbourhood is always selected.
            var query = from pair in list
                        orderby pair.cost ascending
                        select pair;
            return query.ToList();
        }

        public List<Pair> getAllCandidatesFromNeighbourhood(
            int[,] tabuList, int[,] frequencyList,
            int[] departments, int bestCost, 
            int[,] distanceMatrix, int[,] flowMatrix)
        {
            List<Pair> list = new List<Pair>();
            for (int i = 1; i < departments.Length; i++)
            {
                for (int j = i + 1; j < departments.Length; j++)
                {
                    if (!usedMoveToManyTimes(frequencyList, i, j, departments[i], departments[j]))
                    {
                        int[] newDepartments = swapValues(departments, i, j);
                        int cost = computeCost(newDepartments, distanceMatrix, flowMatrix);
                        Pair pair = new Pair(i, j, cost);

                        if (isInTabuList(tabuList, i, j, departments[i], departments[j]))
                        {
                            // Aspiration Criteria - A tabu move will be considered only if it 
                            // provides a better solution than the best solution previously computed 
                            if (cost < bestCost)
                                list.Add(pair);
                        }
                        else
                        {
                            list.Add(pair);
                        }
                    }
                }
            }
            
            // Aspiration Criteria - Order list with the best solution first. This
            // means that the best solution from the neighbourhood is always selected.
            var query = from pair in list
                        orderby pair.cost ascending
                        select pair;
            return query.ToList();
        }

        public bool isInTabuList(int[,] tabuList, int siteA, int siteB, int depA, int depB) 
        {
            // check if the reverse move is in the tabu list
            if (tabuList[depA, siteB] > 0 || tabuList[depB, siteA] > 0)
                return true; // if it is, dont pursue it
            return false; // move not in tabu list
        }

        public bool usedMoveToManyTimes(int[,] frequencyList, int siteA, int siteB, int depA, int depB)
        {
            // check the frequency of the reverse move
            if (frequencyList[depA, siteB] > MOVE_FREQUENCY_LIMIT ||
                frequencyList[depB, siteA] > MOVE_FREQUENCY_LIMIT)
                return true; // if it is, dont pursue it
            return false; // move is allowed
        }

        public int[] swapValues(int[] list, int x, int y)
        {
            // create a new list (a new instance so that the original does not get overwritten)
            int[] newList = new int[21];
            for (int i = 0; i < list.Length; i++)
            {
                newList[i] = list[i];
            }

            // swap values in the array
            int a = newList[x], b = newList[y];
            newList[y] = a;
            newList[x] = b;
            return newList;
        }

        public int computeCost(int[] departments, int[,] distanceMatrix, int[,] flowMatrix)
        {
            // compute cost
            int solution = 0;
            for (int i = 1; i < departments.Length; i++)
            {
                for (int j = 1; j < departments.Length; j++)
                {
                    int flow = flowMatrix[departments[i], departments[j]];
                    int distance = distanceMatrix[i, j];
                    solution = solution + (distance * flow);
                }
            }

            return solution;
        }
    }
}
