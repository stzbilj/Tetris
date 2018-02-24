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
        //DarkBlue empty, Gray block
        private Label[,] labelArray;

        public TetrisField(ref Label[,] labels)
        {
            labelArray = labels;
            for (int i = 0; i < labelArray.GetLength(0); ++i)
            {
                for (int j = 0; j < labelArray.GetLength(1); ++j)
                {
                    labelArray[i, j].BackColor = Color.DarkBlue;
                }
            }
    }

        public Color this[int i, int j] {
            get {
                if (i >= 0 && i < labelArray.GetLength(0) && j >= 0 && j < labelArray.GetLength(1))
                    return labelArray[i, j].BackColor;
                throw new IndexOutOfRangeException();
            }
            set {
                if(i >= 0 && i < labelArray.GetLength(0) && j >= 0 && j < labelArray.GetLength(1))
                    labelArray[i, j].BackColor = value;
            }
        }

        private bool IsRowFull(int row)
        {
            for (int i = 0; i < labelArray.GetLength(1); ++i)
            {
                if (this[row, i] == Color.DarkBlue)
                    return false;
            }
            return true;
        }

        private int CountBlack(int row)
        {
            int br = 0;
            for (int i = 0; i < labelArray.GetLength(1); ++i)
            {
                if (this[row, i] == Color.Black)
                    br++;
            }
            return br;
        }

        private int CountGold(int row)
        {
            int br = 0;
            for (int i = 0; i < labelArray.GetLength(1); ++i)
            {
                if (this[row, i] == Color.Gold)
                    br++;
            }
            return br;
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
            foreach (Point p in tObject)
            {
                this[p.X + row, p.Y + column] = Color.Gray;
            }
        }

        //@return int (helps to calculate score)
        public Tuple<int, int> ClearFullRows()
        {
            int num = 0;
            int bonus = 0;
            int[] numAfterBlack = new int[labelArray.GetLength(1)];
            int numG;
            int numB;
            int numGold;
            List<int> blackColumns = new List<int>();
            for (int i = labelArray.GetLength(0) - 1; i >= 0; i--)
            {
                numG = this.CountGray(i);
                numB = this.CountBlack(i);
                numGold = this.CountGold(i);
                if (numG + numB + numGold == labelArray.GetLength(1))
                {
                    num++;
                    if (numB != 0 || numGold != 0)
                    {
                        for (int k = 0; k < labelArray.GetLength(1); ++k)
                        {
                            if (this[i, k] == Color.Black || this[i, k] == Color.Gold)
                            {
                                this[i, k] = Color.Gray;
                                //numAfterBlack[j] = 0;
                            }
                        }
                        bonus++;
                    }

                    for (int k = 0; k < numAfterBlack.Length; ++k)
                    {
                        if (blackColumns.Contains(k))
                            numAfterBlack[k] += 1;
                    }
                }
                else
                {
                    if (numG + numB + numGold == 0)
                    {
                        for (int j = i + 1; j <= i + num; j++)
                        {
                            for (int k = 0; k < labelArray.GetLength(1); ++k)
                            {
                                if (!blackColumns.Contains(k))
                                {
                                    if (this[j, k] != Color.Yellow && this[j, k] != Color.Black)
                                        this[j, k] = Color.DarkBlue;
                                }
                            }
                        }
                        break;
                    }
                    for (int j = 0; j < labelArray.GetLength(1); ++j)
                    {
                        int tempNum;
                        if (this[i, j] == Color.Black)
                        {
                            blackColumns.Add(j);

                            if (numAfterBlack[j] == 0)
                                tempNum = num;
                            else
                                tempNum = numAfterBlack[j];

                            // now we need to replace those blocks under the black block because
                            // the above blocks can not do that
                            for(int m = 1; m <= tempNum; ++m)
                            {
                                this[i + m, j] = Color.DarkBlue;
                            }
                                //MessageBox.Show("Column" + j + " added to blackColumns!");
                            numAfterBlack[j] = 0;
                        }
                        else if (!blackColumns.Contains(j))
                        {
                            if (this[i, j] != Color.Yellow && this[i, j] != Color.Gold)
                            {
                                this[i + num, j] = this[i, j];
                            }
                            else if (this[i, j] == Color.Gold)
                                this[i + num, j] = Color.DarkBlue;
                            else
                                this[i + num, j] = Color.Gray;
                        }
                        else
                        {   // case when row above black label is full
                            int move = numAfterBlack[j];
                            if (move != 0)
                            {
                                if (this[i, j] != Color.Yellow && this[i, j] != Color.Gold)
                                {
                                    this[i + move, j] = this[i, j];
                                }
                                else if (this[i, j] == Color.Gold)
                                    this[i + move, j] = Color.DarkBlue;
                                else
                                    this[i + move, j] = Color.Gray;

                                if (move != 0)
                                {
                                    this[i, j] = Color.DarkBlue;
                                }
                            }
                        }
                    }
                }
            }
            return new Tuple<int, int>(num, bonus);
        }

        public int Size(int dimension)
        {
            return labelArray.GetLength(dimension);
        }
    }
}
