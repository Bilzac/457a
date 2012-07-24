using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Part1a
    {

        int swarm_size;
        int maxIterations; 
        double c1;
        double c2;
        Particle1a gbest;
        LinkedList<Particle1a> swarm;

        Random r;

        Char type; // decide which PSO version to run

        // Weighted velocity
        double wmax; // max/min weighting factors
        double wmin; 

        // Vmax
        double vmax; //max velocity

        public Part1a(int swarm_size, int maxIterations, double c1, double c2, char type)
        {
            // store swarm size, iterations, c1, c2, part4
            this.swarm_size = swarm_size;
            this.maxIterations = maxIterations; 
            this.c1 = c1; 
            this.c2 = c2;
            this.type = type;

            r = new Random();
            

            // Weighted Velocity
            this.wmax = 1.0; // influence of current velocity
            this.wmin = 0.0; // on velocity update fxn
            
            // Vmax
            vmax = 5.0; // bound on particle velocity

            swarm = new LinkedList<Particle1a>();
            for (int i = 0; i < this.swarm_size; i++)
            {
                swarm.AddLast(new Particle1a());
            }

        }


        public Particle1a RunPSO() {
            int iteration = 0;

            while (iteration < this.maxIterations)
            {
                foreach (Particle1a p in swarm)
                {
                    p.calculateFitness(); //recalculate fitness
                    if (p.GetFitness() < p.GetPBest()) //update personal best
                    {
                        p.SetPBest(p.GetFitness());
                    }
                }
                CalulateLocalBest();

                switch (type)
                {
                    case 'n':
                        CalculateVelocity();
                        break;
                    case 'a':
                        break;
                    case 'b':
                        break;
                    case 'c':
                        break;
                    case 'd':
                        break;
                    case 'e': 
                        break;
                    case 'f': 
                        break;
                    case 'g':
                        break;
                    default:
                        CalculateVelocity();
                        break; 
                }

                foreach (Particle1a p in swarm)
                {
                    if (gbest == null || gbest.GetFitness() > p.GetFitness())
                    {
                        gbest = p;
                    }

                    if (p.GetPBest() <= -1.0316)
                    {
                        gbest = p;
                        break;
                    }
                }

                iteration++;
            }
            return gbest;
        }


        void CalulateLocalBest()
        {
            // update lbest values once all particle pbest values have been calculated
            for (int i = 0; i < swarm_size; i++)
            {
                Particle1a temp_best = swarm.ElementAt(i);

                for (int j = -3; j < 4; j++) // 3 neighbours on each side
                {
                    if (j != 0)
                    { // don't include current particle
                        int neighbour = i + j;
                        if (neighbour <= 0) // to deal with ...27-28-29-0-1-2-3... case
                        {
                            neighbour = neighbour + swarm_size - 1;
                        }
                        else if (neighbour >= swarm_size)
                        {
                            neighbour = neighbour - swarm_size;
                        }

                        // if neighbourhood best is better than current p's pbest
                        if (swarm.ElementAt(neighbour).GetPBest() < temp_best.GetPBest())
                        {
                            temp_best = swarm.ElementAt(neighbour); // store pbest as tempbest
                        }
                    }
                }
                // store tempbest as p's local best
                swarm.ElementAt(i).SetLBest(temp_best);

            }
        }

        void CalculateVelocity()
        {
            foreach (Particle1a p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // update velocity
                double vnew = (double)(v + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (p.GetLBest().GetPBest() - p.GetFitness())));

                p.SetVelocity(vnew); // update particle velocity

                // update solution = add velocity v to each x 
                double[] x_array = p.GetPoints();
                for (int i = 0; i < 2; i++)
                {
                    x_array[i] = x_array[i] + vnew;
                }

            }
            return; 
        }

    }
}
