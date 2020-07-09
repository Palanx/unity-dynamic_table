using UnityEngine;
using System.IO;

namespace Die4Games
{
    public class FileReaderUtility
    {
        public static string GetStringContentFromFile(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
                return null;

            string stringContent = string.Empty;
            using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read))
            {
                StreamReader streamReader = new StreamReader(fileStream);
                stringContent = new StreamReader(fileStream).ReadToEnd();
                streamReader.Close();
                fileStream.Close();
            }
            return stringContent;
        }

        public static FileInfo GetFile(string fileName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
            FileInfo[] allFiles = directoryInfo.GetFiles(fileName);
            if (allFiles.Length == 0)
                return null;
            return allFiles[0];
        }
    }
}
