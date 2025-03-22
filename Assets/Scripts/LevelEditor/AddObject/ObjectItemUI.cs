using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectItemUI : UIElement<ObjectDataObserver>
        {
            [SerializeField] private Image image;

            public override void SetData(ObjectDataObserver data)
            {
                base.SetData(data);
                image.sprite = data.metaData.data.sprite;
                image.color = data.metaData.data.color;
            }
            public override void SetName(string name)
            {
                base.SetName(name);
                data.name.OnlySetData(name);
            }
            public override void BindData()
            {
                data.name.Bind(SetName);
            }
            public override void UnbindData()
            {
                data.name.Unbind(SetName);
            }
        }
    }
}