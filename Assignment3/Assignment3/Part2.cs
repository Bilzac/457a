using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Assignment3
{
    class Part2
    {  
        // CONSTANTS
        private const double NUM_CITIES = 29;
        private const int CONSTANT_VALUE_Q = 5000;
        
        // PRIVATE VARIABLES
        Random random = new Random();
        double[,] distance = new double[30,30];
        double[,] pheromone = new double[30,30];
        List<Ant> ants;
        int maxIterations = 0;
        double alpha = 0;
        double beta = 0;
        double pheramonePersistentConstant = 0;
        int numAnts = 0;

        // BOOL VALUES FOR OPTIONS
        bool onlineUpdate = true;
        bool offlineUpdate = false;
        bool localSearch = false;
        bool cooperative = false;

        // VARIABLES FOR COOPERATIVE ALGORITHM
        List<Ant> antCol1;
        List<Ant> antCol2;
        double[,] pherCol1 = new double[30, 30];
        double[,] pherCol2 = new double[30, 30];

        // CONSTRUCTOR
        public Part2() { }

        public void RunPart2() {
            displayOptions();
        }

        // ANT COLONY METHODS
        public void doSimpleACO() {
            int iteration = 0;

            Ant globalBest = new Ant();
            setConstructionGraph();

            while (iteration < maxIterations) {
                Ant iterationBest = new Ant();
                initializeAnts(); // put all ants in random cities

                // construct a bunch of ant solutions
                foreach (Ant ant in ants) {
                    constructAntSolution(ant);
                    if (ant.distance < iterationBest.distance) // get the local best ant
                        iterationBest = ant;
                    if (ant.distance < globalBest.distance) {
                        displayCurrentBest(ant, iteration);
                        globalBest = ant;
                    }

                    if (localSearch) {
                        Ant improved = applyLocalSearch(ant);
                        if (improved.distance < iterationBest.distance) // get the local best ant
                            iterationBest = improved;
                        if (improved.distance < globalBest.distance) {
                            globalBest = improved;
                        }
                    }
                }

                evaporatePheromoneLevels(); // evaporate some percentage of pheromones

                if (onlineUpdate)
                    onlinePheromoneUpdate(); // update all ant paths
                else if (offlineUpdate)
                    offlinePheromoneUpdate(iterationBest); // update only the iteration best

                iteration++;
            }

            displayGlobalBest(globalBest);
        }

        public void doCooperativeAlgorithm() {
            int iteration = 0;

            Ant globalBest = new Ant();
            setConstructionGraph();

            bool foundNewBest = false;
            while (iteration < maxIterations) {
                Ant iterationBest = new Ant();
                initializeAnts(); // put all ants in random cities

                // get best ant from col 2
                if (foundNewBest) {
                    antCol1.Add(globalBest);
                    foundNewBest = false;
                }

                // construct a bunch of ant solutions from col 1
                foreach (Ant ant in antCol1) {
                    constructAntSolution(ant, 1);
                    if (ant.distance < iterationBest.distance) 
                        iterationBest = ant;
                    if (ant.distance < globalBest.distance) {
                        displayCurrentBest(ant, iteration);
                        globalBest = ant;
                        foundNewBest = true; // transfer to colony 2
                    }
                }

                // get best ant from col 1
                if (foundNewBest) {
                    antCol2.Add(globalBest); // get best ant from col 1
                    foundNewBest = false;
                }

                // construct a bunch of ant solutions from col 2
                foreach (Ant ant in antCol2) {
                    constructAntSolution(ant, 2);
                    if (ant.distance < iterationBest.distance) 
                        iterationBest = ant;
                    if (ant.distance < globalBest.distance) {
                        displayCurrentBest(ant, iteration);
                        globalBest = ant;
                        foundNewBest = true; // transfer to colony 1
                    }
                }

                evaporatePheromoneLevels(); // evaporate some percentage of pheromones
                onlinePheromoneUpdate(); // update all ant paths

                iteration++;
            }

            displayGlobalBest(globalBest);
        }

        public void setConstructionGraph() {
            // set construction graph
            int[,] coordinates = setCoordinates();
            calculateEuclideanDistance(coordinates);
            initializePheromoneLevels(); // add equal amounts on each edge
        }

        public void initializeAnts() {
            if (cooperative) {
                // add equal number of ants to both colonies
                antCol1 = new List<Ant>();
                antCol2 = new List<Ant>();
                for (int i = 0; i < numAnts; i++) {
                    Ant ant = new Ant();
                    ant.initialize(random);
                    antCol1.Add(ant);
                    Ant ant2 = new Ant();
                    ant2.initialize(random);
                    antCol1.Add(ant2);
                }             
            } else {
                ants = new List<Ant>();
                for (int i = 0; i < numAnts; i++) {
                    Ant ant = new Ant();
                    ant.initialize(random);
                    ants.Add(ant);
                }
            }
        }

        public void constructAntSolution(Ant ant, int colonyType = 0) {
            int numCities = ant.remainingCities.Count();
            for (int i = 0; i < numCities; i++) {
                if (!cooperative)
                    applyTransitionRule(ant);
                else if (colonyType == 1) {
                    applyTransitionRuleCooperative(ant, colonyType);
                } else {
                    applyTransitionRuleCooperative(ant, colonyType);
                }
            }
        }

        public void applyTransitionRuleCooperative(Ant ant, int colonyType = 0) {
            int city = ant.currentCity;
            int nextCity = 0;

            double totalProbability = 0;
            double highestProbability = 0;
            List<int> cities = ant.remainingCities;
            foreach (int i in cities) {
                if (colonyType == 1) {
                    totalProbability =
                        totalProbability +
                        (Math.Pow(pherCol1[city, i], alpha) / Math.Pow(distance[city, i], beta));
                }
                else {
                    totalProbability =
                        totalProbability +
                        (Math.Pow(pherCol2[city, i], alpha) / Math.Pow(distance[city, i], beta));
                }
            }

            foreach (int i in cities) {
                double probability = 0;
                if (colonyType == 1) {
                    probability =
                     (Math.Pow(pherCol1[city, i], alpha) / Math.Pow(distance[city, i], beta)) / totalProbability;
                }
                else {
                    probability =
                     (Math.Pow(pherCol2[city, i], alpha) / Math.Pow(distance[city, i], beta)) / totalProbability;
                }

                if (probability > highestProbability) {
                    nextCity = i;
                    highestProbability = probability;
                }
            }

            ant.updateDistance(distance[city, nextCity]);
            ant.addCity(nextCity);
        }

        public void evaporatePheromoneLevels() {
            for (int i = 1; i <= NUM_CITIES; i++) {
                for (int j = 1; j <= NUM_CITIES; j++) {
                    pheromone[i,j] = (1 - pheramonePersistentConstant) * pheromone[i,j];
                    pherCol1[i, j] = (1 - pheramonePersistentConstant) * pherCol1[i, j];
                    pherCol2[i, j] = (1 - pheramonePersistentConstant) * pherCol2[i, j];
                }
            }
        }

        public void onlinePheromoneUpdate() {
            // update each edge
            if (!cooperative) {
                foreach (Ant ant in ants) {
                    for (int i = 0; i < NUM_CITIES - 1; i++) {
                        int x = ant.tour.ElementAt(i);
                        int y = ant.tour.ElementAt(i+1);

                        // use ant quantity model
                        pheromone[x, y] = pheromone[x, y] + (CONSTANT_VALUE_Q / ant.distance);
                        pheromone[y, x] = pheromone[y, x] + (CONSTANT_VALUE_Q / ant.distance);
                    }
                }
            } else {
                foreach (Ant ant in antCol1) {
                    for (int i = 0; i < NUM_CITIES - 1; i++) {
                        int x = ant.tour.ElementAt(i);
                        int y = ant.tour.ElementAt(i + 1);
                        pherCol1[x, y] = pherCol1[x, y] + (CONSTANT_VALUE_Q / ant.distance);
                        pherCol1[y, x] = pherCol1[y, x] + (CONSTANT_VALUE_Q / ant.distance);
                    }
                }

                foreach (Ant ant in antCol2) {
                    for (int i = 0; i < NUM_CITIES - 1; i++) {
                        int x = ant.tour.ElementAt(i);
                        int y = ant.tour.ElementAt(i + 1);
                        pherCol2[x, y] = pherCol2[x, y] + (CONSTANT_VALUE_Q / ant.distance);
                        pherCol2[y, x] = pherCol2[y, x] + (CONSTANT_VALUE_Q / ant.distance);
                    }
                }
            }
        }

        public void offlinePheromoneUpdate(Ant ant) {
            for (int i = 0; i < NUM_CITIES - 1; i++) {
                int x = ant.tour.ElementAt(i);
                int y = ant.tour.ElementAt(i + 1);

                pheromone[x, y] = pheromone[x, y] + pheramonePersistentConstant * (1 / ant.distance);
                pheromone[y, x] = pheromone[y, x] + pheramonePersistentConstant * (1 / ant.distance);
            }
        }

        public Ant applyLocalSearch(Ant ant) {
            Ant improved = copyAnt(ant);
            int[] tour = improved.tour.ToArray();

            swapTwoRandomCities(tour);
            swapTwoRandomCities(tour);
            improved.tour = tour.ToList();
            recalculateDistance(improved);

            return improved;
        }

        public void swapTwoRandomCities(int[] tour) {
            int i = random.Next(1, 29);
            int j = random.Next(1, 29);
            while (i == j) {
                i = random.Next(1, 29);
                j = random.Next(1, 29);
            }
            int k = tour[i];
            tour[i] = tour[j];
            tour[j] = k;
        }

        public void recalculateDistance(Ant ant) {
            ant.distance = 0;
            for (int i = 1; i < NUM_CITIES; i++) {
                ant.updateDistance(distance[ant.tour.ElementAt(i - 1), ant.tour.ElementAt(i)]);
            }
        }

        public void applyTransitionRule(Ant ant) {
            int city = ant.currentCity;
            int nextCity = 0;

            double totalProbability = 0;
            double highestProbability = 0;
            List<int> cities = ant.remainingCities;
            foreach (int i in cities) {
                totalProbability =
                    totalProbability +
                    (Math.Pow(pheromone[city, i], alpha) / Math.Pow(distance[city, i], beta));
            }

            foreach (int i in cities) {
                double probability =
                    (Math.Pow(pheromone[city, i], alpha) / Math.Pow(distance[city, i], beta)) / totalProbability;
                if (probability > highestProbability) {
                    nextCity = i;
                    highestProbability = probability;
                }
            }

            ant.updateDistance(distance[city, nextCity]);
            ant.addCity(nextCity);
        }

        public void initializePheromoneLevels() {
            for (int i = 1; i <= NUM_CITIES; i++) {
                for (int j = 1; j <= NUM_CITIES; j++) {
                    pheromone[i, j] = 1;
                    pherCol1[i, j] = 1;
                    pherCol2[i, j] = 1;
                }
            }
        }

        public void calculateEuclideanDistance(int[,] coordinates) {
            for (int i = 1; i <= NUM_CITIES; i++) {
                int x1 = coordinates[i, 0];
                int y1 = coordinates[i, 1];
                for (int j = 1; j <= NUM_CITIES; j++)
                {
                    int x2 = coordinates[j, 0];
                    int y2 = coordinates[j, 1];
                    distance[i, j] = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                }
            }
        }

        public int[,] setCoordinates() {
            return new int[30, 2] 
            {
                {0, 0}, {1150, 1760}, {630, 1660},  {40, 2090},   {750, 1100},  {750, 2030}, // 1 - 5
                        {1030, 2070}, {1650, 650},  {1490, 1630}, {790, 2260},  {710, 1310}, // 6 - 10
                        {840, 550},   {1170, 2300}, {970, 1340},  {510, 700},   {750, 900}, // 11 - 15
                        {1280, 1200}, {230, 590},   {460, 860},   {1040, 950},  {590, 1390}, // 16 - 20
                        {830, 1770},  {490, 500},   {1840, 1240}, {1260, 1500}, {1280, 790}, // 21 - 25
                        {490, 2130},  {1460, 1420}, {1260, 1910}, {360, 1980} // 26 - 29
            };
        }

        // CONVERSION AND COPY METHODS
        public Ant copyAnt(Ant ant) {
            Ant copy = new Ant(ant.tour.ElementAt(0));
            for (int i = 1; i < NUM_CITIES; i++) {
                copy.updateDistance(distance[ant.tour.ElementAt(i - 1), ant.tour.ElementAt(i)]);
                copy.addCity(ant.tour.ElementAt(i));
            }
            return copy;
        }
        public string convToString(int i) {
            if ((i / 10) > 0)
                return i.ToString();
            else
                return " " + i;
        }

        // DISPLAY METHODS 
        public void displayOptions() {
            while (true) {
                maxIterations = 50;
                alpha = 0.5;
                beta = 0.5;
                pheramonePersistentConstant = 0.3;
                numAnts = 3;
                onlineUpdate = true;
                offlineUpdate = false;
                localSearch = false;
                cooperative = false;
                Console.WriteLine("\n**********");
                Console.WriteLine("Select part:\n" +
                                    "[a] Part 2\n" +
                                    "[b] Part 3\n" +
                                    "[c] Part 4\n" + 
                                    "[d] Part 5\n" + 
                                    "[e] Return");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];
                switch (input) {
                    case 'a': doSimpleACO(); break;
                    case 'b': displayPart3Options(); break;
                    case 'c': displayPart4Options(); break;
                    case 'd': cooperative = true; doCooperativeAlgorithm(); break;
                    case 'e': Console.WriteLine("Exiting..."); return;
                    default:  Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void displayPart3Options() {
            while (true) {
                onlineUpdate = true;
                offlineUpdate = false;
                localSearch = false;
                pheramonePersistentConstant = 0.3;
                numAnts = 3;
                Console.WriteLine("\n**********");
                Console.WriteLine("Select part:\n" +
                                    "[1] Change the values of pheromone persistence constant 3 times\n" +
                                    "[2] Change the values of state transition control parameter 3 times\n" +
                                    "[3] Change the population size twice\n" +
                                    "[4] Turn off online pheromone update and turn on offline update rule\n" +
                                    "[5] Add a simple local search, e.g. 2-opt, to your code\n" +
                                    "[6] Return");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];
                switch (input) {
                    case '1': displayPheromonePersistentConstant(); break;
                    case '2': displayTransmissionRuleParameter(); break;
                    case '3': displayPopulationSizes(); break;
                    case '4': onlineUpdate = false; offlineUpdate = true; doSimpleACO(); break;
                    case '5': localSearch = true; doSimpleACO(); break;
                    case '6': Console.WriteLine("Exiting..."); return;
                    default:  Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void displayPart4Options() {
            while (true) {
                maxIterations = 50;
                Console.WriteLine("\n**********");
                Console.WriteLine("Select iterations:\n" +
                                    "[1] 10\n" +
                                    "[2] 100\n" +
                                    "[3] 1000\n" +
                                    "[4] Return");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];
                switch (input) {
                    case '1': maxIterations = 10; doSimpleACO(); break;
                    case '2': maxIterations = 100; doSimpleACO(); break;
                    case '3': maxIterations = 1000; doSimpleACO(); break;
                    case '4': Console.WriteLine("Exiting..."); return;
                    default: Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void displayPheromonePersistentConstant() {
            while (true) {
                pheramonePersistentConstant = 0.3; // set to default value
                Console.WriteLine("\n**********");
                Console.WriteLine("Select Pheromone Constant:\n" +
                                    "[1] 0.1\n" +
                                    "[2] 0.5\n" +
                                    "[3] 0.9\n" +
                                    "[4] Return");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];
                switch (input) {
                    case '1': pheramonePersistentConstant = 0.1;
                        doSimpleACO(); break;
                    case '2': pheramonePersistentConstant = 0.5;
                        doSimpleACO(); break;
                    case '3': pheramonePersistentConstant = 0.9;
                        doSimpleACO(); break;
                    case '4': Console.WriteLine("Exiting..."); return;
                    default: Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void displayTransmissionRuleParameter() {
            while (true) {
                alpha = 0.5; // set to default values
                beta = 0.5;
                Console.WriteLine("\n**********");
                Console.WriteLine("Select transmission rule parameter:\n" +
                                    "[1] a = 0.1, b = 0.5\n" +
                                    "[2] a = 0.9, b = 0.5\n" +
                                    "[3] a = 0.5, b = 0.1\n" +
                                    "[4] a = 0.5, b = 0.9\n" +
                                    "[5] Return");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];
                switch (input) {
                    case '1': alpha = 0.1; beta = 0.5;
                        doSimpleACO(); break;
                    case '2': alpha = 0.9; beta = 0.5;
                        doSimpleACO(); break;
                    case '3': alpha = 0.5; beta = 0.1;
                        doSimpleACO(); break;
                    case '4': alpha = 0.5; beta = 0.9;
                        doSimpleACO(); break;
                    case '5': Console.WriteLine("Exiting..."); return;
                    default:  Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void displayPopulationSizes() {
            while (true) {
                numAnts = 3;
                Console.WriteLine("\n**********");
                Console.WriteLine("Select population size:\n" +
                                    "[1] population size = 1\n" +
                                    "[2] population size = 5\n" +
                                    "[3] population size = 10\n" +
                                    "[4] population size = 15\n" +
                                    "[5] population size = 20\n" +
                                    "[6] population size = 50\n" +
                                    "[7] Return");
                char input = Console.ReadLine().ToLower().ToCharArray()[0];
                switch (input) {
                    case '1': numAnts = 1; doSimpleACO(); break;
                    case '2': numAnts = 5; doSimpleACO(); break;
                    case '3': numAnts = 10; doSimpleACO(); break;
                    case '4': numAnts = 15; doSimpleACO(); break;
                    case '5': numAnts = 20; doSimpleACO(); break;
                    case '6': numAnts = 50; doSimpleACO(); break;
                    case '7': Console.WriteLine("Exiting..."); return;
                    default: Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void displayCurrentBest(Ant ant, int iter) {
            Console.WriteLine("iter: " + iter + ", Distance: " + ant.distance);
        }

        public void displayGlobalBest(Ant ant) {
            Console.WriteLine("Best Cost: " + ant.distance);
            Console.WriteLine("Best Tour: " + ant.getCity(0) + " " + ant.getCity(1) + " " + ant.getCity(2) + " " + ant.getCity(3) + " " + ant.getCity(4));
            Console.WriteLine("           " + ant.getCity(5) + " " + ant.getCity(6) + " " + ant.getCity(7) + " " + ant.getCity(8) + " " + ant.getCity(9));
            Console.WriteLine("           " + ant.getCity(10) + " " + ant.getCity(11) + " " + ant.getCity(12) + " " + ant.getCity(13) + " " + ant.getCity(14));
            Console.WriteLine("           " + ant.getCity(15) + " " + ant.getCity(16) + " " + ant.getCity(17) + " " + ant.getCity(18) + " " + ant.getCity(19));
            Console.WriteLine("           " + ant.getCity(20) + " " + ant.getCity(21) + " " + ant.getCity(22) + " " + ant.getCity(23) + " " + ant.getCity(24));
            Console.WriteLine("           " + ant.getCity(25) + " " + ant.getCity(26) + " " + ant.getCity(27) + " " + ant.getCity(28));
        }
    }
}
