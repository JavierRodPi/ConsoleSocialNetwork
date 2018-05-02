using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleSocialNetwork.Post;

namespace ConsoleSocialNetwork.DataRepository
{
    public class InMemoryDBPostRepository : IPostRepository
    {
        List<Post.Post> Posts = new List<Post.Post>();

        public Post.Post AddPost(string user, string message, DateTime dateTime)
        {
            var newPost = new Post.Post(user, message, dateTime);
            Posts.Add(newPost);
            return newPost;
        }

        public IEnumerable<Post.Post> GetPostsByUser(string user)
        {
            return Posts.Where(p => p.User == user).OrderByDescending(p => p.PostingDate);
        }

        public IEnumerable<Post.Post> GetWallByUser(IEnumerable<string> users)
        {
            return
                (from user in users
                 join post in Posts on user equals post.User
                 select post
                 ).OrderByDescending(p => p.PostingDate);
        }
    }
}
