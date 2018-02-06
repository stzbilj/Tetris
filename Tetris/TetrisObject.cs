using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TetrisObject
    {
        private int[, ] matrix;

        public TetrisObject(int[ , ] _matrix)
        {
            matrix = new int[3, 3];
            if (_matrix.GetLength(0) == 3 && _matrix.GetLength(0) == 3) {
                for (int i = 0; i < 3; ++i)
                    for (int j = 0; j < 3; ++j)
                        matrix[i, j] = _matrix[i, j];
            }
        }

        private bool IsNull(int dimension, int row) {
            for (int i = 1; i < 3; ++i) {
                if (dimension == 0 && matrix[i, row] != 0) {
                    return false;   
                }
                if (dimension == 1 && matrix[row, i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        
        //Transform matrix so neither 1. row or 1. column are 0
        public void Reposition()
        {
            //row
            while ( this.IsNull(0, 0) ) {
                for (int i = 0; i < 3; ++i) {
                    for (int j = 0; i < 2; ++j) {
                        matrix[i, j] = matrix[i, j + 1];
                    }
                    matrix[i, 2] = 0;
                }
            }

            //columns
            while (this.IsNull(1, 0))
            {
                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; i < 2; ++j)
                    {
                        matrix[j, i] = matrix[j + 1, i];
                    }
                    matrix[2, i] = 0;
                }
            }
        }

        public void RotateLeft()
        {
            int temp;
            //elements in corners
            temp = matrix[0, 0];
            matrix[0, 0] = matrix[0, 2];
            matrix[0, 2] = matrix[2, 2];
            matrix[2, 2] = matrix[2, 0];
            matrix[2, 0] = temp;
            //others
            temp = matrix[0, 1];
            matrix[0, 1] = matrix[1, 2];
            matrix[1, 2] = matrix[2, 1];
            matrix[2, 1] = matrix[1, 0];
            matrix[1, 0] = temp;

            this.Reposition();
        }

        public void RotateRight()
        {
            int temp;
            //elements in corners
            temp = matrix[0, 0];
            matrix[0, 0] = matrix[2, 0];
            matrix[2, 0] = matrix[2, 2];
            matrix[2, 2] = matrix[0, 2];
            matrix[0, 2] = temp;
            //others
            temp = matrix[0, 1];
            matrix[0, 1] = matrix[1, 0];
            matrix[1, 0] = matrix[2, 1];
            matrix[2, 1] = matrix[1, 2];
            matrix[1, 2] = temp;

            this.Reposition();
        }

        public int Size(int dimension) {
            int dim = 0;
            for (int i = 2; i >= 0; --i) {
                if (!this.IsNull(dimension, i)) {
                    dim = i + 1;
                    break;
                }
            }
            return dim;
        }

        public int GetPosition(int i, int j) {
            return matrix[i, j];
        }
    }
}
