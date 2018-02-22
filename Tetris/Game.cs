using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    [Serializable]
    public class Game
    {
        private List<TetrisObject> listOfTetrisObject;
        bool addObstacles;
        bool addGoldenPoint;

        public Game(List<TetrisObject> _listOfTetrisObject, bool _addObstacle=false, bool _addGodelnPoint=false)
        {
            listOfTetrisObject = _listOfTetrisObject;
            addObstacles = _addObstacle;
            addGoldenPoint = _addGodelnPoint;

        }

        public List<TetrisObject> ListOfTetrisObject
        {
            get
            {
                return this.listOfTetrisObject;
            }

        }

        public static bool operator ==(Game g1, Game g2)
        {
            if (g1.ListOfTetrisObject.Count != g2.ListOfTetrisObject.Count)
                return false;
            for (int i=0; i < g1.ListOfTetrisObject.Count;i++)
            {
                if (!g2.ListOfTetrisObject.Contains(g1.ListOfTetrisObject[i]))
                    return false;
            }
            if (g1.addObstacles != g2.addObstacles)
                return false;
            if (g1.addGoldenPoint != g2.addGoldenPoint)
                return false;
            return true;
        }

        public static bool operator !=(Game g1, Game g2)
        {
            if (g1 == g2)
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Game))
                return false;

            return this == (Game)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



    }
}
