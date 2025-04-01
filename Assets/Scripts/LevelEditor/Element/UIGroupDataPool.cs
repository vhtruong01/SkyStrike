using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class UIGroupDataPool<T> : UIGroupPool<T> where T : class
        {
            protected IElementContainer<T> container;

            public override void Init(UnityAction<T> selectCall, UnityAction deselectCall = null)
            {
                base.Init(selectCall, deselectCall);
                container = GetComponent<IElementContainer<T>>();
            }
            public virtual void DisplayDataList()
            {
                Clear();
                foreach (var data in container?.GetDataList().GetList())
                    CreateItem(data);
                if (!canDeselect) SelectFirstItem();
            }
            public virtual UIElement<T> CreateItemAndAddData(T data)
            {
                var dataList = container?.GetDataList();
                if (dataList == null) return null;
                if (data != null)
                    dataList.Add(data);
                else data = dataList.CreateEmpty();
                return CreateItem(data);
            }
            public void RemoveSelectedItem()
            {
                if (container?.GetDataList() == null) return;
                RemoveItem(selectedItemIndex);
            }
            protected override void ReleaseItem(int index)
            {
                container?.GetDataList().Remove(index);
                base.ReleaseItem(index);
            }
        }
    }
}