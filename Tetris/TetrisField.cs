using System;
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
        //Red empty, Grey block
        private Button[,] buttonArray;

        public TetrisField(ref Button[,] buttons)
        {
            buttonArray = buttons;
            for (int i = 0; i < buttonArray.GetLength(0); ++i)
            {
                for (int j = 0; j < buttonArray.GetLength(1); ++j)
                {
                    buttonArray[i, j].BackColor = Color.Red;
                }
            }
        }

        public Color this[int i, int j] {
            get { return buttonArray[i, j].BackColor; }
            set { buttonArray[i, j].BackColor = value; }
        }

        private bool IsRowFull(int row)
        {
            for (int i = 0; i < buttonArray.GetLength(1); ++i)
            {
                if (this[row, i] == Color.Red)
                    return false;
            }
            return true;
        }
        
        private int CountGray(int row)
        {
            int br = 0;
            for (int i = 0; i < buttonArray.GetLength(1); ++i)
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
            for (int i = buttonArray.GetLength(0) - 1; i >= 0; i--) {
                numG = this.CountGray(i);
                if (numG == buttonArray.GetLength(1)) {
                    num++;
                }
                else {
                    if (numG == 0) {
                        for (int j = i + 1; j <= i + num; j++) {
                            for (int k = 0; k < buttonArray.GetLength(1); ++k) {
                                if(this[j, k] != Color.Yellow)
                                    this[j, k] = Color.Red;
                            }
                        }
                        break;
                    }
                    for (int j = 0; j < buttonArray.GetLength(1); ++j) {
                        if (this[i, j] != Color.Yellow)
                            this[i + num, j] = this[i, j];
                        else
                            this[i + num, j] = Color.Gray;
                    }
                }
            }
            return num;
        }

    }
}
