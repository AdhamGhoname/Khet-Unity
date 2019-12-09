﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    Piece[,] data;


    public Board()
    {
        data = new Piece[10, 8];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                data[i, j] = new Piece(Rotation.up, color.red, Type.empty);
            }
        }
        data[9, 7] = new Piece(Rotation.up, color.silver, Type.sphinx);
        data[5, 7] = new Piece(Rotation.up, color.silver, Type.anubis);
        data[4, 7] = new Piece(Rotation.up, color.silver, Type.pharoah);
        data[3, 7] = new Piece(Rotation.up, color.silver, Type.anubis);
        data[2, 7] = new Piece(Rotation.up, color.silver, Type.pyramid);
        data[7, 6] = new Piece(Rotation.right, color.silver, Type.pyramid);

        data[9, 4] = new Piece(Rotation.left, color.silver, Type.pyramid);
        data[9, 5] = new Piece(Rotation.up, color.silver, Type.pyramid);

        data[8, 4] = new Piece(Rotation.right, color.red, Type.pyramid);
        data[8, 5] = new Piece(Rotation.down, color.red, Type.pyramid);

        data[6, 5] = new Piece(Rotation.down, color.red, Type.pyramid);

        data[5, 4] = new Piece(Rotation.up, color.silver, Type.scarab);
        data[4, 4] = new Piece(Rotation.up, color.silver, Type.scarab);

        data[5, 3] = new Piece(Rotation.down, color.red, Type.scarab);
        data[4, 3] = new Piece(Rotation.down, color.red, Type.scarab);

        data[3, 2] = new Piece(Rotation.up, color.silver, Type.pyramid);
        data[2, 1] = new Piece(Rotation.left, color.red, Type.pyramid);


        data[2, 4] = new Piece(Rotation.up, color.silver, Type.pyramid);
        data[2, 3] = new Piece(Rotation.right, color.silver, Type.pyramid);

        data[0, 4] = new Piece(Rotation.down, color.red, Type.pyramid);
        data[0, 3] = new Piece(Rotation.right, color.red, Type.pyramid);


        data[0, 0] = new Piece(Rotation.down, color.red, Type.sphinx);
        data[4, 0] = new Piece(Rotation.down, color.red, Type.anubis);
        data[5, 0] = new Piece(Rotation.down, color.red, Type.pharoah);
        data[6, 0] = new Piece(Rotation.down, color.red, Type.anubis);
        data[7, 0] = new Piece(Rotation.down, color.red, Type.pyramid);
        

    }

    bool CanMove(int currX, int currY, int newX, int newY)
    {

        if (currX < 0 || currX > 9 || currY < 0 || currY > 7)
            return false;

        if (newX < 0 || newY > 9 || newX < 0 || newY > 7)
            return false;

        if (this[currX, currY].type == Type.sphinx)
            return false;

        if (Mathf.Abs(newX - currX) > 1 || Mathf.Abs(newX - currX) > 1)
            return false;



        else if (this[currX, currY].type == Type.scarab)
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
                Debug.Log(this[newX, newY].type.ToString());
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

    public void RotateLeft(int x, int y)
    {
        data[x, y].rotation = (Rotation)(((int)data[x, y].rotation + 3) % 4);
    }

    public void RotateRight(int x, int y)
    {
        data[x, y].rotation = (Rotation)(((int)data[x, y].rotation + 1) % 4);
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
