using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using sea_battle.Models;

namespace sea_battle.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public GameField PlayerField { get; set; }
        public GameField EnemyField { get; set; }
        private Random _rand = new Random();

        private MediaPlayer _backMusic = new MediaPlayer();
        private bool _isMusicPlaying = false;

        public RelayCommand ToggleMusicCommand { get; }

        public int PlayerShipsCount => PlayerField.Ships.Count(s => !s.IsSunk);
        public int EnemyShipsCount => EnemyField.Ships.Count(s => !s.IsSunk);

        public GameViewModel()
        {
            PlayerField = new GameField();
            EnemyField = new GameField();

            PlayerField.RandomPlaceShips();
            EnemyField.RandomPlaceShips();

            ToggleMusicCommand = new RelayCommand(obj => ToggleMusic());

            StartBackgroundMusic();

            foreach (var cell in EnemyField.Cells)
            {
                cell.ShootCommand = new RelayCommand(obj =>
                {
                    bool hit = EnemyField.Shoot(cell.X, cell.Y);
                    OnPropertyChanged(nameof(EnemyShipsCount));

                    if (EnemyField.Ships.All(s => s.IsSunk))
                    {
                        MessageBox.Show("Вітаємо! Ви перемогли!");
                        return;
                    }

                    if (!hit)
                    {
                        ComputerTurn();
                    }
                });
            }
        }

        public void StartBackgroundMusic()
        {
            try
            {
                _backMusic.Open(new Uri("background_music.mp3.wav", UriKind.Relative));
                _backMusic.Volume = 0.5;

                _backMusic.MediaEnded += (s, e) =>
                {
                    _backMusic.Position = TimeSpan.Zero;
                    _backMusic.Play();
                };

                _backMusic.Play();
                _isMusicPlaying = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка відтворення музики: " + ex.Message);
            }
        }

        public void ToggleMusic()
        {
            if (_isMusicPlaying)
                _backMusic.Pause();
            else
                _backMusic.Play();

            _isMusicPlaying = !_isMusicPlaying;
        }

        private void ComputerTurn()
        {
            bool acted = false;
            while (!acted)
            {
                int x = _rand.Next(0, 10);
                int y = _rand.Next(0, 10);

                var targetCell = PlayerField.GetCell(x, y);

                if (targetCell.Status == CellStatus.Hit || targetCell.Status == CellStatus.Miss || targetCell.Status == CellStatus.Destroyed)
                {
                    continue;
                }

                bool hit = PlayerField.Shoot(x, y);
                OnPropertyChanged(nameof(PlayerShipsCount));

                if (PlayerField.Ships.All(s => s.IsSunk))
                {
                    MessageBox.Show("От халепа! Ви програли...");
                    return;
                }

                if (!hit)
                {
                    acted = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}