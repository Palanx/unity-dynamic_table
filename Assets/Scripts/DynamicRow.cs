using System.Collections.Generic;
using UnityEngine;

namespace Die4Games
{
    public class DynamicRow : MonoBehaviour
    {
        public GameObject prefDynamicCol;

        private List<DynamicColumn> dynamicCols = new List<DynamicColumn>();

        public void Build(List<string> data, bool isHeader = false)
        {
            AddColumns(data, isHeader);
        }

        private void AddColumns(List<string> data, bool isHeader)
        {
            if (data == null)
                return;
            for (int i = 0; i < data.Count; i++)
            {
                if (i <= (dynamicCols.Count - 1))
                {
                    dynamicCols[i].Build(data[i], isHeader);
                    dynamicCols[i].gameObject.SetActive(true);
                }
                else
                {
                    DynamicColumn newCol = Instantiate(prefDynamicCol, transform, false).GetComponent<DynamicColumn>();
                    if (newCol == null)
                    {
                        Debug.LogError("Component [DynamicColumn] missing in prefDynamicCol");
                        return;
                    }
                    newCol.Build(data[i], isHeader);
                    dynamicCols.Add(newCol);
                }
            }
        }

        public void CleanColumns()
        {
            foreach (DynamicColumn col in dynamicCols)
            {
                col.gameObject.SetActive(false);
            }
        }
    }
}
