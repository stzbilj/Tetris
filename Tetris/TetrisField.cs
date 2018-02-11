﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class TetrisField
    {
        //Red empty, Gray block
        private Label[,] labelArray;

        public TetrisField(ref Label[,] labels)
        {
            labelArray = labels;
            for (int i = 0; i < labelArray.GetLength(0); ++i)
            {
                for (int j = 0; j < labelArray.GetLength(1); ++j)
                {
                    labelArray[i, j].BackColor = Color.Red;
                }
            }
        }

        public Color this[int i, int j] {
            get {
                if (i > 0 && i < labelArray.GetLength(0) && j > 0 && j < labelArray.GetLength(1))
                    return labelArray[i, j].BackColor;
                throw new IndexOutOfRangeException();
            }
            set {
                if(i > 0 && i < labelArray.GetLength(0) && j > 0 && j < labelArray.GetLength(1))
                    labelArray[i, j].BackColor = value;
            }
        }

        private bool IsRowFull(int row)
        {
            for (int i = 0; i < labelArray.GetLength(1); ++i)
            {
                if (this[row, i] == Color.Red)
                    return false;
            }
            return true;
        }
        
        private int CountGray(int row)
        {
            int br = 0;
            for (int i = 0; i < labelArray.GetLength(1); ++i)
            {
                if (this[row, i] == Color.Gray)
                    br++;
            }
            return br;
        }
        
        public void PlaceObject(int row, int column, TetrisObject tObject)
        {
            for (int i = 0; i < tObject.Size(0); ++i) {
                for (int j = 0; j < tObject.Size(1); ++j) {
                    if (tObject.GetColor(row + i, column + j) == Color.Yellow)
                        this[row + i, column + j] = Color.Gray;
                }
            }
        }

        //@return int (helps to calculate score)
        public int ClearFullRows() {
            int num = 0;
            int numG;
            for (int i = labelArray.GetLength(0) - 1; i >= 0; i--) {
                numG = this.CountGray(i);
                if (numG == labelArray.GetLength(1)) {
                    num++;
                }
                else {
                    if (numG == 0) {
                        for (int j = i + 1; j <= i + num; j++) {
                            for (int k = 0; k < labelArray.GetLength(1); ++k) {
                                if(this[j, k] != Color.Yellow)
                                    this[j, k] = Color.Red;
                            }
                        }
                        break;
                    }
                    for (int j = 0; j < labelArray.GetLength(1); ++j) {
                        if (this[i, j] != Color.Yellow)
                            this[i + num, j] = this[i, j];
                        else
                            this[i + num, j] = Color.Gray;
                    }
                }
            }
            return num;
        }

        public int Size(int dimension)
        {
            return labelArray.GetLength(dimension);
        }
    }
}
