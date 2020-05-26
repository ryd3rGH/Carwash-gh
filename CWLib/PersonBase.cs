using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    /* Базовый класс для клиентов, пользователей, работников */
    public abstract class PersonBase
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
