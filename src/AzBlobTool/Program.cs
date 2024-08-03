



using AzBlobTool.Models;
using System.Net.WebSockets;

Console.WriteLine("Hello, World!");

ArgsBase BuildArgs()
{
    if (args.Length < 2)
    {
        Console.WriteLine("Invalid command");
        PrintInstructions();
        return null;
    }

    var command = args.First();
    if (!Enum.TryParse<ValidArgTypes>(command, out var cmd))
    {
        Console.WriteLine($"'{command}' is an invalid command");
        PrintValidCommands();
        return null;
    }


    var result = cmd switch
    {
        ValidArgTypes.Empty => CreateEmptyArgs(),
        //ValidArgTypes.Upload => throw new NotImplementedException(),
        _ => null
    };


    return null;
}


static EmptyArgs CreateEmptyArgs()
{
    return new();
} 

static void PrintInstructions()
{
    Console.WriteLine("Valid syntax:");
    Console.WriteLine("azblobez <command> <connection string> [arguments]");
    PrintValidCommands();
}

static void PrintValidCommands()
{
    Console.WriteLine("Valid commands:");
    foreach (var valid in Enum.GetNames<ValidArgTypes>())
        Console.WriteLine($" {valid}");
}