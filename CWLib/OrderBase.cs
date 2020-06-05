using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    public abstract class OrderBase
    {
        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public string Comment { get; set; }
        public decimal Sum { get; set; }
        public bool IsPayed { get; set; }
        public Box Box { get; set; }        
        public MoneyType MoneyType { get; set; }
        public List<Worker> Workers { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EstimatedEndTime { get; set; }
        public DateTime? RealEndTime { get; set; }
        public int AdmId { get; set; }
    }
}
