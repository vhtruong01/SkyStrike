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
                EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            }
            public override void Init()
            {
                DisplayReferenceObject(null);
                objectItemUIGroupPool = gameObject.GetComponent<ObjectItemList>();
                objectItemUIGroupPool.Init(SelectReferenceObject);
            }
            protected override void CreateObject(ObjectDataObserver data) => DisplayObject(data);
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (curObjectData == data) return;
                curObjectData = data;
                objectItemUIGroupPool.SelectItem(curObjectData?.refData);
                DisplayReferenceObject(curObjectData?.refData);
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
                data.GetList(out var dataList);
                foreach (var objectData in dataList)
                    DisplayObject(objectData);
                DisplayReferenceObject(null);
            }
            private void DisplayObject(ObjectDataObserver data) => objectItemUIGroupPool.CreateItem(data);
            private void SelectReferenceObject(ObjectDataObserver refData)
            {
                if (curObjectData == null) return;
                if (refData != curObjectData.refData)
                {
                    if (refData == null || refData.IsValidChild(curObjectData))
                    {
                        EventManager.SetRefObject(refData);
                        curObjectData.SetRefData(refData);
                    }
                    else DisplayReferenceObject(null);
                }
                else DisplayReferenceObject(refData);
            }
            private void DisplayReferenceObject(ObjectDataObserver refData)
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