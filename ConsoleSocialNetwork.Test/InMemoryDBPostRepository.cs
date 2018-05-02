using ConsoleSocialNetwork.DataRepository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSocialNetwork.Test
{
    [TestFixture]
    public class InMemoryDBPostRepositoryTest
    {
        InMemoryDBPostRepository inMemoryDBPostRepository;

        [SetUp]
        public void SetUp()
        {
            inMemoryDBPostRepository = new InMemoryDBPostRepository();
        }

        [Test]
        public void InMemoryDBPostRepository_AddPost_PostCorrect_Post()
        {
            var expected = new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2018"));

            var result = inMemoryDBPostRepository.AddPost("Alice", "Alice Message", DateTime.Parse("09/09/2018"));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void InMemoryDBPostRepository_GetPostsByUser_UserHasPostsRightOrder_Posts()
        {
            var expected = new List<Post.Post>{
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017")),
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017"))
                
            };

            inMemoryDBPostRepository.AddPost("Alice", "Alice Message", DateTime.Parse("09/09/2017"));
            inMemoryDBPostRepository.AddPost("Alice", "Alice Message 2", DateTime.Parse("07/09/2017"));

            var result = inMemoryDBPostRepository.GetPostsByUser("Alice").ToList();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void InMemoryDBPostRepository_GetPostsByUser_UserHasPostsIncorrectOrder_Posts()
        {
            var expected = new List<Post.Post>{
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017")),
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017"))
            };

            inMemoryDBPostRepository.AddPost("Alice", "Alice Message", DateTime.Parse("09/09/2017"));
            inMemoryDBPostRepository.AddPost("Alice", "Alice Message 2", DateTime.Parse("07/09/2017"));

            var result = inMemoryDBPostRepository.GetPostsByUser("Alice").ToList();

            CollectionAssert.AreNotEqual(expected, result);
        }

        [Test]
        public void InMemoryDBPostRepository_GetPostsByUser_UserNotExist_Empty()
        {
            var result = inMemoryDBPostRepository.GetPostsByUser("Alice").ToList();

            CollectionAssert.IsEmpty(result);
        }


        [Test]
        public void InMemoryDBPostRepository_GetWallByUser_WallHasPostsRightOrder_Posts()
        {
            var expected = new List<Post.Post>{
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017")),
                new Post.Post("Bob", "Bob Message", DateTime.Parse("08/09/2017")),
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017")),
                new Post.Post("Bob", "Bob Message 2", DateTime.Parse("06/09/2017")),
            };

            inMemoryDBPostRepository.AddPost("Alice", "Alice Message", DateTime.Parse("09/09/2017"));
            inMemoryDBPostRepository.AddPost("Alice", "Alice Message 2", DateTime.Parse("07/09/2017"));
            inMemoryDBPostRepository.AddPost("Bob", "Bob Message", DateTime.Parse("08/09/2017"));
            inMemoryDBPostRepository.AddPost("Bob", "Bob Message 2", DateTime.Parse("06/09/2017"));


            var result = inMemoryDBPostRepository.GetWallByUser(new List<string> { "Alice", "Bob" }).ToList();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void InMemoryDBPostRepository_GetWallByUser_WallHasPostsIncorrectOrder_Posts()
        {
            var expected = new List<Post.Post>{
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017")),
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017"))
            };

            inMemoryDBPostRepository.AddPost("Alice", "Alice Message", DateTime.Parse("09/09/2017"));
            inMemoryDBPostRepository.AddPost("Alice", "Alice Message 2", DateTime.Parse("07/09/2017"));

            var result = inMemoryDBPostRepository.GetWallByUser(new List<string> { "Alice" }).ToList();

            CollectionAssert.AreNotEqual(expected, result);
        }

        [Test]
        public void InMemoryDBPostRepository_GetWallByUser_UserNotExist_Empty()
        {
            var result = inMemoryDBPostRepository.GetWallByUser(new List<string> { "Alice" }).ToList();

            CollectionAssert.IsEmpty(result);
        }


    }
}
