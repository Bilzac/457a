using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics; 

namespace ConsoleApplication1
{
    class Part1b
    {
        // GLOBAL VARIABLES
        int swarm_size; // number of particles in swarm
        LinkedList<Particle> swarm; //swarm = list of particles
        int iterations; // total number of iteration
        double c1; // accel coefficient-pbest
        double c2; // accel coefficient-lbest
        Particle gbest; // best-so-far solution
        Random r;
        Char part4; // decide which PSO version to run

        // Weighted velocity
        double wmax; // max/min weighting factors
        double wmin; 

        // Vmax
        double vmax; //max velocity

        // CONSTRUCTOR
        /// <summary>
        ///  Initialize swarm 
        /// </summary>
        /// <param name="swarm_size"></param>
        public Part1b(int swarm_size, int iterations, double c1, double c2, char part4)
        {
            // store swarm size, iterations, c1, c2, part4
            this.swarm_size = swarm_size;
            this.iterations = iterations; 
            this.c1 = c1; 
            this.c2 = c2;
            this.part4 = part4; 

            // Weighted Velocity
            this.wmax = 1.0; // influence of current velocity
            this.wmin = 0.0; // on velocity update fxn
            
            // Vmax
            vmax = 5.0; // bound on particle velocity

            // init random r
            r = new Random(); 
            
              /*  For each particle
              Initialize particle
              END*/
            // initialize swarm 
            swarm = new LinkedList<Particle>(); 
            for (int i = 0; i < swarm_size; i++)
            {
                // init particle
                Particle p = new Particle(i);
                // add to swarm
                swarm.AddLast(p); 
            }
        }

            
  //While maximum iterations or minimum error criteria is not attained or best achieved 
        
        // RUN ALGORITHM
        public Particle RunParticleSwarmOptimization()
        {
            int count = 0; 

            while (count < iterations)
            {

                /* For each particle
                      Calculate fitness value
                      If the fitness value is better than the best fitness value (pBest) in history
                          set current value as the new pBest
                   End */
                
                // calculate fitness for each particle
                foreach (Particle p in swarm)
                {
                    //p.Print(iterations); 
                    // calculate fitness
                    double current_fitness = CalculateFitness(p);

                    // particle's current fitness is better than its pbest
                    // update pbest with current fitness
                    if ( Math.Abs(current_fitness) < Math.Abs(p.GetPBest()) )
                    {
                        p.SetPBest(current_fitness); 
                    }                    
                }

                /* Choose the particle with the best fitness value of neighbourhood particles as the LBest */
                CalulateLocalBest(); 

                /*
                For each particle
                    Calculate particle velocity according equation (a)
                 * v[] = v[] + c1 * rand() * (pbest[] - present[]) + c2 * rand() * (gbest[] - present[]) (a)
                    Update particle position according equation (b)
                 * present[] = persent[] + v[] (b)
                End */
                
                // choose between the different velocity up date options
                switch(part4)
                {                
                    case 'n': // normal PSO 
                        CalculateVelocity(); 
                        break; 
                    case 'a': // Weighted Inertia - gbest  
                        // Console.WriteLine("Using Weighted Inertia Velocity Update with Global Best"); 
                        // calculate weighting function
                        /* w = wMax-[(wMax-wMin) x iter]/maxIter
                            where wMax= initial weight,
                            wMin = final weight,
                            maxIter = maximum iteration number,
                            iter = current iteration number. */
                        double w = wmax - (((wmax - wmin) * count) / (double) iterations); 
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
                        w = wmax - (((wmax - wmin) * count) / (double)iterations);
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
                        int factor = iterations / 10; 
                        int remainder = 0; 
                        Math.DivRem(count, factor, out remainder);
                        if (remainder == 0) // count is divisible by factor
                        {
                            r = new Random(); 
                        }                        
                        CalculateVelocity(); // use normal velocity
                        break; 
                    default:
                        CalculateVelocity(); //normal velocity
                        break; 
                }
              
                // stopping condn = if pbest achieved (all x = 0) - break
                foreach (Particle p in swarm)
                {
                    // update gbest
                    if (gbest == null)
                    {
                        gbest = p;
                    }
                    else if (Math.Abs(p.GetPBest()) < Math.Abs(gbest.GetPBest()))
                    {
                        gbest = p;                         
                    }

                    // if the sum is 0 - best possible solution, stop
                    if (p.GetPBest() == 0.0)
                    {
                        gbest = p; 
                        break; 
                    }
                }
                //Console.WriteLine("Iteration: " + iterations.ToString()
                //    + "\nBest-so-far: " + gbest.GetPBest().ToString()); 
                count++;

                // print solutions at 0, 10, 100, 1000, 10000 iterations
                //if (count == 0 || count == 10 ||
                //    count == 100 || count == 1000 || count == 10000)
                //{
                //    // print gbest
                //    Console.WriteLine("\n\nIteration Best: ");
                //    gbest.Print(count);
                //}

            }

            // print gbest
            Console.WriteLine("\n\n\n\nGlobal Best: ");
            gbest.Print(iterations);

            return gbest; 
        }

