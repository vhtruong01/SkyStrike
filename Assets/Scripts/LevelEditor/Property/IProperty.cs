namespace SkyStrike
{
    namespace Editor
    {
        public interface IProperty
        {
            public void OnValueChanged();
            public void Display(bool isEnable);
        }
    }
}