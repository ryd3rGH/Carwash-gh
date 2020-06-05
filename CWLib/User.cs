using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    /* Пользователи */
    public class User
    {
        public int? Id { get; set; }
        public string LoginName { get; set; }
        public string Pass { get; set; }

        public override string ToString()
        {
            return LoginName;
        }
    }    
}
