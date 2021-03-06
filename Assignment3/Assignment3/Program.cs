﻿using System;
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
            Part1a parta = null;
            Particle1a rbesta = null;
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
                        rbesta = null;
                        Console.WriteLine("Choose from the following variations: "
                            + "\n   [x] Regular PSO with Global Best"
                            + "\n   [y] Regular PSO with Local Best"
                            + "\n   [a] Inertia Weight Velocity with Global Best"
                            + "\n   [b] Vmax with Global Best"
                            + "\n   [c] Constriction Factor Velocity with Global Best"
                            + "\n   [d] Inertia Weight Velocity with Local Best"
                            + "\n   [e] Vmax with Local Best"
                            + "\n   [f] Constriction Factor Velocity with Local Best"
                            + "\n   [g] Random Number Seed Variation (10x)");
                 
                        Char a1 = Console.ReadLine().ToLower().ToCharArray()[0];

                        for (int i = 0; i < 10; i++)
                        {
                            parta = new Part1a(30, 10000, 2.5, 2.5, a1);
                            Particle1a solution = parta.RunPSO();
                            if (rbesta == null || rbesta.GetFitness() > solution.GetFitness())
                            {
                                rbesta = solution;
                            }
                        }
                        Console.WriteLine("\nBest Solution");
                        Console.WriteLine(rbesta.toString());
                        break;
                    case 'b':
                        // print options
                        Console.WriteLine("Choose from the following variations: " 
                            + "\n   [x] Regular PSO with Global Best"
                            + "\n   [y] Regular PSO with Local Best"
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
                            Particle new_best = partb.RunSwarm();

                            // store run best
                            if (rbest == null)
                            {
                                rbest = new_best;
                            }
                            else if (Math.Abs(rbest.GetFitness()) > Math.Abs(new_best.GetFitness()))
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
