using System;
using System.Collections.Generic;
using System.Text;

namespace CWLib
{
    public abstract class ServiceBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Visibility { get; set; }

        public ServiceBase(string name, string descr, TimeSpan duration, bool vis)
        {
            Name = name;
            Description = descr;
            Duration = duration;
            Visibility = vis;
        }
    }
}
