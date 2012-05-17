using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics; 

namespace Assignment457
{
    class Assignment1b
    {

        //create starting gameboard
        GameBoard start_game = new GameBoard(GameBoard.MinMax.Max, null); //start with max node
        
        Stopwatch timer = new Stopwatch(); 

        public Assignment1b()
        {
        }

        public void RunPartB()
        {
            Console.WriteLine("Search Agent - Alpha/Beta Pruning"); 

            timer.Start();
            AlphaBetaSearch(); 
            timer.Stop(); 
            Console.WriteLine(timer.Elapsed.ToString()); 
            timer.Reset(); 

            return; 
        }


        static void AlphaBetaSearch()
        {


            /*
            alpha-beta(player,board,alpha,beta)
    if(game over in current board position)
        return winner

    children = all legal moves for player from this board
    if(max's turn)
        for each child
            score = alpha-beta(other player,child,alpha,beta)
            if score > alpha then alpha = score (we have found a better best move)
            if alpha >= beta then return alpha (cut off)
        return alpha (this is our best move)
    else (min's turn)
        for each child
            score = alpha-beta(other player,child,alpha,beta)
            if score < beta then beta = score (opponent has found a better worse move)
            if alpha >= beta then return beta (cut off)
        return beta (this is the opponent's best move)
                    */




        }

        static int CalculateAlpha()
        {
            /* Alpha values are stored in the max nodes
             * Alpha value = max(children's Beta Values of min nodes)
             * Because we want to minimize opponent moves
             * 
             */ 
            return 0; 
        }

        static int CalculateBeta()
        {
            /* Beta values are stored in the min nodes
             * Beta value = min(alpha Values of max nodes)
             * Because we want to maximize our moves
             * 
             */ 
            return 0; 
        }

        static int CalculateMoves(GameBoard board)
        {
            /* 
             * 
             * 
             */

            int moves = 0; 

            Colour player_colour = Colour.NONE; 

            if (board.GetNodeType() == GameBoard.MinMax.Max)
            {
                player_colour = Colour.BLACK; 
            }else if (board.GetNodeType() == GameBoard.MinMax.Min)
            {
                player_colour = Colour.WHITE; 
            }

            for (int x = 0; x <= 3; x++)
            {

                for (int y = 0; y <= 3; y++)
                {
                    if (board.ReturnPosition(x,y).stones > 0 && board.ReturnPosition(x,y).colour == player_colour)
                    {
                        //POSSIBLE MOVES
                        //direction N
                        if (board.ReturnPosition(x, y++) != null)//boundary 
                        {
                            if (board.ReturnPosition(x, y++).stones == 0) //no stones
                            {
                                moves++; 
                            }else if(board.ReturnPosition(x,y++).colour == player_colour){
                                moves++; 
                            }



                        }


                    }
                }
            }




            return 0;
        }

    }
}
