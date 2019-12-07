using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    Piece[,] data = new Piece[8, 10];


    bool CanMove(int currX, int currY, int newX, int newY)
    {

        if (currX < 0 || currX > 7 || currY < 0 || currY > 9)
            return false;

        if (newX < 0 || newY > 7 || newX < 0 || newY > 9)
            return false;

        if (this[currX, currY].type == Type.sphinx)
            return false;



        else if (this[currX,currY].type == Type.scarab)
        {
            if (this[newX, newY].type == Type.scarab)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (this[newX, newY].type == Type.empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool MakeMove(int currX, int currY, int newX, int newY)
    {
        if (CanMove(currX, currY, newX, newY))
        {
            Piece replaced = Piece.Clone(this[currX, currY]);
            this[currX, currY] = Piece.Clone(this[newX, newY]);
            this[newX, newY] = replaced;
            return true;
        }
        return false;
    }

    public Piece this[int x, int y]
    {
        get => data[x, y];
        set => data[x, y] = value;
    }



    public override int GetHashCode()
    {
        return base.GetHashCode();
    }



}
