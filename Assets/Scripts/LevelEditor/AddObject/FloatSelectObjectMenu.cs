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
            private ObjectDataObserver curObjectData;

            public override void Awake()
            {
                base.Awake();
                itemUIGroupPool.selectDataCall = SelectReferenceObject;
                DisplayRefObject(null);
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
                if (curObjectData != null)
                    itemUIGroupPool.SelectItem(curObjectData.refData);
                else itemUIGroupPool.SelectNone();
                DisplayRefObject(curObjectData?.refData);
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
                var refData = data as ObjectDataObserver;
                if (curObjectData != null && curObjectData.refData != refData)
                {
                    if (refData == null || refData.IsValidChild(curObjectData))
                    {
                        EventManager.SetRefObject(refData);
                        curObjectData.refData = refData;
                    }
                    else refData = null;
                }
                DisplayRefObject(refData);
            }
            private void DisplayRefObject(ObjectDataObserver refData)
            {
                if (refData != null)
                {
                    itemIcon.sprite = refData.metaData.data.sprite;
                    itemIcon.color = refData.metaData.data.color;
                    itemName.text = "Name: " + refData.name;
                    itemId.text = "Id: " + refData.id;
                }
                else
                {
                    itemIcon.color = new();
                    itemName.text = "";
                    itemId.text = "";
                }
            }
        }
    }
}