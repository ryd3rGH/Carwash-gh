using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    public class CarClass : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public object Clone()
        {
            return new CarClass() { Id = this.Id, Name = this.Name, Price = this.Price };
        }
    }
}
