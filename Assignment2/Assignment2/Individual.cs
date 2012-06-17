using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    class Individual
    {
        // Private variables
        int _rank;
        int _fitness;
        int[] _individual;
        double _normalizedFitness;
        double _rouletteWheelValue;

        // public getters
        public int rank
        {
            set { this._rank = value; }
            get { return _rank; }
        }
        public int fitness 
        { 
            set { this._fitness = value; } 
            get { return _fitness; } 
        }
        public int[] individual
        {
            set { this._individual = value; }
            get { return _individual; }
        }
        public double normalizedFitness
        {
            set { this._normalizedFitness = value; }
            get { return _normalizedFitness; }
        }
        public double rouletteWheelValue
        {
            set { this._rouletteWheelValue = value; }
            get { return _rouletteWheelValue; }
        }

        // Constructor
        public Individual(int[] individual)
        {
            _individual = individual;
        }
    }
}
