﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    class Assignment1c
    {
        private const int MOVE_FREQUENCY_LIMIT = 100;

        public Assignment1c()
        {

        }

        public void RunPartC()
        {
            Console.WriteLine("Search Agent - Tabu search");

            int[,] distanceMatrix = new int[21,21] 
            {  
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 1, 2, 3, 4, 1, 2, 3, 4, 5, 2, 3, 4, 5, 6, 3, 4, 5, 6, 7},
                {0, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 3, 4, 5, 6},
                {0, 2, 1, 0, 1, 2, 3, 2, 1, 2, 3, 4, 3, 2, 3, 4, 5, 4, 3, 4, 5},
                {0, 3, 2, 1, 0, 5, 4, 3, 2, 1, 2, 5, 4, 3, 2, 3, 6, 5, 4, 3, 4},
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
            int[] departments = new int[21] 
                {  
                    0,  // ignore
                    1,  2,  3,  4,  5,  6,  7,  8,  9, 10,
                   11, 12, 13, 14, 15, 16, 17, 18, 19, 20 
                };

            // get initial solution 
            int currentSolution = computeSolution(departments, distanceMatrix, flowMatrix);
            int bestSolution = currentSolution;
            Console.WriteLine(currentSolution);

            // create tabu list
            int[,] tabuList = new int[21, 21];

            // create list to store the frequency of the moves
            int[,] frequencyList = new int[21, 21];

            // find optimal solution
            int counter = 0;
            while (counter < 2000) // stop after 2000 iterations
            {
                counter++;

                // reduce tenure value
                resetTabuList(tabuList);

                // Get sorted candidate list
                List<Pair> candidatePairs = getAllCandidatesFromNeighbourhood(tabuList, frequencyList, departments, bestSolution, distanceMatrix, flowMatrix);

                // make sure that the the candidates list is not empty
                if (candidatePairs.Count > 0)
                {
                    // get the solution of the best candidate
                    Pair bestCandidate = candidatePairs.ElementAt(0);

                    // if the swap improves the solution, save it as best solution
                    if (bestCandidate.solution < bestSolution)
                    {
                        bestSolution = bestCandidate.solution;
                        Console.WriteLine(bestCandidate.solution + ", iter: " + counter + ", swapped (" + bestCandidate.x + "," + bestCandidate.y +")" );
                    }

                    // save ordering
                    departments = swapValues(departments, bestCandidate.x, bestCandidate.y);
                    currentSolution = bestCandidate.solution;

                    int siteA = bestCandidate.x;
                    int siteB = bestCandidate.y;
                    int depA = departments[bestCandidate.y];
                    int depB = departments[bestCandidate.x];

                    // set tenure
                    tabuList[depA, siteA] = 10; 
                    tabuList[depB, siteB] = 10;

                    // set dynamic tabu list
                    //setTenure(tabuList, siteA, siteB, depA, depB);

                    // increase frequency of move
                    //frequencyList[depA, siteA] = frequencyList[depA, siteA] + 1;
                    //frequencyList[depB, siteB] = frequencyList[depB, siteB] + 1;
                }
            }

            Console.WriteLine("The best Solution is " + bestSolution);
            for (int i = 0; i < departments.Length; i++)
            {
                Console.Write(departments[i] + ", ");
            }

            Console.WriteLine();
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
            int[] departments, int bestSolution, 
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
                        int solution = computeSolution(newDepartments, distanceMatrix, flowMatrix);
                        Pair pair = new Pair(i, j, solution, true);

                        if (isInTabuList(tabuList, i, j, departments[i], departments[j]))
                        {
                            // Aspiration Condition - A tabu move will be considered only if it 
                            // provides a better solution than the best solution previously computed 
                            if (solution < bestSolution)
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
                        orderby pair.solution ascending
                        select pair;
            return query.ToList();
        }

        public List<Pair> getAllCandidatesFromNeighbourhood(
            int[,] tabuList, int[,] frequencyList,
            int[] departments, int bestSolution, 
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
                        int solution = computeSolution(newDepartments, distanceMatrix, flowMatrix);
                        Pair pair = new Pair(i, j, solution, true);

                        if (isInTabuList(tabuList, i, j, departments[i], departments[j]))
                        {
                            // Aspiration Criteria - A tabu move will be considered only if it 
                            // provides a better solution than the best solution previously computed 
                            if (solution < bestSolution)
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
                        orderby pair.solution ascending
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

        public int computeSolution(int[] departments, int[,] distanceMatrix, int[,] flowMatrix)
        {
            // find initial solution
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
