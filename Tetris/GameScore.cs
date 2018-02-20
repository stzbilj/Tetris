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
        private Timer timer;
        private bool gameOver;

        public GameScore(ref Timer _timer)
        {
            timer = _timer;
            score = 0;
            gameOver = false;
    }

        public int Score
        {
            get { return score; }
            set
            {
                if(value > 0)
                {
                    score += 100 * value + 100 * (value - 1);
                    if( (score/500) < 10)
                        timer.Interval = 1000 - 90 *(score/500) ;
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
