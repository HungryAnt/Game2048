using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    class GridDeletionCommand : IGridActionCommand
    {
        public int Row { get; set; }
        public int Col { get; set; }

        private GridEntity DeletedGridEntity { get; set; }

        public void Do(GameCore gameCore)
        {
            var gridHolder = gameCore.GetGridHolder(Row, Col);
            DeletedGridEntity = gridHolder.GridEntity;
            gridHolder.SetGridEntity(null);
        }

        public void Undo(GameCore gameCore)
        {
            var gridHolder = gameCore.GetGridHolder(Row, Col);
            gridHolder.SetGridEntity(DeletedGridEntity);
        }
    }
}
