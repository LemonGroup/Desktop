using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using DeskTop.Util;
using DeskTop.Web;

namespace DeskTop
{
    public static class Statistics
    {
        private static Random rnd;
        public static Status Status;
        public static Status StatusDayly;

        static Statistics()
        {
            Status = new Status(0);
            StatusDayly = new Status(0);
        }
        private static int GetRnd()
        {
            if (rnd == null) rnd = new Random();
            Thread.Sleep(100);
            return rnd.Next(0, 10);
        }
        public static async Task<IEnumerable<StatRow>> GetStatistics(DateTime from, DateTime to,
            IEnumerable<Person> persons, IEnumerable<Site> sites)
        {
            var result = new List<StatRow>();
            Status.Max = persons.Count();
            Status.Current = 0;
            /*var data = await GetDaylyStat(from, to, persons, sites);
            var gData = data.GroupBy(d => d.Person);
            foreach (var grp in gData)
                if (grp.Any())
                    result.Add(new StatRow(grp.Key, grp.Sum(p => p.Rank), grp.First().Date));
*/

            // заглушка - возвращаем фейковые  данные

            int days = (int)(to - from).TotalDays + 1; // +1 чтобы не было нуля
            return await Task<IEnumerable<StatRow>>.Factory.StartNew(() =>
            {
                foreach (Person person in persons)
                {
                    Status.CurName = person.Name;
                    Status.Current++;
                    result.Add(new StatRow(person.Name, GetRnd()*days * sites.Count(), @from));
                }

                return result;
            });

        }
        public static async Task<IEnumerable<StatRow>> GetDaylyStat(DateTime from, DateTime to, 
            IEnumerable<Person> persons, IEnumerable<Site> sites)
        {
            var result = new List<StatRow>();

            /*foreach (Person person in persons)
            {

                foreach (Site site in sites)
                {
                    var data = await StatLoader.GetStatistics(from, to, person, site);
                    foreach (StatLoader.DataRow dataRow in data)
                        result.Add(new StatRow(person.Name, dataRow.numberOfNewPages, dataRow.Date));
                }

            }*/
            // заглушка - возвращаем фейковые  данные
            int days = (int)(to - from).TotalDays;
            StatusDayly.Max = days;
            StatusDayly.Current = 0;
            return await Task<IEnumerable<StatRow>>.Factory.StartNew(() =>
            {
                for (int i = 0; i < days; i++)
                {
                    DateTime date = from.AddDays(i);
                    StatusDayly.CurName = "Дата: " + date.ToString("yyyy-mm-dd");
                    StatusDayly.Current = i;
                    var data = GetStatistics(date, date, persons, sites).Result;
                    result.AddRange(data);
                }
                return result;

            });

        }

        public class StatRow
        {
            public StatRow(string person, int rank, DateTime date = new DateTime())
            {
                Person = person;
                Rank = rank;
                Date = date;
            }
            public string Person { get; set; }
            public int Rank { get; set; }

            public DateTime Date { get; set; }
        }
    }
}

