using System.Collections.Generic;

namespace ConsoleSocialNetwork.Followers
{
    public interface IUserFollowersRepository
    {
        UserFollowers AddUser(string user);
        UserFollowers FollowUser(string user, string follower);
        IEnumerable<string> GetFollowed(string user);
        bool ExistUser(string user);
        UserFollowers GetUser(string user);
    }
}
