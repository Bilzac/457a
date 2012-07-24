using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Particle1a
    {
        double[] x = new double[2]; //stores x,y
        double[] velocity = new double[2];//stores velx, vely

        Random r;
        double fitness;
        Particle1a pbest;
        Particle1a lbest;
        double z;

        public Particle1a()
        {
            this.r = new Random();
            this.x[0] = (this.r.NextDouble() * 6) - 3; //Generate a random X [-3,+3]
            this.x[1] = (this.r.NextDouble() * 4) - 2; //Generate a random Y [-2,+2]
            this.velocity[0] = 0;
            this.velocity[1] = 0;
            this.pbest = null;
            this.lbest = null;
            this.calculateFitness();
        }

        public void calculateFitness()
        {
            this.z = this.fitness = (4 - 2.1 * (this.x[0] * this.x[0]) + ((this.x[0] * this.x[0] * this.x[0] * this.x[0]) / 3)) * (this.x[0] * this.x[0]) + this.x[0] * this.x[1] + (-4 + (4 * this.x[1] * this.x[1])) * (this.x[1] * this.x[1]);
            //this.fitness = 1 / z;
        }

        //TODO
        public Particle1a Clone()
        {
            Particle1a p = new Particle1a();

            this.x.CopyTo(p.x, 0);
            this.velocity.CopyTo(p.velocity, 0);
            p.setZ(this.z);
            p.SetFitness(this.fitness);
            return p; 
        }

        public double GetZ()
        {
            return this.z;
        }

        public void setZ(double z)
        {
            this.z = z; 
        }

        public double GetFitness()
        {
            return this.fitness;
        }

               // set fitness 
        public void SetFitness(double fitness)
        {
            this.fitness = fitness; 
        }

        public Particle1a GetPBest()
        {
            return this.pbest;
        }

        public void SetPBest(Particle1a pbest)
        {
            this.pbest = pbest;
        }

        public Particle1a GetLBest()
        {
            return this.lbest;
        }

        public void SetLBest(Particle1a lbest)
        {
            this.lbest = lbest;
        }

        public double GetX(int i)
        {
            return this.x[i];
        }

        public void SetX(int i, double value)
        {
            if (i == 0)
            {
                this.x[i] = Math.Max(Math.Min(value,3),-3);
            }
            else if (i == 1)
            {
                this.x[i] = Math.Max(Math.Min(value, 2), -2);
            }
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

        public String toString()
        {
            return "x: " + this.x[0] + " y: " + this.x[1] + " z: " + this.GetZ();
        }
    }
}
