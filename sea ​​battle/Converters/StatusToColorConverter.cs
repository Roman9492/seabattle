using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using sea_battle.Models;

namespace sea_battle.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CellStatus status = (CellStatus)value;

            if (status == CellStatus.Ship && parameter?.ToString() == "Hide")
            {
                return Brushes.AliceBlue;
            }

            switch (status)
            {
                case CellStatus.Ship:
                    return Brushes.Gray;
                case CellStatus.Miss:
                    return Brushes.LightBlue;
                case CellStatus.Hit:
                    return Brushes.Orange;
                case CellStatus.Destroyed:
                    return Brushes.Red;
                default:
                    return Brushes.AliceBlue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}