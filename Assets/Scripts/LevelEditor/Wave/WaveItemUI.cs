namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemUI : UIElement<WaveDataObserver>
        {
            public override void SetName(string newName)
            {
                if (string.IsNullOrEmpty(data.name.data))
                    Rename(newName);
            }
            public override void BindData()
            {
                data.name.Bind(Rename);
            }
            public override void UnbindData()
            {
                data.name.Unbind(Rename);
            }
            private void Rename(string newName)
            {
                base.SetName(newName);
                data.name.OnlySetData(newName);
            }
            public override WaveDataObserver DuplicateData() => data.Clone();
        }
    }
}