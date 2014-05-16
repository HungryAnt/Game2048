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

namespace Game2048
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameViewModel GameViewModel { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            GameViewModel = new GameViewModel();
            DataContext = GameViewModel;


            AddItem();
        }

        private void AddItem()
        {
            Style style = FindResource("gameGridStyle") as Style;
            Button button = new Button()
                {
                    Width = 50,
                    Height = 50,
                    Style = style
                };

            gameStagePanel.Children.Add(button);
        }
    }
}
