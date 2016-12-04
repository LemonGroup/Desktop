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
    public partial class FrmEditPerson : Window
    {
        private Person person;
        public FrmEditPerson(Person person)
        {
            InitializeComponent();
            DataContext = this.person = person;
            person.BeginEdit();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            person.EndEdit();
            DialogResult = true;
        }

        private void btnCansel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnAddKeyWord_Click(object sender, RoutedEventArgs e)
        {
            string keyWord = txtNewWord.Text;
            if (string.IsNullOrEmpty(keyWord))
            {
                MessageBox.Show("Добавляемое слово не должно быть пустым!");
                return;
            }
            var kw = Repos.KeyWords.Create(keyWord);
            person.KeyWords.Add(kw);
            Repos.KeyWords.Add(kw);
            UiHelper.RefreshCollection(dgKeyWords.ItemsSource);
        }

        private void btnCansel_Click_1(object sender, RoutedEventArgs e)
        {
            person.CancelEdit();
        }
    }
}
