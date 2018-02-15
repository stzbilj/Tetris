using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class MovingObject
    {
        private TetrisField tField;
        private TetrisObject tObject;
        private int row;
        private int column;
        public bool mObjectExists;

        public MovingObject(TetrisField _tField, TetrisObject _tObject)
        {
            tField = _tField;
            tObject = _tObject;
            row = 0;
            column = 4;
            mObjectExists = true;

            InitialDraw();
        }

        private bool InitialDraw()
        {
            if (row == 0 && column == 4 && CheckCollision(Position.SAME))
                return true;
            DrawObject();
            return false;
        }

        public int Row{
            get{ return row; }
        }

        public int Column
        {
            get { return column; }
        }

        public TetrisObject Object
        {
            get { return tObject; }
            set {
                tObject = value;
                if(InitialDraw())
                    throw new Exception();
            }
        }

        //Move left, right, down and rotations are shown with this
        //Call after checking all conditions for move
        private void ClearObject()
        {
            foreach (Point p in tObject)
            {
                tField[row + p.X, column + p.Y] = Color.Red;
            }
        }
        private void DrawObject() { 
            foreach (Point p in tObject)
            {
                tField[row + p.X, column + p.Y] = Color.Yellow;
            }
        }

        public void MoveToSide(Position pos)
        {
            if (!CheckCollision(pos))
            {
                this.ClearObject();
                if (pos == Position.LEFT)
                    column--;
                else
                    column++;
                this.DrawObject();

            }
        }
        public void MoveDown()
        {
            if(!CheckCollision(Position.DOWN))
            {
                this.ClearObject();
                row++;
                this.DrawObject();

            }
            else
            {
                tField.PlaceObject(row, column, tObject);
                tField.ClearFullRows();
                mObjectExists = false;
                row = 0;
                column = 4;
            }
        }
        public bool mObjectExist()
        {
            return mObjectExists;
        }
        public void Rotate(Position pos)
        {
            int moveToLeft = 1;
            int rotate = 0;
            if (pos == Position.ROTATEL)
                rotate--;
            else
                rotate++;
            if (!CheckCollision(pos))
            {  
                this.ClearObject();
                tObject.Rotate(rotate);
                this.DrawObject();
            }
            else
            {
                //Tu bi trebao ici dio kada ga udaljava od zida ili objekta, npr kada je I uz sam zid treba column pomaknuti za dva mjesta ulijevo
                //Razmisliti kako bi to trebalo raditi za sive blokove
            }
        }
        private bool CheckCollision(Position pos)
        {
            int newRow = row;
            int newColumn = column;
            TetrisObject tetrisObject = new TetrisObject(tObject);
            switch (pos)
            {
                case Position.DOWN:
                    newRow++;
                    break;
                case Position.LEFT:
                    newColumn--;
                    break;
                case Position.RIGHT:
                    newColumn++;
                    break;
                case Position.ROTATEL:
                    tetrisObject.Rotate(-1);
                    break;
                 case Position.ROTATER:
                    tetrisObject.Rotate(1);
                    break;
                default:
                    break;
            }
            foreach(Point p in tetrisObject)
            {
                if (p.X + newRow >= tField.Size(0) || newColumn < 0 || newColumn + p.Y >= tField.Size(1))
                    return true;
                if (tField[p.X + newRow, p.Y + newColumn] == Color.Gray)
                    return true;
            }
            
            return false;   
        }
    }
}
