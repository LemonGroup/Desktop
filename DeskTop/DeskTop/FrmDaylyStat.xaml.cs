using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeskTop
{
    /// <summary>
    /// Выводит статистику рейтинга по дням для каждой персоны
    /// </summary>
    public partial class FrmDaylyStat : Window
    {
        private void AddColToDgStat(string header, IValueConverter converter, object parameter)
        {
            var col = new DataGridTextColumn();
            col.Header = header;
            col.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            col.Binding = new Binding { Converter = converter, ConverterParameter = parameter };
            dgStat.Columns.Add(col);
        }
        public FrmDaylyStat(IEnumerable<Statistics.StatRow> data)
        {
            InitializeComponent();
            var groupedData = data.GroupBy(r => r.Date);
            var keywords = data.Select(r => r.KeyWord).Distinct();
            var converter = new ColConverter();
            var dateConverter = new DateColConverter();
            AddColToDgStat("Дата", dateConverter, keywords.First());
            var dateCol = dgStat.Columns[0] as DataGridTextColumn;
            dateCol.Binding.StringFormat = "dd.MM.yyyy";
            dateCol.Width = 70;
            foreach (var keyword in keywords) // создание столбцов 
                AddColToDgStat(keyword, converter, keyword);
            dgStat.DataContext = groupedData;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    class DateColConverter : IValueConverter
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
    class ColConverter : IValueConverter
    {
        public static Statistics.StatRow GetRow(object value, object parameter)
        {
            var data = value as IEnumerable<Statistics.StatRow>;
            if (data == null) return null;
            IEnumerable<Statistics.StatRow> resRows = data.Where(r => r.KeyWord == parameter.ToString());
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
