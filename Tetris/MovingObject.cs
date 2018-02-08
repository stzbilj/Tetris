using System;
using System.Collections.Generic;
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
            column = 5;
        }

        public bool IsObject(int _row, int _column) {
            if(row > _row || row + tObject.Size(0) < _row || column >_column || column + tObject.Size(0) < _column)
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

            for (int i = 0; i < tObject.Size(0); i++)
                for (int j = 0; j < tObject.Size(1); j++)
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
            if (row + tObject.Size(0) < 10)
            {
                for (int i = 0; i < tObject.Size(1); i++)
                    if (tField[row + tObject.Size(0), i] == Color.Yellow)
                        tField[row + tObject.Size(0), i] = Color.Red;
            }
            if (row > 0)
            {
                for (int i = 0; i < tObject.Size(1); i++)
                    if (tField[row - 1, i] == Color.Yellow)
                        tField[row - 1, i] = Color.Red;
            }

            if (column > 0)
            {
                for (int i = 0; i < tObject.Size(0); i++)
                    if (tField[i, column - 1] == Color.Yellow)
                        tField[i, column - 1] = Color.Red;
            }
        }
        //colision functions
    }
}
