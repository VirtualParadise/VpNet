using Microsoft.Extensions.Configuration;
using System;
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


for (int z = -2; z < 2; ++z)
{
    for (int x = -2; x < 2; ++x)
    {
        Console.Write($"Querying {x}, {z}...");
        var result = await client.QueryCellAsync(x, z);
        Console.WriteLine($" found {result.Objects.Count} objects, revision {result.Revision}.");
    }
}

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

