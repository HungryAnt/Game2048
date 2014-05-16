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
            var gridEntities = GameManager.Instance.GameCore.GetAllGridEntities();

            List<GridViewModel> gridViewModels = new List<GridViewModel>();

            foreach (var gridEntity in gridEntities)
            {
                var gridViewModel = new GridViewModel()
                    {
                        Value = gridEntity.Value,
                        ToRow = gridEntity.Owner.Row,
                        ToCol = gridEntity.Owner.Col,
                    };
                gridViewModels.Add(gridViewModel);
            }

            GridViewModels = gridViewModels;
        }

        public void MoveUp()
        {
            GameManager.Instance.GameCore.MoveUp();
            GenerateGirdViewModels();
        }

        public void MoveDown()
        {
            GameManager.Instance.GameCore.MoveDown();
            GenerateGirdViewModels();
        }

        public void MoveLeft()
        {
            GameManager.Instance.GameCore.MoveLeft();
            GenerateGirdViewModels();
        }

        public void MoveRight()
        {
            GameManager.Instance.GameCore.MoveRight();
            GenerateGirdViewModels();
        }
    }
}
