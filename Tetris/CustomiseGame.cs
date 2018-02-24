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
    // the third form, used to create user's own shapes
    public partial class CustomiseGame : Form
    {
        private int shapesChosen = 0;

        List<TetrisObject> listOfShapes = new List<TetrisObject>();

        CheckBox checkbox1 = new CheckBox();
        CheckBox checkbox2 = new CheckBox();
        CheckBox checkbox3 = new CheckBox();
        CheckBox checkbox4 = new CheckBox();
        CheckBox checkbox5 = new CheckBox();
        CheckBox checkbox6 = new CheckBox();
        CheckBox checkbox7 = new CheckBox();

        CheckBox addObstacles = new CheckBox();
        CheckBox addGoldenPoints = new CheckBox();
        CheckBox addParallelGame = new CheckBox();

        Button buttonContinue = new Button();
        Button addingButton = new Button();
        Button play = new Button();

        Label label28 = new Label();
        Label label29 = new Label();
        Label label30 = new Label();
        Label label31 = new Label();
        Label label32 = new Label();
        Label label33 = new Label();
        Label label34 = new Label();
        Label label35 = new Label();
        Label label36 = new Label();

        TextBox explanationsBox = new TextBox();

        private void setLabel(GroupBox groupbox, Label label, int x, int y)
        {
            label.BackColor = Color.Aquamarine;
            groupbox.Controls.Add(label);
            label.Location = new Point(x, y);
            label.Size = new Size(20, 20);
            label.AutoSize = false;
            label.Text = "";
        }

        private void setCheckBox(CheckBox checkbox, int x, int y)
        {
            checkbox.Size = new Size(15, 14);
            groupBox1.Controls.Add(checkbox);
            checkbox.Location = new Point(x, y);
            checkbox.Checked = true;
        }

        private bool addToShapes(int[,] newShape, bool checkEquals = false)
        {
            int equals = 0;

            TetrisObject tetrisObject = new TetrisObject(newShape);

            if (checkEquals)
            {
                foreach (TetrisObject shape in listOfShapes)
                {
                    if (tetrisObject == shape)
                    {
                        MessageBox.Show("The chosen shape has already been chosen before. Please, select a new one.");
                        equals = 1;
                        return false;
                    }
                }
            }

            if(equals == 0)
            {
                listOfShapes.Add(tetrisObject);
                shapesChosen += 1;
                setExplanation();

                if (shapesChosen == 7)
                {
                    play.Enabled = true;
                }

                if (shapesChosen == 10)
                {
                    addingButton.Enabled = false;
                }
            }

            return true;
        }

        // the method sets text to the textbox in groupBox2, depending on the number of chosen shapes
        private void setExplanation()
        {
            explanationsBox.Text = "To start a game, you need 7 - 10 shapes.";
            explanationsBox.AppendText(Environment.NewLine);
            explanationsBox.AppendText("You have " + shapesChosen + " shapes at the moment.");
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (checkbox1.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { 0, 0, 0 } });
            }
            if (checkbox2.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 0 } });
            }
            if (checkbox3.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } });
            }
            if (checkbox4.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 1, 1, 0 }, { 1, 1, 0 }, { 0, 0, 0 } });
            }
            if (checkbox5.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 1, 1, 0 }, { 0, 1, 1 }, { 0, 0, 0 } });
            }
            if (checkbox6.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } });
            }
            if (checkbox7.Checked)
            {
                //shapesChosen += 1;
                addToShapes(new int[,] { { 0, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } });
            }

            if(shapesChosen < 3)
            {
                MessageBox.Show("Please, choose at least 3 standard shapes!");
                shapesChosen = 0;
                listOfShapes.Clear();
            }
            else
            {
                groupBox2.Enabled = true;
                groupBox1.Enabled = false;
            }
        }

        private void labelClicked(object sender, EventArgs e)
        {
            var tempLabel = (Label)sender;
            if (tempLabel.BackColor == Color.Aquamarine)
                tempLabel.BackColor = Color.Blue;
            else
                tempLabel.BackColor = Color.Aquamarine;
        }

        private void addingButtonClicked(object sender, EventArgs e)
        {
            int emptyShape = 1;
            int[,] tetrisMatrix = new int[3, 3];
            if (label28.BackColor == Color.Blue)
            {
                tetrisMatrix[0, 0] = 1;
                label28.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label29.BackColor == Color.Blue)
            {
                tetrisMatrix[0, 1] = 1;
                label29.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label30.BackColor == Color.Blue)
            {
                tetrisMatrix[0, 2] = 1;
                label30.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label31.BackColor == Color.Blue)
            {
                tetrisMatrix[1, 0] = 1;
                label31.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label32.BackColor == Color.Blue)
            {
                tetrisMatrix[1, 1] = 1;
                label32.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label33.BackColor == Color.Blue)
            {
                tetrisMatrix[1, 2] = 1;
                label33.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label34.BackColor == Color.Blue)
            {
                tetrisMatrix[2, 0] = 1;
                label34.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label35.BackColor == Color.Blue)
            {
                tetrisMatrix[2, 1] = 1;
                label35.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }
            if (label36.BackColor == Color.Blue)
            {
                tetrisMatrix[2, 2] = 1;
                label36.BackColor = Color.Aquamarine;
                emptyShape = 0;
            }

            if (emptyShape == 1)
            {
                MessageBox.Show("Please, add a non-empty shape :)");
            }

            else
            {
                if(addToShapes(tetrisMatrix, true))
                    MessageBox.Show("New shape added! You have " + shapesChosen + " shapes now.");
            }
        }

        private void playButtonClicked(object sender, EventArgs e)
        {
            bool addObs = addObstacles.Checked;
            bool addPts = addGoldenPoints.Checked;
            /*if(addParallelGame.Checked)
            {
                ParallelGame parallelGame = new ParallelGame(listOfShapes);
                parallelGame.ShowDialog();
            }*/
         
            Form1 game = new Form1(listOfShapes, addObs, addPts);
            game.ShowDialog();
            
           
        }

        public CustomiseGame()
        {
            // ovo sam mogla i pametnije, tipa da sam stavila na vrijeme textbox na vrh, ali 
            // ovako bih morala sve labele pomicati 
            /*MessageBox.Show("You need 8 - 10 shapes and at least 3 of them have to be standard shapes!\nPress" + 
                " OK to continue");*/

            InitializeComponent();

            this.BackColor = Color.CornflowerBlue;

            // the first standard shape
            setLabel(groupBox1, label1, 20, 41);
            setLabel(groupBox1, label2, 42, 41);
            setLabel(groupBox1, label3, 64, 41);

            // the second standard shape
            setLabel(groupBox1, label4, 144, 41);
            setLabel(groupBox1, label5, 144, 63);
            setLabel(groupBox1, label6, 144, 85);
            setLabel(groupBox1, label7, 166, 85);

            // the third standard shape
            setLabel(groupBox1, label8, 104, 41);
            setLabel(groupBox1, label9, 104, 63);
            setLabel(groupBox1, label10, 104, 85);
            setLabel(groupBox1, label11, 82, 85);

            // the fourth standard shape
            setLabel(groupBox1, label12, 206, 41);
            setLabel(groupBox1, label13, 228, 41);
            setLabel(groupBox1, label14, 206, 63);
            setLabel(groupBox1, label15, 228, 63);

            // the fifth standard shape
            setLabel(groupBox1, label16, 268, 41);
            setLabel(groupBox1, label17, 290, 41);
            setLabel(groupBox1, label18, 290, 63);
            setLabel(groupBox1, label19, 312, 63);

            // the sixth standard shape
            setLabel(groupBox1, label20, 352, 63);
            setLabel(groupBox1, label21, 374, 63);
            setLabel(groupBox1, label22, 396, 63);
            setLabel(groupBox1, label23, 374, 41);

            // the seventh standard shape
            setLabel(groupBox1, label24, 436, 63);
            setLabel(groupBox1, label25, 458, 63);
            setLabel(groupBox1, label26, 458, 41);
            setLabel(groupBox1, label27, 480, 41);

            // set checkboxes
            setCheckBox(checkbox1, 42, 115);
            setCheckBox(checkbox2, 93, 115);
            setCheckBox(checkbox3, 155, 115);
            setCheckBox(checkbox4, 217, 115);
            setCheckBox(checkbox5, 290, 115);
            setCheckBox(checkbox6, 374, 115);
            setCheckBox(checkbox7, 458, 115);

            // add button to groupbox
            buttonContinue.BackColor = Color.DarkBlue;
            buttonContinue.ForeColor = Color.White;
            buttonContinue.Text = "Continue";
            buttonContinue.Size = new Size(60, 30);
            buttonContinue.Click += new EventHandler(buttonContinue_Click);
            groupBox1.Controls.Add(buttonContinue);
            buttonContinue.Location = new Point(228, 160);
            buttonContinue.Show();

            //----------
            // groupBox 2
            //----------
            // add labels to draw shapes
            groupBox2.Enabled = false;

            setLabel(groupBox2, label28, 436, 41);
            label28.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label29, 458, 41);
            label29.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label30, 480, 41);
            label30.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label31, 436, 63);
            label31.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label32, 458, 63);
            label32.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label33, 480, 63);
            label33.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label34, 436, 85);
            label34.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label35, 458, 85);
            label35.Click += new EventHandler(labelClicked);

            setLabel(groupBox2, label36, 480, 85);
            label36.Click += new EventHandler(labelClicked);

            // button to add a new shape
            addingButton.BackColor = Color.DarkBlue;
            addingButton.ForeColor = Color.White;
            addingButton.Size = new Size(62, 20);
            groupBox2.Controls.Add(addingButton);
            addingButton.Location = new Point(436, 120);
            addingButton.Text = "Add";
            addingButton.Click += new EventHandler(addingButtonClicked);

            // groupBox4
            addObstacles.Size = new Size(150, 20);
            addObstacles.Text = "Add obstacles";
            //addObstacles.TextAlign = ContentAlignment.TopCenter;
            groupBox4.Controls.Add(addObstacles);
            addObstacles.Location = new Point(100, 50);

            addGoldenPoints.Size = new Size(150, 20);
            addGoldenPoints.Text = "Add golden points";
            groupBox4.Controls.Add(addGoldenPoints);
            addGoldenPoints.Location = new Point(300, 50);

            /*addParallelGame.Size = new Size(150, 20);
            addParallelGame.Text = "Add Parallel Game";
            groupBox4.Controls.Add(addParallelGame);
            addParallelGame.Location = new Point(350, 50);*/

            // button to start the game
            play.BackColor = Color.DarkBlue;
            play.ForeColor = Color.White;
            play.Size = new Size(542, 40);
            this.Controls.Add(play);
            play.Location = new Point(1, 516);
            play.Text = "Play";
            play.Enabled = false;
            play.Click += new EventHandler(playButtonClicked);

            // text box with explanations
            groupBox2.Controls.Add(explanationsBox);
            explanationsBox.Multiline = true;
            explanationsBox.BorderStyle = BorderStyle.None;
            explanationsBox.Font = new Font("Microsoft San Serif", 12);
            explanationsBox.Location = new Point(50, 50);
            explanationsBox.Enabled = false;
            explanationsBox.Size = new Size(300, 60);
        }

    }
}
