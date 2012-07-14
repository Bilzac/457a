using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Question1 q1 = null;
                Part1b b = null;
                //Question3 q3 = null;

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
                        b = new Part1b(30, 1000, 0.5, 0.5);
                        b.RunParticleSwarmOptimization();
                        break;
                    case 'c':


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
