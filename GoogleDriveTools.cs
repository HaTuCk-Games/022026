using System;
using System.Collections.Generic;
using System.Text;

using UnityGoogleDrive;
using UnityGoogleDrive.Data;

namespace DefaultNamespace
{
    public static class GoogleDriveTools
    {
        public static List<File> FileList()
        {
            List<File> output = new List<File>();
            GoogleDriveFiles.List().Send().OnDone += fileList => { output = fileList.Files; };
            return output;
        }

        public static File Upload(String obj, Action onDone)
        {
            var file = new UnityGoogleDrive.Data.File { Name = "GameData.json", Content = Encoding.ASCII.GetBytes(obj) };
            GoogleDriveFiles.Create(file).Send();
            return file;
        }

        public static File Download(String fileID)
        {
            File output = new File();
            GoogleDriveFiles.Download(fileID).Send().OnDone += file => {output = file;};
            return output;
        }
    }
}
