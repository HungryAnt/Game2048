using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GridMoveCommand : IGridActionCommand
    {
        public int FromRow { get; set; }
        public int FromCol { get; set; }
        public int ToRow { get; set; }
        public int ToCol { get; set; }


        public void Do(GameCore gameCore)
        {
            var fromHolder = gameCore.GetGridHolder(FromRow, FromCol);
            var toHolder = gameCore.GetGridHolder(ToRow, ToCol);


            var gridEntity = fromHolder.GridEntity;
            fromHolder.SetGridEntity(null);

            toHolder.SetGridEntity(gridEntity);
        }

        public void Undo(GameCore gameCore)
        {
            var fromHolder = gameCore.GetGridHolder(FromRow, FromCol);
            var toHolder = gameCore.GetGridHolder(ToRow, ToCol);

            var gridEntity = toHolder.GridEntity;
            toHolder.SetGridEntity(null);

            fromHolder.SetGridEntity(gridEntity);
        }
    }
}
