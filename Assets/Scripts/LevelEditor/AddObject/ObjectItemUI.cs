using TMPro;
using UnityEditor;
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
                var objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver?.name.Unbind(ChangeName);
                this.data = data;
                objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver.name.Bind(ChangeName);
                image.sprite = objectDataObserver.metaData.data.sprite;
                image.color = objectDataObserver.metaData.data.color;
            }
            private void ChangeName(string name) => itemName.text = name;

        }
    }
}