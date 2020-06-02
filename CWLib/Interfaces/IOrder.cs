using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib.Interfaces
{
    public interface IOrder
    {
        bool AddOrder(string connStr, out int? orderId, out string err);
        bool EndOrder(string connStr);
        bool UpdateOrder(string connStr, int personId, string newName, string newPhone, string newEmail, 
            ClientGroup newGroup, Box newBox, List<ClientCar> newCars, List<Worker> newWorkers, 
            List<CarwashService> newServices, MoneyType newMoneyType, out string err);
        bool DeleteOrder(string connStr);
        List<IOrder> GetOrders();
    }
}
