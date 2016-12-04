using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using DeskTop.Util;


namespace DeskTop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected DeskTop.Util.ElementSelector<Site> siteSelector;
        protected Util.ElementSelector<Person> personSelector;
        private StatusIndicator sIndicator;
        public MainWindow()
        {
            InitializeComponent();
            sIndicator = new StatusIndicator(Repos.Status);
            sIndicator.Show();
            Repos.LoadRepos();
            while (!Repos.IsLoaded) { Thread.Sleep(200); }
            sIndicator.Close();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ReBindPersons();
            ReBindSites();
            dtaFrom.SelectedDate = DateTime.Now.AddDays(-1);
            dtaTo.SelectedDate = DateTime.Now;
            SetStatVisible();
        }
        private void ReBindPersons()
        {
            personSelector = new Util.ElementSelector<Person>(Repos.Persons.Items);
            dgPersons.DataContext = personSelector.ToList();
        }
        private void ReBindSites()
        {
            siteSelector = new Util.ElementSelector<Site>(Repos.Sites.Items);
            dgSites.DataContext = siteSelector.ToList(); // List для того чтобы элементы можно было изменять (ставить голочки)
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
        private async void btnShowStat_Click(object sender, RoutedEventArgs e)
        {
            sIndicator = new StatusIndicator(Statistics.Status);
            sIndicator.Show();
            IEnumerable<Statistics.StatRow> data = await Statistics.GetStatistics(dtaFrom.SelectedDate.Value, dtaTo.SelectedDate.Value,
                personSelector.SelectedElements, siteSelector.SelectedElements);
            ctrlStat.DataContext = data;
            SetStatVisible();
            sIndicator.Close();
        }

        private async void btnShowEveryDayStat_Click(object sender, RoutedEventArgs e)
        {
            sIndicator = new StatusIndicator(Statistics.StatusDayly);
            sIndicator.Show();
            IEnumerable<Statistics.StatRow> data = await Statistics.GetDaylyStat(dtaFrom.SelectedDate.Value, dtaTo.SelectedDate.Value,
                personSelector.SelectedElements, siteSelector.SelectedElements);
            if (data.Any())
            {
                ctrlDaylyStat.DataContext = data;
                SetDaylyStatVisible();
            }
            else
                ctrlDaylyStat.DataContext = null;
            sIndicator.Close();
        }

        private void btnPersonsEditor_Click(object sender, RoutedEventArgs e)
        {
            var f = new FrmPersons();
            f.Owner = this;
            f.ShowDialog();
            ReBindPersons();
        }

        private void btnSitesEditor_Click(object sender, RoutedEventArgs e)
        {
            var f = new FrmSites();
            f.Owner = this;
            f.ShowDialog();
            ReBindSites();
        }
    }
}
