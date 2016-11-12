using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop
{
    static class Statistics
    {
        public static IEnumerable<StatRow> GetStatistics(IEnumerable<string> sites)
        {
            // TODO Загрузка данных из базы
            // заглушка - возвращаем фейковые  данные
            var data = new[]
            {
                new StatRow("Путин", 0),
                new StatRow("Медведев", 0),
                new StatRow("Трамп", 0)
            };
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

        public class StatRow
        {
            public StatRow(string keyWord, int rank)
            {
                KeyWord = keyWord;
                Rank = rank;
            }
            public string KeyWord { get; set; }
            public int Rank { get; set; }
        }
    }
}

