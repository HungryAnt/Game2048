using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2048.Models;
using Gods.Foundation;

namespace Game2048.ViewModels
{
    public class GridViewModel
    {
        public object Value { get; set; }

        public int Level { get; set; }

        public int FromRow { get; set; }

        public int FromCol { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public GridStates GridStates { get; set; }

        public GridMoveInfo MoveInfo { get; set; }
    }
}
