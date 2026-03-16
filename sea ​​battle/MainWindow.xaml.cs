using sea_battle.Views;
using System.Windows;

namespace sea_battle
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContainer.Children.Add(new MenuView());
        }
    }
}