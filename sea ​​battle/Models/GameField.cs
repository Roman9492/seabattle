using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace sea_battle.Models
{
    public class GameField
    {
        public const int Size = 10;
        private static readonly int[] DefaultShipConfiguration = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        public ObservableCollection<Cell> Cells { get; set; }
        public List<Ship> Ships { get; set; }

        public GameField()
        {
            Cells = new ObservableCollection<Cell>();
            Ships = new List<Ship>();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Cells.Add(new Cell(x, y));
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            return Cells.FirstOrDefault(c => c.X == x && c.Y == y);
        }

        public void RandomPlaceShips()
        {
            Random rand = new Random();
            int[] shipSizes = DefaultShipConfiguration;

            foreach (int size in shipSizes)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = rand.Next(0, Size);
                    int y = rand.Next(0, Size);
                    bool isVertical = rand.Next(0, 2) == 0;

                    if (CanPlaceShip(x, y, size, isVertical))
                    {
                        PlaceShipOnCells(x, y, size, isVertical);
                        placed = true;
                    }
                }
            }
        }

        private void PlaceShipOnCells(int x, int y, int size, bool isVertical)
        {
            Ship ship = new Ship();
            for (int i = 0; i < size; i++)
            {
                int currX = isVertical ? x : x + i;
                int currY = isVertical ? y + i : y;
                var cell = GetCell(currX, currY);
                cell.Status = CellStatus.Ship;
                ship.Cells.Add(cell);
            }
            Ships.Add(ship);
        }

        private bool CanPlaceShip(int x, int y, int size, bool isVertical)
        {
            for (int i = 0; i < size; i++)
            {
                int currX = isVertical ? x : x + i;
                int currY = isVertical ? y + i : y;

                if (currX >= Size || currY >= Size) return false;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        var neighbor = GetCell(currX + dx, currY + dy);
                        if (neighbor != null && neighbor.Status == CellStatus.Ship) return false;
                    }
                }
            }
            return true;
        }

        public bool Shoot(int x, int y)
        {
            var cell = GetCell(x, y);
            if (cell == null || cell.Status == CellStatus.Hit || cell.Status == CellStatus.Miss || cell.Status == CellStatus.Destroyed)
                return false;

            if (cell.Status == CellStatus.Ship)
            {
                cell.Status = CellStatus.Hit;
                var ship = Ships.FirstOrDefault(s => s.Cells.Contains(cell));

                if (ship != null && ship.IsSunk)
                {
                    MarkShipDestroyedAndBuffer(ship);
                }
                return true;
            }

            cell.Status = CellStatus.Miss;
            return false;
        }

        private void MarkShipDestroyedAndBuffer(Ship ship)
        {
            foreach (var shipCell in ship.Cells)
            {
                shipCell.Status = CellStatus.Destroyed;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        var neighbor = GetCell(shipCell.X + dx, shipCell.Y + dy);

                        if (neighbor != null && neighbor.Status == CellStatus.Empty)
                        {
                            neighbor.Status = CellStatus.Miss;
                        }
                    }
                }
            }
        }
    }
}
