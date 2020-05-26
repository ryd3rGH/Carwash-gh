using System;
using System.Collections.Generic;
using System.Text;

namespace CarwashManager.Classes
{
    interface IOrder
    {
        void GetCarBrands();
        void GetClientGroups();
        void GetClientInfo();
        void GetWorkers();
        void GetServices();
        void GetAdditionalInfo();
        void IsEntityClientOrder(bool isEntity);
    }
}
