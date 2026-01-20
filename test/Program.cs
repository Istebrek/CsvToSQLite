using Azure;
using Azure.Storage.Files.Shares;
using System.Configuration;
using System.IO;

//Detta exempel fungerar!!

string connectionString = "DefaultEndpointsProtocol=https;AccountName=csvtodb;AccountKey=???;EndpointSuffix=core.windows.net";
string shareName = "testingdb"; //skapar upp en ny med detta namn, undersök att använda ett som redan är befintligt.
string fileName = "test.db"; //Undersök att skriva över gammal db
string mydirName = "testdir"; //skapar en ingrening/directory inom file share

string localFilePath = Directory.GetCurrentDirectory() + "/test.db"; //definierar var sqlite db filen är i repot
ShareClient myshare = new ShareClient(connectionString, shareName);
myshare.Create();
ShareDirectoryClient directory = myshare.GetDirectoryClient(mydirName);
directory.Create();
ShareFileClient myfile = directory.GetFileClient(fileName);
using FileStream stream = File.OpenRead(localFilePath);
myfile.Create(stream.Length);
myfile.UploadRange(new HttpRange(0, stream.Length), stream);

Console.WriteLine("done!");