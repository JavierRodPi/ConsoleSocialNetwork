using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleSocialNetwork.Followers;

namespace ConsoleSocialNetwork.Post
{
    public class PostService: IPostService
    {
        IPostRepository postRepository;
        IUserFollowersService userFollowersService;

        public PostService(IPostRepository postRepository, IUserFollowersService userFollowersService)
        {
            this.postRepository = postRepository;
            this.userFollowersService = userFollowersService;
        }

        public Post AddPost(string user, string message, DateTime creationDate)
        {
            userFollowersService.AddUser(user);
            return postRepository.AddPost(user, message, creationDate);
        }

        public IEnumerable<Post> GetPostsByUser(string user)
        {
            return postRepository.GetPostsByUser(user);
        }

        public IEnumerable<Post> GetWallByUser(string user)
        {
            var userFollowers = new List<string> { user };
            var followers = userFollowersService.GetFollowed(user);

            if (followers != null)
                userFollowers.AddRange(followers);
            return postRepository.GetWallByUser(userFollowers);
        }
    }
}
