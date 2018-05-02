using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSocialNetwork.Application
{
    public class SystemClock:IClock
    {
        public DateTime Now => DateTime.Now;

    }
}
