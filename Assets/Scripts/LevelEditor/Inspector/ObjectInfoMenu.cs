using SkyStrike.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class ObjectInfoMenu : SubMenu<ObjectDataObserver>
    {
        [Header("Property")]
        [SerializeField] private StringProperty objectName;
        [SerializeField] private FloatProperty velocity;
        [SerializeField] private FloatProperty delay;
        [SerializeField] private FloatProperty size;
        [SerializeField] private BoolProperty isMaintain;
        [SerializeField] private IntProperty cloneCount;
        [SerializeField] private FloatProperty spawnInterval;
        [Header("Object")]
        [SerializeField] private Image icon;
        [SerializeField] private Image referenceObjectIcon;
        [Header("DropItem")]
        [SerializeField] private List<ItemMetaData> dropItemDataList;
        [SerializeField] private Button selectDropItemBtn;
        [SerializeField] private GameObject dropItemView;
        [SerializeField] private Image dropItemIcon;
        [Header("RefObject")]
        [SerializeField] private TextMeshProUGUI referenceObjectText;
        [SerializeField] private Button referenceObjectBtn;
        [SerializeField] private SelectRefObjectMenu selectRefObjectMenu;
        [Header("Other")]
        [SerializeField] private Button addObjectBtn;
        [SerializeField] private Button pathBtn;
        [SerializeField] private PathMenu pathMenu;
        private DropItemList group;
        private Dictionary<EItem, ItemMetaData> itemDict;

        protected override void Preprocess()
        {
            base.Preprocess();
            addObjectBtn.onClick.AddListener(CreateObject);
            referenceObjectBtn.onClick.AddListener(selectRefObjectMenu.Show);
            pathBtn.onClick.AddListener(pathMenu.Show);
            selectDropItemBtn.onClick.AddListener(() => dropItemView.SetActive(!dropItemView.activeSelf));
            group = gameObject.GetComponent<DropItemList>();
            group.Init(SelectDropItem);
            itemDict = new();
            foreach (var item in dropItemDataList)
                itemDict.Add(item.type, item);
            itemDict.Add(EItem.None, null);
            EventManager.onGetItemMetaData.AddListener(GetItem);
            EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
        }
        public void Start()
        {
            foreach (var item in dropItemDataList)
                group.CreateItem(item);
        }
        private void SelectDropItem(ItemMetaData itemData)
        {
            if (itemData == null)
            {
                data.dropItemType.SetData(EItem.None);
                dropItemIcon.gameObject.SetActive(false);
            }
            else
            {
                data.dropItemType.SetData(itemData.type);
                dropItemIcon.sprite = itemData.sprite;
                dropItemIcon.gameObject.SetActive(true);
            }
        }
        private ItemMetaData GetItem(EItem type)
            => itemDict.TryGetValue(type, out var item) ? item : null;
        private void DisplayReferenceObject(ObjectDataObserver refData)
        {
            if (refData == null)
            {
                referenceObjectIcon.color = new();
                referenceObjectText.text = "";
            }
            else
            {
                referenceObjectIcon.color = refData.metaData.data.color;
                referenceObjectIcon.sprite = refData.metaData.data.sprite;
                referenceObjectText.text = refData.name.data;
            }
            pathBtn.gameObject.SetActive(refData == null && data.id.data != ObjectDataObserver.NULL_OBJECT_ID);
        }
        private void CreateObject()
        {
            if (data != null)
            {
                var cloneData = data.Clone();
                if (data.id.data != ObjectDataObserver.NULL_OBJECT_ID)
                    cloneData.SetRefData(data);
                EventManager.CreateObject(cloneData);
            }
        }
        public override void BindData()
        {
            delay.Bind(data.moveData.delay);
            velocity.Bind(data.moveData.velocity);
            size.Bind(data.size);
            objectName.Bind(data.name);
            cloneCount.Bind(data.cloneCount);
            spawnInterval.Bind(data.spawnInterval);
            isMaintain.Bind(data.isMaintain);
            icon.sprite = data.metaData.data.sprite;
            icon.color = data.metaData.data.color;
            DisplayReferenceObject(data.refData);
            if (data.dropItemType.data != EItem.None)
            {
                foreach (var dropItemData in dropItemDataList)
                    if (dropItemData.type == data.dropItemType.data)
                    {
                        group.SelectItem(dropItemData);
                        return;
                    }
            }
            else group.SelectItem(null);
        }
        public override void UnbindData()
        {
            delay.Unbind();
            size.Unbind();
            velocity.Unbind();
            objectName.Unbind();
            cloneCount.Unbind();
            spawnInterval.Unbind();
            isMaintain.Unbind();
        }
    }
}