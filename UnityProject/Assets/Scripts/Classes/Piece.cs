using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Type
{
    sphinx = 1,
    pyramid = 2,
    scarab = 3,
    anubis = 4,
    pharoah = 5,
    empty = 0
};


public enum Rotation
{
    up = 0,
    down = 1,
    left = 2, 
    right = 3
}


public enum Color
{
    silver = 0,
    red = 1
}

public class Piece
{
    public Rotation rotation;
    public Color color;
    public Type type;

    public Piece(Rotation rotation, Color color, Type type)
    {
        this.rotation = rotation;
        this.color = color;
        this.type = type;
    }

    public static Piece Clone(Piece x)
    {
        return new Piece(x.rotation, x.color, x.type);
    }
}
