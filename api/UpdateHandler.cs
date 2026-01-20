using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System.IO;
using System.Text;
namespace api;

public class UpdateHandler
{
    public string UploadCSV(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName);
        if (!extension.Contains(".csv")) return "Upload valid file.";
        string fileName = "products.csv";
        string path = Path.Combine(Directory.GetCurrentDirectory(), "../");
        using FileStream stream = new FileStream(path + fileName, FileMode.Create);
        file.CopyTo(stream);
        return "Created csv file";
    }

    public string Converter(IFormFile file)
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=csvtodb;AccountKey=???;EndpointSuffix=core.windows.net";
        string shareName = "testdb";
        string fileName = "test.db";
        string mydirName = "testdir";

        string localFilePath = connectionString + "/testdb";
        ShareClient myshare = new ShareClient(connectionString, shareName);
        myshare.Create();
        ShareDirectoryClient directory = myshare.GetDirectoryClient(mydirName);
        directory.Create();
        ShareFileClient myfile = directory.GetFileClient(fileName);
        FileStream stream = File.OpenRead(localFilePath);
        myfile.Create(stream.Length);
        myfile.UploadRange(new HttpRange(0, stream.Length), stream);
        return "";
    }
}
