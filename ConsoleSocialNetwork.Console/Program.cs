using ConsoleSocialNetwork.Application;
using ConsoleSocialNetwork.DataRepository;
using ConsoleSocialNetwork.Followers;
using ConsoleSocialNetwork.Post;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleSocialNetwork.ConsoleApp
{
    class Program
    {
        private const string Exit = "exit";
        private static ServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            Configure();
            var app = serviceProvider.GetService<IApplication>();
            while (true)
            {
                Console.Write("> ");
                var command = Console.ReadLine();
                if (command == Exit)
                {
                    break;
                }
                var response = app.ExecuteCommand(command);

                if (!string.IsNullOrEmpty(response))
                {
                    Console.WriteLine(response);
                }
            }
        }
        static void Configure()
        {
            serviceProvider = new ServiceCollection()
            .AddSingleton<IApplication, Application.Application>()
            .AddSingleton<IClock, SystemClock>()
            .AddSingleton<IPostService, PostService>()
            .AddSingleton<IUserFollowersService, UserFollowersService>()
            .AddSingleton<IPostRepository, InMemoryDBPostRepository>()
            .AddSingleton<IUserFollowersRepository, InMemoryDBUserFollowersRepository>()
            .BuildServiceProvider();
        }
    }
}
