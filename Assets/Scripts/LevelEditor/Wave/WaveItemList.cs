namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemList : UIGroupPoolMoveableElement<WaveDataObserver>
        {
            protected override void DuplicateSelectedItem()
            {
                var item = GetSelectedItem();
                if (item != null && item.data != null)
                    CreateItem(item.data.Clone());
            }
        }
    }
}