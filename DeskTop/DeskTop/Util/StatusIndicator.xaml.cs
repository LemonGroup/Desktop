using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeskTop.Util
{
    /// <summary>
    /// Interaction logic for StatusIndicator.xaml
    /// </summary>
    public partial class StatusIndicator : Window
    {
        public Status SIndicator;
        public StatusIndicator(int max)
        {
            InitializeComponent();
            SIndicator = new Status(max);
            this.DataContext = SIndicator;
        }
    }
}
