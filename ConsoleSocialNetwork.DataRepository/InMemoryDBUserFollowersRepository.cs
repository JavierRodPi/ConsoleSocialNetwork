using System.Collections.Generic;
using System.Linq;
using ConsoleSocialNetwork.Followers;

namespace ConsoleSocialNetwork.DataRepository
{
    public class InMemoryDBUserFollowersRepository : IUserFollowersRepository
    {
        List<UserFollowers> userFollowers = new List<UserFollowers>();

        public UserFollowers AddUser(string user)
        {
            var usr = userFollowers.FirstOrDefault(u => u.User == user);
            if (usr == null)
            {
                usr = new UserFollowers { User = user, Followers = new HashSet<string>() };
                userFollowers.Add(usr);
            }
            return usr;
        }

        public bool ExistUser(string user)
        {
            return userFollowers.Any(u => u.User == user);
        }

        public UserFollowers FollowUser(string user, string follower)
        {
            var usr = userFollowers.FirstOrDefault(u => u.User == user);
            if (usr != null)
                usr.Followers.Add(follower);
        
            return usr;
        }

        public IEnumerable<string> GetFollowed(string user)
        {
            var usr = userFollowers.FirstOrDefault(u => u.User == user);
            if (usr != null)
                return usr.Followers;
            return null;
        }

        public UserFollowers GetUser(string user)
        {
            return userFollowers.FirstOrDefault(u => u.User == user);
        }
    }
}
