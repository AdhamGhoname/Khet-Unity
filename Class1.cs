using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
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
                    data[i, j] = new Piece(Rotation.up, color.red, Type.empty, Value.empty);
                }
            }
            data[9, 7] = new Piece(Rotation.up, color.silver, Type.sphinx, Value.sphinx);
            data[5, 7] = new Piece(Rotation.up, color.silver, Type.anubis, Value.anubis);
            data[4, 7] = new Piece(Rotation.up, color.silver, Type.pharoah, Value.pharoah);
            data[3, 7] = new Piece(Rotation.up, color.silver, Type.anubis, Value.anubis);
            data[2, 7] = new Piece(Rotation.left, color.silver, Type.pyramid, Value.pyramid);


            data[7, 6] = new Piece(Rotation.up, color.silver, Type.pyramid, Value.pyramid);

            data[9, 4] = new Piece(Rotation.down, color.silver, Type.pyramid, Value.pyramid);
            data[9, 3] = new Piece(Rotation.left, color.silver, Type.pyramid, Value.pyramid);


            data[7, 4] = new Piece(Rotation.up, color.red, Type.pyramid, Value.pyramid);
            data[7, 3] = new Piece(Rotation.right, color.red, Type.pyramid, Value.pyramid);



            data[6, 5] = new Piece(Rotation.left, color.red, Type.pyramid, Value.pyramid);

            data[5, 4] = new Piece(Rotation.up, color.silver, Type.scarab, Value.scarab);
            data[4, 4] = new Piece(Rotation.left, color.silver, Type.scarab, Value.scarab);

            data[5, 3] = new Piece(Rotation.left, color.red, Type.scarab, Value.scarab);
            data[4, 3] = new Piece(Rotation.up, color.red, Type.scarab, Value.scarab);


            data[3, 2] = new Piece(Rotation.right, color.silver, Type.pyramid, Value.pyramid);
            data[2, 1] = new Piece(Rotation.down, color.red, Type.pyramid, Value.pyramid);


            data[2, 4] = new Piece(Rotation.left, color.silver, Type.pyramid, Value.pyramid);
            data[2, 3] = new Piece(Rotation.down, color.silver, Type.pyramid, Value.pyramid);

            data[0, 4] = new Piece(Rotation.right, color.red, Type.pyramid, Value.pyramid);
            data[0, 3] = new Piece(Rotation.up, color.red, Type.pyramid, Value.pyramid);


            data[0, 0] = new Piece(Rotation.down, color.red, Type.sphinx, Value.sphinx);
            data[4, 0] = new Piece(Rotation.down, color.red, Type.anubis, Value.anubis);
            data[5, 0] = new Piece(Rotation.down, color.red, Type.pharoah, Value.pharoah);
            data[6, 0] = new Piece(Rotation.down, color.red, Type.anubis, Value.anubis);
            data[7, 0] = new Piece(Rotation.right, color.red, Type.pyramid, Value.pyramid);


        }

        public bool CanMove(int currX, int currY, int newX, int newY)
        {
            if (currX < 0 || currX > 9 || currY < 0 || currY > 7)
                return false;

            if (newX < 0 || newX > 9 || newY < 0 || newY > 7)
                return false;

            if (this[currX, currY].type == Type.sphinx)
                return false;

            if (Math.Abs(newX - currX) > 1 || Math.Abs(newY - currY) > 1)
                return false;

            if (this[currX, currY].color == color.red && newX == 9)
                return false;

            if (this[currX, currY].color == color.silver && newX == 0)
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

        public bool RotateLeft(int x, int y)
        {
            if (this[x, y].type == Type.sphinx && this[x, y].rotation != Rotation.up && this[x, y].rotation != Rotation.down)
                return false;
            this[x, y].rotation = (Rotation)(((int)this[x, y].rotation + 3) % 4);
            return true;
        }

        public bool RotateRight(int x, int y)
        {
            if (this[x, y].type == Type.sphinx && this[x, y].rotation != Rotation.left && this[x, y].rotation != Rotation.right)
                return false;
            this[x, y].rotation = (Rotation)(((int)this[x, y].rotation + 1) % 4);
            return true;
        }

        public Piece this[int x, int y]
        {
            get => data[x, y];
            set => data[x, y] = value;
        }

        public Board Clone()
        {
            Board clone = new Board();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    clone[i, j] = Piece.Clone(this[i, j]);
                }
            }
            return clone;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



    }
}
