using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GridMoveCommand : IGridActionCommand
    {
        public GridMoveInfo MoveInfo { get; set; }

        public void Do(GameCore gameCore)
        {
            var fromHolder = gameCore.GetGridHolder(MoveInfo.FromRow, MoveInfo.FromCol);
            var toHolder = gameCore.GetGridHolder(MoveInfo.ToRow, MoveInfo.ToCol);

            var gridEntity = fromHolder.GridEntity;
            fromHolder.SetGridEntity(null);

            toHolder.SetGridEntity(gridEntity);

            gameCore.AddMoveInfo(gridEntity, MoveInfo);
        }

        public void Undo(GameCore gameCore)
        {
            var fromHolder = gameCore.GetGridHolder(MoveInfo.FromRow, MoveInfo.FromCol);
            var toHolder = gameCore.GetGridHolder(MoveInfo.ToRow, MoveInfo.ToCol);

            var gridEntity = toHolder.GridEntity;
            toHolder.SetGridEntity(null);

            fromHolder.SetGridEntity(gridEntity);
        }
    }
}
