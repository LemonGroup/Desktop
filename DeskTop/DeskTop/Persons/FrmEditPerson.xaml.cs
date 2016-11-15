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

namespace DeskTop.Persons
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
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
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
            person.KeyWords.Add(keyWord);
            UiHelper.RefreshCollection(dgKeyWords.ItemsSource);
        }
    }
}
