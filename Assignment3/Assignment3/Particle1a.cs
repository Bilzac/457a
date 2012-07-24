using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Particle1a
    {
        double[] x = new double[2]; //stores x,y
        Random r;
        double fitness;
        double pbest;
        Particle1a lbest;
        double velocity;
        double z;

        public Particle1a()
        {
            this.r = new Random();
            this.x[0] = (this.r.NextDouble() * 6) - 3; //Generate a random X [-3,+3]
            this.x[1] = (this.r.NextDouble() * 4) - 2; //Generate a random Y [-2,+2]
            this.velocity = 0;
            this.pbest = 100000.0;
            this.calculateFitness();
        }

        public void calculateFitness()
        {
            this.z = (4 - 2.1 * (this.x[0] * this.x[0]) + ((this.x[0] * this.x[0] * this.x[0] * this.x[0]) / 3))*(this.x[0] * this.x[0]) + this.x[0] * this.x[1] + (-4 + (4 * this.x[1] * this.x[1]))*(this.x[1] * this.x[1]);
            this.fitness = 1 / z;
        }

        public double GetZ()
        {
            return this.z;
        }

        public double GetFitness()
        {
            return this.fitness;
        }

        public double GetPBest()
        {
            return this.pbest;
        }

        public void SetPBest(double pbest)
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

        public double[] GetPoints()
        {
            return this.x;
        }

        public double GetVelocity()
        {
            return this.velocity;
        }

        public void SetVelocity( double v )
        {
             this.velocity = v;
        }

        public String toString()
        {
            return "x: " + this.x[0] + " y: " + this.x[1] + " z: " + this.GetZ() + " Best Z: " + this.pbest;
        }
    }
}
