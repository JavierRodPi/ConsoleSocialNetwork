using ConsoleSocialNetwork.Post;
using Moq;
using System;
using System.Collections.Generic;

namespace ConsoleSocialNetwork.Test
{
    static class PostRepositoryMock
    {
        
        public static Mock<IPostRepository> GetMock()
        {
            var mock = new Mock<IPostRepository>();
            mock.Setup(c => c.AddPost("Alice", "Alice Message", DateTime.Parse("01/01/2018"))).Returns(new Post.Post("Alice", "Alice Message", DateTime.Parse("01/01/2018")));
            mock.Setup(c => c.GetPostsByUser("Alice")).Returns(new List<Post.Post> {
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("09/09/2017")),
                new Post.Post("Alice", "Alice Message", DateTime.Parse("07/09/2017"))
            });
            mock.Setup(c => c.GetWallByUser(new List<string> { "Alice", "Bob" })).Returns(new List<Post.Post>{
                new Post.Post("Alice", "Alice Message", DateTime.Parse("09/09/2017")),
                new Post.Post("Bob", "Bob Message", DateTime.Parse("08/09/2017")),
                new Post.Post("Alice", "Alice Message 2", DateTime.Parse("07/09/2017")),
                new Post.Post("Bob", "Bob Message 2", DateTime.Parse("06/09/2017")),
            });
            return mock;
        }
    }
}
