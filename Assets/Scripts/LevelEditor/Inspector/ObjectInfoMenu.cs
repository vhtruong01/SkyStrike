using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : SubMenu
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private Vector2Property scale;
            [SerializeField] private Vector2Property velocity;
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
            private ObjectDataObserver curObjectData;

            public void Awake()
            {
                addObjectBtn.onClick.AddListener(CreateObject);
                referenceObjectBtn.onClick.AddListener(selectRefObjectMenu.Expand);
                EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            }
            private void DisplayReferenceObject(IEditorData data)
            {
                if (data is not ObjectDataObserver refData)
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
            public override bool CanDisplay() => curObjectData != null;
            private void CreateObject()
            {
                var newData = curObjectData?.Clone();
                if (newData != null)
                    EventManager.CreateObject(newData);
            }
            public override void Display(IEditorData data)
            {
                bool isNewData = SetData(data);
                if (!CanDisplay() || (!isNewData && curObjectData.isMetaData))
                {
                    curObjectData = null;
                    UnbindData();
                    Hide();
                    return;
                }
                if (isNewData)
                {
                    UnbindData();
                    BindData();
                    type.text = curObjectData.metaData.data.type;
                    icon.sprite = curObjectData.metaData.data.sprite;
                    icon.color = curObjectData.metaData.data.color;
                    DisplayReferenceObject(curObjectData.refData);
                }
            }
            public override bool SetData(IEditorData data)
            {
                ObjectDataObserver newData = data as ObjectDataObserver;
                if (curObjectData == newData) return false;
                curObjectData = newData;
                if (curObjectData != null)
                {
                    if (!curObjectData.isMetaData)
                    {
                        addObjectBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Duplicate";
                        referenceObjectBtn.interactable = true;
                    }
                    else
                    {
                        addObjectBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Create";
                        referenceObjectBtn.interactable = false;
                        referenceObjectIcon.color = new();
                        referenceObjectText.text = "";
                    }
                }
                return true;
            }
            public override void BindData()
            {
                position.Bind(curObjectData.position);
                scale.Bind(curObjectData.scale);
                rotation.Bind(curObjectData.rotation);
                delay.Bind(curObjectData.delay);
                velocity.Bind(curObjectData.velocity);
                objectName.Bind(curObjectData.name);
            }
            public override void UnbindData()
            {
                position.Unbind();
                scale.Unbind();
                rotation.Unbind();
                delay.Unbind();
                velocity.Unbind();
                objectName.Unbind();
                if (curObjectData == null) return;
                if (curObjectData.isMetaData)
                    curObjectData.ResetData();
            }
        }
    }
}