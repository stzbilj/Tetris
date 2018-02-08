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
        private MovingObject mObject;
        int br = 0;

        public Form1()
        {
            buttonArray = new Button[20, 10];
            this.CreateGrid();
            tField = new TetrisField(ref buttonArray);
            
            this.BackColor = Color.Aqua;
            this.Size = new Size(30 * 20, 30 * 30);
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void CreateGrid()
        {
            for (int i = 0; i < buttonArray.GetLength(0); i++)
                for (int j = 0; j < buttonArray.GetLength(1); j++)
                {
                    buttonArray[i, j] = new Button();
                    buttonArray[i, j].Size = new Size(30, 30);
                    buttonArray[i, j].Location = new Point(31 * j + 1, 31 * i + 1);
                    buttonArray[i, j].Enabled = false;
                    buttonArray[i, j].TabStop = false;
                    buttonArray[i, j].FlatStyle = FlatStyle.Flat;
                    buttonArray[i, j].FlatAppearance.BorderSize = 0;
                    this.Controls.Add(buttonArray[i, j]);
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Called perriodicly
            //We change period depending on score, but not here
            //It onley move object down
            //if(mObject.MoveDown())
                //this.ShowMoveOfTheObject
            //else
                //
                //change grid?
            

        }
    }

}
