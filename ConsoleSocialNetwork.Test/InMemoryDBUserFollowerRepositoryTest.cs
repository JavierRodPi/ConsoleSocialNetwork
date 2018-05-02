using ConsoleSocialNetwork.DataRepository;
using ConsoleSocialNetwork.Followers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSocialNetwork.Test
{
    [TestFixture]
    public class InMemoryDBUserFollowerRepositoryTest
    {
        InMemoryDBUserFollowersRepository inMemoryDBUserFollower;
        [SetUp]
        public void SetUp()
        {
            inMemoryDBUserFollower = new InMemoryDBUserFollowersRepository();
        }
        [Test]
        public void InMemoryDBUserFollowerRepository_ExistUser_NoUser_False()
        {
            var result = inMemoryDBUserFollower.ExistUser("Alice");

            Assert.IsFalse(result);
        }

        [Test]
        public void InMemoryDBUserFollowerRepository_ExistUser_ExistUser_True()
        {
            inMemoryDBUserFollower.AddUser("Alice");

            var result = inMemoryDBUserFollower.ExistUser("Alice");

            Assert.IsTrue(result);
        }


        [Test]
        public void InMemoryDBUserFollowerRepository_AddUser_AddSuccess_User()
        {
            var expectedUser = new UserFollowers { User = "Alice", Followers = new HashSet<string>() };

            var result = inMemoryDBUserFollower.AddUser("Alice");

            Assert.AreEqual(expectedUser, result);
        }

        [Test]
        public void InMemoryDBUserFollowerRepository_FollowUser_NoUser_Null()
        {
            var result = inMemoryDBUserFollower.FollowUser("Alice", "Bob");
            Assert.IsNull(result);
        }

        [Test]
        public void InMemoryDBUserFollowerRepository_FollowUser_ExistUser_User()
        {
            inMemoryDBUserFollower.AddUser("Alice");

            var expected = new UserFollowers { User = "Alice", Followers = new HashSet<string> { "Bob" } };


            var result = inMemoryDBUserFollower.FollowUser("Alice", "Bob");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void InMemoryDBUserFollowerRepository_GetFollowed_ExistUser_FollowedUsers()
        {
            inMemoryDBUserFollower.AddUser("Alice");
            inMemoryDBUserFollower.FollowUser("Alice", "Bob");

            var expected = new List<string> { "Bob" };

            var result = inMemoryDBUserFollower.GetFollowed("Alice");

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void InMemoryDBUserFollowerRepository_GetFollowed_NotExistUser_Null()
        {
            var result = inMemoryDBUserFollower.GetFollowed("Alice");

            Assert.IsNull(result);
        }


        [Test]
        public void InMemoryDBUserFollowerRepository_GetUser_ExistUser_User()
        {
            inMemoryDBUserFollower.AddUser("Alice");

            var expected = new UserFollowers { User = "Alice", Followers = new HashSet<string>() };

            var result = inMemoryDBUserFollower.GetUser("Alice");

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void InMemoryDBUserFollowerRepository_GetUser_NotExistUser_Null()
        {
            var result = inMemoryDBUserFollower.GetUser("Alice");

            Assert.IsNull(result);
        }
    }
}
