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

        //private List<KeyValuePair<GridItem, GridMoveInfo>> _gridMoveInfos = new List<KeyValuePair<GridItem, GridMoveInfo>>();

        private readonly Dictionary<GridItem, GridMoveInfo> _gridMoveInfoMap = new Dictionary<GridItem, GridMoveInfo>();
        private readonly HashSet<GridItem> _deletedGridItems = new HashSet<GridItem>(); 
        private readonly HashSet<GridItem> _newCreatedGridItems = new HashSet<GridItem>(); 

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

            List<IGridActionCommand> commands = new List<IGridActionCommand>();
            AddRandomItems(2, commands);
            commands.ForEach(c => c.Do(this));
        }

        private void AddRandomItems(int initRandomEntityCount, List<IGridActionCommand> commands)
        {
            List<GridHolder> emptyHolders = new List<GridHolder>();

            for (int row = 0; row < ROW_COUNT; ++row)
            {
                for (int col = 0; col < COL_COUNT; ++col)
                {
                    var holder = GetGridHolder(row, col);
                    if (holder.IsTempNull)
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

                    if (gridHolder.IsTempNull)
                    {
                        commands.Add(CreateNewCreatedCommand(gridHolder, GridItem.GetRandomEntity(gridHolder)));
                        //gridHolder.SetGridEntity(GridItem.GetRandomEntity(gridHolder));
                        --initRandomEntityCount;
                    }
                }
            }
        }

        private void AddRandomItemsAfterMoved(List<IGridActionCommand> commands)
        {
            AddRandomItems(1, commands);
        }

        public GridHolder GetGridHolder(int row, int col)
        {
            return _gridData[row][col];
        }

        public List<GridItem> GetAllGridItems()
        {
            var gridEntities = new List<GridItem>();
            for (int row = 0; row < ROW_COUNT; row++)
            {
                for (int col = 0; col < COL_COUNT; col++)
                {
                    GridHolder gridHolder = GetGridHolder(row, col);
                    if (!gridHolder.IsNull)
                    {
                        gridEntities.Add(gridHolder.GridItem);
                    }
                }
            }
            gridEntities.AddRange(_deletedGridItems);
            return gridEntities;
        }

        public void Move(MoveDirection direction)
        {
            _gridMoveInfoMap.Clear();
            _deletedGridItems.Clear();
            
            List<IGridActionCommand> commands = new List<IGridActionCommand>();

            switch (direction)
            {
                case MoveDirection.Up:
                    MoveUp(commands);
                    break;
                case MoveDirection.Down:
                    MoveDown(commands);
                    break;
                case MoveDirection.Left:
                    MoveLeft(commands);
                    break;
                case MoveDirection.Right:
                    MoveRight(commands);
                    break;
            }

            if (commands.Count > 0)
            {
                AddRandomItemsAfterMoved(commands);
                foreach (var command in commands)
                {
                    command.Do(this);
                }
            }
        }

        public void MoveUp(List<IGridActionCommand> commands)
        {
            var gridHolders = new GridHolder[ROW_COUNT];

            for (int col = 0; col < COL_COUNT; ++col)
            {
                int index = 0;

                for (int row = 0; row < ROW_COUNT; ++row)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }

                MoveGrid(gridHolders, commands);
            }
        }

        public void MoveDown(List<IGridActionCommand> commands)
        {
            var gridHolders = new GridHolder[ROW_COUNT];

            for (int col = 0; col < COL_COUNT; ++col)
            {
                int index = 0;
                for (int row = ROW_COUNT-1; row >= 0; --row)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }
                MoveGrid(gridHolders, commands);
            }
        }

        public void MoveLeft(List<IGridActionCommand> commands)
        {
            var gridHolders = new GridHolder[COL_COUNT];

            for (int row = 0; row < ROW_COUNT; ++row)
            {
                int index = 0;
                for (int col = 0; col < COL_COUNT; ++col)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }
                MoveGrid(gridHolders, commands);
            }
        }

        public void MoveRight(List<IGridActionCommand> commands)
        {
            var gridHolders = new GridHolder[COL_COUNT];

            for (int row = 0; row < ROW_COUNT; ++row)
            {
                int index = 0;
                for (int col = COL_COUNT-1; col >= 0; --col)
                {
                    gridHolders[index++] = GetGridHolder(row, col);
                }
                MoveGrid(gridHolders, commands);
            }
        }

        private void MoveGrid(GridHolder[] gridHolders, List<IGridActionCommand> commands)
        {
            foreach (var gridHolder in gridHolders)
            {
                gridHolder.TempItem = gridHolder.GridItem;
            }

            int top = 0;

            for (int current = 1; current < gridHolders.Length;)
            {
                var currentGridHolder = gridHolders[current];
                var topGridHolder = gridHolders[top];

                if (currentGridHolder.IsTempNull)
                {
                    ++current;
                    continue;
                }

                var currentGridEntity = currentGridHolder.TempItem;

                if (topGridHolder.IsTempNull)
                {
                    commands.Add(CreateMoveCommand(currentGridHolder, topGridHolder));

                    gridHolders[current].TempItem = null;
                    gridHolders[top].TempItem = currentGridEntity;
                    ++current;
                    continue;
                }

                var topGridEntity = topGridHolder.TempItem;

                if (currentGridEntity.CanMerge(topGridEntity))
                {
                    // 删除被合并方格
                    commands.Add(CreateGridDeletionCommand(topGridHolder));

                    // 移动合并方格
                    commands.Add(CreateMoveCommand(currentGridHolder, topGridHolder));

                    // 删除移动后的方格
                    commands.Add(CreateGridDeletionCommand(topGridHolder));

                    GridItem newGridItem = currentGridEntity.Merge(topGridEntity);

                    // 创建新的合并后的方块
                    commands.Add(CreateNewCreatedCommand(topGridHolder, newGridItem));

                    gridHolders[current].TempItem = null;
                    gridHolders[top].TempItem = newGridItem;
                    ++top;
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

        private static GridMoveCommand CreateMoveCommand(GridHolder fromHolder, GridHolder toHolder)
        {
            return new GridMoveCommand
                {
                    MoveInfo = new GridMoveInfo()
                        {
                            FromRow = fromHolder.Row,
                            FromCol = fromHolder.Col,
                            ToRow = toHolder.Row,
                            ToCol = toHolder.Col
                        }
                };
        }

        private static GridNewCreatedCommand CreateNewCreatedCommand(GridHolder holder, GridItem newGridItem)
        {
            return new GridNewCreatedCommand()
                {
                    Row = holder.Row,
                    Col = holder.Col,
                    NewGridItem = newGridItem
                };
        }

        private static GridDeletionCommand CreateGridDeletionCommand(GridHolder holder)
        {
            return new GridDeletionCommand()
                {
                    Row = holder.Row,
                    Col = holder.Col
                };
        }

        /// <summary>
        /// 添加方格位置移动信息
        /// </summary>
        /// <param name="gridItem"></param>
        /// <param name="moveInfo"></param>
        public void AddMoveInfo(GridItem gridItem, GridMoveInfo moveInfo)
        {
            _gridMoveInfoMap.Add(gridItem, moveInfo);
        }

        /// <summary>
        /// 获取方格位置移动信息
        /// </summary>
        /// <param name="gridItem"></param>
        /// <param name="moveInfo"></param>
        /// <returns></returns>
        public bool TryGetMoveInfo(GridItem gridItem, out GridMoveInfo moveInfo)
        {
            return _gridMoveInfoMap.TryGetValue(gridItem, out moveInfo);
        }

        /// <summary>
        /// 是否是被删除的方格
        /// </summary>
        /// <returns></returns>
        public bool IsDeletedGridItem(GridItem gridItem)
        {
            return _deletedGridItems.Contains(gridItem);
        }

        /// <summary>
        /// 添加被删除的方格
        /// </summary>
        /// <param name="gridItem"></param>
        public void AddDeletedGridItem(GridItem gridItem)
        {
            _deletedGridItems.Add(gridItem);
        }

        /// <summary>
        /// 是否是新创建的方格
        /// </summary>
        /// <param name="gridItem"></param>
        /// <returns></returns>
        public bool IsNewCreatedItem(GridItem gridItem)
        {
            return _newCreatedGridItems.Contains(gridItem);
        }

        /// <summary>
        /// 添加新创建的方格
        /// </summary>
        /// <param name="gridItem"></param>
        public void AddNewCreatedGridItem(GridItem gridItem)
        {
            _newCreatedGridItems.Add(gridItem);
        }
    }
}
