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
            public int? index { get;set; }
            public GameObject gameObject { get; }
            public UnityEvent<IEditorData> onClick { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public IEditorData data { get; set; }
            public void Init();
            public Image GetBackground();
            public void SelectAndInvoke();
            public void SetData(IEditorData data);
            public void RemoveData();
        }
    }
}