using ConsoleSocialNetwork.Followers;
using NUnit.Framework;
using System.Collections.Generic;

namespace ConsoleSocialNetwork.Test
{
    [TestFixture]
    public class UserFollowersServiceTest
    {
        UserFollowersService userFollowersService;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            userFollowersService = new UserFollowersService(UserFollowersRepositoryMock.GetMock().Object);
        }

        [Test]
        public void UserFollowersService_AddUser_AddSuccess_User()
        {
            var expectedUser = new UserFollowers { User = "Javier", Followers = new HashSet<string>() };

            var result = userFollowersService.AddUser("Javier");

            Assert.AreEqual(expectedUser, result);
        }

        [Test]
        public void UserFollowersService_FollowUser_NoUser_Null()
        {
            Assert.Throws< InvalidUserNameException>(() => userFollowersService.FollowUser("Marco", "Bob"));
        }

        [Test]
        public void UserFollowersService_FollowUser_ExistUser_User()
        {
            var expected = new UserFollowers { User = "Alice", Followers = new HashSet<string> { "Bob" } };

            var result = userFollowersService.FollowUser("Alice", "Bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UserFollowersService_GetFollowed_ExistUser_FollowedUsers()
        {
            var expected = new List<string> { "Bob" };

            var result = userFollowersService.GetFollowed("Alice");

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void UserFollowersService_GetFollowed_NotExistUser_Empty()
        {
            var result = userFollowersService.GetFollowed("Daniel");

            CollectionAssert.IsEmpty(result);
        }
    }
}
