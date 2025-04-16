namespace SkyStrike
{
    namespace Editor
    {
        public interface IProperty
        {
            public void OnValueChanged();
            public void Refresh();
            public void Display(bool isEnabled);
        }
    }
}