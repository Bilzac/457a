using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class Question3
    {

        private double crossover_prob = 0.6;
        private double mutation_prob = 0.25;
        private double totalFitness;
        private int totalPopulation = 50;
        private int generations = 150;

        public static Random r = new Random();

        ArrayList population; //current population
        ArrayList bestPopulation; //best solution across generations

        public Question3()
        {
            initPopulation();
        }

        public Question3(int totalPopulation, int generations, double mutation_prob, double crossover_prob)
        {
            this.totalPopulation = totalPopulation;
            this.generations = generations;
            this.mutation_prob = mutation_prob;
            this.crossover_prob = crossover_prob;
            initPopulation();
        }

        public void initPopulation()
        {
            population = new ArrayList();
            bestPopulation = new ArrayList();
            totalFitness = 0;
            for (int i = 0; i < totalPopulation; i++)
            {
                double ti = 0, td = 0, kp = 0;
                //kp
                kp = Math.Round(((r.NextDouble() * (PIDConfig.MAX_kp - PIDConfig.MIN_kp)) + PIDConfig.MIN_kp), 2);
                //ti
                ti = Math.Round(((r.NextDouble() * (PIDConfig.MAX_ti - PIDConfig.MIN_ti)) + PIDConfig.MIN_ti), 2);
                //td
                td = Math.Round(((r.NextDouble() * (PIDConfig.MAX_td - PIDConfig.MIN_td)) + PIDConfig.MIN_td), 2);
                population.Add(new PIDConfig(kp, ti, td));
                totalFitness += ((PIDConfig)population[i]).getFitness();
            }
            population.Sort();
            bestPopulation.Add(population[0]);
        }

        public ArrayList GetBestPopulation()
        {
            return bestPopulation;
        }

        public void Run()
        {
            
            Random r = new Random();
            for (int i = 0; i < generations; i++)
            {

                PIDConfig currentLeader1 = (PIDConfig)population[0];
                PIDConfig currentLeader2 = (PIDConfig)population[1];

                ArrayList children = new ArrayList();
                double newOverallFitness = currentLeader1.getFitness() + currentLeader2.getFitness();

                while (children.Count < totalPopulation - 2)
                {
                    PIDConfig[] parents = new PIDConfig[2];

                    //Roulette Wheel
                    for(int l = 0; l < 2; l++)
                    {
                        double spin = r.NextDouble();
                        double fitness = 0;
                        int k = 0;
                        while (spin > (fitness += ((PIDConfig)population[k]).getFitness() / totalFitness))
                        {
                            k++;
                        }
                        parents[l] = (PIDConfig)population[k];
                    }

                    PIDConfig child1 = null;
                    PIDConfig child2 = null;
                    
                    //Crossover
                    if (r.NextDouble() <= crossover_prob)
                    {
                        child1  = new PIDConfig((PIDConfig)parents[0], (PIDConfig)parents[1]);
                        child2 = new PIDConfig((PIDConfig)parents[1], (PIDConfig)parents[0]);
                    }
                    else
                    {
                        child1 = new PIDConfig(parents[0]);
                        child2 = new PIDConfig(parents[1]);
                    }

                    if (r.NextDouble() <= mutation_prob)
                    {
                        child1.Mutate();
                    }
                    newOverallFitness += child1.getFitness();
                    children.Add(child1);

                   
                    if (r.NextDouble() <= mutation_prob)
                    {
                        child2.Mutate();
                    }
                    newOverallFitness += child2.getFitness();
                    children.Add(child2);

                }

                //Survival (Best two from previous generation survive on)
                children.Add(currentLeader1);
                children.Add(currentLeader2);

                population = children;
                totalFitness = newOverallFitness;

                population.Sort();
                bestPopulation.Add(population[0]);
                Console.WriteLine(((PIDConfig)population[0]).ToString());
            }
        }


        public static void generateCSV(String filename, ArrayList a)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename + ".csv");
            file.WriteLine("Kp,Ti,Td,ts,tr,ISE,Mp,Fitness");
            for (int i = 0; i < a.Count; i++)
            {
                PIDConfig tmp = (PIDConfig)a[i];
                String line = tmp.KP + "," + tmp.TI + "," + tmp.TD + "," + tmp.TS + "," + tmp.TR + "," + tmp.ISE + "," + tmp.MP + "," + tmp.getFitness();
                file.WriteLine(line);
            }
            file.Close();
        }
    }
}