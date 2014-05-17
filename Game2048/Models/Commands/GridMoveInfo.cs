using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GridMoveInfo
    {
        public int FromRow { get; set; }
        public int FromCol { get; set; }
        public int ToRow { get; set; }
        public int ToCol { get; set; }
    }
}
