using System;
using System.Collections.Generic;

namespace ConsoleSocialNetwork.Followers
{
    public class UserFollowers
    {
        public string User { get; set; }
        public HashSet<string> Followers { get; set; }

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (UserFollowers)obj;
            return User == p.User
                && Followers.SetEquals(p.Followers);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
