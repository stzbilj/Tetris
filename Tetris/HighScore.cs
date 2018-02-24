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
    public partial class HighScore : Form
    {
        private List<Game> gamesAlreadyPlayed;
        private string filePath = Path.GetFullPath("..\\..\\data") + "\\gamesAlreadyPlayed.bin";
        private int idOfGame;
        private Game parallelGame;
        private Game standardGame;
        private string scoreFilePath;
        private List<KeyValuePair<string, int>> listOfScores;
        public HighScore()
        {
            InitializeComponent();

            this.BackColor = Color.CornflowerBlue;
            label1.ForeColor = Color.Yellow;
            label2.ForeColor = Color.Yellow;
            label3.ForeColor = Color.Yellow;

            if(Directory.Exists("..\\..\\data") && File.Exists(filePath))
            {
                gamesAlreadyPlayed = ReadFromBinaryFile<List<Game>>(filePath);

                //Creating list of shapes in game
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

                standardGame = new Game(listOfShapes);
                parallelGame = new Game(listOfShapes, false, false, true);

                if (gamesAlreadyPlayed.Contains(standardGame))
                {
                    idOfGame = gamesAlreadyPlayed.IndexOf(standardGame) + 1;
                    scoreFilePath = Path.GetFullPath("..\\..\\data") + "\\scores\\" + idOfGame.ToString() + ".bin";
                    if (File.Exists(scoreFilePath))
                    {
                        listOfScores = ReadFromBinaryFile<List<KeyValuePair<string, int>>>(scoreFilePath);

                        int length = listOfScores.Count();

                        if (length > 7)
                            length = 7;
                        Label[] labelArray = new Label[7] { sgLabel1, sgLabel2, sgLabel3, sgLabel4, sgLabel5, sgLabel6, sgLabel7 };
                        for (int i = 0; i < length; i++)
                        {
                            labelArray[i].Text = (i + 1).ToString() + ". " + listOfScores[i].Key + " " + listOfScores[i].Value.ToString();
                        }
                    }

                }

                if (gamesAlreadyPlayed.Contains(parallelGame))
                {
                    idOfGame = gamesAlreadyPlayed.IndexOf(parallelGame) + 1;
                    scoreFilePath = Path.GetFullPath("..\\..\\data") + "\\scores\\" + idOfGame.ToString() + ".bin";
                    if (File.Exists(scoreFilePath))
                    {
                        listOfScores = ReadFromBinaryFile<List<KeyValuePair<string, int>>>(scoreFilePath);

                        int length = listOfScores.Count();

                        if (length > 7)
                            length = 7;
                        Label[] labelArray = new Label[7] { pgLabel1, pgLabel2, pgLabel3, pgLabel4, pgLabel5, pgLabel6, pgLabel7 };
                        for (int i = 0; i < length; i++)
                        {
                            labelArray[i].Text = (i + 1).ToString() + ". " + listOfScores[i].Key + " " + listOfScores[i].Value.ToString();
                        }
                    }

                }
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
