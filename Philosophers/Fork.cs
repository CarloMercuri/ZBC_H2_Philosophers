using System;
using System.Collections.Generic;
using System.Text;

namespace Philosophers
{
    public class Fork
    {
        public object Lock { get; set; }

        public int ID { get; set; }

        public Fork(object _lock)
        {
            Lock = _lock;
        }
    }
}
