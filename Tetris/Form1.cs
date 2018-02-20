using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private Label[,] labelArray;
        private Label labelScore;
        private Label[,] labelArrayNext;
        private TetrisField tField;
        private MovingObject mObject;
        private TetrisObject[] listOfObjects;
        private GameScore game;

        public Form1()
        {
            SuspendLayout();
            labelArray = new Label[20, 10];
            labelScore = new Label();
            labelArrayNext = new Label[3, 3];
            this.CreateGrid();
            this.CreateHelp();
            tField = new TetrisField(ref labelArray);
            
            this.BackColor = Color.Aqua;
            InitializeComponent();
            game = new GameScore(ref timer1);
            labelScore.Text = game.Score.ToString();
            //Tetris objects in a game, player can add new objects
            //Stjepan: Forma ce ovo primati i prosljedivati tu listu MovingObject
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

            mObject = new MovingObject(tField, new TetrisObject(listOfObjects[GetRandomNumber()]), new TetrisObject(listOfObjects[GetRandomNumber()]), game);
            this.ClientSize = new Size(10*32 + 3*32 + 50, 32 * 20 + 1);
            ShowNextObject();
            ResumeLayout();
            this.KeyDown += MoveObject;

            game.Start();
        }

        public Form1(List<TetrisObject> listOfShapes)
        {
            SuspendLayout();
            labelArray = new Label[20, 10];
            labelScore = new Label();
            labelArrayNext = new Label[3, 3];
            this.CreateGrid();
            this.CreateHelp();
            tField = new TetrisField(ref labelArray);

            this.BackColor = Color.Aqua;
            InitializeComponent();
            game = new GameScore(ref timer1);

            listOfObjects = new TetrisObject[listOfShapes.Count];
            listOfObjects = listOfShapes.ToArray();
            
            mObject = new MovingObject(tField, new TetrisObject(listOfObjects[GetRandomNumber()]), new TetrisObject(listOfObjects[GetRandomNumber()]), game);
            this.ClientSize = new Size(10 * 32 + 3 * 32 + 50, 32 * 20 + 1);
            ShowNextObject();
            ResumeLayout();
            this.KeyDown += MoveObject;

            game.Start();
        }

        private void CreateGrid()
        {
            for (int i = 0; i < labelArray.GetLength(0); i++)
                for (int j = 0; j < labelArray.GetLength(1); j++)
                {
                    labelArray[i, j] = new Label();
                    labelArray[i, j].Size = new Size(30, 30);
                    labelArray[i, j].Location = new Point(32 * j + 1, 32 * i + 1);
                    this.Controls.Add(labelArray[i, j]);
                }

        }

        private void CreateHelp()
        {
            labelScore = new Label();
            labelScore.Size = new Size(94, 30);
            labelScore.Location = new Point(32 * labelArray.GetLength(1) + 25, 33);
            this.Controls.Add(labelScore);
            labelScore.TextAlign = ContentAlignment.MiddleCenter;
            labelScore.BackColor = Color.Brown;
            for (int i = 0; i < labelArrayNext.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext.GetLength(1); j++)
                {
                    labelArrayNext[i, j] = new Label();
                    labelArrayNext[i, j].Size = new Size(30, 30);
                    labelArrayNext[i, j].Location = new Point(32 * (j + labelArray.GetLength(1) ) + 25, 32 * (i+3) + 1);
                    this.Controls.Add(labelArrayNext[i, j]);
                    labelArrayNext[i, j].BackColor = Color.Brown;
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Down();
        }
        
        private void Down()
        {
            if (!mObject.MoveDown())
            {
                labelScore.Text = game.Score.ToString();
                if (game.GameOver)
                {
                    //poziva se dialog za high score
                    String s = "Kraj: " + game.Score + " Interval: " + timer1.Interval;
                    MessageBox.Show(s);
                }
                else
                {
                    mObject.Object = new TetrisObject(listOfObjects[GetRandomNumber()]);
                    ShowNextObject();
                }
            }
        }
        private void MoveObject(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    mObject.MoveToSide(Position.LEFT);
                    break;
                case Keys.Right:
                    mObject.MoveToSide(Position.RIGHT);
                    break;
                case Keys.Q:
                    mObject.Rotate(Position.ROTATEL);
                    break;
                case Keys.E:
                    mObject.Rotate(Position.ROTATER);
                    break;
                case Keys.Down:
                    this.Down();
                    break;
                case Keys.P:
                    game.Pause();
                    break;
            }

        }

        private int GetRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(0, listOfObjects.Length);
        }

        private void ShowNextObject()
        {
            for (int i = 0; i < labelArrayNext.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext.GetLength(1); j++)
                {
                    labelArrayNext[i, j].BackColor = Color.Brown;
                }
            foreach(Point p in mObject.Object)
            {
                labelArrayNext[p.X, p.Y].BackColor = Color.Pink;
            }
        }
    }

}
