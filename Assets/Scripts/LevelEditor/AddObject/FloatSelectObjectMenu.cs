using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class FloatSelectObjectMenu : Menu, IDragHandler
        {
            [SerializeField] private UIGroupPool itemUIGroupPool;
            [SerializeField] private Image itemIcon;
            [SerializeField] private TextMeshProUGUI itemId;
            [SerializeField] private TextMeshProUGUI itemName;
            [SerializeField] private TextMeshProUGUI currentObjectWarning;
            private ObjectDataObserver curObjectData;

            public override void Awake()
            {
                base.Awake();
                itemUIGroupPool.selectDataCall = SelectReferenceObject;
                DisplayNone();
            }
            public override void Init() { }
            public void OnDrag(PointerEventData eventData)
            {
            }
            protected override void CreateObject(IEditorData data)
            {
                if (data is ObjectDataObserver objectData)
                    DisplayObject(objectData);
            }
            protected override void SelectObject(IEditorData data)
            {
                curObjectData = data as ObjectDataObserver;
                if (curObjectData != null && curObjectData.refData != null)
                    itemUIGroupPool.SelectItem(curObjectData.refData);
                else itemUIGroupPool.SelectNone();
                SelectReferenceObject(curObjectData?.refData);
            }
            protected override void RemoveObject(IEditorData data)
            {
            }
            protected override void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                itemUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.objectList)
                    DisplayObject(objectData);
            }
            private void DisplayObject(ObjectDataObserver data) => itemUIGroupPool.CreateItem(data);
            private void SelectReferenceObject(IEditorData data)
            {
                if (curObjectData != null)
                {
                    curObjectData.refData = data as ObjectDataObserver;
                    if (curObjectData.refData != null)
                    {
                        itemIcon.sprite = curObjectData.refData.metaData.data.sprite;
                        itemIcon.color = curObjectData.refData.metaData.data.color;
                        itemName.text = "Name: " + curObjectData.refData.name;
                        itemId.text = "Id: " + curObjectData.refData.id;
                        currentObjectWarning.gameObject.SetActive(false);
                        if (curObjectData.refData == curObjectData)
                        {
                            curObjectData.refData = null;
                            currentObjectWarning.gameObject.SetActive(true);
                        }
                        return;
                    }
                }
                DisplayNone();
            }
            private void DisplayNone()
            {
                itemIcon.color = new();
                itemName.text = "";
                itemId.text = "";
                currentObjectWarning.gameObject.SetActive(false);
            }
        }
    }
}