        // CALCULATE PARTICLE FITNESS
        double CalculateFitness(Particle p)
        {
            // get array
            double[] x_array = p.GetX();

            double sum = 0; // total 

            //get sum of all x values in array 
            for (int i = 0; i < 10; i++)
            {
                sum = sum + x_array[i];     
            }

            p.SetFitness(sum); //set particle's fitness
            return sum; 
        }

        // CALCULATE LBEST FROM PARTICLE NEIGHBOURHOOD
        void CalulateLocalBest()
        {
            // update lbest values once all particle pbest values have been calculated
            for (int i = 0; i < swarm_size; i++)
            {
                Particle temp_best = swarm.ElementAt(i);

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

        // CALCULATE VELOCITY AND UPDATE POSITION/SOLUTION


        // REGULAR PSO----------------------------------------------------------------------------
        void CalculateVelocity()
        {
            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // update velocity
                double vnew = (double)(v + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (p.GetLBest().GetPBest() - p.GetFitness())));

                p.SetVelocity(vnew); // update particle velocity

                // update solution = add velocity v to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
                }

            }
            return; 
        }

        // OPTION A: Weighted velocity w/ global best        
        void CalculateWeightedVelocityGBest(double w)
        {
            // Vik+1 = wVik +c1 rand1(…) x (pbesti-sik) + c2 rand2(…) x (gbest-sik)

            if (gbest == null) // if no gbest
            {
                gbest = swarm.ElementAt(0); //set gbest as first particle 0
            }

            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // update velocity
                double vnew = (double)( (w * v) + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (gbest.GetPBest() - p.GetFitness())));

                p.SetVelocity(vnew); // update particle velocity

                // update solution = add velocity v to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
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

            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // calculate new velocity
                double vnew = (double)(v + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (gbest.GetPBest() - p.GetFitness())));

                // ensure vnew is within vmax bound
                if (vnew > vmax)
                {
                    vnew = vmax;
                }
                else if (vnew < (-1) * vnew)
                {
                    vnew = (-1) * vmax;
                }

                p.SetVelocity(vnew); // update particle velocity with vnew

                // update solution = add velocity vnew to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
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
            
            if (gbest == null) // if no gbest
            {
                gbest = swarm.ElementAt(0); //set gbest as first particle 0
            }

            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // update velocity
                double vnew = (double)((k * v) + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (gbest.GetPBest() - p.GetFitness())));

                p.SetVelocity(vnew); // update particle velocity

                // update solution = add velocity v to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
                }
            }
            return;
        }

        // OPTION D: Weighted velocity w/ local best        
        void CalculateWeightedVelocityLBest(double w)
        {
            // Vik+1 = wVik +c1 rand1(…) x (pbesti-sik) + c2 rand2(…) x (gbest-sik)

            if (gbest == null) // if no gbest
            {
                gbest = swarm.ElementAt(0); //set gbest as first particle 0
            }

            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // update velocity
                double vnew = (double)((w * v) + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (p.GetLBest().GetPBest() - p.GetFitness())));

                p.SetVelocity(vnew); // update particle velocity

                // update solution = add velocity v to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
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
            if (gbest == null) // if no gbest
            {
                gbest = swarm.ElementAt(0); //set gbest as first particle 0
            }

            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // calculate new velocity
                double vnew = (double)(v + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (p.GetLBest().GetPBest() - p.GetFitness())));

                // ensure vnew is within vmax bound
                if (vnew > vmax)
                {
                    vnew = vmax;
                }
                else if (vnew < (-1) * vnew)
                {
                    vnew = (-1) * vmax;
                }

                p.SetVelocity(vnew); // update particle velocity with vnew

                // update solution = add velocity vnew to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
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

            if (gbest == null) // if no gbest
            {
                gbest = swarm.ElementAt(0); //set gbest as first particle 0
            }

            foreach (Particle p in swarm)
            {
                double v = p.GetVelocity();  // get current velocity

                // update velocity
                double vnew = (double)((k * v) + (c1 * r.NextDouble() * (p.GetPBest() - p.GetFitness()))
                    + (c2 * r.NextDouble() * (p.GetLBest().GetPBest() - p.GetFitness())));

                p.SetVelocity(vnew); // update particle velocity

                // update solution = add velocity v to each x 
                double[] x_array = p.GetX();
                for (int i = 0; i < 10; i++)
                {
                    x_array[i] = x_array[i] + vnew;
                }
            }
            return;
        }


    }
}
