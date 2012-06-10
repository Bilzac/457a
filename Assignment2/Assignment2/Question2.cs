using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class Question2
    {
        private const double INITIAL_TEMPERATURE = 100;
        private const double FINAL_TEMPERATURE = 0.01;
        private const int MAX_ITERATIONS = 200;

        public Question2() { }

        public void RunPartB()
        {
            int[,] distanceMatrix = new int[21, 21] 
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

            while (true)
            {
                Console.WriteLine("\n**********");
                Console.WriteLine("Select algorithm:\n[1] Simulated Annealing\n[2] Genetic algorithm\n[3] Return to Main Menu");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];

                switch (input)
                {
                    case '1':
                        doSimulatedAnnealing(distanceMatrix, flowMatrix);
                        break;
                    case '2':
                        doGeneticAlgorithm(distanceMatrix, flowMatrix);
                        break;
                    case '3':
                        Console.WriteLine("Returning to Main Menu...\n");
                        return;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
        }

        /* ===================== Simulated Annealing Methods =============================== */
        public void doSimulatedAnnealing(int[,] distanceMatrix, int[,] flowMatrix)
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
            Console.WriteLine("Cost: " + currentCost + ", temp: 100, iter: 0");

            // set initial temperature
            double temperature = INITIAL_TEMPERATURE;

            int iterations = 0;
            while (temperature >= FINAL_TEMPERATURE) // stop after final temperature is reached
            {
                while (iterations <= MAX_ITERATIONS) 
                { 
                    iterations++;

                    // Get random candidate
                    int[] newDepartments = getRandomCandidateFromNeighbourhood(departments);
                    int newCost = computeCost(newDepartments, distanceMatrix, flowMatrix);

                    int deltaCost = newCost - currentCost;

                    // generate random number (for acceptance probability)
                    Random random = new Random();
                    double randomNumber = random.NextDouble();
                    double acceptanceProb = Math.Exp((-1 * deltaCost) / temperature);

                    if (deltaCost < 0 || // if new cost is better, keep it
                         randomNumber < acceptanceProb) // check acceptance probability
                    {
                        departments = newDepartments;
                        currentCost = newCost;
                    }

                    if (currentCost < bestCost)
                    {
                        bestCost = currentCost;
                        Console.WriteLine("Cost: " + currentCost + ", temp: " + temperature + ", iter: " + iterations);
                    }
                }

                temperature = updateTemperature(temperature);
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

        public double updateTemperature(double temperature)
        {
            return temperature * 0.95;
        }

        public int[] getRandomCandidateFromNeighbourhood(int[] departments)
        {
            Random random = new Random();
            int i = random.Next(1, 21);
            int j = random.Next(1, 21);

            while (i == j)
            {
                i = random.Next(1, 21);
                j = random.Next(1, 21);
            }

            int[] newDepartments = swapValues(departments, i, j);
            return newDepartments;
        }

        /* ====================== Genetic Algorithm Methods ================================= */
        public void doGeneticAlgorithm(int[,] distanceMatrix, int[,] flowMatrix)
        {

        }

        /* ============================= Common methods ===================================== */
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
