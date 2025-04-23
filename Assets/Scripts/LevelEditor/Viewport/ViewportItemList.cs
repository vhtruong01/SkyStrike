namespace SkyStrike.Editor
{
    public class ViewportItemList : UIGroupPool<ObjectDataObserver>
    {
        public IScalableScreen screen { private get; set; }

        protected override UIElement<ObjectDataObserver> CreateObject()
        {
            var item = base.CreateObject() as ViewportItemUI;
            item.screen = screen;
            return item;
        }
    }
}