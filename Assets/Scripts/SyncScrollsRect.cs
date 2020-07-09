using UnityEngine;
using UnityEngine.UI;

namespace Die4Games
{
    public class SyncScrollsRect : MonoBehaviour
    {
        public ScrollRect scrollToSync;

        public void Sync(Vector2 position)
        {
            scrollToSync.horizontalNormalizedPosition = position.x;
        }
    }
}
