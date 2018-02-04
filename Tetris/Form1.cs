using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        //ubaciti timer i funkciju koja reagira na event
        private Button[,] buttonArray;
        private TetrisField tField;

        public Form1()
        {
            buttonArray = new Button[20, 10];
            tField = new TetrisField();

            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++)
                {
                    buttonArray[i, j] = new Button();
                    buttonArray[i, j].Size = new Size(30, 30);
                    buttonArray[i, j].Location = new Point(31*j + 1, 31*i + 1);
                    buttonArray[i, j].Enabled = false;
                    //color will depend on Teteris field
                    switch (tField.GetType(i, j))
                    {
                        case 0:
                            buttonArray[i, j].BackColor = Color.Red;
                            break;
                        case 1:
                            buttonArray[i, j].BackColor = Color.Blue;
                            break;
                        case 2:
                            buttonArray[i, j].BackColor = Color.Yellow;
                            break;
                        default:
                            buttonArray[i, j].BackColor = Color.Black;
                            break;

                    }
                    buttonArray[i, j].TabStop = false;
                    buttonArray[i, j].FlatStyle = FlatStyle.Flat;
                    buttonArray[i, j].FlatAppearance.BorderSize = 0;
                    this.Controls.Add(buttonArray[i, j]);
                }
            this.BackColor = Color.Aqua;
            this.Size = new Size(30 * 20, 30 * 30);
            InitializeComponent();
        }

        
    }

    

    /*public class TetrisObject
    {
        private int[,] tObject;
        
    }*/
}
