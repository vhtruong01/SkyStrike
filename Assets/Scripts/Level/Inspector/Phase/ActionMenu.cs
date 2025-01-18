using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ActionMenu : MonoBehaviour
        {
            [SerializeField] protected TextMeshProUGUI index;
            public ActionUI actionUI { get; set; }

            public abstract void Display(ActionUI actionUI);
            public abstract void Clear();
        }
    }
}