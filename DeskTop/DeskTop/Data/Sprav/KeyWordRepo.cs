using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Data.Sprav;
using DeskTop.Web;


namespace DeskTop
{
    public class KeyWordRepo : AbstractRepo<KeyWord>
    {

        public override KeyWord Create(string par)
        {
            return new KeyWord(NextKey, par);
        }
        protected override int GetKey(KeyWord item)
        {
            return item.Id;
        }

        public KeyWordRepo() : base()  { }
        public KeyWordRepo(DataLoader loader) : base(loader)
        {

        }
    }
}
