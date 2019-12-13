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
        const int MaxLev = 4;
        static int[] bestChoice = new int[0];

        static int[] GetBestMove(Board board)
        {
            Minimax(board, MaxLev, false);
            return bestChoice;
        }
        static void Main()
        {
            Board board = new Board();
            //board[0, 3].type = Type.empty;
            //board[0, 3].value = Value.empty;

            int[] move = GetBestMove(board);
            Console.WriteLine(move.Length);
            foreach (int x in move)
            {
                Console.WriteLine(x);
            }
        }

  
        //turn = false for red.
        static public int Minimax(Board board, int numlev, bool turn)
        {

            if (numlev == 0)
            {
                int value = CheckBoard(board);

                return value;
            }
            if (turn)
            {
                int max = -999999999;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i,j].type != Type.empty && board[i,j].color == color.silver)
                        {
                            for (int a = -1; a < 2; a++)
                            {
                                for (int b = -1; b < 2; b++)
                                {
                                    //Try all rotations;
                                    if ((a == b && b == 0))
                                    {
                                        Board clboard1 = board.Clone();
                                        Board clboard2 = board.Clone();
                                        bool turnLeft = clboard1.RotateLeft(i, j);

                                        bool turnRight = clboard2.RotateRight(i, j);
                                        if (turnLeft)
                                        {
                                            Laser.shoot_laser(ref clboard1, (int)clboard1[9, 7].rotation, 9, 7);
                                            int rleft = Minimax(clboard1, numlev - 1, !turn);
                                            if (max < rleft)
                                            {
                                                max = rleft;
                                                if (numlev == MaxLev)
                                                {
                                                    int[] best = new int[3];
                                                    best[0] = i;
                                                    best[1] = j;
                                                    best[2] = -1;
                                                    bestChoice = best;
                                                }
                                            }
                                        }
                                        if (turnRight)
                                        {
                                            Laser.shoot_laser(ref clboard2, (int)clboard2[9, 7].rotation, 9, 7);
                                            int rright = Minimax(clboard2, numlev - 1, !turn);
                                            if (max < rright)
                                            {
                                                max = rright;
                                                if (numlev == MaxLev)
                                                {
                                                    int[] best = new int[3];
                                                    best[0] = i;
                                                    best[1] = j;
                                                    best[2] = 1;
                                                    bestChoice = best;
                                                }
                                            }
                                        }
                                    }
                                    //Try to move the piece at (i, j) by displacement of (a, b)
                                    else if (board.CanMove(i, j, i + a, j + b))
                                    {
                                        Board clboard = board.Clone();
                                        clboard.MakeMove(i, j, i + a, j + b);
                                        Laser.shoot_laser(ref clboard, (int)clboard[9, 7].rotation, 9, 7);


                                        if (numlev == MaxLev)
                                        {
                                            int val = Minimax(clboard, numlev - 1, !turn);
                                            if (max < val) 
                                            {
                                                max = val;
                                                int[] best = new int[4];
                                                best[0] = i;
                                                best[1] = j;
                                                best[2] = i + a;
                                                best[3] = j + b;
                                                bestChoice = best;
                                            }
                                        }
                                        else
                                        {
                                            max = Math.Max(max, Minimax(clboard, numlev-1, !turn));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return max;
            }
            else
            {
                int min = 999999999;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j].type != Type.empty && board[i, j].color == color.red)
                        {
                            for (int a = -1; a < 2; a++)
                            {
                                for (int b = -1; b < 2; b++)
                                {
                                    //Try all rotations;
                                    if ((a == b && b == 0))
                                    {
                                        Board clboard1 = board.Clone();
                                        Board clboard2 = board.Clone();
                                        bool turnLeft = clboard1.RotateLeft(i, j);
                                        bool turnRight = clboard2.RotateRight(i, j);
                                        if (turnLeft)
                                        {
                                            List<Tuple<int,int>> points = Laser.shoot_laser_path(ref clboard1, (int)clboard1[0, 0].rotation, 0, 0);
                                            int rleft = Minimax(clboard1, numlev - 1, !turn);
                                            if (min >= rleft)
                                            {
                                                min = rleft;
                                                if (numlev == MaxLev)
                                                {
                                                    Console.WriteLine(clboard1[0, 0].rotation);
                                                    Console.WriteLine(clboard1[4, 0].type);
                                                    Console.WriteLine(points[points.Count - 1]);
                                                    int[] best = new int[3];
                                                    best[0] = i;
                                                    best[1] = j;
                                                    best[2] = -1;
                                                    bestChoice = best;
                                                }
                                            }
                                        }
                                        if (turnRight)
                                        {
                                            Laser.shoot_laser(ref clboard2, (int)clboard2[0, 0].rotation, 0, 0);
                                            int rright = Minimax(clboard2, numlev - 1, !turn);
                                            if (min > rright)
                                            {
                                                min = rright;
                                                if (numlev == MaxLev)
                                                {
                                                    int[] best = new int[3];
                                                    best[0] = i;
                                                    best[1] = j;
                                                    best[2] = 1;
                                                    bestChoice = best;
                                                }
                                            }
                                        }
                                    }
                                    //Try to move the piece at (i, j) by displacement of (a, b)
                                    else if (board.CanMove(i, j, i + a, j + b))
                                    {
                                        Board clboard = board.Clone();
                                        clboard.MakeMove(i, j, i + a, j + b);
                                        Laser.shoot_laser(ref clboard, (int)clboard[0, 0].rotation, 0, 0);


                                        if (numlev == MaxLev)
                                        {
                                            int val = Minimax(clboard, numlev - 1, !turn);
                                            if (min > val)
                                            {
                                                min = val;
                                                int[] best = new int[4];
                                                best[0] = i;
                                                best[1] = j;
                                                best[2] = i + a;
                                                best[3] = j + b;
                                                bestChoice = best;
                                            }
                                        }
                                        else
                                        {
                                            min = Math.Min(min, Minimax(clboard, numlev - 1, !turn));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return min;
            }
        }
        static int CheckBoard(Board piece )
        {

            int my_board = 0;
            int opp_board = 0;


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (piece[i, j].color == color.silver && piece[i,j].type != Type.empty)
                    {
                        my_board += (int)piece[i, j].value;

                    }
                    if (piece[i, j].color == color.red && piece[i, j].type != Type.empty)
                    {
                        opp_board += (int)piece[i, j].value;

                    }

                }
            }

            return (my_board - opp_board);
        }

    }

}