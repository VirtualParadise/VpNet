using System;
using System.Threading.Tasks;
using VpNet;

Console.WriteLine("Hello World!");

VirtualParadiseClient client = new();
client.AvatarEntered += VirtualParadiseClientOnAvatarEnter;
client.AvatarLeft += VirtualParadiseClientOnAvatarLeave;

await client.ConnectAsync();
await client.LoginAsync("<<your username here>>", "<<yourpassword>>", "<<yourbotname>>");
await client.EnterAsync("<<world here>>");
client.UpdateAvatar();

while (true)
{
    await Task.Delay(10000);
}


void VirtualParadiseClientOnAvatarLeave(VirtualParadiseClient sender, AvatarLeaveEventArgs args)
{
    sender.ConsoleMessage($"{args.Avatar.Name} has left {client.Configuration.World.Name}");
}

void VirtualParadiseClientOnAvatarEnter(VirtualParadiseClient sender, AvatarEnterEventArgs args)
{
    sender.ConsoleMessage(args.Avatar, "greetings", $"Welcome to {client.Configuration.World.Name}, {args.Avatar.Name}.");
}

