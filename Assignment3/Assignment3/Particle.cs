using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Particle
    {
        // GLOBAL VARIABLES
        public double[] x;
        Random r; 
        double fitness;
        Particle pbest; // personal best
        Particle lbest; // local best
        public double[] velocity; 
        int particle_number; 

        // constructor
        public Particle(int p_number)
        {
            // init random 
            r = new Random();

            // init array
            this.x = new double[10];
            this.velocity = new double[10]; 

            // init pbest, velocity
            this.pbest = null; //high number
            this.lbest = null; 
           
            // store particle number
            this.particle_number = p_number;

            // populate x value with random numbers from -10.0 to 10.0
            for (int i = 0; i < 10; i++)
            {
                r = new Random(p_number + i + r.Next());
                this.x[i] = r.Next(-10, 10) * r.NextDouble(); // x[i]                 
                this.velocity[i] = 0.0;                    
            }
        }

        public Particle Clone()
        {
            Particle p = new Particle(this.particle_number);
            
            this.x.CopyTo(p.x, 0);
            this.velocity.CopyTo(p.velocity, 0); 
            
            p.SetFitness(this.fitness);
            return p; 
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
        public Particle GetPBest()
        {
            return this.pbest;
        }

        // set personal best pbest
        public void SetPBest(Particle pbest)
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
        public double GetX(int i)
        {
            return this.x[i]; 
        }

        public void SetX(int i, double value)
        {
            this.x[i] = value;
        }

        // get velocity
        public double GetVelocity(int i)
        {
            return this.velocity[i]; 
        }

        // set velocity
        public void SetVelocity(int i, double value)
        {
            this.velocity[i] = value; 
        }

        // particle number
        public int GetParticleNumber()
        {
            return this.particle_number; 
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
            Console.WriteLine("\nSolution Sum: " + fitness.ToString());
            Console.WriteLine("--------------------------------\n");
        }


    }
}
