using ConsoleSocialNetwork.Post;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSocialNetwork.Test
{
    static class PostServiceMock
    {
        public static Mock<IPostService> GetMock()
        {
            var mock = new Mock<IPostService>();
            mock.Setup(c => c.AddPost("Alice", "Hello world", DateTime.Parse("27/04/2018 2:02:04"))).Returns(new Post.Post("Alice", "Hello world", DateTime.Parse("27/04/2018 2:02:04")));
            mock.Setup(c => c.GetPostsByUser("Alice")).Returns(new List<Post.Post> {
                new Post.Post("Alice", "Great day", DateTime.Parse("27/04/2018 2:02:02")),
                new Post.Post("Alice", "Hello world", DateTime.Parse("27/04/2018 2:01:00"))
            });
            mock.Setup(c => c.GetWallByUser("Alice")).Returns(new List<Post.Post>{
                new Post.Post("Alice", "Nice weather", DateTime.Parse("27/04/2018 2:02:02")),
                new Post.Post("Bob", "Second post", DateTime.Parse("27/04/2018 2:01:00")),
                new Post.Post("Alice", "Hello world", DateTime.Parse("27/04/2018 2:00:00")),
                new Post.Post("Bob", "First post", DateTime.Parse("27/04/2018 1:00:00"))
            });
            return mock;
        }
    }
}
