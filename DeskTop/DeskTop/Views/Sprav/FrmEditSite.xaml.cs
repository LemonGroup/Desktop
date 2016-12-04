using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FrmEditPerson.xaml
    /// </summary>
    public partial class FrmEditSite : Window
    {
        private Site site;
        public FrmEditSite(Site site)
        {
            InitializeComponent();
            DataContext = this.site = site;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCansel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnCansel_Click_1(object sender, RoutedEventArgs e)
        {
            site.CancelEdit();
        }
    }
}
