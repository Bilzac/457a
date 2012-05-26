using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    class Pair
    {
        // Private variables
        int _x;
        int _y;
        int _solution;
        int _tenure;

        // Public getters
        public int x 
        {
            get { return this._x; }
        }
        public int y 
        {
            get { return this._y; }
        }
        public int solution 
        {
            get { return this._solution; }
        }
        public int tenure 
        {
            set { this._tenure = value; } 
            get { return this._tenure; }
        }

        // Constructor
        public Pair(int x, int y, int solution)
        {
            _x = x;
            _y = y;
            _solution = solution;
        }
    }
}
