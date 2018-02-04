using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TetrisField
    {
        //0 empty, 1 block, 2 moving object 
        private int[,] matrix;

        public TetrisField()
        {
            matrix = new int[22, 10];
            //field is empty at start
            for (int i = 0; i < 22; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        private bool IsRowFull(int row)
        {
            for (int i = 0; i < 10; ++i)
            {
                if (matrix[row, i] == 0 || matrix[row, i] == 2)
                    return false;
            }
            return true;
        }

        public int GetType(int row, int column)
        {
            return matrix[row + 2, column];
        }


    }
}
