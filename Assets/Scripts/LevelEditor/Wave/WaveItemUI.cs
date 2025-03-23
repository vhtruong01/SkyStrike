namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemUI : UIElement<WaveDataObserver>
        {
            public override void SetName(string name)
            {
                if (!string.IsNullOrEmpty(data.name.data))
                    Rename(name);
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
                base.SetName(name);
                data.name.OnlySetData(name);
            }
            public override WaveDataObserver DuplicateData() => data.Clone();   
        }
    }
}