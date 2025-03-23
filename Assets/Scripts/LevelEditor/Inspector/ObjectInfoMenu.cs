using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : SubMenu<ObjectDataObserver>
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private Vector2Property scale;
            [SerializeField] private FloatProperty velocity;
            [SerializeField] private FloatProperty rotation;
            [SerializeField] private StringProperty objectName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button addObjectBtn;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private Image referenceObjectIcon;
            [SerializeField] private Button referenceObjectBtn;
            [SerializeField] private TextMeshProUGUI referenceObjectText;
            [SerializeField] private FloatSelectRefObjectMenu selectRefObjectMenu;

            public void Awake()
            {
                addObjectBtn.onClick.AddListener(CreateObject);
                referenceObjectBtn.onClick.AddListener(selectRefObjectMenu.Show);
            }
            public override void Init()
            {
                EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            }
            private void DisplayReferenceObject(ObjectDataObserver refData)
            {
                if (refData == null)
                {
                    referenceObjectIcon.color = new();
                    referenceObjectText.text = "";
                }
                else
                {
                    referenceObjectIcon.color = refData.metaData.data.color;
                    referenceObjectIcon.sprite = refData.metaData.data.sprite;
                    referenceObjectText.text = refData.name.data;
                }
            }
            private void CreateObject()
            {
                if (data != null)
                    EventManager.CreateObject(data.Clone());
            }
            public override void BindData()
            {
                position.Bind(data.position);
                scale.Bind(data.scale);
                rotation.Bind(data.rotation);
                delay.Bind(data.delay);
                velocity.Bind(data.velocity);
                objectName.Bind(data.name);
                type.text = data.metaData.data.type;
                icon.sprite = data.metaData.data.sprite;
                icon.color = data.metaData.data.color;
                DisplayReferenceObject(data.refData);
            }
            public override void UnbindData()
            {
                position.Unbind();
                scale.Unbind();
                rotation.Unbind();
                delay.Unbind();
                velocity.Unbind();
                objectName.Unbind();
            }
        }
    }
}