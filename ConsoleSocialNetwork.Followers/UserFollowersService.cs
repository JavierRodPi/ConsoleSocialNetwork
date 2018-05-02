using System.Collections.Generic;

namespace ConsoleSocialNetwork.Followers
{
    public class UserFollowersService : IUserFollowersService
    {
        IUserFollowersRepository userFollowersRepository;

        public UserFollowersService(IUserFollowersRepository userFollowersRepository)
        {
            this.userFollowersRepository = userFollowersRepository;
        }

        public UserFollowers FollowUser(string user, string follower)
        {
            var userExist = userFollowersRepository.ExistUser(user);
            if (!userExist)
                throw new InvalidUserNameException(user);

            var followerExist = userFollowersRepository.ExistUser(follower);
            if (!followerExist)
                throw new InvalidUserNameException(follower);

            return userFollowersRepository.FollowUser(user, follower);
        }

        public UserFollowers AddUser(string user)
        {
            if (!userFollowersRepository.ExistUser(user))
                return userFollowersRepository.AddUser(user);
            return userFollowersRepository.GetUser(user);
        }

        public IEnumerable<string> GetFollowed(string user)
        {
            return userFollowersRepository.GetFollowed(user);
        }
    }
}
