using System.Windows.Controls;
using sea_battle.ViewModels;

namespace sea_battle.Views
{
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            this.DataContext = new GameViewModel();
        }
    }
}