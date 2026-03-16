using System.Windows;
using System.Windows.Controls;
using sea_battle.Models; 
using sea_battle.Views;  

namespace sea_battle.Views
{
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();

            BtnEasy.Click += (s, e) => StartGame(Difficulty.Easy);
            BtnMedium.Click += (s, e) => StartGame(Difficulty.Medium);
            BtnHard.Click += (s, e) => StartGame(Difficulty.Hard);
        }

        private void StartGame(Difficulty difficulty)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContainer.Children.Clear();
                mainWindow.MainContainer.Children.Add(new GameView());
            }
        }
    }
}