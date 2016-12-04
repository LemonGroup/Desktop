using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DeskTop.Auth
{
    public static class User
    {
        public static string Token { get {return "fake_token";}}
        public static bool IsAdmin { get { return true; } }
    }
}
