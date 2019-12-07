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


public enum Value
{
    pyramid = 2,
    anubis = 4,
    pharoah = 100,
    sphinx = 0,
    scarab = 0,
    empty = 0
};

public enum Rotation
{
    up = 1,
    down = 3,
    left = 4, 
    right = 2,
    reverse = -1
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
    public Value value;

    public Piece(Rotation rotation, Color color, Type type, Value value)
    {
        this.value = value;
        this.rotation = rotation;
        this.color = color;
        this.type = type;
    }

    public static Piece Clone(Piece x)
    {
        return new Piece(x.rotation, x.color, x.type,x.value);
    }
}
