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
                Console.WriteLine("Assignment Part (a,b,c):");
                char a = Console.ReadLine().ToCharArray()[0];

                switch (a)
                {
                    case 'a':
                        Assignment1a parta = new Assignment1a(); 
                        parta.PartA();
                        break;
                    case 'b':
                        Assignment1b partb = new Assignment1b();
                        partb.RunPartB();
                        break;
                    default:
                        Assignment1c partC = new Assignment1c();
                        partC.RunPartC();
                        break; 
                }
                
            }
        }


    }
}
