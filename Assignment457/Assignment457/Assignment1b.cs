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
        GameBoard start_game = new GameBoard(GameBoard.MinMax.Max); //start with max node
        
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
            function alphabeta(node, depth, α, β, Player)         
            if  depth = 0 or node is a terminal node
                return the heuristic value of node
            if  Player = MaxPlayer
                for each child of node
                    α := max(α, alphabeta(child, depth-1, α, β, not(Player) ))     
                    if β ≤ α
                        break                             (* Beta cut-off *)
                return α
            else
                for each child of node
                    β := min(β, alphabeta(child, depth-1, α, β, not(Player) ))     
                    if β ≤ α
                        break                             (* Alpha cut-off *)
                return β 
                    */




        }

    }
}
