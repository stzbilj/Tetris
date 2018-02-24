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
    // the initial form of the game where game option is chosen
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            this.BackColor = Color.CornflowerBlue;

            button1.BackColor = Color.DarkBlue;
            button2.BackColor = Color.DarkBlue;
            btnParallelGame.BackColor = Color.DarkBlue;
            button4.BackColor = Color.DarkBlue;

            button1.ForeColor = Color.Yellow;
            button2.ForeColor = Color.Yellow;
            btnParallelGame.ForeColor = Color.Yellow;
            button4.ForeColor = Color.Yellow;

            label1.ForeColor = Color.Yellow;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomiseGame form3 = new CustomiseGame();
            form3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Tetris.Form1();
            form1.ShowDialog();
        }

        private void btnParallelGame_Click(object sender, EventArgs e)
        {
            List<TetrisObject> listOfShapes = new List<TetrisObject>();

            int[,] objekt1 = new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { 0, 0, 0 } };
            TetrisObject tObject1 = new TetrisObject(objekt1);
            listOfShapes.Add(tObject1);

            int[,] objekt2 = new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 0 } };
            TetrisObject tObject2 = new TetrisObject(objekt2);
            listOfShapes.Add(tObject2);

            int[,] objekt3 = new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } };
            TetrisObject tObject3 = new TetrisObject(objekt3);
            listOfShapes.Add(tObject3);

            int[,] objekt4 = new int[,] { { 1, 1, 0 }, { 1, 1, 0 }, { 0, 0, 0 } };
            TetrisObject tObject4 = new TetrisObject(objekt4);
            listOfShapes.Add(tObject4);

            int[,] objekt5 = new int[,] { { 1, 1, 0 }, { 0, 1, 1 }, { 0, 0, 0 } };
            TetrisObject tObject5 = new TetrisObject(objekt5);
            listOfShapes.Add(tObject5);

            int[,] objekt6 = new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
            TetrisObject tObject6 = new TetrisObject(objekt6);
            listOfShapes.Add(tObject6);

            int[,] objekt7 = new int[,] { { 0, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
            TetrisObject tObject7 = new TetrisObject(objekt7);
            listOfShapes.Add(tObject7);

            ParallelGame parallelGame = new ParallelGame(listOfShapes);
            parallelGame.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HighScore highScore = new HighScore();
            highScore.ShowDialog();
        }
    }
}
