using NUnit.Framework;
using System;

namespace ConsoleSocialNetwork.Test
{
    [TestFixture]
    public class ApplicationTest
    {
        Application.Application application;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            application = new Application.Application(PostServiceMock.GetMock().Object, UserFollowersServiceMock.GetMock().Object, new SystemClockStatic());
        }

        [Test]
        public void Application_ExecuteCommand_InvalidCommand_ErrorMessage()
        {
            var expected = "Command not valid";
            Assert.AreEqual(expected, application.ExecuteCommand("%$&£"));
            Assert.AreEqual(expected, application.ExecuteCommand("Alice folows Javier"));
            Assert.AreEqual(expected, application.ExecuteCommand("Bob ->message"));
        }

        [Test]
        public void Application_ExecuteCommand_Posting_Null()
        {
            var result = application.ExecuteCommand("Alice -> Message Alice");
            Assert.IsNull(result);
        }

        [Test]
        public void Application_ExecuteCommand_ReadingUserExist_PostsString()
        {
            var expected = "Great day (2 seconds ago)" +
               $"{Environment.NewLine}" +
               "Hello world (1 minute ago)";
            var result = application.ExecuteCommand("Alice");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Application_ExecuteCommand_ReadingUserNotExist_Empty()
        {
            var result = application.ExecuteCommand("Javier");
            Assert.IsEmpty(result);
        }

        [Test]
        public void Application_ExecuteCommand_FollowUserExist_Null()
        {
            var result = application.ExecuteCommand("Alice follows Bob");
            Assert.IsNull(result);
        }

        [Test]
        public void Application_ExecuteCommand_FollowUserNotExist_Error()
        {
            var expected = "Invalid User Name: Javier";
            var result = application.ExecuteCommand("Alice follows Javier");
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Application_ExecuteCommand_WallUSerNotExist_Empty()
        {
            var result = application.ExecuteCommand("Javier wall");
            Assert.IsEmpty(result);
        }

        [Test]
        public void Application_ExecuteCommand_WallUSerNotExist_PostsString()
        {
            var expected = "Alice - Nice weather (2 seconds ago)" +
                $"{Environment.NewLine}" +
                "Bob - Second post (1 minute ago)" +
                $"{Environment.NewLine}" +
                "Alice - Hello world (2 minutes ago)" +
                $"{Environment.NewLine}" +
                "Bob - First post (1 hour ago)";
            var result = application.ExecuteCommand("Alice wall");
            Assert.AreEqual(expected, result);
        }
    }
}
