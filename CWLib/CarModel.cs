using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    /* Модель авто */
    public class CarModel
    {
        public CarBrand Brand { get; set; }
        public CarCategory Category { get; set; }
        public CarClass Class { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
