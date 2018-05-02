using System;

namespace ConsoleSocialNetwork.Application
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
