using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TetrisObject:IEnumerable<Point>
    {   
        //Yellow moving object, Red empty field
        private int[, ] matrix;
        private List<List<Point>> rotationPoints;
        private int rotation;

        public TetrisObject(int[ , ] _matrix)
        {
            matrix = new int[3, 3];
            matrix = _matrix;
            this.Reposition();

            rotationPoints = new List<List<Point>>();
            List<Point> points;
            for(int k = 0; k < 4; ++k)
            {
                if (k != 0)
                    RotateMatrixRight();
                points = new List<Point>();
                Debug.WriteLine("Rotation: {0}", k);
                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        if (matrix[i, j] == 1) {
                            Debug.WriteLine("i: {0} j: {1}", i, j);
                            points.Add(new Point(i, j));
                        }
                    }
                }
                rotationPoints.Add(points);
            }
            rotation = 0;
        }

        public TetrisObject(TetrisObject tetrisObject)
        {
            matrix = tetrisObject.matrix;
            rotationPoints = tetrisObject.rotationPoints;
            rotation = tetrisObject.rotation;
        }

        // two object are equal if their matrices are equal (also when rotated)
        public static bool operator ==(TetrisObject a, TetrisObject b)
        {
            int i = 0;
            int j = 0;

            /*Console.WriteLine("a");
            for(i = 0; i < 3; ++i)
            {
                for (j = 0; j < 3; ++j)
                    Console.Write(a.matrix[i, j] + " ");
                Console.WriteLine();
            }*/

            //Console.WriteLine("b");
            for (i = 0; i < 3; ++i)
            {
                for (j = 0; j < 3; ++j)
                {
                    //Console.Write(b.matrix[i, j] + " ");
                    if (a.matrix[i, j] != b.matrix[i, j])
                        break;
                }

                if (j < 3)
                    break;
                //Console.WriteLine();
            }

            if (i == 3)
                return true;
            //Console.WriteLine();
            //Console.WriteLine();

            /*if (a.matrix == b.matrix)
                return true;*/

            for (int k = 0; k < 4; ++k)
            {
                a.RotateMatrixRight();
                //Console.WriteLine("Rotated " + k + ":");
                //Console.WriteLine("a");
                /*for (i = 0; i < 3; ++i)
                {
                    for (j = 0; j < 3; ++j)
                        Console.Write(a.matrix[i, j] + " ");
                    Console.WriteLine();
                }*/
                //Console.WriteLine("b");
                for (i = 0; i < 3; ++i)
                {
                    for (j = 0; j < 3; ++j)
                    {
                        //Console.Write(b.matrix[i, j] + " ");
                        if (a.matrix[i, j] != b.matrix[i, j])
                            break;
                    }

                    if (j < 3)
                        break;
                    //Console.WriteLine();
                }
                //Console.WriteLine();
                //Console.WriteLine();
                //if (a.matrix == b.matrix)
                if(i == 3)
                    return true;
            }

            return false;
        }

        public static bool operator !=(TetrisObject a, TetrisObject b)
        {
            if (a == b)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TetrisObject))
                return false;

            return this == (TetrisObject)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private bool IsNull(int dimension, int row) {
            for (int i = 0; i < 3; ++i) {
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
        
        //Transform matrix so neither 1. row or 1. column are Red
        public void Reposition()
        {
            //row
            while ( this.IsNull(0, 0) ) {
                for (int i = 0; i < 3; ++i) {
                    for (int j = 0; j < 2; ++j) {
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
                    for (int j = 0; j < 2; ++j)
                    {
                        matrix[j, i] = matrix[j + 1, i];
                    }
                    matrix[2, i] = 0;
                }
            }
        }

        public void Rotate(int i)
        {
            rotation = (rotation + i + 4) % 4;
            //Debug.WriteLine("{0}", rotation);

        }

        private void RotateMatrixRight()
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

        public IEnumerator<Point> GetEnumerator()
        {
            foreach (Point p in rotationPoints[rotation])
                yield return p;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
