using System;

namespace ConsoleSocialNetwork.Followers
{
    public class InvalidUserNameException : Exception
    {
        public InvalidUserNameException()
        {

        }

        public InvalidUserNameException(string name)
            : base(String.Format("Invalid User Name: {0}", name))
        {

        }

    }
}
