using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletSelectionItemUI : UIElement<BulletDataObserver>
        {
            [SerializeField] private BoolProperty check;

            public override void BindData()
                => data.name.Bind(SetName);
            public override void UnbindData()
                => data.name.Unbind(SetName);
            public override void Click()
            {
                base.Click();
                Check(!check.GetValue());
            }
            public void Check(bool isCheck)
                => check.SetValue(isCheck);
        }
    }
}