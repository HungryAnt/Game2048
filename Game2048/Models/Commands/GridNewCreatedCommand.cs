using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    class GridNewCreatedCommand : IGridActionCommand
    {
        public GridEntity NewGridEntity { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public void Do(GameCore gameCore)
        {
            var gridHolder = gameCore.GetGridHolder(Row, Col);
            gridHolder.SetGridEntity(NewGridEntity);
        }

        public void Undo(GameCore gameCore)
        {
            var gridHolder = gameCore.GetGridHolder(Row, Col);
            gridHolder.SetGridEntity(null);
        }
    }
}
