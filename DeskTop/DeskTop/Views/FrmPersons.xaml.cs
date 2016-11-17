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

namespace DeskTop.Persons
{
    /// <summary>
    /// Interaction logic for FrmPersons.xaml
    /// </summary>
    public partial class FrmPersons : Window
    {
        public FrmPersons()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var person = new Person("");
            var f = new FrmEditPerson(person) {Title = "Добавление персоны"};
            if (f.ShowDialog()!=true) return;
            Repos.Persons.Add(person);
            UiHelper.RefreshCollection(lstPersons.ItemsSource);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить выбранные объекты?", "Удаление элементов",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            foreach (object item in lstPersons.SelectedItems)
            {
                var person = item as Person;
                Repos.Persons.Delete(person.Name);
            }
            UiHelper.RefreshCollection(lstPersons.ItemsSource);
        }



        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstPersons.SelectedItems.Count == 0) return;
            var person = (Person)lstPersons.SelectedItems[0];
            string oldName = person.Name;
            var f = new FrmEditPerson(person) { Title = "Редактивароние персоны" };
            if (f.ShowDialog() != true) return;
            Repos.Persons.Update(oldName, person);
            UiHelper.RefreshCollection(lstPersons.ItemsSource);
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
