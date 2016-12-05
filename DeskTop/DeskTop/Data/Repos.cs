

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Data.Sprav;
using DeskTop.Util;
using DeskTop.Web;

namespace DeskTop
{
    public static class Repos
    {
        private static SitesRepo _sitesRepo;
        private static PersonRepo _personRepo;
        private static KeyWordRepo _keyWordRepo;
        public static Status Status;
        public static PersonRepo Persons { get { return _personRepo; } }
        public static SitesRepo Sites { get { return _sitesRepo; } }
        public static KeyWordRepo KeyWords { get { return _keyWordRepo; } }
        public static bool IsLoaded { get; private set; }

        static Repos()
        {
            Status = new Status(3);

        }

        public static void LoadRepos()
        {
            try
            {
                var dlPerosns = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/persons");
                var dlSites = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/sites");
                var dlKW = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/keywords");
                _sitesRepo = new SitesRepo(dlSites);
                _keyWordRepo = new KeyWordRepo(dlKW);
                _personRepo = new PersonRepo(dlPerosns);
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки справочников!");
            }
            /*

            // fake data
            var task = new Task(() =>
            {
                Status.CurName = "Загрузка сайтов";
                Thread.Sleep(100);
                Status.Current++;
            });
            var task2 = task.ContinueWith(t =>
            {
                Status.CurName = "Загрузка ключевых слов";
                Thread.Sleep(100);
                Status.Current++;
            });
            var task3 = task2.ContinueWith(t =>
            {
                Status.CurName = "Загрузка Персон";
                Thread.Sleep(100);
                Status.Current++;
            });
            var task4 = task3.ContinueWith(t =>
            {
                _sitesRepo = new SitesRepo();
                _keyWordRepo = new KeyWordRepo();
                _personRepo = new PersonRepo();
                _sitesRepo.Add(new Site(1, "www.site1.ru"));
                _sitesRepo.Add(new Site(2, "www.site2.ru"));
                _sitesRepo.Add(new Site(3, "www.site3.ru"));
                _keyWordRepo.Add(new KeyWord(1, "Путину") { personId = 1 });
                _keyWordRepo.Add(new KeyWord(2, "Медведеву") { personId = 2 });
                _keyWordRepo.Add(new KeyWord(3, "Навальному") { personId = 3 });
                _keyWordRepo.Add(new KeyWord(4, "Путиным") { personId = 1 });
                _personRepo.Add(new Person(1, "Путин"));
                _personRepo.Add(new Person(2, "Медведев"));
                _personRepo.Add(new Person(3, "Навальный"));
                _personRepo.KeywordsToPersons();
                IsLoaded = true;

            });
            task.Start();
            task.Wait();        */
        }


    }
}
