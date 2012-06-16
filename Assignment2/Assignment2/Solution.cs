using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class Solution
    {
        // CLASS VARIABLES
        int cost = 0; // cost of solution
        LinkedList<int>[] cars; // store car routes
        int car_number = 0; //number of cars in solution

        // constructor
        public Solution(int car_number)
        {
            this.car_number = car_number; 
            cars = new LinkedList<int>[car_number];

            // initialize array of linked lists
            for (int i = 0; i < car_number; i++)
            {
                cars[i] = new LinkedList<int>();
            }

        }

        // clone
        public Solution CloneSolution()
        {
            Solution s = new Solution(this.car_number);
            s.cost = 0;

            // initialize array of linked lists
            for (int i = 0; i < car_number; i++)
            {
                //get city in each route
                foreach (int city in this.GetRoute(i))
                {
                    s.GetRoute(i).AddLast(city); 
                }               
            }

            return s; 
        }
        
        // get the whole array
        public LinkedList<int>[] GetCars(){
            return this.cars; 
        }

        // get route for a particular car
        public LinkedList<int> GetRoute(int i)
        {
            return this.cars[i]; 
        }


        // get, set, add cost
        public int GetCost()
        {
            return this.cost;
        }

        public void AddCost(int cost)
        {
            this.cost = this.cost + cost;
        }

        // return lenth of cars array
        public int GetLength()
        {
            return cars.Length; 
        }

        // return car number
        public int GetCarNumber()
        {
            return this.car_number; 
        }

        // print solution
        public void PrintSolution()
        {
            // go through each array
            for (int x = 0; x < car_number; x++)
            {
                Console.Write("Route " + x + ": "); 

                LinkedList<int> route = GetRoute(x);
                foreach (int i in route)
                {
                   Console.Write((i+1) + " "); 
                }
                Console.WriteLine(); 

            }
            // print cost
            Console.WriteLine("Cost: " + cost);
            Console.WriteLine("\n"); 
        }

    }
}

