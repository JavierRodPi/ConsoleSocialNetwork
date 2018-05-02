using ConsoleSocialNetwork.Followers;
using ConsoleSocialNetwork.Post;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleSocialNetwork.Application
{
    public class Application: IApplication
    {
        private enum Commands
        {
            None,
            Posting,
            Reading,
            Follow,
            Wall
        }
        private class Command
        {
            public Commands CommandName { get; set; }
            public string User { get; set; }
            public string UserToFollow { get; set; }
            public string Message { get; set; }
        }


        private readonly IPostService postService;
        private readonly IUserFollowersService userFollowersService;
        private readonly IClock clock;

        private Command command;

        public Application(IPostService postService, IUserFollowersService userFollowersService, IClock clock)
        {
            this.postService = postService;
            this.userFollowersService = userFollowersService;
            this.clock = clock;
        }


        public string ExecuteCommand(string commandString)
        {
            ExtracCommand(commandString);
            switch (command.CommandName)
            {
                case Commands.Posting:
                    AddPost(command.User, command.Message);
                    break;
                case Commands.Reading:
                    return GetUserPosts(command.User).ToString();
                case Commands.Follow:
                    return Follow(command.User, command.UserToFollow);
                case Commands.Wall:
                    return GetUserWall(command.User).ToString();
                default:
                    return "Command not valid";
            }
            return null;
        }

        private void ExtracCommand(string commandString)
        {
            command = new Command();
            if (!ExtractPostCommand(commandString))
                if (!ExtractFollowCommand(commandString))
                    if (!ExtractWallCommand(commandString))
                        if (!ExtractReadingCommand(commandString))
                            return;
        }

        private bool ExtractPostCommand(string commandString)
        {
            string postPattern = "^(\\w+) -> (.*)$";
            var matches = Regex.Match(commandString, postPattern);

            if (matches.Success
                && !string.IsNullOrEmpty(matches.Groups[1].Value)
                && !string.IsNullOrEmpty(matches.Groups[2].Value))
            {
                command.CommandName = Commands.Posting;
                command.User = matches.Groups[1].Value;
                command.Message = matches.Groups[2].Value;

                return true;
            }
            return false;
        }

        private bool ExtractReadingCommand(string commandString)
        {
            string postPattern = "^(\\w+)$";
            var matches = Regex.Match(commandString, postPattern);

            if (matches.Success && !string.IsNullOrEmpty(matches.Groups[0].Value))
            {
                command.CommandName = Commands.Reading;
                command.User = matches.Groups[1].Value;
                return true;
            }
            return false;
        }

        private bool ExtractFollowCommand(string commandString)
        {
            string postPattern = "^(\\w+) follows (\\w+)";
            var matches = Regex.Match(commandString, postPattern);

            if (matches.Success
                && !string.IsNullOrEmpty(matches.Groups[1].Value)
                && !string.IsNullOrEmpty(matches.Groups[2].Value)
            )
            {
                command.CommandName = Commands.Follow;
                command.User = matches.Groups[1].Value;
                command.UserToFollow = matches.Groups[2].Value;
                return true;
            }
            return false;
        }

        private bool ExtractWallCommand(string commandString)
        {
            string postPattern = "^(\\w+) wall$";
            var matches = Regex.Match(commandString, postPattern);

            if (matches.Success && !string.IsNullOrEmpty(matches.Groups[0].Value))
            {
                command.CommandName = Commands.Wall;
                command.User = matches.Groups[1].Value;
                return true;
            }
            return false;
        }

        private StringBuilder GetUserPosts(string user)
        {
            var posts = postService.GetPostsByUser(user);

            return new StringBuilder(string.Join(Environment.NewLine, posts.Select(p => PrintPost(p, false))));
        }

        private StringBuilder GetUserWall(string user)
        {
            var posts = postService.GetWallByUser(user);

            return new StringBuilder(
                string.Join(Environment.NewLine,
                    posts.Select(p => PrintPost(p, true))
                )
            );

        }

        private Post.Post AddPost(string user, string message)
        {
            return postService.AddPost(user, message, DateTime.Now);
        }

        private string Follow(string user, string follow)
        {
            try
            {
                userFollowersService.FollowUser(user, follow);
                return null;
            }
            catch (InvalidUserNameException ex)
            {
                return ex.Message;
            }
        }

        private string PrintPost(Post.Post post, bool addUser)
        {
            string printedPost = "";
            if (addUser)
                printedPost = $"{post.User} - ";
            return printedPost + $"{post.Message} ({GetDateDifferences(post.PostingDate)} ago)";
        }

        private string GetDateDifferences(DateTime dateTime)
        {
            var actualTime = clock.Now;

            var timeDifference = (actualTime - dateTime);

            int difference = (int)timeDifference.TotalDays;

            if (difference > 0)
                return $"{difference} day{(difference > 1 ? "s" : "")}";

            difference = (int)timeDifference.TotalHours;
            if (difference > 0)
                return $"{difference} hour{(difference > 1 ? "s" : "")}";

            difference = (int)timeDifference.TotalMinutes;
            if (difference > 0)
                return $"{difference} minute{(difference > 1 ? "s" : "")}";

            difference = (int)timeDifference.TotalSeconds;
            return $"{difference} second{(difference > 1 ? "s" : "")}";
        }
    }
}
