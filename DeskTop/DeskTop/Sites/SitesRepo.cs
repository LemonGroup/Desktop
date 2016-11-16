using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Persons;

namespace DeskTop
{
    public class SitesRepo : AbstractRepo<string, int>
    {
        private int lastKey;
        protected override string Create(int key) { return ""; }

        public override void Save()
        {
            MessageBox.Show("Сайты сохранены (заглушка)");
        }

        protected override int GetKey(string item)
        {
            return item.GetHashCode();
        }

        public SitesRepo(IEnumerable<string> items) : base(items) { }
    }
}
