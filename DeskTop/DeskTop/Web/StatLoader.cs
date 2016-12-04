using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskTop.Web
{
    public static class StatLoader
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";
        private static DataLoader dlStat;
        static StatLoader()
        {
            dlStat = new DataLoader("http://yrsoft.cu.cc:8080", "/stat/daily_stat");
        }

        public static async Task<IEnumerable<DataRow>> GetStatistics(DateTime from, DateTime to,
            Person person, Site site)
        {
            var getStr = string.Format("?siteId={0}&personId={1}&start_date={2}&end_date={3}\'",
                site.Id, person.Id, from.ToString(DATE_FORMAT), to.ToString(DATE_FORMAT));
            var strData = await dlStat.GetData(getStr);
            if (string.IsNullOrEmpty(strData)) return new DataRow[0];
            var data = JsonConvert.DeserializeObject<DataRow[]>(strData);
            return data;

        }
        public class DataRow
        {
            private string date;
            public int numberOfNewPages;

            public DateTime Date
            {
                get
                {
                    DateTime dta;
                    DateTime.TryParseExact(date, DATE_FORMAT, null, DateTimeStyles.None, out dta);
                    return dta;
                }
            } 
        }
    }
}
