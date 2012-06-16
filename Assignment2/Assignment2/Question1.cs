﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assignment2
{
    class Question1
    {
        int[] depot = new int[] {1, 2, 3, 4, 5}; //distance from city to dept
        int[,] distance = new int[,]  //distance between cities
            {
                {0, 3, 4, 5, 6},
                {3, 0, 6, 9, 8},
                {4, 6, 0, 1, 1},
                {5, 9, 1, 0, 2},
                {6, 8, 1, 2, 0}
            };
        int[] service = new int[] { 10, 20, 30, 40, 50 }; //service time at each city 
        

        // SOLUTION
        int car_number = 0; //start time for cars
        int cities = 0;
       // Solution soln = null; 
       // LinkedList<int>[] cars; //routes of each car

        // SIMULATED ANNEALING
        int temperature = 0; //temperature
        int iterations = 0; //number of iterations at each temperature
        double alpha = 0; //annealing schedule
        
        // RANDOM
        Random r = new Random();

        public Question1() //constructor
        {

        }

        public Question1(int car_number, int start_temp, int iterations, double alpha)
        {
            this.car_number = car_number;
            this.cities = depot.Length; // number of cities 
            
            //create new solution - initial solution
            //soln = new Solution(car_number);
                        
            //go through all cities
            //add them randomly to a car
            /*for (int i = 0; i < cities; i++)
            {
                int random = r.Next(car_number-1);
                //soln.GetRoute(random).AddLast(i); 
            }
            */
            this.temperature = start_temp;
            this.iterations = iterations; 
            this.alpha = alpha; 

        }

        // MAIN
        public void RunPartA()
        {
            Solution best_soln = new Solution(car_number); 
            
            // random initial solution
            //go through all cities
            //add them randomly to a car
            for (int i = 0; i < cities; i++)
            {
                int random = r.Next(car_number - 1);
                best_soln.GetRoute(random).AddLast(i);
            }
            int best_cost = EvaluateCost(best_soln); //eval initial cost

            Console.WriteLine("INITIAL SOLUTION");             
            best_soln.PrintSolution();

            // start SA processs
            while (temperature != 0)
            {
                int count = iterations;
                // # of iterations per temperature
                while (count > 0)
                {
                    // create new solution
                    Solution candidate = best_soln.CloneSolution();
                    
                    // operator - switch cities between routes
                    candidate = SwitchCityRoute(candidate);

                    int current_cost = EvaluateCost(candidate);
                    //candidate.PrintSolution();

                    // figure out if solution is going to be the best
                    if (SolutionAcceptance(best_soln, candidate) == true)
                    {
                        best_soln = candidate; // candidate is new best
                        best_cost = candidate.GetCost(); 
                    }
                    Console.WriteLine("Temperature: {0}, Count: {1}", temperature, count); 
                    best_soln.PrintSolution(); 
                    // should we store 'all time best'?

                    count--; 
                }

                // decrease temperature
                CoolingSchedule(); 
            }
        }


        public void SetCars(int car_number)
        {
            this.car_number = car_number; 
        }
        
        // FITNESS FUNCTION
        // fitness function = for each car route
        // (sum of all city distances + distance from depot to first city
        //                  + distance from last city back to depot)  
        //          + sum of all service times in each city

        public int EvaluateCost(Solution soln)
        {
            // go through array of routes
            for (int x = 0; x < soln.GetLength(); x++)
            {                
                LinkedList<int> route = soln.GetRoute(x);

                if (route.Count > 0)
                {
                    // distance from depot to first city
                    int city = Convert.ToInt16(route.First.Value);
                    soln.AddCost(depot[city]); //distance from depot to first
                    city = Convert.ToInt16(route.Last.Value);
                    soln.AddCost(depot[city]); //distance from depot to last

                    // go through route's cities and add distances 
                    for (int a = 1; a <= route.Count; a++)
                    {
                        //start with city -1 and city and add distances
                        soln.AddCost(distance[a - 1, a]);

                        //add service times for each city
                        soln.AddCost(service[a - 1]);
                    }
                }
            }
            return soln.GetCost();
        }

        // OPERATOR
        // 1. switch city from 1 route to another
        // 2. switch order of city in route

        public Solution SwitchCityRoute(Solution soln)
        {           
            // first random route
            int random_route1 = r.Next(car_number - 1);

            // make sure route is not empty 
            while (soln.GetRoute(random_route1).Count < 1)
            {
                random_route1 = r.Next(car_number - 1);
            }

            // second random route
            int random_route2 = r.Next(car_number - 1);

            // make sure route2 is not same as route1
            while (random_route1 == random_route2)
            {
                random_route2 = r.Next(car_number - 1);
            }

            // route1 and route2 are non-empty, so can trade cities
            if (soln.GetRoute(random_route2).Count > 1)
            {
                int random_city1 = r.Next(soln.GetRoute(random_route1).Count - 1); //random city position from route1
                int random_city2 = r.Next(soln.GetRoute(random_route2).Count - 1); //random city position from route2

                int city1 = soln.GetRoute(random_route1).ElementAt(random_city1); // city 1
                int city2 = soln.GetRoute(random_route2).ElementAt(random_city2); //city 2

                // remove city 1 from route 1 and add it to route 2
                soln.GetRoute(random_route1).Remove(city1);
                soln.GetRoute(random_route2).AddLast(city1);

                // remove city 2 from route 2 and add it to route 1
                soln.GetRoute(random_route2).Remove(city2);
                soln.GetRoute(random_route1).AddLast(city2);
            }
            else if (soln.GetRoute(random_route2).Count == 1) //one city in route 2
            {
                int random_city1 = r.Next(soln.GetRoute(random_route1).Count - 1); //random city position from route1
                int random_city2 = 0; // first city position from route2

                int city1 = soln.GetRoute(random_route1).ElementAt(random_city1); // city 1
                int city2 = soln.GetRoute(random_route2).ElementAt(random_city2); //city 2

                // remove city 1 from route 1 and add it to route 2
                soln.GetRoute(random_route1).Remove(city1);
                soln.GetRoute(random_route2).AddLast(city1);

                // remove city 2 from route 2 and add it to route 1
                soln.GetRoute(random_route2).Remove(city2);
                soln.GetRoute(random_route1).AddLast(city2);
            }
            else // route2 is empty
            {
                // remove city1 from route 1 and add it to route2
                int random_city1 = r.Next(soln.GetRoute(random_route1).Count - 1); //random city position from route1
                int city1 = soln.GetRoute(random_route1).ElementAt(random_city1); // city 1
                soln.GetRoute(random_route1).Remove(city1); //remove from route1
                soln.GetRoute(random_route2).AddLast(city1); //add to route2
            }

            return soln; 
        }

        public void SwitchCityOrder(Solution soln)
        {

        }

        // TEMPERATURE
        // start with high temperature
        // number of iterations at each temperature
        // reduce temperature by alpha - cooling schedule 
        // tnew = t x alpha

        public void CoolingSchedule()
        {
            temperature = (int) ((double)temperature * alpha); 
        }

        // SOLUTION DECISION
        // initial solution 
        // evaluate solution
        // if solution difference cost > 0 -> Paccept = 1
        // else cost <= 0 -> Paccept > random 

        public bool SolutionAcceptance(Solution best, Solution candidate)
        {
            if (best.GetCost() - candidate.GetCost() > 0) // candidate cost is less
            {
                return true; // candidate is accepted
            }
            else 
            {
                // accpetance probability = e^-cost/temp
                int negcost = candidate.GetCost() - (2 * candidate.GetCost());
                double exp = (double)negcost / (double)temperature; 
                double prob = Math.Exp(exp); // acceptance probability

                double threshold = r.NextDouble(); // acceptance threshold - random

                if (prob.CompareTo(threshold) > 0) // if accept prob > accept thresh
                {
                    Console.WriteLine("probability of acceptance: {0}/nthreshold of acceptance: {1}",
                        prob, threshold); 
                    return true; // candidate accepted
                }
            }    
            return false; // candidate not accepted
        }

        


    }
}