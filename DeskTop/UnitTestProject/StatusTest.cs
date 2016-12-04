using System;
using System.Threading;
using System.Threading.Tasks;
using DeskTop.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class StatusTest
    {
        [TestMethod]
        public  void TestMethod1()
        {
            int max = 15;
            string msg = "test message";
            var f = new StatusIndicator(max);
            var t =  Task.Factory.StartNew(() =>
            {
             
            for (int i = 0; i < max; i++)
            {
                f.SIndicator.Current++;
                f.SIndicator.CurName = msg + i;
                Thread.Sleep(300);
            }                  
            });
            f.ShowDialog();

             
            //f.Close();
        }
        
        public void Count(Status status, int max)
        {
         
        }
    }
}
