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
            public UnityEvent<int> onSelectUI { get; set; }
            public GameObject gameObject { get; }
            public void Init();
            public Image GetBackground();
            public void SelectAndInvoke();
        }
    }
}