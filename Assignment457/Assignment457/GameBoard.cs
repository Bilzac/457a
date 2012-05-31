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
        MinMax node_type; //min - player 1 = white, max - player 2 (us) = black
        //int number_moves; 

        //store parent
        GameBoard parent = null; 
        //store children
        List<GameBoard> children = null; 
        
        public enum MinMax //specify whether this is a min node or a max node
        {
            Min = 0, Max, Null
        };


        // default constructor
        public GameBoard()
        {
            //parents and children
            this.parent = null;
            this.children = new List<GameBoard>(); 


            this.board = new GamePiece[4,4]; //the game board - 4x4 array

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    this.board[i, j] = new GamePiece(Colour.NONE, 0);
                }
            }
            
            this.board[0, 3].stones = 10; //player 1 - BLACK stones (them)
            this.board[0, 3].colour = Colour.BLACK; //MIN

            this.board[3, 0].stones = 10; //player2 - WHITE stones (us)
            this.board[3, 0].colour = Colour.WHITE; //MAX

            this.node_type = MinMax.Null; 
        }

        public GameBoard(MinMax node_type, GameBoard parent)
        {
            //parents and children
            this.parent = parent;
            this.children = new List<GameBoard>();

            this.board = parent.CopyBoard(); 

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
               
        public GameBoard GetParent()
        {
            return this.parent;
        }

        public List<GameBoard> GetChildren()
        {
            return this.children; 
        }

        //set methods
        public bool SetParent(GameBoard parent)
        {
            this.parent = parent;
            return true; 
        }

        public bool SetNodeType(MinMax type)
        {
            this.node_type = type;
            return true; 
        }

        public bool SetAlphaBetaValue(int alpha_beta)
        {
            if (this.node_type == MinMax.Max)
            {
                this.alpha = alpha_beta;
            }
            else if (this.node_type == MinMax.Min)
            {
                this.beta = alpha_beta; 
            }
            return true; 
        }

        //Add children
        public bool AddChildren(GameBoard child)
        {
            this.children.Add(child);
            return true; 
        }

        public GamePiece ReturnPosition(int x, int y)
        {
            if (x <= 3 && y <= 3 && x >= 0 && y >= 0) //boundary check
            {
                return this.board[x, y];
            }
            return null; 
        }

        public bool SetStones(int x, int y, int stones, Colour colour)
        {
            if (x <= 3 && y <= 3 && x >= 0 && y >= 0) //boundary check
            {
                this.board[x, y].stones = stones;
                this.board[x, y].colour = colour; 

                if (stones == 0)//no stones on square - null colour
                {
                    this.board[x, y].colour = Colour.NONE; 
                }

                return true; 
            }
            return false; 
        }

        public GamePiece GetGamePiece(int x, int y)
        {
            if (x <= 3 && y <= 3 && x >= 0 && y >= 0) //boundary check
            {
                return this.board[x, y];
            }
            return null; 
        }

        public GamePiece[,] CopyBoard()
        {
            GamePiece[,] clone = new GamePiece[4, 4];

            for (int x = 0; x <= 3; x++)
            {
                for (int y = 0; y <= 3; y++)
                {
                    clone[x, y] = new GamePiece(this.board[x, y].colour, this.board[x, y].stones); 
                    //clone[x, y].stones = this.board[x, y].stones;
                    //clone[x, y].colour = this.board[x, y].colour;
                }
            }
            return clone; 
        }
        /*
        public GameBoard CloneBoard()
        {
            GameBoard clone = 


        }*/


    }
}
