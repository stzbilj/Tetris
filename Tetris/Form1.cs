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
        private Label[,] labelArray;
        private TetrisField tField;
        private MovingObject mObject;
        private TetrisObject[] listOfObjects;
        bool mObjectExists = false;
        int br = 0;

        public Form1()
        {
            labelArray = new Label[20, 10];
            this.CreateGrid();
            tField = new TetrisField(ref labelArray);
            
            this.BackColor = Color.Aqua;
            this.Size = new Size(30 * 20, 30 * 30);
            InitializeComponent();
            timer1.Enabled = true;
            
            //Tetris objects in a game, player can add new objects
            int[,] objekt1 = new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { 0, 0, 0 } };
            TetrisObject tObject1 = new TetrisObject(objekt1);

            int[,] objekt2 = new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 0 } };
            TetrisObject tObject2 = new TetrisObject(objekt2);

            int[,] objekt3 = new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } };
            TetrisObject tObject3 = new TetrisObject(objekt3);

            int[,] objekt4 = new int[,] { { 1, 1, 0 }, { 1, 1, 0 }, { 0, 0, 0 } };
            TetrisObject tObject4 = new TetrisObject(objekt4);

            int[,] objekt5 = new int[,] { { 1, 1, 0 }, { 0, 1, 1 }, { 0, 0, 0 } };
            TetrisObject tObject5 = new TetrisObject(objekt5);

            int[,] objekt6 = new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
            TetrisObject tObject6 = new TetrisObject(objekt6);

            int[,] objekt7 = new int[,] { { 0, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
            TetrisObject tObject7 = new TetrisObject(objekt7);

            listOfObjects = new TetrisObject[7] { tObject1,tObject2,tObject3,tObject4,tObject5,tObject6,tObject7};



        }

        private void CreateGrid()
        {
            for (int i = 0; i < labelArray.GetLength(0); i++)
                for (int j = 0; j < labelArray.GetLength(1); j++)
                {
                    labelArray[i, j] = new Label();
                    labelArray[i, j].Size = new Size(30, 30);
                    labelArray[i, j].Location = new Point(31 * j + 1, 31 * i + 1);
                    this.Controls.Add(labelArray[i, j]);
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
            
            //Ovo je jako glupo ali trenutno nemam bolju ideju
            Random rnd = new Random();
            int objBroj;
            if(!mObjectExists)
            {
                objBroj = rnd.Next(0, 7);
                mObject = new MovingObject(tField, listOfObjects[objBroj]);
                mObjectExists = true;
            }
            if (mObject.mObjectExist())
            {
                mObject.MoveDown();             
            }
            else
            {
                objBroj = rnd.Next(0, 7);
                mObject = new MovingObject(tField, listOfObjects[objBroj]);
            }
            

        }
    }

}
