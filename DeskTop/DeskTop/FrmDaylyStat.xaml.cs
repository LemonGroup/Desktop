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
        public FrmDaylyStat(IEnumerable<Statistics.StatRow> data)
        {
            InitializeComponent();
            var groupedData = data.GroupBy(r => r.Date);
            var keywords = data.Select(r => r.KeyWord).Distinct();
            ColConverter converter = new ColConverter();
            foreach(var keyword in keywords) // создание столбцов 
            {
                var col = new DataGridTextColumn();
                col.Header = keyword;
                col.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                col.Binding = new Binding() { Converter = converter, ConverterParameter = keyword };
                dgStat.Columns.Add(col);
            }
            dgStat.DataContext = groupedData;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
     class ColConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as IEnumerable<Statistics.StatRow>;
            if (data == null) return "";
            var resRow = data.Where(r => r.KeyWord == parameter.ToString());
            if (!resRow.Any()) return "";
            return resRow.First().Rank;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
