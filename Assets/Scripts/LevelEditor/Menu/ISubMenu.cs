namespace SkyStrike
{
    namespace Editor
    {
        public interface ISubMenu : IObserver, IMenu
        {
            public bool CanDisplay();
        }
    }
}