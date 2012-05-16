using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment457
{
    //the "node" class  - contains array of GamePiece objects
    class GameBoard
    {
        //class variables
        GamePiece[,] board = null;
        int alpha;
        int beta;
        MinMax node_type; 
        
        public enum MinMax //specify whether this is a min node or a max node
        {
            Min = 0, Max
        };


        // default constructor
        public GameBoard()
        {
            board = new GamePiece[4,4]; //the game board - 4x4 array
        }

        public GameBoard(MinMax type)
        {
            if (type == MinMax.Max)
            {
                this.node_type = MinMax.Max;
            }
            else
            {
                this.node_type = MinMax.Min; 
            }
        }



        //public get and set methods
        public MinMax GetNodeType()
        {
            return this.node_type;
        }

        public int GetAlphaBetaValue()
        {
            if (this.node_type == MinMax.Max)
            {
                return this.alpha;
            }
            else if(this.node_type == MinMax.Min)
            {
                return this.beta; 
            }

            return 0; 
        }

    }
}
