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

        public GameStageViewModel()
        {
        }

        void Init()
        {
            GridViewModel gridViewModel = new GridViewModel()
                {
                    Value = "2048"
                };
        }

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
            var gridEntities = gameCore.GetAllGridEntities();

            List<GridViewModel> gridViewModels = new List<GridViewModel>();

            foreach (var gridEntity in gridEntities)
            {
                var gridViewModel = new GridViewModel()
                    {
                        Value = gridEntity.Value,
                        ToRow = gridEntity.Owner.Row,
                        ToCol = gridEntity.Owner.Col,
                    };

                GridMoveInfo moveInfo;
                if (gameCore.TryGetMoveInfo(gridEntity, out moveInfo))
                {
                    gridViewModel.GridStates |= GridStates.Moved;
                    gridViewModel.MoveInfo = moveInfo;
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
    }
}
