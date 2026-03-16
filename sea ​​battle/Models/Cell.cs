using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input; 

namespace sea_battle.Models
{
    public enum CellStatus { Empty, Ship, Miss, Hit, Destroyed }

    /// <summary>
    /// Представляє окрему клітинку ігрового поля.
    /// </summary>
    public class Cell : INotifyPropertyChanged
    {
        private CellStatus _status;

        /// <summary> Координата X клітинки на ігровому полі. </summary>
        public int X { get; set; }

        /// <summary> Координата Y клітинки на ігровому полі. </summary>
        public int Y { get; set; }

        public ICommand ShootCommand { get; set; }

        public CellStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Status = CellStatus.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
