using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class FloatSelectRefObjectMenu : Menu
        {
            [SerializeField] private Image itemIcon;
            [SerializeField] private TextMeshProUGUI itemId;
            [SerializeField] private TextMeshProUGUI itemName;
            private ObjectItemList objectItemUIGroupPool;
            private ObjectDataObserver curObjectData;

            public override void Awake()
            {
                base.Awake();
                EventManager.onSetRefObject.AddListener(DisplayRefObject);
            }
            public override void Init()
            {
                DisplayRefObject(null);
                objectItemUIGroupPool = gameObject.GetComponent<ObjectItemList>();
                objectItemUIGroupPool.Init(SelectReferenceObject);
            }
            protected override void CreateObject(ObjectDataObserver data) => DisplayObject(data);
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (curObjectData == data) return;
                curObjectData = data;
                objectItemUIGroupPool.SelectItem(curObjectData?.refData);
                DisplayRefObject(curObjectData?.refData);
            }
            protected override void RemoveObject(ObjectDataObserver data)
            {
                if (curObjectData == data)
                    SelectObject(null);
                else if (curObjectData.refData == data)
                    SelectReferenceObject(null);
                objectItemUIGroupPool.RemoveItem(data);
            }
            protected override void SelectWave(WaveDataObserver data)
            {
                objectItemUIGroupPool.Clear();
                foreach (var objectData in data.GetList())
                    DisplayObject(objectData);
                DisplayRefObject(null);
            }
            private void DisplayObject(ObjectDataObserver data) => objectItemUIGroupPool.CreateItem(data);
            private void SelectReferenceObject(ObjectDataObserver refData)
            {
                if (curObjectData != null && curObjectData.refData != refData && (refData == null || refData.IsValidChild(curObjectData)))
                {
                    EventManager.SetRefObject(refData);
                    curObjectData.SetRefData(refData);
                }
                else DisplayRefObject(null);
            }
            private void DisplayRefObject(ObjectDataObserver refData)
            {
                if (refData != null && curObjectData != null)
                {
                    itemIcon.sprite = refData.metaData.data.sprite;
                    itemIcon.color = refData.metaData.data.color;
                    itemId.text = "Id: " + refData.id;
                    itemName.text = "Name: " + refData.name.data;
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