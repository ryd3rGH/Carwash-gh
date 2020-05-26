using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    /* Марка авто */
    public class CarBrand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsFast { get; set; } //если true - при выборе марки авто в заказе, то эта марка будет наверху списка
        public List<CarModel> Models { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
