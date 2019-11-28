using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSALGOPROJECT
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int desx = 0;
            int desy = 0;
            int desr = 0;
            Piece[,] board = new Piece[8, 10];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                    if (board[i, j].type != 0)
                    {
                        int bestmove;
                        bestmove = Minimax(ref board,i, j, 4, true,board[i,j].rotation,ref desx,ref desy,ref desr);
                    }
            }
        }
        static public int Minimax(ref Piece[,] board, int i, int j, int numlev, bool turn, int rot,ref int desx,ref int desy,ref int desr)
        {
            
            if (numlev == 0)
            {
                return 0;// initial  value of board goodness
            } 
            if (turn)
            {
                int max = -999999999;
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if (!(a == b && b == 0) && (board[i + a, j + b].type == 0))
                        {
                            board[i + a, j + b] = board[i, j];
                            board[i, j].type = 0;
                            if (numlev == 4)
                            {
                                if (max < Minimax(ref board,i + a, j + b, numlev - 1, false, board[i + a, j + b].rotation,ref desx,ref desy,ref desr)){
                                    desr = board[i + a, j + b].rotation;
                                    desx = i + a;
                                    desy = i + b;
                                }
                            }
                            else
                            {
                                max = Math.Max(max, Minimax(ref board,i + a, j + b, numlev - 1, false, board[i + a, j + b].rotation,ref desx,ref desy,ref desr)); 
                            }
                            board[i, j] = board[i + a, j + b];
                            board[i + a, j + b].type = 0;
                        }
                        else if ((a == b && b == 0))
                        {
                            if (max < Minimax(ref board,i, j, numlev - 1, false, (board[i, j].rotation + 1) % 4,ref desx,ref desy,ref desr))
                            {
                                max = Minimax(ref board,i, j, numlev - 1, false, (board[i, j].rotation + 1) % 4, ref desx, ref desy, ref desr);
                                if(numlev == 4)
                                {
                                    desr = (board[i, j].rotation + 1) % 4;
                                    desx = i;
                                    desy = i;
                                }
                            }
                            else if (max < Minimax(ref board,i, j + b, numlev - 1, false, (board[i, j].rotation + 3) % 4, ref desx, ref desy, ref desr))
                            {
                                max = Minimax(ref board,i, j, numlev - 1, false, (board[i, j].rotation + 3) % 4, ref desx, ref desy, ref desr);
                                if (numlev == 4)
                                {
                                    desr = (board[i, j].rotation + 3) % 4;
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
                        if (!(a == b && b == 0) && (board[i + a, j + b].type == 0))
                        {
                            board[i + a, j + b] = board[i, j];
                            board[i, j].type = 0;
                            min = Math.Min(min, Minimax(ref board,i + a, j + b, numlev - 1, true, board[i + a, j + b].rotation, ref desx, ref desy, ref desr));
                            board[i, j] = board[i + a, j + b];
                            board[i + a, j + b].type = 0;
                        }
                        else if ((a == b && b == 0))
                        {
                            if (min < Minimax(ref board,i, j, numlev - 1, true, (board[i, j].rotation + 1) % 4, ref desx, ref desy, ref desr))
                            {
                                min = Minimax(ref board,i, j, numlev - 1, true, (board[i, j].rotation + 1) % 4, ref desx, ref desy, ref desr);
                            }
                            else if (min < Minimax(ref board,i, j + b, numlev - 1, true, (board[i, j].rotation + 3) % 4, ref desx, ref desy, ref desr))
                            {
                                min = Minimax(ref board,i, j, numlev - 1, true, (board[i, j].rotation + 3) % 4, ref desx, ref desy, ref desr);
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}