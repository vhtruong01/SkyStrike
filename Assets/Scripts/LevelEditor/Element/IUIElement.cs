using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Editor
{
    public interface IUIElement : IObject
    {
        public int? index { get; set; }
        public UnityEvent<int> onSelectUI { get; }
        public void Init();
        public void SetBackgroundColor(Color c);
        public void SelectAndInvoke();
    }
}