using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Assignment2
{
    class Question2
    {
        // Simulated Annealing constants
        private const double INITIAL_TEMPERATURE = 100;
        private const double FINAL_TEMPERATURE = 0.01;
        private const int MAX_ITERATIONS = 200;

        // Genetic Algorithm constants
        private const bool CASE1 = true;
        private const int POPULATION_CASE1 = 50;
        private const int GENERATIONS_CASE1 = 1000;

        private const bool CASE2 = false;
        private const int POPULATION_CASE2 = 100;
        private const int GENERATIONS_CASE2 = 500;

        private const double CROSSOVER_PROBABILITY = 0.7;
        private const double MUTATION_PROBABILITY = 0.2;

        Random random = new Random();
        int[,] distanceMatrix;
        int[,] flowMatrix;

        Stopwatch stopWatch = new Stopwatch();

        public Question2() { }

        public void RunPartB()
        {
            distanceMatrix = new int[21, 21] 
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

            flowMatrix = new int[21, 21]
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
                Console.WriteLine("Select algorithm:\n[1] Simulated Annealing\n[2] Genetic algorithm: Case 1\n" + 
                                  "[3] Genetic algorithm: Case 2\n[4] Return to Main Menu");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];

                stopWatch.Reset();
                switch (input)
                {
                    case '1':
                        stopWatch.Start();
                        doSimulatedAnnealing();
                        stopWatch.Stop();
                        displayTimingInformation(stopWatch.ElapsedMilliseconds.ToString());                        
                        break;
                    case '2':
                        stopWatch.Start();
                        doGeneticAlgorithm(CASE1);
                        stopWatch.Stop();
                        displayTimingInformation(stopWatch.ElapsedMilliseconds.ToString());  
                        break;
                    case '3':
                        stopWatch.Start();
                        doGeneticAlgorithm(CASE2);
                        stopWatch.Stop();
                        displayTimingInformation(stopWatch.ElapsedMilliseconds.ToString());  
                        break;
                    case '4':
                        Console.WriteLine("Returning to Main Menu...\n");
                        return;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
        }

        /* ===================== Simulated Annealing Methods =============================== */
        public void doSimulatedAnnealing()
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
            int currentCost = computeCost(departments);
            int bestCost = currentCost;
            Console.WriteLine("Cost: " + currentCost + ", temp: 100, iter: 0");

            // set initial temperature
            double temperature = INITIAL_TEMPERATURE;

            int iterations = 0;
            while (temperature >= FINAL_TEMPERATURE) // stop after final temperature is reached
            {
                iterations = 0;
                while (iterations < MAX_ITERATIONS)
                {
                    iterations++;

                    // Get random candidate
                    int[] newDepartments = doRandomSwap(departments);
                    int newCost = computeCost(newDepartments);
                    int deltaCost = newCost - currentCost;

                    // generate random number (for acceptance probability)
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
                        Console.WriteLine(
                            string.Format("Cost: {0}, temp: {1:N2}, iter: {2}", bestCost, temperature, iterations));
                    }
                }
                temperature = updateTemperature(temperature);
            }

            displayBestSolution(bestCost, departments);
        }

        public double updateTemperature(double temperature)
        {
            return temperature * 0.95;
        }

        /* ====================== Genetic Algorithm Methods ================================= */
        public void doGeneticAlgorithm(bool caseType)
        {
            // Set values of population and generation based on which type they selected
            int generations = getGeneration(caseType);
            int population = getPopulation(caseType);
         
            // intialize population with the specified number of individuals
            Individual[] parents = initializePopulation(population).ToArray();

            int bestCost = parents[0].fitness;
            int[] bestSolution = parents[0].individual;
            
            int iterations = 0;
            while (iterations < generations && bestCost > 2570)
            {
                // display the best in each generation
                var sorted = from individual in parents
                             orderby individual.fitness ascending
                             select individual;
                Individual ind = sorted.ToList().First();
                if (ind.fitness < bestCost)
                {
                    bestCost = ind.fitness;
                    bestSolution = ind.individual;
                    Console.WriteLine("Cost: " + bestCost + ", generation: " + iterations);
                }

                // do genetic algorithm
                List<Individual> children = new List<Individual>();
                for (int i = 0; i < population; i = i + 2)
                {
                    Individual[] selectedParents = selectParents(parents);
                    selectedParents = applyCrossOver(selectedParents[0], selectedParents[1]);
                    children.Add(mutateOffspring(selectedParents[0]));
                    children.Add(mutateOffspring(selectedParents[1]));
                }

                // take 1 individuals with the highest fitness to continue in the next generation (elitism)
                var parentsSorted = from individual in parents
                                    orderby individual.fitness ascending
                                    select individual;
                List<Individual> parentsChosen = parentsSorted.Take(1).ToList();

                updateFitness(children);
                var childrenSorted = from individual in children
                                     orderby individual.fitness ascending
                                     select individual;
                List<Individual> childrenChosen = childrenSorted.Take(population - 1).ToList();
          
                parents = parentsChosen.Concat(childrenChosen).ToArray();

                iterations++;
            }

            displayBestSolution(bestCost, bestSolution);
        }

        public List<Individual> initializePopulation(int population)
        {
            List<Individual> randomCandidates = new List<Individual>();
            int candidates = 0;

            int[] numbers = new int[20]
            {  
                1,  2,  3,  4,  5,  6,  7,  8,  9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20 
            };

            while (candidates < population)
            {
                int[] departments = createRandomCandidate(numbers);
                // check for duplicate
                if (!candidateExists(departments, randomCandidates))
                {
                    Individual individual = new Individual(departments);
                    individual.fitness = computeCost(departments);
                    randomCandidates.Add(individual);
                    candidates++;
                }
            }

            return randomCandidates;
        }

        public int[] createRandomCandidate(int[] numbers)
        {
	        List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
	        foreach (int num in numbers)
	        {
	            list.Add(new KeyValuePair<int, int>(random.Next(1,100), num));
	        }

	        // Sort list by the random number
	        var sorted = from item in list
		                 orderby item.Key ascending
		                 select item;
            
	        // Create new array and copy values to array
	        int[] result = new int[numbers.Length + 1];
	        int index = 1; // the first one is always 0
	        foreach (KeyValuePair<int, int> pair in sorted)
	        {
	            result[index] = pair.Value;
	            index++;
	        }

	        return result;
        }

        public bool candidateExists(int[] departments, List<Individual> population)
        {
            var sorted = from item in population
                         where departments.SequenceEqual(item.individual)
                         select item;
            if (sorted.Count() > 0)
                return true;
            return false;
        }

        public Individual[] selectParents(Individual[] parents)
        {
            // tournament selection
            Individual[] selectedParents = new Individual[2];
            for (int i = 0; i < selectedParents.Length; i++)
            {
                // select two random children
                int j = random.Next(0, parents.Length - 1);
                int k = random.Next(0, parents.Length - 1);
                while (j == k)
                {
                    j = random.Next(0, parents.Length - 1);
                    k = random.Next(0, parents.Length - 1);
                }

                Individual child1 = parents[j];
                Individual child2 = parents[k];

                // select the fitness of the two               
               if (child1.fitness < child2.fitness)
                    selectedParents[i] = child1;
               else
                    selectedParents[i] = child2;
            } 
            
            return selectedParents;
        }

        public Individual[] applyCrossOver(Individual parent1, Individual parent2)
        {
            double randomNumber = random.NextDouble();
            if (randomNumber < CROSSOVER_PROBABILITY)
            {
                // Order 1 crossover
                Individual[] children = new Individual[2];
                children[0] = new Individual();
                children[1] = new Individual();
                int rand1 = random.Next(1, 10);
                int rand2 = random.Next(11, 20);

                // copy a random selected set from parents to children
                for (int i = rand1; i <= rand2; i++)
                {
                    children[0].individual[i] = parent1.individual[i];
                    children[1].individual[i] = parent2.individual[i];
                }

                // copy the rest of parent2 into child1
                copyRestIntoChild(parent2, children[0], rand2);
                // copy the rest of parent1 into child2
                copyRestIntoChild(parent1, children[1], rand2);

                return children;
            }
               
            return new Individual[] { parent1, parent2};
        }

        public Individual mutateOffspring(Individual offspring)
        {
            double randomNumber = random.NextDouble();

            // Swap mutation
            Individual mutatedChild = copyIndividual(offspring);

            if (randomNumber < MUTATION_PROBABILITY)
            {
                mutatedChild.individual = doRandomSwap(mutatedChild.individual);
            }
            return mutatedChild;
        }

        public void updateFitness(List<Individual> population)
        {
            foreach (Individual ind in population)
            {
                ind.fitness = computeCost(ind.individual);
            }
        }

        public void copyRestIntoChild(Individual parent, Individual child, int startingPosition)
        {
            int childCounter = (startingPosition == 20) ? 1 : startingPosition + 1;
            int i = childCounter;
            for (i = childCounter; i != startingPosition; i++)
            {
                if (!child.individual.Contains(parent.individual[i]))
                {
                    child.individual[childCounter] = parent.individual[i];
                    childCounter++;

                    if (childCounter > 20)
                        childCounter = 1;
                }

                if (i == 20)
                    i = 0;
            }
            if (child.individual[childCounter] == 0)
            {
                child.individual[childCounter] = parent.individual[i];
            }
        }

        public int getGeneration(bool caseType)
        {
            if (caseType) return GENERATIONS_CASE1;
            else return GENERATIONS_CASE2;
        }
        public int getPopulation(bool caseType)
        {
            if (caseType) return POPULATION_CASE1;
            else return POPULATION_CASE2;
        }

        /* ============================= Common methods ===================================== */
        public int[] swapValues(int[] list, int x, int y)
        {
            // create a new list (a new instance so that the original does not get overwritten)
            int[] newList = copyArray(list);

            // swap values in the array
            int a = newList[x], b = newList[y];
            newList[y] = a;
            newList[x] = b;
            return newList;
        }

        public Individual copyIndividual(Individual individual)
        {
            Individual newInd = new Individual();
            newInd.individual = copyArray(individual.individual);
            newInd.fitness = individual.fitness;
            return newInd;
        }

        public int[] copyArray(int[] oldList)
        {
            int[] newList = new int[21];
            for (int i = 0; i < oldList.Length; i++)
            {
                newList[i] = oldList[i];
            }
            return newList;
        }

        public int[] doRandomSwap(int[] item)
        {
            int i = random.Next(1, 20);
            int j = random.Next(1, 20);

            while (i == j)
            {
                i = random.Next(1, 20);
                j = random.Next(1, 20);
            }
            return swapValues(item, i, j);
        }

        public int computeCost(int[] departments)
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

        public void displayBestSolution(int bestCost, int[] departments)
        {
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

        public void displayTimingInformation(string time)
        {
            Console.WriteLine("Total time: " + time + " milliseconds");
        }

        public string convToString(int i)
        {
            if ((i / 10) > 0)
                return i.ToString();
            else
                return " " + i;
        }
    }
}
