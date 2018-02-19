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
    }
}
