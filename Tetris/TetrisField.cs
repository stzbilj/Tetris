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
                    if (numB != 0)
                    {
                        for (int k = 0; k < labelArray.GetLength(1); ++k)
                        {
                            if (this[i, k] == Color.Black)
                            {
                                this[i, k] = Color.Gray;
                            }
                        }
                        bonus++;
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
                                if (this[j, k] != Color.Yellow && this[j, k] != Color.Black)
                                    this[j, k] = Color.DarkBlue;
                            }
                        }
                        break;
                    }
                    for (int j = 0; j < labelArray.GetLength(1); ++j)
                    {
                        if (this[i, j] == Color.Black)
                        {
                            blackColumns.Add(j);
                            Console.WriteLine("BLACK POINT at " + i + " " + j);
                            continue;
                            //MessageBox.Show("Column" + j + " added to blackColumns!");
                        }
                        else if (!blackColumns.Contains(j))
                        {
                            Console.WriteLine("NonblackColumns: " + i + " " + j);
                            if (this[i, j] != Color.Yellow)
                                this[i + num, j] = this[i, j];

                            else
                                this[i + num, j] = Color.Gray;
                        }
                        else
                        {   // case when row above black label is full
                            int l = 0;
                            for( l = 0; l < num; ++l)
                            {
                                if (this[i + l, j] == Color.Black)
                                    break;
                            }

                            if(l < num)
                            {
                                if (this[i, j] != Color.Yellow)
                                    this[i + num, j] = this[i, j];

                                else
                                    this[i + num, j] = Color.Gray;
                            }
                            //Console.WriteLine("blackColumns: " + i + " " + j);
                        }

                        /*if (this[i, j] != Color.Yellow && this[i, j] != Color.Black && this[i + num, j] != Color.Black)
                            this[i + num, j] = this[i, j];
                        else if(this[i, j] == Color.Black || this[i + num, j] == Color.Black)
                        {
                            if (this[i, j] == Color.Black && this[i + num, j] != Color.Black)
                            {
                                this[i, j] = Color.Black;
                            }
                            else if(this[i, j] != Color.Black && this[i + num, j] == Color.Black)
                            {
                                if(CountBlack(i+num) + CountGold(i + num) + CountGray(i + num) != labelArray.GetLength(1))
                                    this[i + num, j] = Color.Black;
                                else
                                {
                                    this[i + num, j] = this[i, j];
                                }
                            }
                            else
                            {
                                if (CountBlack(i + num) + CountGold(i + num) + CountGray(i + num) != labelArray.GetLength(1))
                                    this[i + num, j] = Color.Black;
                                else
                                {
                                    this[i + num, j] = this[i, j];
                                }
                                this[i, j] = Color.Black;
                            }
                        }
                        else
                            this[i + num, j] = Color.Gray;
                    }*/
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
