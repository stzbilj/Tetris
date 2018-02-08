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

        //colision functions
    }
}
