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

        public Site Create(string url) { return new Site(NextKey, url); }

        public override void Save()
        {
            MessageBox.Show("Сайты сохранены (заглушка)");
        }
        protected override int GetKey(Site item)
        {
            return item.Id;
        }

        public SitesRepo() : base()  { }
        public SitesRepo(DataLoader loader) : base()
        {
            var data = loader.GetData<JsonClasses.JsonSite>();
            foreach (JsonClasses.JsonSite jsonSite in data)
            {
                Site site = new Site(jsonSite.id, jsonSite.site);
                Add(site);
            }
        }
    }
}
