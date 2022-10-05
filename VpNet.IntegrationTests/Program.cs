using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VpNet;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

VirtualParadiseClient client = new();
client.AvatarEntered += VirtualParadiseClientOnAvatarEnter;
client.AvatarLeft += VirtualParadiseClientOnAvatarLeave;

await client.ConnectAsync();
await client.LoginAsync(config["Username"], config["Password"], config["BotName"]);
await client.EnterAsync(config["World"]);
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

