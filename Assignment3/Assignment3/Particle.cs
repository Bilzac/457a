using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Particle
    {
        // GLOBAL VARIABLES
        double[] x;
        Random r; 
        double fitness;
        double pbest; // personal best
        Particle lbest; // local best
        double velocity; 
        int particle_number; 

        // constructor
        public Particle(int p_number)
        {
            // init random 
            r = new Random(); 
            
            // init array
            this.x = new double [10]; 

            // init pbest, velocity
            this.pbest = 100000.0; //high number
            this.velocity = 0.0; 
            
            // store particle number
            this.particle_number = p_number; 

            // populate x value with random numbers from -10.0 to 10.0
            for(int i = 0; i < 10; i++)
            {                
                int neg = r.Next(2); //random 1 or 0
                if (neg == 1) // neg == 1
                {
                    this.x[i] = (-1) * r.NextDouble(); // x[i] is negative
                }
                else
                {
                    this.x[i] = r.NextDouble(); // x[i] is positive
                }
            }

        }
    
        // get fitness
        public double GetFitness()
        {
            return this.fitness;
        }

        // set fitness 
        public void SetFitness(double fitness)
        {
            this.fitness = fitness; 
        }

        // get personal best pbest
        public double GetPBest()
        {
            return this.pbest;
        }

        // set personal best pbest
        public void SetPBest(double pbest)
        {
            this.pbest = pbest; 
        }

        // get local best lbest
        public Particle GetLBest()
        {
            return this.lbest;
        }

        // set local best lbest
        public void SetLBest(Particle lbest)
        {
            this.lbest = lbest;
        }

        // get array of x values
        public double[] GetX()
        {
            return this.x; 
        }

        // get velocity
        public double GetVelocity()
        {
            return this.velocity; 
        }

        // set velocity
        public void SetVelocity(double velocity)
        {
            this.velocity = velocity; 
        }

        // print particle
        public void Print(int iteration)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Printing at iteration: " + iteration.ToString());
            Console.WriteLine("Particle #: " + particle_number);
            Console.Write("Solution Elements (X Values): "); 
            for (int i = 0; i < 10; i++)
            {
                Console.Write(x[i] + " "); 
            }
            Console.WriteLine("\nSolution Sum: " + pbest);
            Console.WriteLine("--------------------------------\n");
        }


    }
}
