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
            private void SelectObject(IEditorData data)
            {
                curObjectDataObserver = data as ObjectDataObserver;
                copyBtn.interactable = removeBtn.interactable = cutBtn.interactable = curObjectDataObserver != null;
            }
            public void Copy()
            {
                tempItemData = curObjectDataObserver;
                if (!pasteBtn.interactable && tempItemData != null)
                    pasteBtn.interactable = true;
            }
            public void Paste()
            {
                EventManager.CreateObject(tempItemData.Clone());
            }
            public void Remove()
            {
                if (curObjectDataObserver != null)
                    EventManager.RemoveObject(curObjectDataObserver);
            }
            public void Cut()
            {
                Copy();
                Remove();
            }
        }
    }
}