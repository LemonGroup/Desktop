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
using System.Windows.Shapes;
using DeskTop.Util;


namespace DeskTop.Views
{
    /// <summary>
    /// Interaction logic for FrmPersons.xaml
    /// </summary>
    public partial class FrmSites : Window
    {
        public FrmSites()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var site = Repos.Sites.Create("");
            var f = new FrmEditSite(site) { Title = "Добавление сайта" };
            if (f.ShowDialog() != true) return;
            Repos.Sites.Add(site);
            UiHelper.RefreshCollection(lstSites.ItemsSource);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить выбранные объекты?", "Удаление элементов",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            foreach (object item in lstSites.SelectedItems)
            {
                var site = item as Site;
                Repos.Sites.Delete(site.Id);
            }
            UiHelper.RefreshCollection(lstSites.ItemsSource);
        }



        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstSites.SelectedItems.Count == 0) return;
            var site = (Site)lstSites.SelectedItems[0];
            site.BeginEdit();
            var f = new FrmEditSite(site) { Title = "Редактивароние сайта" };
            if (f.ShowDialog() != true) return;
            site.EndEdit();
            UiHelper.RefreshCollection(lstSites.ItemsSource);
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            Repos.Sites.Save();
        }
    }
}
