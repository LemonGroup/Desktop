﻿

using DeskTop.Web;

namespace DeskTop
{
    public static class Repos
    {
        private static SitesRepo _sitesRepo;
        private static PersonRepo _personRepo;
        static Repos()
        {
            var dl = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/persons");
            _sitesRepo = new SitesRepo();
            _personRepo = new PersonRepo(dl);

            // fake data
            /*
            _sitesRepo.Add(new Site(1, "www.site1.ru"));
            _sitesRepo.Add(new Site(2, "www.site2.ru"));
            _sitesRepo.Add(new Site(3, "www.site3.ru"));
            _personRepo.Add(new Person("Путин"));
            _personRepo.Add(new Person("Медведев"));
            _personRepo.Add(new Person("Навальный"));*/
        }
        public static PersonRepo Persons { get { return _personRepo; } }
        public static SitesRepo Sites { get { return _sitesRepo; } }

    }
}
