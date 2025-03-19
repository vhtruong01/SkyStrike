using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectItemUI : UIElement
        {
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI itemName;

            public override void SetData(IEditorData data)
            {
                var objectDataObserver = data as ObjectDataObserver;
                image.sprite = objectDataObserver.metaData.data.sprite;
                image.color = objectDataObserver.metaData.data.color;
                base.SetData(data);
            }
            private void ChangeName(string name) => itemName.text = name;
            public override void BindData()
            {
                var objectDataObserver = data as ObjectDataObserver;
                objectDataObserver.name.Bind(ChangeName);
            }
            public override void UnbindData()
            {
                var objectDataObserver = data as ObjectDataObserver;
                objectDataObserver.name.Unbind(ChangeName);
            }
        }
    }
}