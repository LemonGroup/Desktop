using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Web;


namespace DeskTop
{
    public class SitesRepo : AbstractRepo<Site>
    {
        public override Site Create(string par)
        {
            return new Site(NextKey, par);
        }
        protected override int GetKey(Site item)
        {
            return item.Id;
        }
        public SitesRepo() : base()  { }
        public SitesRepo(DataLoader loader) : base(loader) { }
    }
}
