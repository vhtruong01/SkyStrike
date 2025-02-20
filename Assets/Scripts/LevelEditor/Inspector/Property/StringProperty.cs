namespace SkyStrike
{
    namespace Editor
    {
        public class StringProperty : Property<string>
        {
            public override void Awake()
            {
                base.Awake();
                x.onValueChanged.AddListener(s =>
                {
                    OnValueChanged();
                });
            }
            public override void OnValueChanged()
            {
                value = x.text;
                onValueChanged.Invoke(value);
            }
        }
    }
}