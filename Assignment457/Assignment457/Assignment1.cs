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

                Console.WriteLine("Assignment Part (a,b,c):");
                char a = Console.ReadLine().ToCharArray()[0];

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
                    default:
                        partc = new Assignment1c();
                        partc.RunPartC();
                        break; 
                }
                
            }
        }


    }
}
