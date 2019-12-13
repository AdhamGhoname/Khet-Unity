using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace laser_not_final
{
    class laser_fn
    {
        
        
        static void shoot_laser(ref Piece[,] piece, int laser_direction, int i, int j)
        {
            
            while (i >= 0 && j >= 0 && i <= 7 && j <= 9)
            {
                
                if (piece[i, j].type != Type.empty)
                {
                
                    
                    switch(piece[i,j].type)
                    {
                        case Type.scarab:
                            {
                                rotate_laser( ref laser_direction,  piece,  i, j); 
                                break;
                            }


                        case Type.pyramid:
                            {
                                if ((int)piece[i, j].rotation != laser_direction && ((int)piece[i, j].rotation+1 %4) != laser_direction)
                                {
                                    rotate_laser(ref laser_direction, piece, i, j);
                                }

                                else
                                {
                                    piece[i, j].type = Type.empty;
                                    return;
                                }
                                break;
                            }

                        case Type.anubis:
                            {

                                if(Math.Abs((int)piece[i, j].rotation - laser_direction) == 2)
                                {
                                    piece[i, j].type = Type.empty;
                                }
                                return;
                               
                            }

                        case Type.pharoah:
                            {

                                piece[i, j].type = Type.empty;
                                return;
                              
                            }

                        default:
                            {
                                break;
                            }
                    }
                    
                }

               
                new_laser_pos(laser_direction, ref i, ref j);

            }


        }




        static List<Tuple<int,int>> shoot_laser_path(ref Piece[,] piece, int laser_direction, int i, int j)
        {
            


            List<Tuple<int, int>> coordinates_list = new List<Tuple<int, int>>();
            bool stop = false;

            while (i >= 0 && j >= 0 && i <= 7 && j <= 9)
            {
                
                if (piece[i, j].type != Type.empty)
                {
                    var coordinates = Tuple.Create(i, j);
                    coordinates_list.Add(coordinates);

                    switch (piece[i, j].type)
                    {
                        case Type.scarab:


                            {
                                rotate_laser(ref laser_direction, piece, i, j);
                                break;
                            }


                        case Type.pyramid:
                            {
                                if ((int)piece[i, j].rotation != laser_direction && ((int)piece[i, j].rotation+1 % 4)  != laser_direction)
                                {
                                    rotate_laser(ref laser_direction, piece, i, j);
                                }

                                else
                                {
                                    piece[i, j].type = Type.empty;

                                    stop = true;
                                   
                                }
                                break;
                            }

                        case Type.anubis:
                            {
                                if (Math.Abs((int)piece[i, j].rotation - laser_direction) != 2)
                                {
                                    piece[i, j].type = Type.empty;
                                }
                                stop = true;
                                break;
                            }

                        case Type.pharoah:
                            {
                                piece[i, j].type = Type.empty;
                               stop = true;
                                    break;
                            }

                        default:
                            {
                                break;
                            }


                    }
                    
                }

                if (stop == true)
                {
                    break;
                }
                new_laser_pos(laser_direction, ref i, ref j);

            }
            return coordinates_list;

        }



        static void new_laser_pos( int laser_direction, ref int i , ref int j)
        {
            switch (laser_direction)
            {
                case 0:
                    {
                        --i;
                        break;
                    }
                case 1:
                    {
                        ++j;
                        break;
                    }
                case 2:
                    {
                        ++i;
                        break;
                    }
                case 3:
                    {
                        --j;
                        break;
                    }
            }


           
        }
        static void rotate_laser(ref int laser_direction,Piece[,] piece, int i,int j)
        {
           

            if ((int)piece[i, j].rotation == 2 || (int)piece[i, j].rotation == 0)
            {

                piece[i, j].rotation = Rotation.right;
            }
            else piece[i, j].rotation = Rotation.reverse;
            {
                if (laser_direction % 2 != 0)
                {
                    laser_direction = (laser_direction % 4) + (int)piece[i, j].rotation;
                }

                else laser_direction = (laser_direction % 4) -(int) piece[i, j].rotation;

                if (laser_direction == 4) laser_direction = 0;
                else if (laser_direction == -1) laser_direction= 3;

            }


        }
    }

}

