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

        private GridItem DeletedGridItem { get; set; }

        public void Do(GameCore gameCore)
        {
            var gridHolder = gameCore.GetGridHolder(Row, Col);
            DeletedGridItem = gridHolder.GridItem;
            gridHolder.SetGridEntity(null);
            gameCore.AddDeletedGridItem(DeletedGridItem);
        }

        public void Undo(GameCore gameCore)
        {
            var gridHolder = gameCore.GetGridHolder(Row, Col);
            gridHolder.SetGridEntity(DeletedGridItem);
        }
    }
}
