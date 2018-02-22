using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class FinishForm : Form
    {
        private string filePath = Path.GetFullPath("..\\..\\data") + "\\gamesAlreadyPlayed.bin";
        private List<Game> gamesAlreadyPlayed;
        private int score;
        private string name;
        private int idOfGame;
        private Game game;
        private string scoreFilePath;
        private List<KeyValuePair<string, int>> listOfScores;
        public FinishForm(string _score, Game _game)
        {
            InitializeComponent();

            score = Convert.ToInt32(_score);
            game = _game;
            listOfScores = new List<KeyValuePair<string, int>>();

            scoreLabel.Text = _score;
            if(!Directory.Exists("..\\..\\data"))
            {
                Directory.CreateDirectory("..\\..\\data");
                Directory.CreateDirectory("..\\..\\data\\scores");
            }
            if (File.Exists(filePath))
            {
                gamesAlreadyPlayed = ReadFromBinaryFile<List<Game>>(filePath);
                Console.WriteLine(gamesAlreadyPlayed.Count.ToString());
            }
            else
                gamesAlreadyPlayed = new List<Game>();

            IsGamePlayed();
        }

        public void IsGamePlayed()
        {
            if (gamesAlreadyPlayed.Contains(game))
            {
                idOfGame = gamesAlreadyPlayed.IndexOf(game) + 1;
                scoreFilePath = Path.GetFullPath("..\\..\\data") + "\\scores\\" + idOfGame.ToString() + ".bin";
                listOfScores = ReadFromBinaryFile<List<KeyValuePair<string, int>>>(scoreFilePath);
                showListOfScores();
            }
            else
            {
                gamesAlreadyPlayed.Add(game);
                idOfGame = gamesAlreadyPlayed.Count();
                scoreFilePath = Path.GetFullPath("..\\..\\data") + "\\scores\\" + idOfGame.ToString() + ".bin";
                WriteToBinaryFile<List<Game>>(filePath, gamesAlreadyPlayed);
            }
        }

        public void sortingListOfScores()
        {
            KeyValuePair<string, int> temp = new KeyValuePair<string, int>();

            for (int i = 0; i < listOfScores.Count; i++)
            {
                for (int j = i; j < listOfScores.Count; j++)
                {
                    if (listOfScores[i].Value < listOfScores[j].Value)
                    {
                        temp = listOfScores[i];
                        listOfScores[i] = listOfScores[j];
                        listOfScores[j] = temp;
                    }
                }
            }
        }
        public void showListOfScores()
        {
            sortingListOfScores();

            int length = listOfScores.Count();

            if (length > 7)
                length = 7;
            Label[] labelArray = new Label[7] { label3, label4, label5, label6, label7, label8, label9 };
            for (int i = 0; i < length; i++)
            {
                labelArray[i].Text = (i + 1).ToString() + " " + listOfScores[i].Key + " " + listOfScores[i].Value.ToString();
            }
        }
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            KeyValuePair<string, int> o = new KeyValuePair<string, int>(name, score);
            listOfScores.Add(o);

            saveBtn.Enabled = false;
            showListOfScores();
            WriteToBinaryFile<List<KeyValuePair<string, int>>>(scoreFilePath, listOfScores);
        }
    }
}
