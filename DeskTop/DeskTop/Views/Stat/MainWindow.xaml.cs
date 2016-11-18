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
using DeskTop.Persons;
using DeskTop.Sites;

namespace DeskTop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Util.ElementSelector<Site> siteSelector;
        protected Util.ElementSelector<Person> personSelector;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            siteSelector = new Util.ElementSelector<Site>(Repos.Sites.Items);
            dgSites.DataContext = siteSelector.ToList(); // List для того чтобы элементы можно было изменять (ставить голочки)
            personSelector = new Util.ElementSelector<Person>(Repos.Persons.Items);
            dgPersons.DataContext = personSelector.ToList();
            dtaFrom.SelectedDate = DateTime.Now.AddDays(-1);
            dtaTo.SelectedDate = DateTime.Now;
        }

        private void btnShowStat_Click(object sender, RoutedEventArgs e)
        {
            //dgKeyWords.DataContext = Statistics.GetStatistics(siteSelector.SelectedElements);
        }

        private void btnShowEveryDayStat_Click(object sender, RoutedEventArgs e)
        {
            var data = Statistics.GetDaylyStat(new DateTime(2016, 10, 25), new DateTime(2016, 11, 05));
            var f = new FrmDaylyStat(data);
            f.ShowDialog();
        }
    }
}
