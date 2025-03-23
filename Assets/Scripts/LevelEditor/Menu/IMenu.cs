namespace SkyStrike
{
    namespace Editor
    {
        public interface IMenu : IObject
        {
            public void Init();
            public void Hide();
            public void Show();
        }
    }
}