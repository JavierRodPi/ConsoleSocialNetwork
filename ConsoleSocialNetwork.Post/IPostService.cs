using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleSocialNetwork.Post
{
    public interface IPostService
    {
        Post AddPost (string user, string message, DateTime creationDate);
        IEnumerable<Post> GetPostsByUser (string user);
        IEnumerable<Post> GetWallByUser (string user);
    }
}
