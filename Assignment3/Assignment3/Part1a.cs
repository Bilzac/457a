using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;


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
                Particle1a p = new Particle1a();
                swarm.AddLast(p);
                p.SetPBest(p);
            }

        }


        public Particle1a RunPSO() {
            int iteration = 0;
            gbest = swarm.ElementAt(0); // first particle
            //while (iteration < this.maxIterations)
            do
            {
                foreach (Particle1a p in swarm)
                {
                    p.calculateFitness(); //recalculate fitness
                    if (p.GetFitness() < p.GetPBest().GetFitness()) //update personal best
                    {
                        p.SetPBest(p.Clone());
                    }
                }

                CalculateLocalBest();

                // calculate gbest
                foreach (Particle1a p in swarm)
                {
                    if (p.GetFitness() < gbest.GetFitness())
                    {
                        Console.WriteLine("Updated!");
                        gbest = p.Clone();
                    }
                }

                switch (type)
                {
                    case 'x': // normal PSO - global best
                        CalculateVelocity();
                        break;
                    case 'y': // normal PSO - local best
                        CalculateVelocityLBest();
                        break;
                    case 'a': // Weighted Inertia - gbest  
                        // Console.WriteLine("Using Weighted Inertia Velocity Update with Global Best"); 
                        // calculate weighting function
                        /* w = wMax-[(wMax-wMin) x iter]/maxIter
                            where wMax= initial weight,
                            wMin = final weight,
                            maxIter = maximum iteration number,
                            iter = current iteration number. */
                        double w = (double)(wmax - (((wmax - wmin) * (double)iteration) / (double)maxIterations));
                        // update velocity and position
                        CalculateWeightedVelocityGBest(w);
                        break;
                    case 'b': // Vmax - Gbest
                        //Console.WriteLine("Using Vmax Velocity Update with Global Best"); 
                        CalculateVmaxVelocityGBest();
                        break;
                    case 'c': // Constriction factor - gbest
                        // Console.WriteLine("Using Constriction Factor Velocity Update with Global Best"); 
                        // update velocity and position 
                       CalculateConstrictionFactorVelocityGBest();
                        break;
                    case 'd': // Weighted Inertia - lbest   
                        // Console.WriteLine("Using Weighted Inertia Velocity Update with  Local Best"); 
                        w = (double)(wmax - (((wmax - wmin) * (double)iteration) / (double)maxIterations));
                        // update velocity and position
                        CalculateWeightedVelocityLBest(w);
                        break;
                    case 'e': // Vmax - lbest
                        //Console.WriteLine("Using Vmax Velocity Update with Local Best");
                        CalculateVmaxVelocityLBest();
                        break;
                    case 'f': // Constriction factor - lbest
                        // update velocity and position 
                        CalculateConstrictionFactorVelocityLBest();
                        break;
                    case 'g': // change random r - seed?
                        int factor = maxIterations / 10;
                        int remainder = 0;
                        Math.DivRem(iteration, factor, out remainder);
                        if (remainder == 0) // count is divisible by factor
                        {
                            r = new Random(iteration);
                        }
                        CalculateVelocity(); // use normal velocity
                        break;
                    default:
                        CalculateVelocity(); //normal velocity
                        break;
                }
                iteration++;

                // gbest.Print(count); 
                // print solutions at 0, 10, 100, 1000, 10000 iterations
                if (iteration == 0 || iteration < 10 ||
                    iteration == 100 || iteration == 1000 || iteration == 10000)
                {
                    // print gbest                
                    Console.WriteLine("\n\nIteration Best @ iteration #"+ iteration +" : ");
                    Console.WriteLine(gbest.toString());
                }


            } while (iteration < this.maxIterations);
            return gbest;
        }


        // CALCULATE LBEST FROM PARTICLE NEIGHBOURHOOD
        void CalculateLocalBest()
        {
            // update lbest values once all particle pbest values have been calculated
            for (int i = 0; i < swarm_size; i++)
            {
                Particle1a temp_best = swarm.ElementAt(i);

                for (int j = -3; j <= 3; j++) // 3 neighbours on each side
                {
                    if (j != 0)
                    { // don't include current particle
                        int neighbour = i + j;
                        if (neighbour < 0) // to deal with ...27-28-29-0-1-2-3... case
                        {
                            neighbour = neighbour + swarm_size;
                        }
                        else if (neighbour >= swarm_size)
                        {
                            neighbour = neighbour - swarm_size;
                        }

                        // if neighbourhood best is better than current p's pbest
                        if (swarm.ElementAt(neighbour).GetFitness() < temp_best.GetFitness())
                        {
                            temp_best = swarm.ElementAt(neighbour); // store pbest as tempbest                           
                        }
                    }
                }
                // store tempbest as p's local best
                swarm.ElementAt(i).SetLBest(temp_best.Clone());
            }
        }

        void CalculateVelocity()
        {
            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    //velocity
                    double vnew = p.GetVelocity(i) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                        + (c2 * r.NextDouble() * (gbest.GetX(i) - p.GetX(i)));
                    p.SetVelocity(i, vnew);
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }
            }
            return;
        }

        
        void CalculateVelocityLBest()
        {
            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    //velocity
                    double vnew = p.GetVelocity(i) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                        + (c2 * r.NextDouble() * (p.GetLBest().GetX(i) - p.GetX(i)));
                    p.SetVelocity(i, vnew);
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }
            }
            return;
        }

      
        void CalculateWeightedVelocityGBest(double w)
        {
            // Vik+1 = wVik +c1 rand1(…) x (pbesti-sik) + c2 rand2(…) x (gbest-sik)

            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    //velocity
                    double vnew = (w * p.GetVelocity(i)) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                        + (c2 * r.NextDouble() * (gbest.GetX(i) - p.GetX(i)));
                    //store velocity
                    p.SetVelocity(i, vnew);
                    // update position
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }
            }
            return;
        }



        // OPTION B: Vmax Velocity w/ global best
        void CalculateVmaxVelocityGBest()
        {
            /* vnew = vold + c1*rand1*(pbest - xfitness) + c2*rand2*(gbest - xfitness)
                xfitness' = xfitness + vnew

                if currentv > vmax then 
	                currentv = vmax
                else if currentv < -vmax then 
	                currentv = -vmax
                end */
            if (gbest == null) // if no gbest
            {
                gbest = swarm.ElementAt(0); //set gbest as first particle 0
            }

            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    double vnew = p.GetVelocity(i) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                                           + (c2 * r.NextDouble() * (gbest.GetX(i) - p.GetX(i)));
                    // ensure vnew is within vmax bound
                    if (vnew > vmax)
                    {
                        vnew = vmax;
                    }
                    else if (vnew < ((-1) * vnew))
                    {
                        vnew = (-1) * vmax;
                    }
                    //store velocity
                    p.SetVelocity(i, vnew);
                    // update position
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }

            }
            return;
        }

        // OPTION C: Constriction factor veloity w/ global best
        void CalculateConstrictionFactorVelocityGBest()
        {
            /* vnew = K[vcurrent +c1*rand1*(pbest - currentfitness) + c2*rand2*(gbest - currentfitness)]
            xfitness' = xfitness + vnew

            phi = c1 + c2, phi > 4
            K = 2/mag(2-phi-sqrt(phi^2 - 4*phi)) */

            // constriction factor calculation
            double phi = c1 + c2;
            double k = 2 / (Complex.Abs(2 - phi - Math.Sqrt(Math.Pow(phi, 2) - (4 * phi))));

            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    double vnew = k * (p.GetVelocity(i) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                                          + (c2 * r.NextDouble() * (gbest.GetX(i) - p.GetX(i))));

                    //store velocity
                    p.SetVelocity(i, vnew);
                    // update position
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }

            }
            return;
        }

        // OPTION D: Weighted velocity w/ local best        
        void CalculateWeightedVelocityLBest(double w)
        {
            // Vik+1 = wVik +c1 rand1(…) x (pbesti-sik) + c2 rand2(…) x (gbest-sik)
            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    //velocity
                    double vnew = (w * p.GetVelocity(i)) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                        + (c2 * r.NextDouble() * (p.GetLBest().GetX(i) - p.GetX(i)));
                    //store velocity
                    p.SetVelocity(i, vnew);
                    // update position
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }

            }
            return;
        }

        // OPTION E: Vmax Velocity w/ local best
        void CalculateVmaxVelocityLBest()
        {
            /* vnew = vold + c1*rand1*(pbest - xfitness) + c2*rand2*(gbest - xfitness)
                xfitness' = xfitness + vnew

                if currentv > vmax then 
	                currentv = vmax
                else if currentv < -vmax then 
	                currentv = -vmax
                end */
            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    double vnew = p.GetVelocity(i) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                                             + (c2 * r.NextDouble() * (p.GetLBest().GetX(i) - p.GetX(i)));
                    // ensure vnew is within vmax bound
                    if (vnew > vmax)
                    {
                        vnew = vmax;
                    }
                    else if (vnew < (-1) * vnew)
                    {
                        vnew = (-1) * vmax;
                    }
                    //store velocity
                    p.SetVelocity(i, vnew);
                    // update position
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }

            }
            return;
        }

        // OPTION F: Constriction factor veloity w/ local best
        void CalculateConstrictionFactorVelocityLBest()
        {
            /* vnew = K[vcurrent +c1*rand1*(pbest - currentfitness) + c2*rand2*(gbest - currentfitness)]
            xfitness' = xfitness + vnew

            phi = c1 + c2, phi > 4
            K = 2/mag(2-phi-sqrt(phi^2 - 4*phi)) */

            // constriction factor calculation
            double phi = c1 + c2;
            double k = 2 / (Complex.Abs(2 - phi - Math.Sqrt(Math.Pow(phi, 2) - (4 * phi))));

            foreach (Particle1a p in swarm)
            {
                for (int i = 0; i < 2; i++)
                {
                    double vnew = k * (p.GetVelocity(i) + (c1 * r.NextDouble() * (p.GetPBest().GetX(i) - p.GetX(i)))
                                          + (c2 * r.NextDouble() * (p.GetLBest().GetX(i) - p.GetX(i))));
                    //store velocity
                    p.SetVelocity(i, vnew);
                    // update position
                    double xnew = p.GetX(i) + vnew;
                    p.SetX(i, xnew);
                }
            }
            return;
        }

    }
}
