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

            displayBoard(start_game);

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
            GameBoard.MinMax child_type = GameBoard.MinMax.Null; 

            if (board.GetNodeType() == GameBoard.MinMax.Max)
            {
                player_colour = Colour.BLACK;
                child_type = GameBoard.MinMax.Min; 
            }else if (board.GetNodeType() == GameBoard.MinMax.Min)
            {
                player_colour = Colour.WHITE;
                child_type = GameBoard.MinMax.Max; 
            }

            for (int x = 0; x <= 3; x++)
            {

                for (int y = 0; y <= 3; y++)
                {
                    if (board.ReturnPosition(x,y).stones > 0 && board.ReturnPosition(x,y).colour == player_colour)
                    {
                        //if statement to sort number of stones
                        //1-2 stones = 1 square; 3 stones = 2 squares, 4+ stones = 3 squares
                        if (board.ReturnPosition(x, y).stones == 1 || board.ReturnPosition(x, y).stones == 2)
                        {

                        }
                        else if (board.ReturnPosition(x, y).stones == 3)
                        {

                        }
                        else
                        {

                        }

                        //POSSIBLE MOVES
                        //direction N
                        int a = x; 
                        int b = y++; 
                        if (board.ReturnPosition(a, b) != null)//boundary 
                        {
                            if (board.ReturnPosition(a, b).stones == 0 ||board.ReturnPosition(x,y++).colour == player_colour) 
                                //no opponent in next square N
                            {
                                GameBoard child_board = new GameBoard(child_type, board); 

                               // if(a,

                                moves++; 
                            }



                        }


                    }
                }
            }




            return 0;
        }


        public static void displayBoard(GameBoard gameBoard)
        {
            Console.WriteLine("-------------------------------------");

            int counter = 0;
            while (counter < 4)
            {
                displayPieceColour(gameBoard.GetGamePiece(counter, 3).colour, gameBoard.GetGamePiece(counter, 3).stones);
                counter++;
            }
            Console.WriteLine("|");
            Console.WriteLine("-------------------------------------");

            counter = 0;
            while (counter < 4)
            {
                displayPieceColour(gameBoard.GetGamePiece(counter, 2).colour, gameBoard.GetGamePiece(counter, 2).stones);
                counter++;
            }
            Console.WriteLine("|");
            Console.WriteLine("-------------------------------------");

            counter = 0;
            while (counter < 4)
            {
                displayPieceColour(gameBoard.GetGamePiece(counter, 1).colour, gameBoard.GetGamePiece(counter, 1).stones);
                counter++;
            }
            Console.WriteLine("|");
            Console.WriteLine("-------------------------------------");

            counter = 0;
            while (counter < 4)
            {
                displayPieceColour(gameBoard.GetGamePiece(counter, 0).colour, gameBoard.GetGamePiece(counter, 0).stones);
                counter++;
            }
            Console.WriteLine("|");
            Console.WriteLine("-------------------------------------");
        }

        public static void displayPieceColour(Colour colour, int num)
        {
            if (num == 10)
            {
                if (colour == Colour.WHITE)
                    Console.Write("|///" + num + "///");
                else if (colour == Colour.BLACK)
                    Console.Write("|..." + num + "...");
                else
                    Console.Write("|   " + num + "   ");
            }
            else
            {
                if (colour == Colour.WHITE)
                    Console.Write("|///" + num + "////");
                else if (colour == Colour.BLACK)
                    Console.Write("|..." + num + "....");
                else
                    Console.Write("|   " + num + "    ");
            }
        }
    }
}
