using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeskTop.Util
{
    public static class UiHelper
    {
        public static void RefreshItems(object itemsSource)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(itemsSource);
            view.Refresh();
        }
    }
}
