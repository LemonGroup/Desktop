using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeskTop.Views
{
    /// <summary>
    /// Interaction logic for CtrlDaylyStat.xaml
    /// </summary>
    public partial class CtrlDaylyStat : UserControl
    {
        public CtrlDaylyStat()
        {
            InitializeComponent();

        }

        private void AddColToDgStat(string header, IValueConverter converter, object parameter)
        {
            var col = new DataGridTextColumn();
            col.Header = header;
            col.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            col.Binding = new Binding { Converter = converter, ConverterParameter = parameter };
            dgStat.Columns.Add(col);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dgStat.Columns.Clear();
            var data = DataContext as IEnumerable<Statistics.StatRow>;
            if (data == null)
            {
                DataContext = null;
                return;
            }
            var groupedData = data.GroupBy(r => r.Date);
            var keywords = data.Select(r => r.KeyWord).Distinct();
            AddColToDgStat("Дата", Converters.DateColConv, keywords.First());
            var dateCol = dgStat.Columns[0] as DataGridTextColumn;
            dateCol.Binding.StringFormat = "dd.MM.yyyy";
            dateCol.Width = 70;
            foreach (var keyword in keywords) // создание столбцов 
                 AddColToDgStat(keyword, Converters.ColConv, keyword);
            dgStat.DataContext = groupedData;
        }
    }
}
