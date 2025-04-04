using UnityEngine.UI;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IUIElement : IObject
        {
            public int? index { get; set; }
            public UnityEvent<int> onSelectUI { get; set; }
            public void Init();
            public Image GetBackground();
            public void SelectAndInvoke();
        }
    }
}