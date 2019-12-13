using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
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
        public Value value;

        public Piece(Rotation rotation, color color, Type type, Value value)
        {
            this.rotation = rotation;
            this.color = color;
            this.type = type;
            this.value = value;
        }

        public static Piece Clone(Piece x)
        {
            return new Piece(x.rotation, x.color, x.type, x.value);
        }

        public static bool Compare(Piece a, Piece b)
        {
            if (a.rotation == b.rotation && a.value == b.value && a.color == b.color && a.type == b.type)
                return true;
            return false;
        }
    }

}
