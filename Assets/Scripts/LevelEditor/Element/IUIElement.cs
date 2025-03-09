using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IUIElement : IPointerClickHandler
        {
            public GameObject gameObject { get; }
            public UnityEvent<IEditorData> onClick { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public int? index { get;set; }

            public Image GetBackground();
            public void SetData(IEditorData data);
            public IEditorData GetData();
            public void RemoveData();
            public void Select();
        }
    }
}