using SkyStrike.Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
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
            [SerializeField] private List<ItemData> dropItemDataList;
            [SerializeField] private Button selectDropItemBtn;
            [SerializeField] private GameObject dropItemView;
            [SerializeField] private Image dropItemIcon;
            [Header("RefObject")]
            [SerializeField] private TextMeshProUGUI referenceObjectText;
            [SerializeField] private Button referenceObjectBtn;
            [SerializeField] private FloatSelectRefObjectMenu selectRefObjectMenu;
            [Header("Other")]
            [SerializeField] private Button addObjectBtn;
            [SerializeField] private Button pathBtn;
            [SerializeField] private PathMenu pathMenu;
            private DropItemList dropItemUIGroupPool;


            public override void Awake()
            {
                addObjectBtn.onClick.AddListener(CreateObject);
                referenceObjectBtn.onClick.AddListener(selectRefObjectMenu.Show);
                pathBtn.onClick.AddListener(pathMenu.Show);
                selectDropItemBtn.onClick.AddListener(() => dropItemView.SetActive(!dropItemView.activeSelf));
            }
            public override void Init()
            {
                base.Init();
                dropItemUIGroupPool = gameObject.GetComponent<DropItemList>();
                dropItemUIGroupPool.Init(SelectDropItem);
                foreach (var item in dropItemDataList)
                    dropItemUIGroupPool.CreateItem(item);
                EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            }
            private void SelectDropItem(ItemData itemData)
            {
                if (itemData == null)
                {
                    data.dropItemType = EItem.None;
                    dropItemIcon.gameObject.SetActive(false);
                }
                else
                {
                    data.dropItemType = itemData.type;
                    dropItemIcon.sprite = itemData.sprite;
                    dropItemIcon.gameObject.SetActive(true);
                }
            }
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
                if (data.dropItemType != EItem.None)
                {
                    foreach (var dropItemData in dropItemDataList)
                        if (dropItemData.type == data.dropItemType)
                        {
                            dropItemUIGroupPool.SelectAndInvokeItem(dropItemData);
                            return;
                        }
                }
                else dropItemUIGroupPool.SelectAndInvokeItem(null);
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
}