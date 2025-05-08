using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    [RequireComponent (typeof(ObjectItemList))]
    public class SelectRefObjectMenu : EventNotifyMenu
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemId;
        [SerializeField] private TextMeshProUGUI itemName;
        private ObjectItemList group;
        private ObjectDataObserver curObjectData;

        protected override void Preprocess()
        {
            base.Preprocess();
            EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            DisplayReferenceObject(null);
            group = gameObject.GetComponent<ObjectItemList>();
            group.Init(SelectReferenceObject);
        }
        protected override void CreateObject(ObjectDataObserver data)
            => DisplayObject(data);
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (curObjectData == data) return;
            curObjectData = data;
            group.SelectItem(curObjectData?.refData);
            DisplayReferenceObject(curObjectData?.refData);
        }
        protected override void RemoveObject(ObjectDataObserver data)
        {
            if (curObjectData == data)
                SelectObject(null);
            else if (curObjectData.refData == data)
                SelectReferenceObject(null);
            group.RemoveItem(data);
        }
        protected override void SelectWave(WaveDataObserver data)
        {
            group.Clear();
            data.GetList(out var dataList);
            foreach (var objectData in dataList)
                DisplayObject(objectData);
            DisplayReferenceObject(null);
        }
        private void DisplayObject(ObjectDataObserver data) 
            => group.CreateItem(data);
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