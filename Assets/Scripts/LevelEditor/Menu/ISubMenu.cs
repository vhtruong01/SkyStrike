using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public interface ISubMenu : IObserver
        {
            public GameObject gameObject { get; }
            public bool SetData(IEditorData data);
            public void Display(IEditorData data);
            public bool CanDisplay();
            public void Hide();
            public void Show();
        }
    }
}