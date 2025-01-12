using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Menu : MonoBehaviour
        {
            [SerializeField] protected Button collapseBtn;

            public virtual void Awake() => collapseBtn.onClick.AddListener(HandleCollapse);
            public virtual void HandleCollapse() => gameObject.SetActive(false);
        }
    }
}