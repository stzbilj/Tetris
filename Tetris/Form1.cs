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
        private Label labelLevel;
        private Label[,] labelArrayNext;
        private Label goldenPts = new Label();
        private TetrisField tField;
        private MovingObject mObject;
        private TetrisObject[] listOfObjects;
        private GameScore game;
        private Game newGame;

        private bool addObstacles;
        private bool addGoldenPoints;
        private bool blackFieldAdded;

        private Tuple<int, int> goldenPosition;

        private int goldenPointsInterval;


        Random rnd = new Random();

        public Form1(bool _addObstacles = false, bool _addGoldenPoints = false)
        {
            SuspendLayout();
            addObstacles = _addObstacles;
            addGoldenPoints = _addGoldenPoints;
            blackFieldAdded = false;
            goldenPointsInterval = rnd.Next(15, 50);
            labelArray = new Label[20, 10];
            labelScore = new Label();
            labelLevel = new Label();
            labelArrayNext = new Label[3, 3];
            this.CreateGrid();
            this.CreateHelp();
            tField = new TetrisField(ref labelArray);
            
            this.BackColor = Color.CornflowerBlue;
            InitializeComponent();
            game = new GameScore(ref timer1);

            labelScore.Text = "SCORE:\n" + game.Score.ToString();
            labelLevel.Text = "LEVEL: " + game.Level.ToString();

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
            List<TetrisObject> list = new List<TetrisObject>() { tObject1, tObject2, tObject3, tObject4, tObject5, tObject6, tObject7 };
            newGame = new Game(list);

            mObject = new MovingObject(tField, new TetrisObject(listOfObjects[GetRandomNumber()]), new TetrisObject(listOfObjects[GetRandomNumber()]), game);
            this.ClientSize = new Size(10*32 + 3*32 + 50, 32 * 20 + 1);

            goldenPosition = new Tuple<int, int>(0, 0);

            ShowNextObject();
            ResumeLayout();
            this.KeyDown += MoveObject;

            game.Start();
        }

        public Form1(List<TetrisObject> listOfShapes, bool _addObstacles = false, bool _addGoldenPoints = false)
        {
            SuspendLayout();
            addObstacles = _addObstacles;
            addGoldenPoints = _addGoldenPoints;
            blackFieldAdded = false;
            Random rnd = new Random();
            goldenPointsInterval = rnd.Next(15, 50);
            labelArray = new Label[20, 10];
            labelScore = new Label();
            labelLevel = new Label();
            labelArrayNext = new Label[3, 3];
            this.CreateGrid();
            this.CreateHelp();

            InitializeComponent();
            game = new GameScore(ref timer1);
            labelScore.Text = "Score: " + game.Score.ToString();
            labelLevel.Text = "Level: " + game.Level.ToString();
  
            tField = new TetrisField(ref labelArray);

            this.BackColor = Color.CornflowerBlue;
            
            //game = new GameScore(ref timer1);

            listOfObjects = new TetrisObject[listOfShapes.Count];
            listOfObjects = listOfShapes.ToArray();
            newGame = new Game(listOfShapes, _addObstacles, _addGoldenPoints);
            
            mObject = new MovingObject(tField, new TetrisObject(listOfObjects[GetRandomNumber()]), new TetrisObject(listOfObjects[GetRandomNumber()]), game);
            this.ClientSize = new Size(10 * 32 + 3 * 32 + 50, 32 * 20 + 1);

            goldenPosition = new Tuple<int, int>(0, 0);

            ShowNextObject();
            ResumeLayout();
            this.KeyDown += MoveObject;

            game.Start();
        }

        private Tuple<int, int> findNewColoredField(Color color)
        {

            for(int i = 0; i < 1000; ++i)
            {
                int x1 = rnd.Next(4, 19);
                int y1 = rnd.Next(0, 9);

                if (tField[x1, y1] == Color.DarkBlue)
                {
                    tField[x1, y1] = color;
                    blackFieldAdded = true;
                    return new Tuple<int, int>(x1, y1);
                }
            }

            return new Tuple<int, int>(0, 0);
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
            labelScore.Size = new Size(104, 61);
            labelScore.Location = new Point(32 * labelArray.GetLength(1) + 20, 33);
            this.Controls.Add(labelScore);
            labelScore.TextAlign = ContentAlignment.MiddleCenter;
            labelScore.BackColor = Color.DarkBlue;
            labelScore.ForeColor = Color.White;
            labelScore.Font = new Font(labelScore.Font, FontStyle.Bold);

            labelLevel.Size = new Size(104, 30);
            labelLevel.Location = new Point(32 * labelArray.GetLength(1) + 20, 96);
            this.Controls.Add(labelLevel);
            labelLevel.BackColor = Color.DarkBlue;
            labelLevel.ForeColor = Color.White;
            labelLevel.Font = new Font(labelLevel.Font, FontStyle.Bold);
            labelLevel.TextAlign = ContentAlignment.MiddleCenter; 
            for (int i = 0; i < labelArrayNext.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext.GetLength(1); j++)
                {
                    labelArrayNext[i, j] = new Label();
                    labelArrayNext[i, j].Size = new Size(30, 30);
                    labelArrayNext[i, j].Location = new Point(32 * (j + labelArray.GetLength(1) ) + 25, 32 * (i+4) + 1);
                    this.Controls.Add(labelArrayNext[i, j]);
                    labelArrayNext[i, j].BackColor = Color.DarkBlue;
                }

            if (addGoldenPoints)
            {
                goldenPts.Text = "Golden: 0";
                goldenPts.Size = new Size(104, 30);
                this.Controls.Add(goldenPts);
                goldenPts.Location = new Point(32 * labelArray.GetLength(1) + 20, 250);
                goldenPts.BackColor = Color.DarkBlue;
                goldenPts.ForeColor = Color.White;
                goldenPts.Font = new Font(goldenPts.Font, FontStyle.Bold);
                goldenPts.TextAlign = ContentAlignment.MiddleCenter;
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
                labelScore.Text = "Score: " + game.Score.ToString();
                labelLevel.Text = "Level: " + game.Level.ToString();
                goldenPts.Text = "Golden: " + mObject.goldenPoints.ToString();
                if (game.GameOver)
                {
                    FinishForm f3 = new FinishForm(game.Score.ToString(), newGame);
                    f3.ShowDialog();
                }
                else
                {
                    // addObstacles is true if the checBox 'Add obsticles' was checked
                    // if addObstacles is true, then check if the black field has already been added at this level
                    if (addObstacles && (game.Level % 2 == 0))
                    {
                        if (!blackFieldAdded)
                        {
                            findNewColoredField(Color.Black);
                        }
                    }
                    else
                        blackFieldAdded = false;

                    // addGoldenPoints is true if the checkBox 'Add golden points' was checked
                    // if addGoldenPoints is true, pick a random time interval to add next golden field
                    if (addGoldenPoints)
                    {
                        if (goldenPosition.Item1 != 0)
                        {
                            /*if (tField[goldenPosition.Item1, goldenPosition.Item2] != Color.Yellow)
                                tField[goldenPosition.Item1, goldenPosition.Item2] = Color.Green;
                            else
                                tField[goldenPosition.Item1, goldenPosition.Item2] = Color.Gray;*/
                            if (tField[goldenPosition.Item1, goldenPosition.Item2] == Color.Gold)
                                tField[goldenPosition.Item1, goldenPosition.Item2] = Color.DarkBlue;
                            goldenPosition = new Tuple<int, int>(0, 0);
                        }
                        if (goldenPointsInterval == 0)
                        {
                            Tuple<int, int> temp = findNewColoredField(Color.Gold);
                            //findNewColoredField(Color.Gold);
                            goldenPosition = new Tuple<int, int>(temp.Item1, temp.Item2);
                            goldenPointsInterval = rnd.Next(15, 30);
                        }
                        else
                        {
                            goldenPointsInterval -= 1;
                        }
                    }
                    goldenPts.Text = "Golden: " + mObject.goldenPoints.ToString();

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
                case Keys.Up:
                    if (mObject.goldenPoints > 0 && mObject.MoveUp())
                    {
                        mObject.goldenPoints--;
                        goldenPts.Text = "Golden: " + mObject.goldenPoints.ToString();
                    }
                    break;
                case Keys.R:
                    if (mObject.goldenPoints > 0)
                    {
                        TetrisObject tetrisObject = new TetrisObject(listOfObjects[GetRandomNumber()]);
                        while (tetrisObject == mObject.Object)
                        {
                            tetrisObject = new TetrisObject(listOfObjects[GetRandomNumber()]);
                        }
                        mObject.Object = tetrisObject;
                        ShowNextObject();
                        mObject.goldenPoints--;
                        goldenPts.Text = "Golden: " + mObject.goldenPoints.ToString();
                    }
                    break;
                case Keys.P:
                    game.Pause();
                    break;
            }

        }

        private int GetRandomNumber()
        {
            return rnd.Next(0, listOfObjects.Length);
        }

        private void ShowNextObject()
        {
            for (int i = 0; i < labelArrayNext.GetLength(0); i++)
                for (int j = 0; j < labelArrayNext.GetLength(1); j++)
                {
                    labelArrayNext[i, j].BackColor = Color.DarkBlue;
                }
            foreach(Point p in mObject.Object)
            {
                labelArrayNext[p.X, p.Y].BackColor = Color.Yellow;
            }
        }
    }

}
