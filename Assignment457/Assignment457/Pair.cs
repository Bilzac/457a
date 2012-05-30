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
        int _cost;

        // Public getters
        public int x 
        {
            get { return this._x; }
        }
        public int y 
        {
            get { return this._y; }
        }
        public int cost 
        {
            get { return this._cost; }
        }
        
        public Pair(int x, int y, int cost)
        {
            _x = x;
            _y = y;
            _cost = cost;
        }
    }
}
