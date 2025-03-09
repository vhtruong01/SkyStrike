using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class HierarchyItemUI : UIElement
        {
            private static readonly int paddingLen = 30;
            [SerializeField] private RectTransform spaceItem;
            [SerializeField] private TextMeshProUGUI objectName;

            //public override void OnPointerClick(PointerEventData eventData) => InvokeData();
        }
    }
}