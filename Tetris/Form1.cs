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

        //Move left, right, down and rotations are shown with this
        //Call after checking all conditions for move
        private void ShowMoveOfObject()
        {

            for (int i = 0; i < mObject.Object.Size(0); i++)
                for (int j = 0; j < mObject.Object.Size(1); j++)
                {
                    if (mObject.IsObject(i, j))
                    {
                        buttonArray[i, j].BackColor = Color.Yellow;
                    }
                    else {
                        if (buttonArray[i, j].BackColor == Color.Yellow)
                            buttonArray[i, j].BackColor = Color.Red;
                    }
                }
            if (mObject.Row + mObject.Object.Size(0) < 10) {
                for(int i = 0; i < mObject.Object.Size(1); i++)
                    if (buttonArray[ mObject.Row + mObject.Object.Size(0), i].BackColor == Color.Yellow)
                        buttonArray[ mObject.Row + mObject.Object.Size(0), i].BackColor = Color.Red;
            }
            if (mObject.Row > 0)
            {
                for (int i = 0; i < mObject.Object.Size(1); i++)
                    if (buttonArray[mObject.Row - 1, i].BackColor == Color.Yellow)
                        buttonArray[mObject.Row - 1, i].BackColor = Color.Red;
            }

            if (mObject.Column > 0)
            {
                for (int i = 0; i < mObject.Object.Size(0); i++)
                    if (buttonArray[i, mObject.Column - 1].BackColor == Color.Yellow)
                        buttonArray[i, mObject.Column - 1].BackColor = Color.Red;
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
