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
        static void Main(string[] args)
        {

            Piece[,] board = new Piece[8, 10];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                    if (board[i, j].type !=0)
                    {
                        int bestmove;
                        var newpos = new Tuple<int, int>(0,0);
                        bestmove = Minimax(ref board, i, j, 4, true ,newpos ); 
                    }

            }
        }
       static public int Minimax(ref Piece[,] board, int i, int j, int numlev, bool turn ,Tuple<int,int> newpos)
        {
            if (numlev == 0)
            {
                return 0;// initial  value of board goodness
            }
            if (turn)
            {
                int max = -999999;
                for (int a = -1; a < 2; a++)
                {
                    for (int b=-1; b<2; b++)
                    {
                        if (!(a == b && b == 0) && (board[i+a, j+b].type == 0))
                        {
                            board[i + a, j + b] = board[i, j];
                            board[i, j].type = 0;
                           // if (numlev==4)
                            max = Math.Max(max, Minimax(ref board, i + a, j + b, numlev-1,false, newpos));
                            board[i, j] = board[i + a, j + b];
                            board[i+a, j+b].type = 0;

                        }

                    }
                }
            }
            else
            {
                int min = 200000;
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if (!(a ==b && b == 0) && (board[i + a, j + b].type ==0))
                        {
                            board[i + a, j + b] = board[i, j];
                            board[i, j].type = 0;
                            min = Math.Min(min, Minimax(ref board, i + a, j + b, numlev-1,true, newpos));
                            board[i, j] = board[i + a, j + b];
                            board[i + a, j + b].type = 0;
                        }

                    }
                }
            }
            return 0;
        }
    }


    
}