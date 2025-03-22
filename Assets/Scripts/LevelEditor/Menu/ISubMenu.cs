using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public interface ISubMenu : IObserver
        {
            public GameObject gameObject { get; }
            public void Init();
            public bool CanDisplay();
            public void Hide();
            public void Show();
        }
    }
}