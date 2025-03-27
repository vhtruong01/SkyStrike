using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class HierarchyContent : MonoBehaviour, IDragHandler
        {
            public void OnDrag(PointerEventData eventData)
            {
                print(eventData.pointerDrag);
            }
        }
    }
}