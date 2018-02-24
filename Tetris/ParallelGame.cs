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
    public partial class ParallelGame : Form
    {
        private Label[,] labelArray1;
        private Label[,] labelArray2;
        private Label labelScore;
        private Label labelLevel;
        private Label labelFocus;
        private Label[,] labelArrayNext1;
        private Label[,] labelArrayNext2;
        private TetrisField tField1;
        private TetrisField tField2;
        private MovingObject mObject1;
        private MovingObject mObject2;
        private MovingObject mObjectFocus;
        private TetrisObject[] listOfObjects;
        private GameScore game;
        private Game newGame;
        Random rnd = new Random();
        private bool flag;

        public ParallelGame(List<TetrisObject> listOfShapes)
        {

            SuspendLayout();

            labelArray1 = new Label[20, 10];
            labelArray2 = new Label[20, 10];

            labelScore = new Label();
            labelLevel = new Label();
            labelFocus = new Label();

            labelArrayNext1 = new Label[3, 3];
            labelArrayNext2 = new Label[3, 3];

            InitializeComponent();
            game = new GameScore(ref timer1);
            labelScore.Text = "SCORE:\n" + game.Score.ToString();
            labelLevel.Text = "LEVEL: " + game.Level.ToString();
            labelFocus.Text = "<--"; 

            this.CreateGrid();

            this.CreateHelp();
            tField1 = new TetrisField(ref labelArray1);
            tField2 = new TetrisField(ref labelArray2);


            this.BackColor = Color.CornflowerBlue;
            //game = new GameScore(ref timer1);

            listOfObjects = new TetrisObject[listOfShapes.Count];
            listOfObjects = listOfShapes.ToArray();
            newGame = new Game(listOfShapes, false, false, true);

            mObject1 = new MovingObject(tField1, new TetrisObject(listOfObjects[GetRandomNumber()]), new TetrisObject(listOfObjects[GetRandomNumber()]), game);
            mObject2 = new MovingObject(tField2, new TetrisObject(listOfObjects[GetRandomNumber()]), new TetrisObject(listOfObjects[GetRandomNumber()]), game);

            this.ClientSize = new Size(10 * 32 + 3 * 32 + 50 + 10 * 32, 32 * 20 + 1);

            mObjectFocus = mObject1;
            flag = true;


            ShowNextObject();
            ResumeLayout();
            this.KeyDown += MoveObject;

            game.Start();
        }

        private int GetRandomNumber()
        {
            return rnd.Next(0, listOfObjects.Length);
        }

        private void ShowNextObject()
        {
            for (int i = 0; i < labelArrayNext1.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext1.GetLength(1); j++)
                {
                    labelArrayNext1[i, j].BackColor = Color.DarkBlue;
                }
            foreach (Point p in mObject1.Object)
            {
                labelArrayNext1[p.X, p.Y].BackColor = Color.Yellow;
            }

            for (int i = 0; i < labelArrayNext2.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext2.GetLength(1); j++)
                {
                    labelArrayNext2[i, j].BackColor = Color.DarkBlue;
                }
            foreach (Point p in mObject2.Object)
            {
                labelArrayNext2[p.X, p.Y].BackColor = Color.Yellow;
            }
        }

        private void CreateGrid()
        {
            for (int i = 0; i < labelArray1.GetLength(0); i++)
                for (int j = 0; j < labelArray1.GetLength(1); j++)
                {
                    labelArray1[i, j] = new Label();
                    labelArray1[i, j].Size = new Size(30, 30);
                    labelArray1[i, j].Location = new Point(32 * j + 1, 32 * i + 1);
                    this.Controls.Add(labelArray1[i, j]);

                    labelArray2[i, j] = new Label();
                    labelArray2[i, j].Size = new Size(30, 30);
                    labelArray2[i, j].Location = new Point(32 * j + 1 + 466, 32 * i + 1);
                    this.Controls.Add(labelArray2[i, j]);
                }

        }

        private void CreateHelp()
        {
            //labelScore = new Label();
            labelScore.Size = new Size(94, 62);
            labelScore.Location = new Point(32 * labelArray1.GetLength(1) + 25, 33);
            this.Controls.Add(labelScore);
            labelScore.TextAlign = ContentAlignment.MiddleCenter;
            labelScore.BackColor = Color.DarkBlue;
            labelScore.ForeColor = Color.White;
            labelScore.Font = new Font(labelScore.Font, FontStyle.Bold);

            labelLevel.Size = new Size(94, 30);
            labelLevel.Location = new Point(32 * labelArray1.GetLength(1) + 25, 93);
            this.Controls.Add(labelLevel);
            labelLevel.BackColor = Color.DarkBlue;
            labelLevel.TextAlign = ContentAlignment.MiddleCenter;
            labelLevel.ForeColor = Color.White;
            labelLevel.Font = new Font(labelLevel.Font, FontStyle.Bold);

            labelFocus.Size = new Size(94, 30);
            labelFocus.Location = new Point(32 * labelArray1.GetLength(1) + 25, 14*32 + 1);
            this.Controls.Add(labelFocus);
            labelFocus.BackColor = Color.DarkBlue;
            labelFocus.TextAlign = ContentAlignment.MiddleCenter;
            labelFocus.ForeColor = Color.Yellow;
            labelFocus.Font = new Font(labelFocus.Font, FontStyle.Bold);

            for (int i = 0; i < labelArrayNext1.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext1.GetLength(1); j++)
                {
                    labelArrayNext1[i, j] = new Label();
                    labelArrayNext1[i, j].Size = new Size(30, 30);
                    labelArrayNext1[i, j].Location = new Point(32 * (j + labelArray1.GetLength(1)) + 25, 32 * (i + 4) + 1);
                    this.Controls.Add(labelArrayNext1[i, j]);
                    labelArrayNext1[i, j].BackColor = Color.DarkBlue;

                    labelArrayNext2[i, j] = new Label();
                    labelArrayNext2[i, j].Size = new Size(30, 30);
                    labelArrayNext2[i, j].Location = new Point(32 * (j + labelArray1.GetLength(1)) + 25, 32 * (i + 10) + 1);
                    this.Controls.Add(labelArrayNext2[i, j]);
                    labelArrayNext2[i, j].BackColor = Color.DarkBlue;
                }
        }

        private void Down1()
        {
            if (!mObject1.MoveDown())
            {
                labelScore.Text = "SCORE:\n" + game.Score.ToString();
                labelLevel.Text = "LEVEL: " + game.Level.ToString();
                if (game.GameOver)
                {
                    FinishForm f3 = new FinishForm(game.Score.ToString(), newGame);
                    f3.ShowDialog();
                }

                mObject1.Object = new TetrisObject(listOfObjects[GetRandomNumber()]);
                ShowNextObject();
            }
        }
        private void Down2()
        {
            if (!mObject2.MoveDown())
            {
                labelScore.Text = "SCORE:\n" + game.Score.ToString();
                labelLevel.Text = "LEVEL: " + game.Level.ToString();
                if (game.GameOver)
                {
                    FinishForm f3 = new FinishForm(game.Score.ToString(), newGame);
                    f3.ShowDialog();
                }

                mObject2.Object = new TetrisObject(listOfObjects[GetRandomNumber()]);
                ShowNextObject();
            }
        }

        private void MoveObject(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
            {
                if (flag)
                {
                    mObjectFocus = mObject2;
                    flag = false;
                    labelFocus.Text = "-->";
                }
                else
                {
                    mObjectFocus = mObject1;
                    flag = true;
                    labelFocus.Text = "<--";
                }
                    
                
            }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    mObjectFocus.MoveToSide(Position.LEFT);
                    break;
                case Keys.Right:
                    mObjectFocus.MoveToSide(Position.RIGHT);
                    break;
                case Keys.Q:
                    mObjectFocus.Rotate(Position.ROTATEL);
                    break;
                case Keys.E:
                    mObjectFocus.Rotate(Position.ROTATER);
                    break;
                case Keys.Down:
                    if (flag)
                        this.Down1();
                    else
                        this.Down2();
                    break;
                case Keys.P:
                    game.Pause();
                    break;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.Down1();
            this.Down2();
        }
    }
}

