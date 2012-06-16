using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class Individual
    {
        int _fitness;
        int[] _individual;

        // public getters
        public int fitness { get { return _fitness; } }
        public int[] individual { get { return _individual; } }

        // Constructor
        public Individual(int[] individual, int fitness)
        {
            _individual = individual;
            _fitness = fitness;
        }
    }
}
