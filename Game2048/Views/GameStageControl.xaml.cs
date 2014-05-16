using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game2048.ViewModels;

namespace Game2048.Views
{
    /// <summary>
    /// GameStageControl.xaml 的交互逻辑
    /// </summary>
    public partial class GameStageControl : UserControl
    {
        private GameStageViewModel GameStageViewModel { get; set; }
        private readonly Style _gridItemStyle;

        public GameStageControl()
        {
            InitializeComponent();
            _gridItemStyle = FindResource("gameGridStyle") as Style;
        }

        public void Init()
        {
            GameStageViewModel = new GameStageViewModel();
            DataContext = GameStageViewModel;

            UpdateGridItems();
        }

        public void UpdateGridItems()
        {
            gameStagePanel.Children.Clear();

            double itemWidth = gameStagePanel.ActualWidth/GameStageViewModel.GridColCount;
            double itemHeight = gameStagePanel.ActualHeight/GameStageViewModel.GridRowCount;

            GameStageViewModel.GenerateGirdViewModels();

            if (GameStageViewModel.GridViewModels != null)
            {
                foreach (var gridViewModel in GameStageViewModel.GridViewModels)
                {
                    var itemControl = CreateGridItem(itemWidth, itemHeight, gridViewModel.Value);
                    double locX = gridViewModel.ToCol*itemWidth;
                    double locY = gridViewModel.ToRow*itemHeight;
                    Canvas.SetLeft(itemControl, locX);
                    Canvas.SetTop(itemControl, locY);
                    gameStagePanel.Children.Add(itemControl);
                }
            }
        }

        private UIElement CreateGridItem(double width, double height, object value)
        {
            Button button = new Button()
                {
                    Width = width,
                    Height = height,
                    Style = _gridItemStyle,
                    Content = value,
                    Focusable = false
                };
            return button;
        }

        private void GameStage_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    GameStageViewModel.MoveUp();
                    break;
                case Key.Down:
                    GameStageViewModel.MoveDown();
                    break;
                case Key.Left:
                    GameStageViewModel.MoveLeft();
                    break;
                case Key.Right:
                    GameStageViewModel.MoveRight();
                    break;
                default:
                    return;
            }

            UpdateGridItems();
            e.Handled = true;
        }
    }
}
