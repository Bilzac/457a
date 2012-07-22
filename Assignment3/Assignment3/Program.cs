using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment3;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /* part b stuff */
            Part1b partb = null;
            int brun = 0; // track runs 
            Particle rbest = null; // run best    
            
            
            while (true)
            {
                Console.WriteLine("===============================================================");
                Console.WriteLine("ECE457A - ASSIGNMENT 3");
                Console.WriteLine("Shweta Aladi, Bilal Arshad, Pooja Sardesai");
                Console.WriteLine("===============================================================\n");
                Console.WriteLine("Select the assignment question:");
                Console.WriteLine("[A] Camelback Function (PSO)");
                Console.WriteLine("[B] Sphere Function (PSO)");
                Console.WriteLine("[C] Travelling Salesman Problem (ACO)");
                Console.WriteLine("[Q] Quit");
                char a = Console.ReadLine().ToLower().ToCharArray()[0];

                switch (a)
                {
                    case 'a':

                        break;
                    case 'b':
                        // print options
                        Console.WriteLine("Choose from the following variations: " 
                            + "\n   [n] Regular PSO"
                            + "\n   [a] Inertia Weight Velocity with Global Best"
                            + "\n   [b] Vmax with Global Best"
                            + "\n   [c] Constriction Factor Velocity with Global Best"
                            + "\n   [d] Inertia Weight Velocity with Local Best"
                            + "\n   [e] Vmax with Local Best"
                            + "\n   [f] Constriction Factor Velocity with Local Best"
                            + "\n   [g] Random Number Seed Variation (10x)");

                        Char b1 = Console.ReadLine().ToLower().ToCharArray()[0];
                        // run part 1b - need to add switch based on b1
                        for (int x = 0; x < 10; x++)
                        {
                            partb = new Part1b(30, 10000, 2.5, 2.5, b1);
                            Particle new_best = partb.RunParticleSwarmOptimization();

                            // store run best
                            if (rbest == null)
                            {
                                rbest = new_best;
                            }
                            else if (Math.Abs(rbest.GetPBest()) > Math.Abs(new_best.GetPBest()))
                            {
                                rbest = new_best;
                            }
                            brun++; // run partb 
                        }
                        Console.WriteLine("\nRun: " + brun.ToString()
                            + "\nOption: " + b1);
                        rbest.Print(brun);

                        break; 
                        
                    case 'c':
                        Part2 p2 = new Part2();
                        p2.RunPart2();
                        break;
                    case 'q':
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
        }
    }
}
