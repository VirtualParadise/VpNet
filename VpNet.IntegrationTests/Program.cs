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

await TestQuery(client);

await TestBuilding(client);

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

static async Task TestQuery(VirtualParadiseClient client)
{
    for (int z = -2; z < 2; ++z)
    {
        for (int x = -2; x < 2; ++x)
        {
            Console.Write($"Querying {x}, {z}...");
            var result = await client.QueryCellAsync(x, z);
            Console.WriteLine($" found {result.Objects.Count} objects, revision {result.Revision}.");
        }
    }
}

static async Task TestBuilding(VirtualParadiseClient client)
{
    var objectId = await client.AddObjectAsync(new VpObject
    {
        Model = "sign1.rwx",
        Description = "VpNet\ntest\nobject",
        Action = "create sign",
        Position = new Vector3(-15, 0, -15),
    });

    await Task.Delay(1000);

    await client.ChangeObjectAsync(new VpObject
    {
        Id = objectId,
        Model = "sign1.rwx",
        Description = "VpNet\ntest\nobject",
        Action = "create sign",
        Position = new Vector3(-15, 0.1, -15)
    });

    await Task.Delay(1000);

    await client.DeleteObjectAsync(objectId);
}