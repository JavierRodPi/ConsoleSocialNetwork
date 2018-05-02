using System.Collections.Generic;

namespace ConsoleSocialNetwork.Followers
{
    public interface IUserFollowersService
    {
        UserFollowers AddUser(string user);
        UserFollowers FollowUser(string user, string follower);
        IEnumerable<string> GetFollowed(string user);
    }
}
