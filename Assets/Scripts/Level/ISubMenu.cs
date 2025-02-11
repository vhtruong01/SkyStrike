namespace SkyStrike
{
    namespace Editor
    {
        public interface ISubMenu
        {
            public void SetData(IData data);
            public void Display(IData data);
        }
    }
}