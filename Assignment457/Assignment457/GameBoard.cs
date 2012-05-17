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
            Min = 0, Max, Null
        };


        // default constructor
        public GameBoard()
        {
            this.board = new GamePiece[4,4]; //the game board - 4x4 array

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    this.board[i, j] = new GamePiece(Colour.NONE, 0);
                }
            }
            
            this.board[1, 4].stones = 10; //player 1 - white stones (them)
            this.board[1, 4].colour = Colour.WHITE;

            this.board[4, 1].stones = 10; //player2 - black stones (us)
            this.board[4, 1].colour = Colour.BLACK;

            this.node_type = MinMax.Null; 
        }

        public GameBoard(MinMax node_type)
        {
            this.board = new GamePiece[4, 4]; //the game board - 4x4 array
            this.board = new GamePiece[4, 4]; //the game board - 4x4 array

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    this.board[i, j] = new GamePiece(Colour.NONE, 0);
                }
            }

            this.board[0, 3].stones = 10; //player 1 - white stones (them)
            this.board[0, 3].colour = Colour.WHITE;

            this.board[3, 0].stones = 10; //player2 - black stones (us)
            this.board[3, 0].colour = Colour.BLACK; 

            if (node_type == MinMax.Max)
            {
                this.node_type = MinMax.Max;
                this.alpha = 0; 
            }
            else
            {
                this.node_type = MinMax.Min;
                this.beta = 0; 
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

        public GamePiece[,] GetGameBoard()
        {
            return this.board;
        }

        public GamePiece GetGamePiece(int x, int y)
        {
            return board[x, y]; 
        }

    }
}
