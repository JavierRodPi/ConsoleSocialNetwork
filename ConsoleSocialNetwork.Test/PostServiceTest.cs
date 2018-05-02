using ConsoleSocialNetwork.Post;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSocialNetwork.Test
{
    [TestFixture]
    public class PostServiceTest
    {
        PostService postService;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            postService = new PostService(PostRepositoryMock.GetMock().Object, UserFollowersServiceMock.GetMock().Object);
        }


        [Test]
        public void PostService_AddPost_PostCorrect_Post()
        {
            var expected = new Post.Post("Alice", "Alice Message", DateTime.Parse("01/01/2018"));

            var result = postService.AddPost("Alice", "Alice Message", DateTime.Parse("01/01/2018"));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void PostService_GetPostsByUser_User_Posts()
        {
            var expected = new List<Post.Post>{
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017")),
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017"))

            };

            var result = postService.GetPostsByUser("Alice").ToList();

            CollectionAssert.AreEqual(expected, result);
        }
        
        [Test]
        public void PostService_GetPostsByUser_UserNotExist_Empty()
        {
            var result = postService.GetPostsByUser("Marco").ToList();

            CollectionAssert.IsEmpty(result);
        }


        [Test]
        public void PostService_GetWallByUser_UserExist_Posts()
        {
            var expected = new List<Post.Post>{
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017")),
                new Post.Post("Bob", "Bob Message", DateTime.Parse("08/09/2017")),
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017")),
                new Post.Post("Bob", "Bob Message 2", DateTime.Parse("06/09/2017")),
            };


            var result = postService.GetWallByUser("Alice").ToList();

            CollectionAssert.AreEqual(expected, result);
        }
        
        [Test]
        public void PostService_GetWallByUser_UserNotExist_Empty()
        {
            var result = postService.GetWallByUser("Marco");

            CollectionAssert.IsEmpty(result);
        }

}
}
