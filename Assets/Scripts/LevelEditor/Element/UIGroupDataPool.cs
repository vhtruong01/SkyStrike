using UnityEngine.Events;

namespace SkyStrike.Editor
{
    public abstract class UIGroupDataPool<T> : UIGroupPool<T> where T : IEditor
    {
        protected IElementContainer<T> container;

        public override void Init(UnityAction<T> selectCall, UnityAction<T> deselectCall = null)
        {
            base.Init(selectCall, deselectCall);
            container = GetComponent<IElementContainer<T>>();
        }
        public virtual void DisplayDataList()
        {
            Clear();
            container.GetDataList().GetList(out var dataList);
            foreach (var data in dataList)
                CreateItem(data);
            if (!canDeselect) SelectFirstItem();
        }
        public virtual UIElement<T> CreateItemAndAddData(T data)
        {
            var dataList = container?.GetDataList();
            if (dataList == null) return null;
            if (data != null)
                dataList.Add(data);
            else dataList.CreateEmpty(out data);
            return CreateItem(data);
        }
        public void CreateEmptyItem() => CreateItemAndAddData(default);
        public void DuplicateSelectedItem()
        {
            var selectedItem = GetSelectedItem();
            if (selectedItem != null)
            {
                var itemData = GetSelectedItem().DuplicateData();
                if (itemData != null)
                    CreateItemAndAddData(itemData);
            }
        }
        public virtual bool RemoveSelectedItem()
        {
            if (container?.GetDataList() == null) return false;
            return RemoveItem(selectedItemIndex);
        }
        protected override void ReleaseItem(int index)
        {
            container?.GetDataList().Remove(index, out _);
            base.ReleaseItem(index);
        }
    }
}