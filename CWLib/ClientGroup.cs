using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    public class ClientGroup
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public bool? Visibility { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
