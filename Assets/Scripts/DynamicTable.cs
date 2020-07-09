using UnityEngine;
using MiniJSON;
using System.IO;
using System.Collections;

namespace Die4Games
{
    public class DynamicTable : MonoBehaviour
    {
        private void Start()
        {
            string fileName = "JsonChallenge.json";

            FileInfo file = FileReaderUtility.GetFile(fileName);
            if (file == null)
            {
                Debug.LogError($"File {fileName} does not exist.");
                return;
            }

            string jsonText = FileReaderUtility.GetStringContentFromFile(file);
            if (string.IsNullOrEmpty(jsonText))
            {
                Debug.LogError($"File {fileName} is empty.");
                return;
            }

            IDictionary json = Json.Deserialize(jsonText) as IDictionary;
            string titulo = json["Title"] as string;
            IList headers = json["ColumnHeaders"] as IList;
            IList data = json["Data"] as IList;
            foreach (IDictionary item in data)
            {
                foreach (string header in headers)
                {
                    print(item[header]);
                }
            }
        }
    }
}

