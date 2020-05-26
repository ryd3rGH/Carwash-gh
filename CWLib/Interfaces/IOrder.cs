using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib.Interfaces
{
    public interface IOrder
    {
        bool AddOrder(string connStr, out int? orderId, out string err);
        bool EndOrder(string connStr);
        bool UpdateOrder();
        bool DeleteOrder(string connStr);
        List<IOrder> GetOrders();
    }
}
