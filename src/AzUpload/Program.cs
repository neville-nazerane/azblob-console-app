using Azure.Storage.Blobs;

#region Setup

string connectionString = args[0];
string containerName = args[1];


string[] options = [
        "EmptyContainer"
    ];

string sourceDirectory = args.ElementAtOrDefault(2) ?? Directory.GetCurrentDirectory();

string[] selectedOptions = [];
if (args.Length > 2)
{
    if (options.Contains(args[2]))
        selectedOptions = args.Skip(2).ToArray();
    else if (args.Length > 3)
        selectedOptions = args.Skip(3).ToArray();
}

Console.WriteLine("\n\n\n");
#endregion

#region logic

var container = new BlobContainerClient(connectionString, containerName);

// clean up if needed
if (selectedOptions.Contains("EmptyContainer"))
{
    Console.WriteLine("Emptying container");
    await foreach (var blob in container.GetBlobsAsync())
        await container.GetBlobClient(blob.Name).DeleteAsync();
}

// uploading files
var chunks = Directory.GetFiles(sourceDirectory).Chunk(5);
foreach (var chunk in chunks)
    await Task.WhenAll(chunk.Select(UploadFileAsync));

Console.WriteLine("Upload complete");

#endregion

Task UploadFileAsync(string file)
{
    Console.WriteLine("Uploading " + file);
    var blob = container.GetBlobClient(Path.GetFileName(file));
    return blob.UploadAsync(file, true);
}



