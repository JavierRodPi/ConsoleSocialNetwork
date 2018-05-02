using ConsoleSocialNetwork.Application;
using System;

namespace ConsoleSocialNetwork.Test
{
    public class SystemClockStatic : IClock
    {
        public DateTime Now => DateTime.Parse("27/04/2018 2:02:04");
    }
}
