namespace SkyStrike
{
    namespace Game
    {
        public class ItemManager : PoolManager<Item>
        {
            public override void Awake()
            {
                base.Awake();
                EventManager.onRemoveItem.AddListener(RemoveItem);
            }
            //public Item CreateItem(ItemData )
        }
    }
}