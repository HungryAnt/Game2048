using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GameCore
    {
        private static readonly Random RANDOM = new Random();

        private const int ROW_COUNT = 4;
        private const int COL_COUNT = 4;

        private GridHolder[][] _gridData;

        public GameCore()
        {
            InitGrid();
        }

        public void InitGrid()
        {
            _gridData = new GridHolder[ROW_COUNT][];
            for (int row = 0; row < ROW_COUNT; ++row)
            {
                _gridData[row] = new GridHolder[COL_COUNT];
                for (int col = 0; col < COL_COUNT; ++col)
                {
                    _gridData[row][col] = new GridHolder(row, col);
                }
            }

            int initRandomEntityCount = 2;

            while (initRandomEntityCount > 0)
            {
                int row = RANDOM.Next(ROW_COUNT);
                int col = RANDOM.Next(COL_COUNT);

                var gridHolder = _gridData[row][col];

                if (gridHolder.IsNull)
                {
                    gridHolder.SetGridEntity(GridEntity.GetRandomEntity(gridHolder));
                    --initRandomEntityCount;
                }
            }
        }

        private GridHolder GetGridHolder(int row, int col)
        {
            return _gridData[row][col];
        }

        public void MoveDown()
        {
            GridHolder[] gridHolders = new GridHolder[ROW_COUNT];

            for (int col = 0; col < COL_COUNT; ++col)
            {
                for (int row = 0; row < ROW_COUNT; ++row)
                {
                    gridHolders[row] = GetGridHolder(row, col);
                    
                }
            }
        }

        private void MoveGrid(GridHolder[] gridHolders)
        {
            int top = 0;
            var topGridHolder = gridHolders[top];

            for (int current = 1; current < gridHolders.Length; current++)
            {
                var currentGridHolder = gridHolders[current];
                if (currentGridHolder.IsNull)
                {
                    continue;
                }

                if (topGridHolder.IsNull)
                {
                    gridHolders[current].SetGridEntity(null);
                    gridHolders[top].SetGridEntity(currentGridHolder.GridEntity);
                    top = current;
                    continue;
                }

                var currentGridEntity = currentGridHolder.GridEntity;
                var topGridEntity = topGridHolder.GridEntity;

                if (currentGridEntity.TryMerge(topGridEntity))
                {
                    gridHolders[current].SetGridEntity(null);
                    gridHolders[top].SetGridEntity(currentGridEntity);
                    top = current;
                    currentGridEntity.IsMerged = true;

                }
            }
        }
    }
}
