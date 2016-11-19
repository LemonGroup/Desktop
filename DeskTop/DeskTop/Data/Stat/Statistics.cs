using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // TODO Загрузка данных из базы
            // заглушка - возвращаем фейковые  данные
            int days = (int)(to - from).TotalDays + 1; // +1 чтобы не было нуля
            foreach (Person person in persons)
                yield return new StatRow(person.Name, GetRnd()*days, from);
        }
        public static IEnumerable<StatRow> GetDaylyStat(DateTime from, DateTime to, 
            IEnumerable<Person> persons, IEnumerable<Site> sites)
        {
            // TODO Загрузка данных из базы
            // заглушка - возвращаем фейковые  данные
            List<StatRow> data = new List<StatRow>();
            int days = (int)(to - from).TotalDays;
            for(int i = 0; i<days; i++)
            {
                DateTime date = from.AddDays(i);
                data.AddRange(GetStatistics(date,date, persons, sites));
            }
            return data;
        }

        public class StatRow
        {
            public StatRow(string keyWord, int rank, DateTime date = new DateTime())
            {
                KeyWord = keyWord;
                Rank = rank;
                Date = date;
            }
            public string KeyWord { get; set; }
            public int Rank { get; set; }

            public DateTime Date { get; set; }
        }
    }
}

