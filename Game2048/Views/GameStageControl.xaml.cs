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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game2048.Models;
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
                    var itemControl = CreateGridItem(itemWidth, itemHeight, gridViewModel);

                    if ((gridViewModel.GridStates & GridStates.Moved) == GridStates.Moved)
                    {
                        ProcessMoveGridItem(itemControl, gridViewModel, itemWidth, itemHeight);
                    }
                    else
                    {
                        double locX = gridViewModel.ToCol * itemWidth;
                        double locY = gridViewModel.ToRow * itemHeight;
                        Canvas.SetLeft(itemControl, locX);
                        Canvas.SetTop(itemControl, locY);
                    }

                    if ((gridViewModel.GridStates & GridStates.Deleted) == GridStates.Deleted)
                    {
                        ProcessDeleteGridItem(itemControl);
                    }

                    if ((gridViewModel.GridStates & GridStates.NewCreated) == GridStates.NewCreated)
                    {
                        ProcessNewCreatedGridItem(itemControl);
                    }

                    gameStagePanel.Children.Add(itemControl);
                }
            }
        }

        private UIElement CreateGridItem(double width, double height, GridViewModel gridViewModel)
        {
            Button button = new Button()
                {
                    Width = width,
                    Height = height,
                    Style = _gridItemStyle,
                    Content = gridViewModel,
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

        private static DoubleAnimation CreateMoveAnimation(double from, double to)
        {
            return new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(0.1)));
        }

        private void ProcessMoveGridItem(UIElement itemControl, GridViewModel gridViewModel, double itemWidth, double itemHeight)
        {
            GridMoveInfo moveInfo = gridViewModel.MoveInfo;
            if (moveInfo.FromCol != moveInfo.ToCol)
            {
                itemControl.BeginAnimation(
                    Canvas.LeftProperty,
                    CreateMoveAnimation(moveInfo.FromCol * itemWidth, moveInfo.ToCol * itemWidth));
            }
            else
            {
                Canvas.SetLeft(itemControl, moveInfo.ToCol * itemWidth);
            }

            if (moveInfo.FromRow != moveInfo.ToRow)
            {
                itemControl.BeginAnimation(
                    Canvas.TopProperty,
                    CreateMoveAnimation(moveInfo.FromRow * itemHeight, moveInfo.ToRow * itemHeight));
            }
            else
            {
                Canvas.SetTop(itemControl, moveInfo.ToRow * itemHeight);
            }
        }

        private void ProcessDeleteGridItem(UIElement itemControl)
        {
            var animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.01)));
            animation.BeginTime = TimeSpan.FromSeconds(0.09);

            itemControl.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        private void ProcessNewCreatedGridItem(UIElement itemControl)
        {
            itemControl.RenderTransformOrigin = new Point(0.5, 0.5);

            var scaleTransform = new ScaleTransform(0, 0);
            itemControl.RenderTransform = scaleTransform;

            //itemControl.SetValue(UIElement.OpacityProperty, 0);
            var opacityAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.1)));

            itemControl.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            var animation = CreateRenderScaleAnimation();
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        private static DoubleAnimation CreateRenderScaleAnimation()
        {
            return new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.2)))
                {
                    BeginTime = TimeSpan.FromSeconds(0)
                };
        }
    }
}
