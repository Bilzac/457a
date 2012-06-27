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
                        q3 = new Question3();
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
