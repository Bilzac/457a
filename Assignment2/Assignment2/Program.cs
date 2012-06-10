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
                        q1 = new Question1();
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
