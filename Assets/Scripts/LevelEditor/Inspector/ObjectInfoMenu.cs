using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : SubMenu<ObjectDataObserver>
        {
            [SerializeField] private StringProperty objectName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button addObjectBtn;
            [SerializeField] private Button pathBtn;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private Image referenceObjectIcon;
            [SerializeField] private Button referenceObjectBtn;
            [SerializeField] private TextMeshProUGUI referenceObjectText;
            [SerializeField] private FloatSelectRefObjectMenu selectRefObjectMenu;
            [SerializeField] private PathMenu pathMenu;

            public void Awake()
            {
                addObjectBtn.onClick.AddListener(CreateObject);
                referenceObjectBtn.onClick.AddListener(selectRefObjectMenu.Show);
                pathBtn.onClick.AddListener(pathMenu.Show);
            }
            public override void Init()
            {
                base.Init();
                EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            }
            private void DisplayReferenceObject(ObjectDataObserver refData)
            {
                if (refData == null)
                {
                    referenceObjectIcon.color = new();
                    referenceObjectText.text = "";
                    pathBtn.gameObject.SetActive(true);
                }
                else
                {
                    referenceObjectIcon.color = refData.metaData.data.color;
                    referenceObjectIcon.sprite = refData.metaData.data.sprite;
                    referenceObjectText.text = refData.name.data;
                    pathBtn.gameObject.SetActive(false);
                }
            }
            private void CreateObject()
            {
                if (data != null)
                {
                    var cloneData = data.Clone();
                    if (data.id != ObjectDataObserver.NULL_OBJECT_ID)
                        cloneData.SetRefData(data);
                    EventManager.CreateObject(cloneData);
                }
            }
            public override void BindData()
            {
                delay.Bind(data.delay);
                objectName.Bind(data.name);
                type.text = data.metaData.data.type;
                icon.sprite = data.metaData.data.sprite;
                icon.color = data.metaData.data.color;
                DisplayReferenceObject(data.refData);
            }
            public override void UnbindData()
            {
                delay.Unbind();
                objectName.Unbind();
            }
        }
    }
}