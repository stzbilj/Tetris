using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class MovingObject
    {
        private TetrisField tField;
        private TetrisObject tObject;
        private TetrisObject nextObject;
        private GameScore game;
        private int row;
        private int column;
        public int goldenPoints;
        
        public MovingObject(TetrisField _tField, TetrisObject _tObject, TetrisObject _nextObject, GameScore _game)
        {
            tField = _tField;
            tObject = _tObject;
            nextObject = _nextObject;
            game = _game;
            row = 0;
            column = 4;
            goldenPoints = 0;
            InitialDraw();
        }

        private bool InitialDraw()
        {
            if (row == 0 && column == 4 && CheckCollision(Position.SAME))
                return false;
            DrawObject();
            return true;
        }

        public int Row{
            get{ return row; }
        }

        public int Column
        {
            get { return column; }
        }

        public TetrisObject Object
        {
            get { return nextObject; }
            set {
                nextObject = value;
            }
        }
        
        private void ClearObject()
        {
            foreach (Point p in tObject)
            {
                tField[row + p.X, column + p.Y] = Color.DarkBlue;
            }
        }

        private void DrawObject() { 
            foreach (Point p in tObject)
            {
                tField[row + p.X, column + p.Y] = Color.Yellow;
            }
        }

        public void MoveToSide(Position pos)
        {
            if (!CheckCollision(pos))
            {
                this.ClearObject();
                if (pos == Position.LEFT)
                    column--;
                else
                    column++;
                this.DrawObject();

            }
        }

        public bool MoveDown()
        {
            if(!CheckCollision(Position.DOWN))
            {
                this.ClearObject();
                row++;
                this.DrawObject();
                return true;
            }
            else
            {
                tField.PlaceObject(row, column, tObject);
                Tuple<int, int> temp = new Tuple<int, int>(0, 0);
                temp = tField.ClearFullRows();
                game.Score = temp.Item1;
                game.Bonus = temp.Item2;
                while (game.Score / game.Level >= 1000)
                    game.Level = game.Level + 1;
                row = 0;
                column = 4;
                tObject = nextObject;
                if (!InitialDraw())
                {
                    game.EndGame();
                }
                return false;
            }
        }

        public bool MoveUp()
        {
            if (!CheckCollision(Position.UP))
            {
                this.ClearObject();
                row--;
                this.DrawObject();
                return true;
            }
            return false;
        }

        public void Rotate(Position pos)
        {
            int rotate = 0;
            if (pos == Position.ROTATEL)
                rotate--;
            else
                rotate++;
            if (!CheckCollision(pos))
            {
                this.ClearObject();
                tObject.Rotate(rotate);
                if (tObject.Size(1) + column >= tField.Size(1))
                {
                    column = tField.Size(1) - tObject.Size(1);
                }
                this.DrawObject();
            }
        }

        private bool CheckCollision(Position pos)
        {
            int newRow = row;
            int newColumn = column;
            TetrisObject tetrisObject = new TetrisObject(tObject);
            switch (pos)
            {
                case Position.UP:
                    newRow--;
                    if (newRow < 0)
                        return true;
                    break;
                case Position.DOWN:
                    newRow++;
                    break;
                case Position.LEFT:
                    newColumn--;
                    break;
                case Position.RIGHT:
                    newColumn++;
                    break;
                case Position.ROTATEL:
                    tetrisObject.Rotate(-1);
                    if (tetrisObject.Size(1) + newColumn  >= tField.Size(1))
                    {
                        newColumn = tField.Size(1) - tetrisObject.Size(1);
                    }
                    break;
                 case Position.ROTATER:
                    tetrisObject.Rotate(1);
                    if (tetrisObject.Size(1) + newColumn >= tField.Size(1))
                    {
                        newColumn = tField.Size(1) - tetrisObject.Size(1);
                    }
                    break;
                default:
                    break;
            }
            foreach(Point p in tetrisObject)
            {

                if (p.X + newRow >= tField.Size(0) || newColumn < 0 || newColumn + p.Y >= tField.Size(1))
                    return true;
                if (tField[p.X + newRow, p.Y + newColumn] == Color.Gold)
                {
                    goldenPoints += 3;
                    //tField[p.X + newRow, p.Y + newColumn] = Color.Yellow;
                }
                if (tField[p.X + newRow, p.Y + newColumn] == Color.Gray || tField[p.X + newRow, p.Y + newColumn] == Color.Black)
                    return true;
            }
            
            return false;   
        }
    }
}
