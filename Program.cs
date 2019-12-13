using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace ConsoleApp4
{
     
    class Program
    {
        static  int desx = 0;
         static int desy = 0;
        static int desr = 0;

        static void Main(string[] args)
        {

            Board board = new Board ();
            // 1 and 2 the notation of the best piece can move now , 3 4 and 5 which move or rotate is best foe this piece 
            Tuple<int, int, int , int , int > finaldesyrd = new Tuple<int, int, int , int , int >(-1 , -1 , -1  , -1 , -1 );
            int bestmove = 9999999 , r ;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                    if (board[i, j].type != Type.empty)
                    {
                        r  = Minimax( board, i, j, 4, true, (int)board[i, j].rotation);
                        if (r>bestmove)
                        {
                            finaldesyrd.Item1.Equals(i);
                            finaldesyrd.Item2.Equals(j);
                            finaldesyrd.Item3.Equals(desx);
                            finaldesyrd.Item4.Equals(desy);
                            finaldesyrd.Item5.Equals(desr);
                            bestmove = r;
                        }
                    }
            }
        }
        static public int Minimax(Board board , int i, int j, int numlev, bool turn, int rot)
        {

            if (numlev == 0)
            {
                return CheckBoard(board);
            }
            if (turn)
            {
                int max = -999999999;
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if (board.CanMove(i, j, i + a, i + b))
                        {
                            Board clboard = new Board.Clone(board);
                            clboard.MakeMove(i, j, i + a, i + b);

                            if (numlev == 4)
                            {

                                if (max < Minimax(clboard, i + a, j + b, numlev - 1, false, (int)clboard[i + a, j + b].rotation))
                                {
                                    desr = (int)board[i + a, j + b].rotation;
                                    desx = i + a;
                                    desy = i + b;
                                }
                            }
                            else
                            {
                                max = Math.Max(max, Minimax(clboard, i + a, j + b, numlev - 1, false, (int)clboard[i + a, j + b].rotation));
                            }
                        }
                        else if ((a == b && b == 0))
                        {
                            Board clboard = new Board.Clone(board);
                            int rright = Minimax(clboard, i, j, numlev - 1, false, (int)(clboard[i, j].rotation + 1) % 4);
                            int rleft = Minimax(clboard, i, j, numlev - 1, false, (int)(clboard[i, j].rotation + 3) % 4);
                            if (max < rright)
                            {
                                max = rright ;
                                if (numlev == 4)
                                {
                                    desr = (int)(board[i, j].rotation + 1) % 4;
                                    desx = i;
                                    desy = i;
                                }
                            }
                        else if (max < rleft)
                        {
                            max = rleft;
                            if (numlev == 4)
                            {
                                desr = (int)(board[i, j].rotation + 3) % 4;
                                desx = i;
                                desy = i;
                            }
                        }
                        }
                    }
                }
            }
            else
            {
                int min = 999999999;
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if (board.CanMove(i, j, i + a, i + b))
                        {
                            Board clboard = new Board.Clone(board);
                            clboard.MakeMove(i, j, i + a, i + b);
                            min = Math.Min(min, Minimax(clboard, i + a, j + b, numlev - 1, true, (int)clboard[i + a, j + b].rotation));
                        }
                        else if ((a == b && b == 0))
                        {
                            Board clboard = new Board.Clone(board);
                            int rright  = Minimax(clboard, i, j, numlev - 1, true, (int)(clboard[i, j].rotation + 1) % 4);
                            int rleft = Minimax(clboard, i, j, numlev - 1, true, (int)(clboard[i, j].rotation + 3) % 4);

                            if (min < rright)
                                min = rright;

                            else if (min < rleft)
                                min = rleft;
                            
                        }
                    }
                  
                }
            }
            return 0;
        }
        static int CheckBoard(Board piece )
        {

            int my_board = 0;
            int opp_board = 0;


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (piece[i, j].color == Color.red)
                    {
                        my_board += (int)piece[i, j].value;

                    }
                    if (piece[i, j].color == Color.silver)
                    {
                        opp_board += (int)piece[i, j].value;

                    }

                }
            }

            return my_board - opp_board;
        }

    }

}