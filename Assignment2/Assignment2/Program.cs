using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Question1 q1 = null;
                Question2 q2 = null;
                Question3 q3 = null;

                Console.WriteLine("===============================================================");
                Console.WriteLine("ECE457-ASSIGNMENT 2");
                Console.WriteLine("Shweta Aladi, Bilal Arshad, Pooja Sardesai");
                Console.WriteLine("===============================================================\n");
                Console.WriteLine("Select the assignment question:");
                Console.WriteLine("[A] Vehicle Routing Problem");
                Console.WriteLine("[B] QAP Assignment");
                Console.WriteLine("[C] PID Controller");
                Console.WriteLine("[Q] Quit");
                char a = Console.ReadLine().ToLower().ToCharArray()[0];

                switch (a)
                {
                    case 'a':                        
                        Console.WriteLine("Run with constraint? Y/N");
                        char b = Console.ReadLine().ToLower().ToCharArray()[0];
                        if (b == 'y')
                        {
                            q1 = new Question1(6, 100, 200, 0.9, 50); //constraint
                        }
                        else if (b == 'n')
                        {
                            q1 = new Question1(6, 100, 200, 0.9); // no constraint
                        }
                        else
                        {
                            break; 
                        }
                        
                        q1.RunPartA(); 
                        break;
                    case 'b':
                        q2 = new Question2();
                        q2.RunPartB();
                        break;
                    case 'c':
                        Console.WriteLine("Use default values? Y/N [No. of Generations: 50, Population: 150, Mutation Prob.: 0.25, Crossover Prob.: 0.6 ]");
                        char i = Console.ReadLine().ToLower().ToCharArray()[0];
                        String filename = "";
                        if (i == 'y')
                        {
                            Console.WriteLine("Enter File Name to save results: [Leave Blank To NOT Save the Results]");
                            filename = Console.ReadLine();

                            q3 = new Question3();
                        }
                        else if (i == 'n')
                        {
                            Console.WriteLine("Enter File Name to save results: [Leave Blank To NOT Save the Results]");
                            filename = Console.ReadLine();

                            try
                            {
                                int generations = 0;
                                while (generations <= 0)
                                {
                                    Console.WriteLine("Enter No. of Generations: [>0]");
                                    generations = int.Parse(Console.ReadLine());
                                }

                                int population = 0;
                                while (population <= 2)
                                {
                                    Console.WriteLine("Enter Population Size: [>2]");
                                    population = int.Parse(Console.ReadLine());
                                }

                                double mutationProb = -1;
                                while ( !(mutationProb >= 0 && mutationProb <= 1) )
                                {
                                    Console.WriteLine("Enter Mutation Prob.: [0 - 1]");
                                    mutationProb = double.Parse(Console.ReadLine());
                                }

                                double crossoverProb = -1;
                                while ( !(crossoverProb >= 0 && crossoverProb <= 1) )
                                {
                                    Console.WriteLine("Enter Crossover Prob.: [0 - 1]");
                                    crossoverProb = double.Parse(Console.ReadLine());
                                }

                                q3 = new Question3(generations, population, mutationProb, crossoverProb);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error Occured: " + e.ToString());
                                break;
                            } 

                        } else {
                            break;
                        }
                        q3.Run();
                        if (!filename.Equals(""))
                        {
                            Question3.generateCSV(filename,q3.GetBestPopulation(),q3.GetAllFitness());
                        }
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
