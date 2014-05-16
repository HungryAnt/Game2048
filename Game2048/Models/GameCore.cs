using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GameCore
    {
        private static readonly Random RANDOM = new Random();

        public const int ROW_COUNT = 4;
        public const int COL_COUNT = 4;

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

            AddRandomItems(2);
        }

        private void AddRandomItems(int initRandomEntityCount)
        {
            List<GridHolder> emptyHolders = new List<GridHolder>();

            for (int row = 0; row < ROW_COUNT; ++row)
            {
                for (int col = 0; col < COL_COUNT; ++col)
                {
                    var holder = GetGridHolder(row, col);
                    if (holder.IsNull)
                    {
                        emptyHolders.Add(holder);
                    }
                }
            }

            if (emptyHolders.Count >= initRandomEntityCount)
            {
                while (initRandomEntityCount > 0)
                {
                    int randIndex = RANDOM.Next(emptyHolders.Count);

                    var gridHolder = emptyHolders[randIndex];

                    if (gridHolder.IsNull)
                    {
                        gridHolder.SetGridEntity(GridEntity.GetRandomEntity(gridHolder));
                        --initRandomEntityCount;
                    }
                }
            }
        }

        private void AddRandomItemsAfterMoved()
        {
            AddRandomItems(1);
        }

        private GridHolder GetGridHolder(int row, int col)
        {
            return _gridData[row][col];
        }

        public List<GridEntity> GetAllGridEntities()
        {
            var gridEntities = new List<GridEntity>();
            for (int row = 0; row < ROW_COUNT; row++)
            {
                for (int col = 0; col < COL_COUNT; col++)
                {
                    GridHolder gridHolder = GetGridHolder(row, col);
                    if (!gridHolder.IsNull)
                    {
                        gridEntities.Add(gridHolder.GridEntity);
                    }
                }
            }
            return gridEntities;
        }

        public void MoveUp()
        {
            GridHolder[] gridHolders = new GridHolder[ROW_COUNT];

            for (int col = 0; col < COL_COUNT; ++col)
            {
                int index = 0;

                for (int row = 0; row < ROW_COUNT; ++row)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }

                MoveGrid(gridHolders);
            }

            AddRandomItemsAfterMoved();
        }

        public void MoveDown()
        {
            GridHolder[] gridHolders = new GridHolder[ROW_COUNT];

            for (int col = 0; col < COL_COUNT; ++col)
            {
                int index = 0;
                for (int row = ROW_COUNT-1; row >= 0; --row)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }
                MoveGrid(gridHolders);
            }

            AddRandomItemsAfterMoved();
        }

        public void MoveLeft()
        {
            GridHolder[] gridHolders = new GridHolder[ROW_COUNT];

            for (int row = 0; row < ROW_COUNT; ++row)
            {
                int index = 0;
                for (int col = 0; col < COL_COUNT; ++col)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }
                MoveGrid(gridHolders);
            }

            AddRandomItemsAfterMoved();
        }

        public void MoveRight()
        {
            GridHolder[] gridHolders = new GridHolder[ROW_COUNT];

            for (int row = 0; row < ROW_COUNT; ++row)
            {
                int index = 0;
                for (int col = COL_COUNT-1; col >= 0; --col)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }
                MoveGrid(gridHolders);
            }

            AddRandomItemsAfterMoved();
        }

        private void MoveGrid(GridHolder[] gridHolders)
        {
            int top = 0;

            for (int current = 1; current < gridHolders.Length;)
            {
                var currentGridHolder = gridHolders[current];
                var topGridHolder = gridHolders[top];

                if (currentGridHolder.IsNull)
                {
                    ++current;
                    continue;
                }

                var currentGridEntity = currentGridHolder.GridEntity;

                if (topGridHolder.IsNull)
                {
                    gridHolders[current].SetGridEntity(null);
                    gridHolders[top].SetGridEntity(currentGridEntity);
                    ++current;
                    continue;
                }

                var topGridEntity = topGridHolder.GridEntity;

                if (currentGridEntity.TryMerge(topGridEntity))
                {
                    gridHolders[current].SetGridEntity(null);
                    gridHolders[top].SetGridEntity(currentGridEntity);
                    ++top;
                    currentGridEntity.IsMerged = true;
                    currentGridEntity.IsMoved = true;
                    topGridEntity.IsBeMerged = true;
                    ++current;
                }
                else
                {
                    ++top;
                    if (current == top)
                    {
                        ++current;
                    }
                }
            }
        }
    }
}
