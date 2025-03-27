using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class UIGroupPoolMoveableElement<T> : UIGroupPool<T> where T : class
        {
            [SerializeField] private Button addBtn;
            [SerializeField] private Button removeBtn;
            [SerializeField] private Button duplicateBtn;
            [SerializeField] private Button moveLeftBtn;
            [SerializeField] private Button moveRightBtn;
            private IElementContainer<T> container;

            public override void Awake()
            {
                base.Awake();
                addBtn?.onClick.AddListener(CreateEmptyItem);
                removeBtn?.onClick.AddListener(RemoveSelectedItem);
                duplicateBtn?.onClick.AddListener(DuplicateSelectedItem);
                moveLeftBtn?.onClick.AddListener(MoveLeftSelectedItem);
                moveRightBtn?.onClick.AddListener(MoveRightSelectedItem);
            }
            public override void Init(UnityAction<T> selectCall, UnityAction deselectCall = null)
            {
                base.Init(selectCall, deselectCall);
                container = GetComponent<IElementContainer<T>>();
            }
            public void DisplayDataList()
            {
                Clear();
                foreach (var data in container?.GetDataList().GetList())
                    base.CreateItem(data);
                if (!canDeselect) SelectFirstItem();
            }
            public override UIElement<T> CreateItem(T data)
            {
                var dataList = container?.GetDataList();
                if (dataList == null) return null;
                if (data != null)
                    dataList.Add(data);
                else data = dataList.CreateEmpty();
                return base.CreateItem(data);
            }
            protected void CreateEmptyItem() => CreateItem(null);
            protected void DuplicateSelectedItem()
            {
                var itemData = GetSelectedItem()?.DuplicateData();
                if (itemData != null)
                    CreateItem(itemData);
            }
            protected void RemoveSelectedItem()
            {
                if (container?.GetDataList() == null) return;
                RemoveItem(selectedItemIndex);
            }
            protected override void ReleaseItem(int index)
            {
                container?.GetDataList().Remove(index);
                base.ReleaseItem(index);
            }
            public override void MoveItemArray(ref int startIndex, int newIndex, int len = 1)
            {
                var dataList = container?.GetDataList();
                if (dataList == null) return;
                base.MoveItemArray(ref startIndex, newIndex, len);
                for (int i = 0; i < items.Count; i++)
                    dataList.Set(i, items[i].data);
            }
            protected void MoveLeftSelectedItem()
            {
                if (container?.GetDataList() == null) return;
                if (Count <= 1 || selectedItemIndex == -1) return;
                if (selectedItemIndex - 1 >= 0)
                {
                    SwapItem(selectedItemIndex, selectedItemIndex - 1);
                    selectedItemIndex -= 1;
                }
                else MoveItemArray(ref selectedItemIndex, -1);
            }
            protected void MoveRightSelectedItem()
            {
                if (container?.GetDataList() == null) return;
                if (Count <= 1 || selectedItemIndex == -1) return;
                if (selectedItemIndex + 1 < items.Count)
                {
                    SwapItem(selectedItemIndex, selectedItemIndex + 1);
                    selectedItemIndex += 1;
                }
                else MoveItemArray(ref selectedItemIndex, 0);
            }
            private void SwapItem(int i1, int i2)
            {
                var dataList = container?.GetDataList();
                if (dataList == null) return;
                if (i1 > i2)
                    (i1, i2) = (i2, i1);
                var item1 = GetItem(i1);
                var item2 = GetItem(i2);
                item1.gameObject.transform.SetSiblingIndex(i2 + pool.CountInactive);
                item2.gameObject.transform.SetSiblingIndex(i1 + pool.CountInactive);
                (items[i2], items[i1]) = (items[i1], items[i2]);
                dataList.Swap(i1, i2);
                items[i2].index = i2;
                items[i1].index = i1;
            }
        }
    }
}