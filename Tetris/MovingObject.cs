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

        public MovingObject(TetrisField _tField, TetrisObject _tObject)
        {
            tField = _tField;
            tObject = _tObject;
            row = 0;
            column = 4;
        }

        public bool IsObject(int _row, int _column) {
            if ( _row - row < 0 || _row - row >= tObject.Size(0) || _column - column < 0 || _column - column >= tObject.Size(1))
                return false;
            if (tObject.GetColor(_row - row, _column - column) == Color.Yellow)
                return true;
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
            set { tObject = value; }
        }

        //Move left, right, down and rotations are shown with this
        //Call after checking all conditions for move
        private void MoveObject()
        {
            int maxSize = tObject.Size(0);
            if (maxSize < tObject.Size(1))
            {
                maxSize = tObject.Size(1);
            }
            
            for (int i = row - 1; i < row + maxSize + 1; i++)
                for (int j = column - 1; j < column + maxSize + 1; j++)
                {
                    if (i >= 0 && i < tField.Size(0) && j >= 0 && j < tField.Size(1))
                    {
                        if (this.IsObject(i, j))
                        {
                            tField[i, j] = Color.Yellow;
                        }
                        else
                        {
                                if (tField[i, j] == Color.Yellow)
                                    tField[i, j] = Color.Red;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
        }
        public void MoveDown()
        {
            if(!CheckCollision(Position.DOWN))
            {
                this.MoveObject();
            }
            else
            {
                tField.PlaceObject(row, column, tObject);
            }
        }
        private bool CheckCollision(Position pos)
        {
            int newRow = row;
            int newColumn = column;
            TetrisObject newObject = new TetrisObject(tObject);
            bool rotation = false;
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
                    newObject.RotateLeft();
                    rotation = true;
                    break;
                case Position.ROTATER:
                    newObject.RotateRight();
                    rotation = true;
                    break;
            }
            if (rotation)
            {
                if (newRow + newObject.Size(0) > tField.Size(0))
                    return true;
                if (newColumn + newObject.Size(1) > tField.Size(1))
                {
                    newColumn = newColumn + newObject.Size(1) - newObject.Size(0);
                    if (newColumn < 0)
                        return true;
                    
                }
            }
            if (newRow + newObject.Size(0) > tField.Size(0) || newColumn < 0 || newColumn + newObject.Size(1) >= tField.Size(1) )
            {
                return true;
            }
            for (int i = newObject.Size(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < newObject.Size(1); j++) {
                    if(tField[newRow + i, newColumn + j] == Color.Gray && newObject.GetColor(i, j) == Color.Yellow)
                    {
                        return true;
                    }
                }
            }

            row = newRow;
            column = newColumn;
            return false;   
        }
    }
}
