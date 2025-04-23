namespace SkyStrike.Editor
{
    public interface IMenu : IObject, IInitiable
    {
        public void Hide();
        public void Show();
        public void Restore();
        public void SaveSetting();
    }
}