using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public interface ISubMenu
        {
            public GameObject gameObject { get; }
            public bool SetData(IData data);
            public void Display(IData data);
            public bool CanDisplay();
            public void Hide();
            public void Show();
        }
    }
}