﻿using System;
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

namespace DeskTop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Util.ElementSelector<string> siteSelector;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            siteSelector = new Util.ElementSelector<string>(SiteList.GetSiteList());
            dgSites.DataContext = siteSelector.ToList(); // для того чтобы элементы можно было изменять (ставить голочки)
        }

        private void btnShowStat_Click(object sender, RoutedEventArgs e)
        {
            dgKeyWords.DataContext = Statistics.GetStatistics(siteSelector.SelectedElements);
        }

        private void btnShowEveryDayStat_Click(object sender, RoutedEventArgs e)
        {
            var data = Statistics.GetDaylyStat(new DateTime(2016, 10, 25), new DateTime(2016, 11, 05));
            var f = new FrmDaylyStat(data);
            f.ShowDialog();
        }
    }
}
