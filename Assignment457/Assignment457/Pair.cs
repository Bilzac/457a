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
        bool _isInitialized;

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
        public bool isInitialized
        {
            get { return this._isInitialized; }
        }

        // Constructor
        public Pair()
        {
            _isInitialized = false;
        }
        
        public Pair(int x, int y, int solution, bool isInitialized)
        {
            _x = x;
            _y = y;
            _solution = solution;
            _isInitialized = true;
        }
    }
}
