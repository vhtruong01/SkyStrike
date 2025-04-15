namespace SkyStrike
{
    namespace Editor
    {
        public interface IMenu : IObject, IInitiable
        {
            public void Hide();
            public void Show();
        }
    }
}