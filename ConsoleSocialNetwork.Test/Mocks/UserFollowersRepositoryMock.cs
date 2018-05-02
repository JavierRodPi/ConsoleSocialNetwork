using ConsoleSocialNetwork.Followers;
using Moq;
using System.Collections.Generic;

namespace ConsoleSocialNetwork.Test
{
    static class UserFollowersRepositoryMock
    {
        public static Mock<IUserFollowersRepository> GetMock()
        {
            var mock = new Mock<IUserFollowersRepository>();
            mock.Setup(c => c.ExistUser("Alice")).Returns(true);
            mock.Setup(c => c.ExistUser("Bob")).Returns(true);
            mock.Setup(c => c.AddUser("Javier")).Returns(new UserFollowers { User = "Javier", Followers = new HashSet<string>() });
            mock.Setup(c => c.FollowUser("Alice", "Bob")).Returns(new UserFollowers { User = "Alice", Followers = new HashSet<string> { "Bob" } });
            mock.Setup(c => c.GetFollowed("Alice")).Returns(new List<string> { "Bob" });
            return mock;
        }
    }
}
