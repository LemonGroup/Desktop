using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeskTop.Util
{
    public static class UiHelper
    {
        public static void RefreshCollection(object itemSource)
        {
            CollectionViewSource.GetDefaultView(itemSource).Refresh();
        }
    }
}
