using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop
{
    static class SiteList
    {
        public static IEnumerable<string> GetSiteList()
        {
            //TODO реализовать метод получения списков сайтов из базы
            return new [] { "https://yandex.ru/", "https://lenta.ru/", "yaplakal.com" };
        }
    }
}
