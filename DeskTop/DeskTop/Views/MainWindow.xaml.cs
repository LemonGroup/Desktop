using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace DeskTop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected DeskTop.Util.ElementSelector<Site> siteSelector;
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
            SetStatVisible();
        }

        private void SetStatVisible()
        {
            ctrlDaylyStat.Visibility = Visibility.Collapsed;
            ctrlStat.Visibility = Visibility.Visible;
        }
        private void SetDaylyStatVisible()
        {
            ctrlDaylyStat.Visibility = Visibility.Visible;
            ctrlStat.Visibility = Visibility.Collapsed;
        }
        private void btnShowStat_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Statistics.StatRow> data = Statistics.GetStatistics(dtaFrom.SelectedDate.Value, dtaTo.SelectedDate.Value,
                personSelector.SelectedElements, siteSelector.SelectedElements);
            ctrlStat.DataContext = data;
            SetStatVisible();
        }

        private void btnShowEveryDayStat_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Statistics.StatRow> data = Statistics.GetDaylyStat(dtaFrom.SelectedDate.Value, dtaTo.SelectedDate.Value,
                personSelector.SelectedElements, siteSelector.SelectedElements);
            ctrlDaylyStat.DataContext = data;
            SetDaylyStatVisible();
        }

        private void btnPersonsEditor_Click(object sender, RoutedEventArgs e)
        {
            var f = new FrmPersons();
            f.ShowDialog();
        }

        private void btnSitesEditor_Click(object sender, RoutedEventArgs e)
        {
            var f = new FrmSites();
            f.ShowDialog();
        }
    }
}
