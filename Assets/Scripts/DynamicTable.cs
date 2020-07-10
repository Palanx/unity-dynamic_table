using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;

namespace Die4Games
{
    public class DynamicTable : MonoBehaviour
    {
        public Text txtTitle;
        public GameObject prefDynamicRow;
        public Transform headerContent;
        public Transform rowContent;

        private DynamicRow dynamicHeader = null;
        private List<DynamicRow> dynamicRows = new List<DynamicRow>();

        private bool processing = false;

        private void Awake()
        {
            CleanTitle();
            InitHeader();
        }

        private void InitHeader()
        {
            if (dynamicHeader != null)
                return;
            DynamicRow newRow = Instantiate(prefDynamicRow, headerContent, false).GetComponent<DynamicRow>();
            if (newRow == null)
            {
                Debug.LogError("Component [DynamicRow] missing in prefDynamicRow");
                return;
            }
            dynamicHeader = newRow;
        }

        public void LoadJSON(string fileName)
        {
            if (processing)
                return;
            processing = true;
            FileInfo file = FileReaderUtility.GetFile(fileName);
            if (file == null)
            {
                Debug.LogError($"File {fileName} does not exist.");
                processing = false;
                return;
            }

            string jsonText = FileReaderUtility.GetStringContentFromFile(file);
            if (string.IsNullOrEmpty(jsonText))
            {
                Debug.LogError($"File {fileName} is empty.");
                processing = false;
                return;
            }

            ProcessJSON(jsonText);
        }

        private void ProcessJSON(string jsonText)
        {
            CleanRows();
            CleanTitle();
            JSONNode json = JSON.Parse(jsonText);
            string titulo = json["Title"].Value;
            JSONNode headers = json["ColumnHeaders"];
            JSONNode data = json["Data"];

            List<string> headerData = new List<string>();
            List<List<string>> rowsData = new List<List<string>>();

            if (headers == null)
            {
                Debug.LogError($"[ColumnHeaders] list is missing in JSON");
                processing = false;
                return;
            }
            foreach (JSONNode header in headers.Values)
            {
                headerData.Add(header.Value);
            }

            if (data == null)
            {
                Debug.LogError($"[Data] list is missing in JSON");
                processing = false;
                return;
            }
            foreach (JSONNode item in data.AsArray)
            {
                List<string> colData = new List<string>();
                foreach (string header in headerData)
                {
                    JSONNode value = item[header];
                    if (value == null)
                        colData.Add("-");
                    else
                        colData.Add(value.Value);
                }
                rowsData.Add(colData);
            }

            SetTitle(titulo);
            AddHeader(headerData);
            AddRows(rowsData);

            processing = false;
        }

        public void CleanTitle()
        {
            if (txtTitle != null)
                txtTitle.text = string.Empty;
        }

        public void CleanRows()
        {
            if(dynamicHeader != null)
                dynamicHeader.CleanColumns();
            foreach (DynamicRow row in dynamicRows)
            {
                row.CleanColumns();
                row.gameObject.SetActive(false);
            }
        }

        public void SetTitle(string text)
        {
            if (txtTitle == null || text == null)
                return;
            txtTitle.text = text;
        }

        public void AddHeader(List<string> data)
        {
            if (data == null)
                return;
            dynamicHeader.Build(data, true);
        }

        public void AddRows(List<List<string>> rowsData)
        {
            if (rowsData == null)
                return;
            for (int i = 0; i < rowsData.Count; i++)
            {
                if(i <= (dynamicRows.Count - 1))
                {
                    dynamicRows[i].Build(rowsData[i]);
                    dynamicRows[i].gameObject.SetActive(true);
                }
                else
                {
                    DynamicRow newRow = Instantiate(prefDynamicRow, rowContent, false).GetComponent<DynamicRow>();
                    if (newRow == null)
                    {
                        Debug.LogError("Component [DynamicRow] missing in prefDynamicRow");
                        return;
                    }
                    newRow.Build(rowsData[i]);
                    dynamicRows.Add(newRow);
                }
            }
        }
    }
}

