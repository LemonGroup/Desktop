

using System;
using System.Windows;
using DeskTop.Web;

namespace DeskTop
{
    public static class Repos
    {
        private static SitesRepo _sitesRepo;
        private static PersonRepo _personRepo;
        private static KeyWordRepo _keyWordRepo;
        static Repos()
        {
            try
            {
                var dlPerosns = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/persons");
                var dlSites = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/sites");
                var dlKW = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/keywords");
                _sitesRepo = new SitesRepo(dlSites);
                _keyWordRepo = new KeyWordRepo(dlKW);
                _personRepo = new PersonRepo(dlPerosns);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки справочников!");
            }


            // fake data
            
           /* _sitesRepo.Add(new Site(1, "www.site1.ru"));
            _sitesRepo.Add(new Site(2, "www.site2.ru"));
            _sitesRepo.Add(new Site(3, "www.site3.ru"));
            _personRepo.Add(new Person("Путин"));
            _personRepo.Add(new Person("Медведев"));
            _personRepo.Add(new Person("Навальный"));*/
        }
        public static PersonRepo Persons { get { return _personRepo; } }
        public static SitesRepo Sites { get { return _sitesRepo; } }
        public static KeyWordRepo KeyWords { get { return _keyWordRepo;} }

    }
}
