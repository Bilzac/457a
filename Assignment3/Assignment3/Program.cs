﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Part 1B: ");
            
            for (int i = 0; i < 10; i++)
            {
                Part1b b = new Part1b(30, 1000, 0.5, 0.5);
                b.RunParticleSwarmOptimization();

            }
            while (true)
            {    

            }

        }
    }
}
