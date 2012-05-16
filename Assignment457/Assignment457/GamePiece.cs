using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    //The object that stores the colour and number of pieces in each square
    //black, white, null; number = 0 to 10

    public enum Colour
    {
        WHITE,
        BLACK,
        NONE
    }

    // a square on the board
    class GamePiece
    {
        // private variables
        private Colour _colour;
        private byte _stones;

        // Public getters and setters
        public Colour colour
        {
            set { this._colour = value; }
            get { return this._colour; }
        }
        public byte stones
        {
            set { this._stones = value; }
            get { return this._stones; }
        }

         //default constructor 
        public GamePiece(Colour colour, byte stones)
        {
            _colour = colour;
            _stones = stones;
        }
    }
}
