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
    right = 1,
    down = 2,
    left = 3, 
    reverse = -1
}


public enum color
{
    silver = 0,
    red = 1
}

public class Piece
{
    public Rotation rotation;
    public color color;
    public Type type;

    public Piece(Rotation rotation, color color, Type type)
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
