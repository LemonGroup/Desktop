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
        private static StatRow[] GetData(DateTime date) // генерация случ данных по 3-м персонам
        {          
            var data = new[]
            {
                new StatRow("Путин", GetRnd(), date),
                new StatRow("Медведев", GetRnd(), date),
                new StatRow("Трамп", GetRnd(), date)
            };
            return data;
        }
        public static IEnumerable<StatRow> GetStatistics(DateTime from, DateTime to,
            IEnumerable<Person> persons, IEnumerable<Site> sites)
        {
            // TODO Загрузка данных из базы
            // заглушка - возвращаем фейковые  данные
            var data = GetData(DateTime.Now);
            switch (sites.Count())
            {
                case 1:
                    data[0].Rank = 5;
                    data[1].Rank = 2;
                    data[2].Rank = 4;
                    break;
                case 2:
                    data[0].Rank = 10;
                    data[1].Rank = 17;
                    data[2].Rank = 28;
                    break;
                case 3:
                    data[0].Rank = 55;
                    data[1].Rank = 22;
                    data[2].Rank = 44;
                    break;
            }
            return data;
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
                data.AddRange(GetData(date));
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

