using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Identification
    {
        private int _numPassword;
        private string _password;

        public Identification(int numPassword, string password)
        {
            _numPassword = numPassword;
            _password = password;
        }

        public int NumPassword
        {
            get { return _numPassword; }
            set { _numPassword = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public override string ToString()
        {
            return NumPassword.ToString();
        }
    }
}
