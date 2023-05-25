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

var terrainNodes = await client.QueryTerrainAsync(0, 0, new int[4 * 4]);
Console.WriteLine("Received {0} terrain nodes", terrainNodes.Length);

client.UpdateAvatar();

await TestGetUserAsync(client);

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

static async Task TestGetUserAsync(VirtualParadiseClient client)
{
    var task1 = client.GetUserAsync(1);
    var task2 = client.GetUserAsync(1);
    var user1 = await task1;
    var user2 = await task2;
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