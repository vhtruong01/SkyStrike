namespace SkyStrike.Editor
{
    public interface IMenu : IInitalizable
    {
        public void Hide();
        public void Show();
        public void SaveSetting();
    }
}