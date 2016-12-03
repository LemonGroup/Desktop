using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using DeskTop.Web;

namespace DeskTop
{
    public static class Statistics
    {
        private static Random rnd;
        private static int GetRnd()
        {
            if (rnd == null) rnd = new Random();
            return rnd.Next(0, 10);
        }
        public static IEnumerable<StatRow> GetStatistics(DateTime from, DateTime to,
            IEnumerable<Person> persons, IEnumerable<Site> sites)
        {
            
            var data = GetDaylyStat(from, to, persons, sites).GroupBy(d => d.Person);
            foreach (var grp in data)
            {
                if (grp.Any())
                    yield return new StatRow(grp.Key, grp.Sum(p=>p.Rank), grp.First().Date);
            }


            // заглушка - возвращаем фейковые  данные

            /*int days = (int)(to - from).TotalDays + 1; // +1 чтобы не было нуля
            foreach (Person person in persons)
                yield return new StatRow(person.Name, GetRnd()*days, from);*/


        }
        public static IEnumerable<StatRow> GetDaylyStat(DateTime from, DateTime to, 
            IEnumerable<Person> persons, IEnumerable<Site> sites)
        {

            foreach (Person person in persons)
            {
                foreach (Site site in sites)
                {
                    var data = StatLoader.GetStatistics(from, to, person, site);
                    foreach (StatLoader.DataRow dataRow in data)
                    {
                        yield return new StatRow(person.Name, dataRow.numberOfNewPages, dataRow.Date);
                    }
                }
            }
            // заглушка - возвращаем фейковые  данные
            /* List<StatRow> data = new List<StatRow>();
             int days = (int)(to - from).TotalDays;
             for(int i = 0; i<days; i++)
             {
                 DateTime date = from.AddDays(i);
                 data.AddRange(GetStatistics(date,date, persons, sites));
             }
             return data;*/
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

