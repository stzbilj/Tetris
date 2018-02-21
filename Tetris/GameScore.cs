using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tetris
{
    class GameScore
    {
        private int score;
        private int level;
        private int bonus;
        private Timer timer;
        private bool gameOver;

        public GameScore(ref Timer _timer)
        {
            timer = _timer;
            score = 0;
            level = 1;
            bonus = 0;
            gameOver = false;
        }

        public int Score
        {
            get { return score; }
            set
            {
                if (value > 0)
                {
                    score += 100 * value + 100 * (value - 1);
                    if ((score / 1000) < 10)
                        timer.Interval = 1000 - 90 * Level - 60;
                }
            }
        }

        public int Level
        {
            get { return level; }
            set
            {
                if (value > 0)
                    level = value;
            }
        }

        public int Bonus
        {
            get { return bonus; }
            set
            {
                if(value > 0)
                {
                    bonus += value * 500;
                }
            }
        }

        public void Start()
        {
            if(!gameOver)
                timer.Enabled = true;
        }

        public void Pause()
        {
            if (!gameOver)
                timer.Enabled = timer.Enabled ^ true;
        }

        public bool GameOver
        {
            get { return gameOver; }
        }

        public void EndGame() 
        {
            gameOver= true;
            timer.Enabled = false;
        }
    }
}
