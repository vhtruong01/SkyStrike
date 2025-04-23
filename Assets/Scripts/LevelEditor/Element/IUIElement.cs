using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike.Editor
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