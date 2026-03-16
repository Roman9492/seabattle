using System.Collections.Generic;
using System.Linq;

namespace sea_battle.Models
{
    public class Ship
    {
        public List<Cell> Cells { get; set; } = new List<Cell>();

        public bool IsSunk => Cells.All(c => c.Status == CellStatus.Hit || c.Status == CellStatus.Destroyed);

        public void MarkAsDestroyed()
        {
            foreach (var cell in Cells)
                cell.Status = CellStatus.Destroyed;
        }
    }
}