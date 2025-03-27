using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemUITool : MonoBehaviour
        {
            [SerializeField] private Button copyBtn;
            [SerializeField] private Button pasteBtn;
            [SerializeField] private Button removeBtn;
            [SerializeField] private Button cutBtn;
            [SerializeField] private NormalButton snapButton;
            private ObjectDataObserver tempItemData;
            private ObjectDataObserver curObjectDataObserver;

            public void Awake()
            {
                copyBtn.interactable = removeBtn.interactable = cutBtn.interactable = pasteBtn.interactable = false;
                copyBtn.onClick.AddListener(Copy);
                pasteBtn.onClick.AddListener(Paste);
                removeBtn.onClick.AddListener(Remove);
                cutBtn.onClick.AddListener(Cut);
                EventManager.onSelectObject.AddListener(SelectObject);
            }
            public void Start()
            {
                snapButton.AddListener(SnappableElement.EnableSnapping, SnappableElement.isSnap);
            }
            private void SelectObject(ObjectDataObserver data)
            {
                curObjectDataObserver = data;
                copyBtn.interactable = removeBtn.interactable = cutBtn.interactable = curObjectDataObserver != null;
            }
            protected void Copy()
            {
                tempItemData = curObjectDataObserver;
                if (!pasteBtn.interactable && tempItemData != null)
                    pasteBtn.interactable = true;
            }
            protected void Paste()
            {
                EventManager.CreateObject(tempItemData.Clone());
            }
            protected void Remove()
            {
                if (curObjectDataObserver != null)
                {
                    EventManager.RemoveObject(curObjectDataObserver);
                    curObjectDataObserver = null;
                    removeBtn.interactable = cutBtn.interactable = false;
                }
            }
            protected void Cut()
            {
                Copy();
                Remove();
            }
        }
    }
}