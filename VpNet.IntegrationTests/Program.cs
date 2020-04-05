using System;
using System.Threading.Tasks;

namespace VpNet.IntegrationTests
{
    class Program
    {
        static ManagedApi.Instance instance;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            instance = new ManagedApi.Instance();
            await instance.ConnectAsync();
            await instance.LoginAsync("<<your username here>>", "<<yourpassword>>", "VPNET2.x");
            await instance.EnterAsync("<<world here>>");
            instance.UpdateAvatar();
            instance.OnAvatarEnter += Instance_OnAvatarEnter;
            instance.OnAvatarLeave += Instance_OnAvatarLeave;
            Console.ReadKey();
        }

        private static void Instance_OnAvatarLeave(ManagedApi.Instance sender, AvatarLeaveEventArgs args)
        {
            sender.ConsoleMessage($"{args.Avatar.Name} has left {instance.Configuration.World.Name}");
        }

        private static void Instance_OnAvatarEnter(ManagedApi.Instance sender, AvatarEnterEventArgs args)
        {
            sender.ConsoleMessage(args.Avatar, "greetings", $"Welcome to {instance.Configuration.World.Name}, {args.Avatar.Name}.");
        }
    }
}
