# AzureLocal_ManipulateAndSearch_Files

Here are all the functions you can use:

        String ListFoldersWithJson();
  
        String ListFolders();
        
        string ListFiles(string directoryName);

        string uploadfile(string filePath, string fileName, string directory);
        
        string downloadfile(string fileName, string directoryName,string downloadPath);
        
        string downloadZipFile(string fileName, string directoryName, string downloadPath);
      
        string unzipFilesystem(string fileName, string directoryName);
    
        bool uploaddirectory(string containerName, string localStoragePath, string prefixAzureFolderName);
      
        bool zipDirectory(string directoryName);

        bool backupContainer();
