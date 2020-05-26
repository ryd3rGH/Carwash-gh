using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    public class ClientPlateForSearchList
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public int ModelId { get; set; }

        public override string ToString()
        {
            return Plate;
        }
    }
}
