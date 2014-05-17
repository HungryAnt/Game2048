using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2048.Models;

namespace Game2048.ViewModels
{
    class GameStageViewModel
    {
        public List<GridViewModel> GridViewModels { get; private set; } 

        public int GridRowCount
        {
            get { return GameCore.ROW_COUNT; }
        }

        public int GridColCount
        {
            get { return GameCore.COL_COUNT; }
        }

        public void GenerateGirdViewModels()
        {
            var gameCore = GameManager.Instance.GameCore;
            var gridItems = gameCore.GetAllGridItems();

            List<GridViewModel> gridViewModels = new List<GridViewModel>();

            foreach (var gridItem in gridItems)
            {
                var gridViewModel = new GridViewModel()
                    {
                        Value = gridItem.Value,
                        Level = gridItem.Level,
                        Row = gridItem.Owner.Row,
                        Col = gridItem.Owner.Col,
                    };

                GridMoveInfo moveInfo;
                if (gameCore.TryGetMoveInfo(gridItem, out moveInfo))
                {
                    gridViewModel.GridStates |= GridStates.Moved;
                    gridViewModel.MoveInfo = moveInfo;
                }

                if (gameCore.IsDeletedGridItem(gridItem))
                {
                    gridViewModel.GridStates |= GridStates.Deleted;
                }

                if (gameCore.IsNewCreatedItem(gridItem))
                {
                    gridViewModel.GridStates |= GridStates.NewCreated;
                }

                gridViewModels.Add(gridViewModel);
            }

            GridViewModels = gridViewModels;
        }

        public void MoveUp()
        {
            GameManager.Instance.GameCore.Move(MoveDirection.Up);
            GenerateGirdViewModels();
        }

        public void MoveDown()
        {
            GameManager.Instance.GameCore.Move(MoveDirection.Down);
            GenerateGirdViewModels();
        }

        public void MoveLeft()
        {
            GameManager.Instance.GameCore.Move(MoveDirection.Left);
            GenerateGirdViewModels();
        }

        public void MoveRight()
        {
            GameManager.Instance.GameCore.Move(MoveDirection.Right);
            GenerateGirdViewModels();
        }

        public void EnlargeGrid(GridViewModel gridViewModel)
        {
            GameManager.Instance.GameCore.EnlargeGrid(gridViewModel.Row, gridViewModel.Col);
            GenerateGirdViewModels();
        }
    }
}
