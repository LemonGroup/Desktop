using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeskTop.Views
{

    public static class Converters
    {
        static Converters()
        {
            ColConv = new ColConverter();
            DateColConv = new DateColConverter();
        }
        public static DateColConverter DateColConv { get; }
        public static ColConverter ColConv { get; }
        public class DateColConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var resRow = ColConverter.GetRow(value, parameter);
                if (resRow == null) return "";
                return resRow.Date;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        public class ColConverter : IValueConverter
        {
            public static Statistics.StatRow GetRow(object value, object parameter)
            {
                var data = value as IEnumerable<Statistics.StatRow>;
                if (data == null) return null;
                IEnumerable<Statistics.StatRow> resRows = data.Where(r => r.Person == parameter.ToString());
                if (!resRows.Any()) return null;
                return resRows.First();
            }
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var resRow = GetRow(value, parameter);
                if (resRow == null) return "";
                return resRow.Rank;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    }
}
