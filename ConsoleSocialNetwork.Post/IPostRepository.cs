using System;
using System.Collections.Generic;

namespace ConsoleSocialNetwork.Post
{
    public interface IPostRepository
    {
        Post AddPost(string user, string message, DateTime dateTime);
        IEnumerable<Post> GetPostsByUser(string user);
        IEnumerable<Post> GetWallByUser(IEnumerable<string> users);
    }
}
