using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Data.Sprav
{
    public class KeyWord : IEditableObject
    {
        public int Id { get; set; }
        public int personId;
        public string Word { get; set; }
        private string oldWord;
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

        public void BeginEdit()
        {
            oldWord = Word;
        }

        public void EndEdit()
        {
            Repos.KeyWords.Update(this);
        }

        public void CancelEdit()
        {
            Word = oldWord;
        }
    }
}
