using System;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using DeskTop;
using DeskTop.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class LoadPersonsTest
    {
        [TestMethod]
        public void LoadPersonsTesting()
        {
            /*var dl = new DataLoader("http://yrsoft.cu.cc:8080", "/catalog/persons");
            var tmp =  dl.GetData<DataLoader.JsonPerson>().ToArray();*/
            var tmp = new CrudSprav<DeskTop.Site>("http://yrsoft.cu.cc:8080", "/catalog/sites");
            var t =  tmp.Create(new DeskTop.Site(5,"Test"));
        }
[TestMethod]
        public void DelTest()
        {
            Repos.LoadRepos();
            while (!Repos.IsLoaded) { Thread.Sleep(200);}
            var k = Repos.KeyWords.Create("testWord");
            k.personId = 100;
            Repos.KeyWords.Add(k);
            Repos.KeyWords.Save();
        }
    }
}
