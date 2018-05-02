using ConsoleSocialNetwork.Followers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSocialNetwork.Test
{
    static class UserFollowersServiceMock
    {
        public static Mock<IUserFollowersService> GetMock()
        {
            var mock = new Mock<IUserFollowersService>();
            mock.Setup(c => c.AddUser("Javier")).Returns(new UserFollowers { User = "Javier", Followers = new HashSet<string>() });
            mock.Setup(c => c.FollowUser("Alice", "Bob")).Returns(new UserFollowers { User = "Alice", Followers = new HashSet<string> { "Bob" } });
            mock.Setup(c => c.FollowUser("Alice", "Javier")).Throws(new InvalidUserNameException("Javier"));
            mock.Setup(c => c.GetFollowed("Alice")).Returns(new List<string> { "Bob" });
            return mock;
        }
    }
}
