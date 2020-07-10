using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Die4Games
{
    public class DynamicColumn : MonoBehaviour
    {
        public Text txtMessage;

        private void Awake()
        {
            InitText();
        }

        private void InitText()
        {
            if (txtMessage == null)
                txtMessage = GetComponentInChildren<Text>();
            if (txtMessage == null)
            {
                Debug.LogError("[Text] component missing in DynamicColumn children");
                return;
            }
            txtMessage.text = string.Empty;
        }

        public void Build(string text, bool isHeader)
        {
            if (txtMessage == null || text == null)
                return;
            if (isHeader)
                txtMessage.fontStyle = FontStyle.Bold;
            txtMessage.text = text;
        }
    }
}
