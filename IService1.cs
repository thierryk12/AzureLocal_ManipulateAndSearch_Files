using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {




        [OperationContract]
        string ListFoldersWithJson();

        [OperationContract]
        List<String> ListFolders();

        [OperationContract]
        string ListFiles(string directoryName);

            

        [OperationContract]
        string uploadfile(string filePath, string fileName, string directory);

        [OperationContract]
        string downloadfile(string fileName, string directoryName,string downloadPath);

        [OperationContract]
        string downloadZipFile(string fileName, string directoryName, string downloadPath);

        [OperationContract]
        string unzipFilesystem(string fileName, string directoryName);

        [OperationContract]
        bool uploaddirectory(string containerName, string localStoragePath, string prefixAzureFolderName);

        [OperationContract]
        bool zipDirectory(string directoryName);

        [OperationContract]
        bool backupContainer();
       
            
            // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
