using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    /* Категории работников */
    /* Администраторы, мойщики, шиномонтажники и т.д. */
    public class WorkersCategory
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public override string ToString()
        {
            return GroupName;
        }
    }
}
