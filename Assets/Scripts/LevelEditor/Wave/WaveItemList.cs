namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemList : UIGroupDataPool<WaveDataObserver>
        {
            public override void MoveItemArray(ref int startIndex, int newIndex, int len = 1)
            {
                var dataList = container?.GetDataList();
                if (dataList == null) return;
                base.MoveItemArray(ref startIndex, newIndex, len);
                for (int i = 0; i < items.Count; i++)
                    dataList.Set(i, items[i].data);
            }
            public void MoveLeftSelectedItem()
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
            public void MoveRightSelectedItem()
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
                //
                //dataList.Swap(i1, i2);
                items[i2].index = i2;
                items[i1].index = i1;
            }
        }
    }
}