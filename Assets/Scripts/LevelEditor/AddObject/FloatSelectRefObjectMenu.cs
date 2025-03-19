using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class FloatSelectRefObjectMenu : Menu
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
            }
            public override void Init() => DisplayRefObject(null);
            protected override void CreateObject(IEditorData data)
            {
                if (data is ObjectDataObserver objectData)
                    DisplayObject(objectData);
            }
            protected override void SelectObject(IEditorData data)
            {
                curObjectData = data as ObjectDataObserver;
                itemUIGroupPool.SelectItem(curObjectData?.refData);
                DisplayRefObject(curObjectData?.refData);
            }
            protected override void RemoveObject(IEditorData data)
            {
                if (curObjectData == data)
                    SelectObject(null);
                else if (curObjectData.refData == data)
                    SelectReferenceObject(null);
                itemUIGroupPool.RemoveItem(data);
            }
            protected override void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                itemUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.GetList())
                    DisplayObject(objectData);
                DisplayRefObject(null);
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