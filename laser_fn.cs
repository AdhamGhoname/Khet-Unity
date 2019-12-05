using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laser_not_final
{
    class laser_fn
    {
        static void Main(string[] args)
        {

            string[,] board = new string[8, 10];
            int[,] pieces = new int[8, 10];
            string[,] laser = new string[8, 10];
            string[,] name = new string[8, 10];


            board[0, 2] = "RA";
            board[0, 3] = "RPh";
            board[0, 4] = "RA";
            board[2, 5] = "RPy";
            board[2, 6] = "RPy";
            board[3, 5] = "RPy";
            board[5, 1] = "WPy";
            board[5, 2] = "WPy";
            board[5, 5] = "WPy";
            board[6, 1] = "WPy";
            board[6, 2] = "WPy";
            board[6, 5] = "WPy";
            board[7, 3] = "WA";
            board[7, 4] = "WPh";

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    laser[i, j] = " ";
                    pieces[i, j] = 0;
                }
            }


              pieces[4, 9] = 3;
              pieces[6, 8] = 4;
              pieces[2, 6] = 3;
              pieces[4, 6] = 1;
              pieces[2, 4] = 2;
              pieces[6, 4] = 1;
            
            name[4, 9] = "Py";
            name[6, 8] = "Py";
            name[2, 6] = "S";
            name[4, 6] = "Py";
            name[2, 4] = "Py";
            name[6, 4] = "S";

            /*  pieces[5, 5] = 4;
                name[5, 5] = "Py";*/

            Laser(pieces, laser , name);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(laser[i, j] + ' ');
                }
                Console.WriteLine();
            }

           
            /*  int x = CheckBoard(board);
              Console.WriteLine(x);*/
        }




        static int CheckBoard(string[,] board)
        {

            int my_board = 0;
            int opp_board = 0;


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (board[i, j] == "WA")
                    {
                        opp_board += 4;
                    }
                    else if (board[i, j] == "WPy")
                    {
                        opp_board += 2;
                    }
                    else if (board[i, j] == "WPh")
                    {
                        opp_board += 100;
                    }


                    else if (board[i, j] == "RA")
                    {
                        my_board += 4;
                    }
                    else if (board[i, j] == "RPy")
                    {
                        my_board += 2;
                    }
                    else if (board[i, j] == "RPh")
                    {
                        my_board += 100;
                    }
                }
            }

            Console.WriteLine(my_board);
            Console.WriteLine(opp_board);

            return my_board - opp_board;
        }
        static void Laser(int[,] pieces, string[,] laser , string[,] name)
        {


            int i;
            int j;
            i = 7;
            j = 9;
            int LaserDir = 1;
            bool stop = false;

            while (i >= 0 && j >= 0 && i <= 7 && j <= 9)
            {
                
                
                if (stop == true)
                {
                    break;
                }
                laser[i, j] = "*";

                if (pieces[i, j] != 0)
                {
                
                    
                    switch(name[i,j])
                    {
                        case "S":
                            {
                                rotate_laser( ref LaserDir,  pieces,  i, j); 
                                break;
                            }
                            

                        case "Py":
                            {
                                if (pieces[i, j] != LaserDir && (pieces[i, j]%4) +1 != LaserDir)
                                {
                                    rotate_laser(ref LaserDir,pieces, i, j);
                                }

                                else
                                {
                                    stop = true;
                                }
                                break;
                            }

                        default:
                            {
                                stop = true;
                                break;
                            }
                    }
                    
                }

                new_laser_pos( LaserDir, ref i, ref j);

            }


        }
        static void new_laser_pos( int LaserDir , ref int i , ref int j)
        {
            switch (LaserDir)
            {
                case 1:
                    {
                        --i;
                        break;
                    }
                case 2:
                    {
                        ++j;
                        break;
                    }
                case 3:
                    {
                        ++i;
                        break;
                    }
                case 4:
                    {
                        --j;
                        break;
                    }
            }


           
        }
        static void rotate_laser(ref int LaserDir,int[,] pieces, int i,int j)
        {

            if (pieces[i, j] == 3 || pieces[i, j] == 1)
            {
                pieces[i, j] = 1;
            }
            else pieces[i, j] = -1;
            {
                LaserDir = ((LaserDir * 3) % 4) + pieces[i, j];
                if (LaserDir == 0) LaserDir = 4;
                else if (LaserDir == -1) LaserDir = 3;

            }


        }
    }

}

