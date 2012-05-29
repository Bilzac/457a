using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assignment457
{
    class Assignment1
    {

        static void Main(string[] args)
        {

            while (true)
            {

                Assignment1a parta = null;
                Assignment1b partb = null;
                Assignment1c partc = null;

                Console.WriteLine("===============================================================");
                Console.WriteLine("ECE457-ASSIGNMENT 1");
                Console.WriteLine("Shweta Aladi, Bilal Arshad, Pooja Sardesai"); 
                Console.WriteLine("===============================================================\n\n");
                Console.WriteLine("Select the assignment question:\n[A] Maze Traversal\n[B] Conga Game\n[C] QAP Assignment"
                    + "\n[Q] Quit");
                char a = Console.ReadLine().ToLower().ToCharArray()[0];

                switch (a)
                {
                    case 'a':
                        parta = new Assignment1a(); 
                        parta.PartA();
                        break;
                    case 'b':
                        partb = new Assignment1b();
                        partb.RunPartB();
                        break;
                    case 'c':
                        partc = new Assignment1c();
                        partc.RunPartC();
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
