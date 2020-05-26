using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    /* Базовый класс для всех клиентов */
    public abstract class ClientBase : PersonBase
    {
        public ClientGroup Group { get; set; }     
        public DateTime DateAdd {get;set;} //дата добавления клиента
        public List<ClientCar> Cars { get; set; } //автомобили клиента

        public abstract int GetOrderClientsCar(string connStr, string licPlate, int idModel);
    }
}
