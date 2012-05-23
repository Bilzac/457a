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
        //GameBoard start_game = new GameBoard(GameBoard.MinMax.Max, null); //start with max node
        
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
    
    
                    */
            //GameBoard parent = new GameBoard(GameBoard.MinMax.Min, null); //player starts
            GameBoard parent = new GameBoard();
            parent.SetNodeType(GameBoard.MinMax.Min);
            int total_moves = 0; 

            //random game//
            parent.SetStones(0, 0, 10, Colour.WHITE);
            parent.SetStones(1, 0, 3, Colour.BLACK);
            parent.SetStones(0, 1, 3, Colour.BLACK);
            parent.SetStones(1, 1, 4, Colour.BLACK);
            parent.SetStones(3, 0, 0, Colour.NONE);
            parent.SetStones(0, 3, 0, Colour.NONE); 
            

            Console.WriteLine("Starting move:"); 
            displayBoard(parent); 

            bool turn = false; //turn 0 = player's turn(black,min), turn 1 = our turn(white,max)
            GameBoard next_move = null;

            while(CalculateMoves(parent) != 0 && total_moves < 10)
            {   
                if (turn == false)//their turn (BLACK)
                {                    
                    Console.WriteLine("Their Move / Player 1");
                    //next_move = CalculateBeta(parent);

                    //Random moves :(
                    //choose random child
                    next_move = parent.GetChildren().First();
                    parent.SetAlphaBetaValue(next_move.GetAlphaBetaValue());

                    turn = true; 
                }
                else //my move (WHITE)
                {                    
                    Console.WriteLine("My Move / Player 2");
                    next_move = CalculateAlpha(parent);
                    turn = false; 
                }

                
                displayBoard(next_move);
                parent = next_move;
                total_moves++; 
            }

            //WINNER
            if (turn == true)
            {
                Console.WriteLine("Player 1 wins");
            }
            else
            {
                Console.WriteLine("Computer wins");
            }
        }

        static GameBoard CalculateAlpha(GameBoard parent)
        {
            /* Alpha values are stored in the max nodes
             * Alpha value = max(children's Beta Values of min nodes)
             * Because we want to minimize opponent moves             * 
             */
            //     if(max's turn)
            //for each child
            //    score = alpha-beta(other player,child,alpha,beta)
            //    if score > alpha then alpha = score (we have found a better best move)
            //    if alpha >= beta then return alpha (cut off)
            //return alpha (this is our best move)   
            GameBoard best_child = parent.GetChildren().ElementAt(0); 

            foreach (GameBoard child in parent.GetChildren())
            {                
                if (CalculateMoves(child) >= parent.GetAlphaBetaValue()) //get max
                {
                    best_child = child;
                    parent.SetAlphaBetaValue(best_child.GetAlphaBetaValue());
                }
                Console.Write(child.GetAlphaBetaValue().ToString() + ", "); 
            }
            
            Console.WriteLine("Alpha Value: " + best_child.GetAlphaBetaValue().ToString()); 
            return best_child; 

        }

        static GameBoard CalculateBeta(GameBoard parent)
        {
            /* Beta values are stored in the min nodes
             * Beta value = min(alpha Values of max nodes)
             * Because we want to maximize our moves
             * 
             */

            //        else (min's turn)
            //for each child
            //    score = alpha-beta(other player,child,alpha,beta)
            //    if score < beta then beta = score (opponent has found a better worse move)
            //    if alpha >= beta then return beta (cut off)
            //return beta (this is the opponent's best move)
            GameBoard worst_child = parent.GetChildren().ElementAt(0) ;

            foreach (GameBoard child in parent.GetChildren())
            {               
                if (CalculateMoves(child) <= parent.GetAlphaBetaValue())
                {
                    worst_child = child;
                    parent.SetAlphaBetaValue(worst_child.GetAlphaBetaValue());
                }
                Console.Write(child.GetAlphaBetaValue().ToString() + ", "); 
            }
                     
            Console.WriteLine("Beta Value: " + worst_child.GetAlphaBetaValue().ToString()); 
            return worst_child; 
            
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
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //south x2
                            a = x;
                            b = y - 1;
                            c = x;
                            d = y - 2;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //eastx2
                            a = x - 1;
                            b = y;
                            c = x - 2;
                            d = y;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //westx2
                            a = x + 1;
                            b = y;
                            c = x + 2;
                            d = y;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //nex2
                            a = x + 1;
                            b = y - 1;
                            c = x + 2;
                            d = y - 2;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //nwx2
                            a = x - 1;
                            b = y - 1;
                            c = x - 2;
                            d = y - 2;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //se x2
                            a = x + 1;
                            b = y + 1;
                            c = x + 2;
                            d = y + 2;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);

                            //swx2
                            a = x - 1;
                            b = y + 1;
                            c = x - 2;
                            d = y + 2;
                            moves = CheckTwoSquares(board, child_type, player_colour, x, y, a, b, c, d, moves);
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
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //south x3
                            a = x;
                            b = y - 1;
                            c = x;
                            d = y - 2;
                            e = x;
                            f = y - 3;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //westx3
                            a = x - 1;
                            b = y;
                            c = x - 2;
                            d = y;
                            e = x - 3;
                            f = y;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //eastx3
                            a = x + 1;
                            b = y;
                            c = x + 2;
                            d = y;
                            e = x + 3;
                            f = y;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //nex3
                            a = x + 1;
                            b = y + 1;
                            c = x + 2;
                            d = y + 2;
                            e = x + 3;
                            f = y + 3;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //nwx3 //---------????
                            a = x - 1;
                            b = y + 1;
                            c = x - 2;
                            d = y + 2;
                            e = x - 3;
                            f = y + 3;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //se x3
                            a = x + 1;
                            b = y - 1;
                            c = x + 2;
                            d = y - 2;
                            e = x + 3;
                            f = y - 3;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);

                            //swx3
                            a = x - 1;
                            b = y - 1;
                            c = x - 2;
                            d = y - 2;
                            e = x - 3;
                            f = y - 3;
                            moves = CheckThreeSquares(board, child_type, player_colour, x, y, a, b, c, d, e, f, moves);
                           
                        }
                    }
                }
            }
            //store moves in parent
            board.SetAlphaBetaValue(moves); 
            return moves; 
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
                    child.SetStones(x, y, 0, player_colour);
                                        
                    int original_stones = child.ReturnPosition(a, b).stones; //adjacent square
                    child.SetStones(a, b, stones+original_stones, player_colour);

                    //add child to parent
                    board.AddChildren(child);
                    //displayBoard(child); 
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
                            child.SetStones(x, y, 0, player_colour);

                            int original_stones = child.ReturnPosition(a, b).stones;
                            child.SetStones(a, b, original_stones + 1, player_colour); //first square
                            stones--; //one stone moved into a,b

                            original_stones = child.ReturnPosition(c, d).stones; //rest of stones moved into c,d
                            child.SetStones(c, d, stones + original_stones, player_colour); //second square
                            
                            //add child to parent
                            board.AddChildren(child);
                            //displayBoard(child); 
                            moves++;
                        }else
                        {
                            moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);
                        }
                    }
                    else
                    {
                        moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);
                    }
                }else
                {
                    moves = CheckAdjacentMove(board, child_type, player_colour, x, y, a, b, moves);
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
                                    child.SetStones(x, y, 0, player_colour);

                                    int original_stones = child.ReturnPosition(a, b).stones;
                                    child.SetStones(a, b, original_stones + 1, player_colour); //first square
                                    stones--;

                                    original_stones = child.ReturnPosition(c, d).stones;
                                    child.SetStones(c, d, 2 + original_stones, player_colour); //second square
                                    stones = stones - 2;

                                    original_stones = child.ReturnPosition(e, f).stones;
                                    child.SetStones(e, f, stones + original_stones, player_colour); //third square

                                    //add child to parent
                                    board.AddChildren(child);
                                    //displayBoard(child); 
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

            return; 
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

