using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public enum Type
    {
        sphinx=1,
        pyramid=2,
        scarab=3,
        anubis=4,
        pharoah=5,
        empty =0 ,
    };


    public class Piece
    {

        public char rotation;
        public char color;
        public Type type;

    }
  
}
