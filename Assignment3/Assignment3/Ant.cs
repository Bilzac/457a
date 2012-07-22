using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    class Ant
    {
        // PRIVATE VARIABLES
        List<int> _tour;
        List<int> _remaining;
        double _distance;
        int _currentCity;

        // GETTERS AND SETTERS
        public double distance {
            set { this._distance = value; } 
            get { return this._distance; }
        }
        public int currentCity 
        {
            set { this._currentCity = value; }
            get { return this._currentCity; }
        }
        public List<int> remainingCities {
            set { this._remaining = value; }
            get { return this._remaining; }
        }
        public List<int> tour {
            set { this._tour = value; }
            get { return this._tour; }
        }

        // CONSTRUCTOR
        public Ant() {
            _tour = new List<int>(); // intialize list
            _remaining = new List<int>();
            _distance = int.MaxValue;
        }

        public Ant(int startCity) {
            _tour = new List<int>(); // intialize list
            _remaining = new List<int>();
            _distance = 0;

            // get starting city
            _tour.Add(startCity);
            _currentCity = startCity;

            // set remaining cities
            for (int i = 1; i < 30; i++) {
                _remaining.Add(i);
            }
            _remaining.Remove(startCity);
        }

        public void initialize(Random random) {
            // Ant is being used; set distance to 0
            _distance = 0;

            // get starting city
            int randomCity = random.Next(1, 29);
            _tour.Add(randomCity);
            _currentCity = randomCity;

            // set remaining cities
            for (int i = 1; i < 30; i++) {
                _remaining.Add(i);
            }
            _remaining.Remove(randomCity);
        }

        // PUBLIC METHODS
        public bool visitedCity(int city) {
            var sorted = from item in _tour
                         where item == city
                         select item;
            if (sorted.Count() > 0)
                return true;
            return false;
        }

        public void updateDistance(double distance) {
            _distance = _distance + distance;
        }

        public void addCity(int city) {
            _tour.Add(city);
            _currentCity = city;
            _remaining.Remove(city);
        }
    }
}
