using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperClassLibrary
{
    public class GameCell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsVisited { get; set; }
        public bool Live { get; set; }
        public int NumOfNeighbors { get; set; }

        public GameCell()
        {
            Row = -1;
            Col = -1;
            IsVisited = false;
            Live = false;
            NumOfNeighbors = 0;
        }

    }
}
