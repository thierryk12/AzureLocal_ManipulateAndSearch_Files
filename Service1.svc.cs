using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using WCFServiceWebRole1.Class;

using System.IO.Compression;

using Microsoft.WindowsAzure.ServiceRuntime;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public string ListFiles(string directoryName)
        {
            //Connect to my container and find the directory
            CloudBlobDirectory dir = connectContainer("mycontainer").GetDirectoryReference(directoryName);

            //Get the List of all files and directories 
            var list = dir.ListBlobs();

            //FileClass is the class we will use when convert list of string to json format
            List<FileClass> filelist = new List<FileClass>();

            //Loop the list and convert it to a filesclass list
            foreach (var item in list)
            {
                var file = item as CloudBlockBlob;
                FileClass f = new FileClass();
                f.Filename = file.Name;
                filelist.Add(f);

            }

            //Finally convert to JSON format
            return JsonConvert.SerializeObject(filelist);
        }
        public string ListFoldersWithJson()
        {


            var list = connectContainer("mycontainer").ListBlobs();

            List<Folder> folderlist = new List<Folder>();

            foreach (var item in list)
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    var directory = item as CloudBlobDirectory;
                    Folder folder = new Folder();
                    folder.Name = directory.Prefix.ToString();
                    folderlist.Add(folder);

                }

            }


            return JsonConvert.SerializeObject(folderlist);
        }
        public List<string> ListFolders()
        {

            var list = connectContainer("mycontainer").ListBlobs();

            List<String> folderlist = new List<String>();
            foreach (var item in list)
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    var directory = item as CloudBlobDirectory;
                    folderlist.Add(directory.Prefix.ToString());
                }
            }
            return folderlist;
        }
        public string downloadfile(string filename, string directoryName,string downloadPath)
        {

            using (var fileStream = File.OpenWrite(downloadPath))
            {
                findFile(filename, directoryName).DownloadToStream(fileStream);
            }

            return "download!!";
        }
        public string downloadZipFile(string filename, string directory, string downloadPath)
        {
            downloadfile(filename, directory, downloadPath);

            return "downloadZip!!";
        }

        private void downloadFolder(string folderName,string containerName)
        {
            LocalResource myStorage = RoleEnvironment.GetLocalResource("LocalStorage1");

            CloudBlobDirectory dir = connectContainer("mycontainer").GetDirectoryReference(folderName);

            Directory.CreateDirectory(myStorage.RootPath + "mycontainer" + "\\" + folderName);

            var list = dir.ListBlobs();

            foreach (var item in list)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    var file = item as CloudBlockBlob;

                    string filename = file.Parent.Uri.MakeRelativeUri(file.Uri).ToString();
                    string filePath = Path.Combine(myStorage.RootPath + "mycontainer" + "\\" + folderName, filename);

                    findFile(filename, folderName).DownloadToFile(filePath, FileMode.Create);
                }

                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    var directory = item as CloudBlobDirectory;
                    downloadFolder(directory.Prefix.ToString(), containerName);
                }
            }




        }

        public string uploadfile(string filepath, string filename, string directory)
        {

            using (var fileStream = File.OpenRead(filepath))
            {
                findFile(filename, directory).UploadFromStream(fileStream);
            }
            return "sss";
        }
        public bool uploaddirectory(string containerName, string localstoragepath, string prefixAzureFolderName)
        {

            LocalResource myStorage = RoleEnvironment.GetLocalResource("LocalStorage1");

            string[] filePaths = Directory.GetFiles(localstoragepath);

            foreach (var item in filePaths)
            {
                //we will not copy the zip file so we do the filter here
                if (!Path.GetExtension(item).Equals(".zip"))
                {
                    //Get the filename
                    string result;
                    result = Path.GetFileName(item);
                    //Upload the file
                    uploadfile(item,result, prefixAzureFolderName);

                }
            }

            var folder = new DirectoryInfo(localstoragepath);
            var subFolders = folder.GetDirectories();

            foreach (var directoryInfo in subFolders)
            {
                var prefix = directoryInfo.Name;

                uploaddirectory("mycontainer", myStorage.RootPath + prefix, prefixAzureFolderName + "/" + prefix);

            }

            return true;
        }
        

        public bool zipDirectory(string directory)
        {
            downloadFolder(directory,"mycontainer");

            LocalResource myStorage = RoleEnvironment.GetLocalResource("LocalStorage1");

            string directoryName = myStorage.RootPath + "mycontainer\\" + directory+"\\";
            string zipFileFullPath = myStorage.RootPath + "mycontainer\\" + directory + ".zip";
            string zipFileName = directory + ".zip";
            ZipFile.CreateFromDirectory(directoryName, zipFileFullPath, CompressionLevel.Fastest, true);

            uploadfile(zipFileFullPath, zipFileName, "archive");

            File.Delete(zipFileFullPath);

            return true;
        }
        public string unzipFilesystem(string filename, string directory)
        {

            try
            {
                //Get the local storage
                LocalResource myStorage = RoleEnvironment.GetLocalResource("LocalStorage1");

                //Download the zip file from blob storage to the local storage
                //use the create mode to override the file 
                string filePath = Path.Combine(myStorage.RootPath, filename);
                findFile(filename, directory).DownloadToFile(filePath, FileMode.Create);

                //Get the name of zip file without the extension like ".zip"
                string foldername = Path.GetFileNameWithoutExtension(filePath);

                //unzip all files in the local storage root
                ZipFile.ExtractToDirectory(filePath, myStorage.RootPath);

                uploaddirectory("mycontainer", myStorage.RootPath, foldername);

                return "success";


            }
            catch (RoleEnvironmentException e)
            {
                return "The local resource isn't defined or doesn't exist. \n" + e.ToString();
            }

        }

        public static CloudBlobContainer connectContainer(string containerName)
        {
            var connectionString = "UseDevelopmentStorage=true";
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        public static CloudBlockBlob findFile(string filename, string directory)
        {
            if (string.IsNullOrEmpty(directory) == true)
            {
                return connectContainer("mycontainer").GetBlockBlobReference(filename);

            }
            else
            {
                CloudBlobDirectory dir = (CloudBlobDirectory)connectContainer("mycontainer").GetDirectoryReference(directory);
                return dir.GetBlockBlobReference(filename);


            }
        }

        public static void uploadbackupfile(string zipFilePath, string zipfilename, string containername)

        {
            using (var fileStream = File.OpenRead(zipFilePath))
            {
                CloudBlockBlob cloudblockblob = connectContainer(containername).GetBlockBlobReference(zipfilename);
                cloudblockblob.UploadFromStream(fileStream);
            }
        }

        public bool backupContainer()
        {



            LocalResource myStorage = RoleEnvironment.GetLocalResource("Backup");
            var list = connectContainer("mycontainer").ListBlobs();

            Directory.CreateDirectory(myStorage.RootPath + "mycontainer");

            foreach (var item in list)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    var file = item as CloudBlockBlob;

                    string filename = file.Name;

                    string filePath = Path.Combine(myStorage.RootPath + "mycontainer", filename);

                    findFile(filename, string.Empty).DownloadToFile(filePath, FileMode.Create);
                }

                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    var directory = item as CloudBlobDirectory;

                    downloadFolder(directory.Prefix.ToString(),"backup");

                }



            }


            string directoryName = myStorage.RootPath + "mycontainer";

            DateTime time = DateTime.Now;
            string format = "h-mm-M-d-yy";
            string zipfilename = "mycontainer-" + time.ToString(format) + ".zip";
            string zipFilePath = myStorage.RootPath + zipfilename;

            ZipFile.CreateFromDirectory(directoryName, zipFilePath, CompressionLevel.Fastest, true);

            uploadbackupfile(zipFilePath, zipfilename, "backup");

            Directory.Delete(myStorage.RootPath + "mycontainer", true);

            return true;
        }

        
    }



}
