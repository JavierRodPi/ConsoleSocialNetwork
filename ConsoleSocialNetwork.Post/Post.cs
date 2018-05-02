using System;

namespace ConsoleSocialNetwork.Post
{
    public class Post
    {
        public string User { get; }
        public string Message { get; set; }
        public DateTime PostingDate { get; }
        public Post(string user, string message, DateTime postingDate)
        {
            User = user;
            Message = message;
            PostingDate = postingDate;
        }
        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (Post)obj;
            return
                User == p.User
                && Message == p.Message
                && PostingDate == p.PostingDate;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
