using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace DeskTop
{
    public class SitesRepo : AbstractRepo<Site, int>
    {
        private int lastKey; // счетчик id для создаваемых элементов, растет в сторону уменьшения
        //(реальный id появится в БД)
        public Site Create(string url) { return new Site(lastKey--, url); }

        public override void Save()
        {
            MessageBox.Show("Сайты сохранены (заглушка)");
        }

        protected override int GetKey(Site item)
        {
            return item.Id;
        }

        public SitesRepo() : base()
        {
            lastKey = -1;
        }
    }
}
