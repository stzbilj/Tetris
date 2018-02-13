using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TetrisObject
    {   
        //Yellow moving object, Red empty field
        private Color[, ] matrix;
        private int X = 0;
        private int Y = 0;
        public TetrisObject(int[ , ] _matrix)
        {
            matrix = new Color[3, 3];
            if (_matrix.GetLength(0) == 3 && _matrix.GetLength(0) == 3) {
                for (int i = 0; i < 3; ++i) {
                    for (int j = 0; j < 3; ++j) {
                        if(_matrix[i, j] == 1)
                            matrix[i, j] = Color.Yellow;
                        else
                            matrix[i, j] = Color.Red;
                    }
                }
            }
            this.Reposition();
            
            for (int i = 2; i >= 0; --i)
            {
                if (!this.IsNull(0, i) && Y == 0)
                {
                    Y = i + 1;
                }
                if (!this.IsNull(1, i) && X == 0)
                {
                    X = i + 1;
                }
            }
            
        }

        public TetrisObject(TetrisObject tObject)
        {
            this.matrix = new Color[3, 3];

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    this.matrix[i, j] = tObject.matrix[i, j];
                }
            }

            this.X = tObject.X;
            this.Y = tObject.Y;

        }

        private bool IsNull(int dimension, int row) {
            for (int i = 0; i < 3; ++i) {
                if (dimension == 0 && matrix[i, row] != Color.Red) {
                    return false;   
                }
                if (dimension == 1 && matrix[row, i] != Color.Red)
                {
                    return false;
                }
            }
            return true;
        }
        
        //Transform matrix so neither 1. row or 1. column are Red
        public void Reposition()
        {
            //row
            while ( this.IsNull(0, 0) ) {
                for (int i = 0; i < 3; ++i) {
                    for (int j = 0; j < 2; ++j) {
                        matrix[i, j] = matrix[i, j + 1];
                    }
                    matrix[i, 2] = Color.Red;
                }
            }

            //columns
            while (this.IsNull(1, 0))
            {
                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        matrix[j, i] = matrix[j + 1, i];
                    }
                    matrix[2, i] = Color.Red;
                }
            }
        }

        public void RotateLeft()
        {
            Color temp;
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

            int t = X;
            X = Y;
            Y = t;
        }

        public void RotateRight()
        {
            Color temp;
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

            int t = X;
            X = Y;
            Y = t;
        }

        public int Size(int dimension) {
            if (dimension == 0) return X;
            if (dimension == 1) return Y;
            return 0;
        }

        public Color GetColor(int i, int j) {
            return matrix[i, j];
        }
    }
}
