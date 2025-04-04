namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemList : UIGroupPool<ObjectDataObserver>
        {
            public GridScreen screen { private get; set; }

            protected override UIElement<ObjectDataObserver> CreateObject()
            {
                var item = base.CreateObject() as ViewportItemUI;
                item.screen = screen;
                return item;
            }
        }
    }
}