﻿using System;
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
                        //1-2 stones = 1 square
                        if (board.ReturnPosition(x, y).stones == 1 || board.ReturnPosition(x, y).stones == 2)
                        {   
                            //north 
                            int a = x;
                            int b = y + 1;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                            //south
                            a = x;
                            b = y - 1;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                            //east
                            a = x - 1;
                            b = y;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                            //west
                            a = x + 1;
                            b = y;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                            //ne
                            a = x + 1;
                            b = y - 1;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                           //nw
                            a = x - 1;
                            b = y - 1;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                           //se 
                            a = x + 1;
                            b = y + 1;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 

                            //sw
                            a = x - 1;
                            b = y + 1;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 
                        }
                        else if (board.ReturnPosition(x, y).stones == 3)//3 stones = 2 squares
                        {
                            //north x2
                            int a = x;
                            int b = y + 1;
                            int c = x;
                            int d = y + 2; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //south x2
                            a = x;
                            b = y - 1;
                            c = x;
                            d = y - 2; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //eastx2
                            a = x - 1;
                            b = y;
                            c = x - 2;
                            d = y; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //westx2
                            a = x + 1;
                            b = y;
                            c = x + 2;
                            d = y; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //nex2
                            a = x + 1;
                            b = y - 1;
                            c = x + 2;
                            d = y - 2; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //nwx2
                            a = x - 1;
                            b = y - 1;
                            c = x - 2;
                            d = y - 2; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //se x2
                            a = x + 1;
                            b = y + 1;
                            c = x + 2;
                            d = y + 2; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //swx2
                            a = x - 1;
                            b = y + 1;
                            c = x - 2;
                            d = y + 2; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 
                        }
                        else //  4+ stones = 3 squares
                        {
                            //north x3
                            int a = x;
                            int b = y + 1;
                            int c = x;
                            int d = y + 2;
                            int e = x;
                            int f = y + 3; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //south x3
                            a = x;
                            b = y - 1;
                            c = x;
                            d = y - 2;
                            e = x;
                            f = y - 3; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //eastx3
                            a = x - 1;
                            b = y;
                            c = x - 2;
                            d = y;
                            e = x - 3;
                            f = y;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //westx3
                            a = x + 1;
                            b = y;
                            c = x + 2;
                            d = y;
                            e = x + 3;
                            f = y;
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //nex3
                            a = x + 1;
                            b = y - 1;
                            c = x + 2;
                            d = y - 2;
                            e = x + 3;
                            f = y - 3; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //nwx3
                            a = x - 1;
                            b = y - 1;
                            c = x - 2;
                            d = y - 2;
                            e = x - 3;
                            f = y - 3; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //se x3
                            a = x + 1;
                            b = y + 1;
                            c = x + 2;
                            d = y + 2;
                            e = x + 3;
                            f = y + 3; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);

                            //swx3
                            a = x - 1;
                            b = y + 1;
                            c = x - 2;
                            d = y + 2;
                            e = x - 3;
                            f = y + 3; 
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves); 
                        }

                    }

                }
            }
            return 0; 
        }


        static int CheckAdjacentMove(GameBoard board, GameBoard.MinMax child_type, Colour player_colour, int x, int y, int a, int b,
            int moves)
        {           
            //check boundary of array
            if (a >= 0 && a <= 3 && b >= 0 && b <= 3)
            {
                //check adjacent square is empty
                if (board.ReturnPosition(a, b).stones == 0 || board.ReturnPosition(a, b).colour == player_colour)
                {
                    //new child
                    GameBoard child = new GameBoard(child_type, board);

                    int stones = child.ReturnPosition(x, y).stones;
                    child.SetStones(x, y, 0);
                    
                    int original_stones = child.ReturnPosition(a, b).stones; //adjacent square
                    child.SetStones(a, b, stones+original_stones);

                    //add child to parent
                    board.AddChildren(child);

                    moves++;
                }
            }
            return moves; 
        }


        static int CheckTwoSquares(GameBoard board, GameBoard.MinMax child_type, Colour player_colour, int x, int y, 
            int a, int b, int c, int d, int moves)
        {
            //check boundary of array
            if (a >= 0 && a <= 3 && b >= 0 && b <= 3)
            {
                //check adjacent square is empty
                if (board.ReturnPosition(a, b).stones == 0 || board.ReturnPosition(a, b).colour == player_colour)
                {
                    if (c >= 0 && c <= 3 && d >= 0 && d <= 3)
                    {
                        if (board.ReturnPosition(c, d).stones == 0 || board.ReturnPosition(c, d).colour == player_colour)
                        {
                            //new child
                            GameBoard child = new GameBoard(child_type, board);

                            int stones = child.ReturnPosition(x, y).stones;
                            child.SetStones(x, y, 0);

                            int original_stones = child.ReturnPosition(a, b).stones;
                            child.SetStones(a, b, original_stones + 1); //first square
                            stones--;

                            original_stones = child.ReturnPosition(c, d).stones;
                            child.SetStones(c, d, stones + original_stones); //second square

                            //add child to parent
                            board.AddChildren(child);
                            moves++;
                        }
                    }
                    else
                    {
                        moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);
                    }
                }
            }
            return moves;
        }

        static int CheckThreeSquares(GameBoard board, GameBoard.MinMax child_type, Colour player_colour, int x, int y,
            int a, int b, int c, int d, int e, int f, int moves)
        {
            //check boundary of array
            if (a >= 0 && a <= 3 && b >= 0 && b <= 3)
            {
                //check adjacent square is empty
                if (board.ReturnPosition(a, b).stones == 0 || board.ReturnPosition(a, b).colour == player_colour)
                {
                    if (c >= 0 && c <= 3 && d >= 0 && d <= 3)
                    {
                        if (board.ReturnPosition(c, d).stones == 0 || board.ReturnPosition(c, d).colour == player_colour)
                        {
                            if (e >= 0 && e <= 3 && f >= 0 && f <= 3)
                            {
                                if (board.ReturnPosition(e, f).stones == 0 || board.ReturnPosition(e, f).colour == player_colour)
                                {

                                    //new child
                                    GameBoard child = new GameBoard(child_type, board);

                                    int stones = child.ReturnPosition(x, y).stones;
                                    child.SetStones(x, y, 0);

                                    int original_stones = child.ReturnPosition(a, b).stones;
                                    child.SetStones(a, b, original_stones + 1); //first square
                                    stones--;

                                    original_stones = child.ReturnPosition(c, d).stones;
                                    child.SetStones(c, d, stones + original_stones); //second square
                                    stones = stones - 2;

                                    original_stones = child.ReturnPosition(e, f).stones;
                                    child.SetStones(e, f, stones + original_stones); //third square

                                    //add child to parent
                                    board.AddChildren(child);
                                    moves++;
                                }
                                else //only 2 square moves possible
                                {
                                    moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);
                                }
                            }
                        }
                        else //only 1 square move possible
                        {
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);
                        }
                    }
                }
            }
            return moves;
        }

            
    }

}

