using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Laser
{


    static void shoot_laser(ref Board piece, int laser_direction, int i, int j)
    {

        while (i >= 0 && j >= 0 && i <= 7 && j <= 9)
        {

            if (piece[i, j].type != Type.empty)
            {


                switch (piece[i, j].type)
                {
                    case Type.scarab:
                        {
                            rotate_laser(ref laser_direction, piece, i, j);
                            break;
                        }


                    case Type.pyramid:
                        {
                            if ((int)piece[i, j].rotation != laser_direction && ((int)piece[i, j].rotation + 1 % 4) != laser_direction)
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

                            if (Math.Abs((int)piece[i, j].rotation - laser_direction) == 2)
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




    public static List<Tuple<int, int>> shoot_laser_path(ref Board piece, int laser_direction, int i, int j)
    {



        List<Tuple<int, int>> coordinates_list = new List<Tuple<int, int>>();
        bool stop = false;

        while (i >= 0 && j >= 0 && i <= 9 && j <= 7)
        {

            if (piece[i, j].type != Type.empty)
            {
                Debug.Log("Piece: " + piece[i, j].type + "i: " + i + " " + "j: " + j);
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
                            Debug.Log((int)piece[i, j].rotation + " " + laser_direction);
                            if ((int)piece[i, j].rotation != laser_direction && (((int)piece[i, j].rotation + 1) % 4) != laser_direction)
                            {
                                rotate_laser(ref laser_direction, piece, i, j);
                                Debug.Log("not  killing " + i + " " + j);

                            }

                            else
                            {
                                Debug.Log("killing " + i + " " + j);
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
        Debug.Log(stop);
        if (!stop)
        {
            coordinates_list.Add(new Tuple<int, int>(i, j));
        }
        return coordinates_list;

    }



    static void new_laser_pos(int laser_direction, ref int i, ref int j)
    {
        switch (laser_direction)
        {
            case 0:
                {
                    --j;
                    break;
                }
            case 1:
                {
                    ++i;
                    break;
                }
            case 2:
                {
                    ++j;
                    break;
                }
            case 3:
                {
                    --i;
                    break;
                }
        }



    }
    static void rotate_laser(ref int laser_direction, Board piece, int i, int j)
    {

        int rot;
        if (piece[i, j].rotation == Rotation.down || piece[i, j].rotation == Rotation.up)
        {

            rot = (int)Rotation.right;
        }
        else
            rot = (int)Rotation.reverse;


        if (laser_direction % 2 != 0)
        {
            laser_direction = (laser_direction % 4) + rot;
        }

        else laser_direction = (laser_direction % 4) - rot;

        if (laser_direction == 4) laser_direction = 0;
        else if (laser_direction == -1) laser_direction = 3;



    }
}
