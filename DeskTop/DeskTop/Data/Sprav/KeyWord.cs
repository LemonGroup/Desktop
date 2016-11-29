using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Data.Sprav
{
    public class KeyWord
    {
        public int Id { get; set; }
        public int personId;
        public string Word { get; set; }
        public KeyWord() { }

        public KeyWord(int id, string word)
        {
            Id = id;
            Word = word;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
