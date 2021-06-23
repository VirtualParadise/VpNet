using System;

namespace VpNet.IntegrationTests
{
    class Program
    {
        static VirtualParadiseClient s_client;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            s_client = new VirtualParadiseClient();
            MainAsync(args);
            s_client.OnAvatarEnter += VirtualParadiseClientOnAvatarEnter;
            s_client.OnAvatarLeave += VirtualParadiseClientOnAvatarLeave;
            Console.ReadKey();

        }

        private static void VirtualParadiseClientOnAvatarLeave(VirtualParadiseClient sender, AvatarLeaveEventArgs args)
        {
            sender.ConsoleMessage($"{args.Avatar.Name} has left {s_client.Configuration.World.Name}");
        }

        private static void VirtualParadiseClientOnAvatarEnter(VirtualParadiseClient sender, AvatarEnterEventArgs args)
        {
            sender.ConsoleMessage(args.Avatar, "greetings", $"Welcome to {s_client.Configuration.World.Name}, {args.Avatar.Name}.");
        }

        static async void MainAsync(string[] args)
        {
            await s_client.ConnectAsync();
            await s_client.LoginAsync("<<your username here>>", "<<yourpassword>>", "<<yourbotname>>");
            await s_client.EnterAsync("<<world here>>");
            s_client.UpdateAvatar();
        }
    }
}
