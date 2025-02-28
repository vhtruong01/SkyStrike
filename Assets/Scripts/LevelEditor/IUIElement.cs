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
            public UnityEvent onClick { get; set; }
            public Image GetBackground();
        }
    }
}