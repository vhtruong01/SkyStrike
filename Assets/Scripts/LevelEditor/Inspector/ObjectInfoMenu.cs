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
            [SerializeField] private FloatProperty velocity;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private FloatProperty size;
            [SerializeField] private IntProperty cloneCount;
            [SerializeField] private Image icon;
            [SerializeField] private Button addObjectBtn;
            [SerializeField] private Button pathBtn;
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
                }
                else
                {
                    referenceObjectIcon.color = refData.metaData.data.color;
                    referenceObjectIcon.sprite = refData.metaData.data.sprite;
                    referenceObjectText.text = refData.name.data;
                }
                pathBtn.gameObject.SetActive(refData == null && data.id != ObjectDataObserver.NULL_OBJECT_ID);
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
                size.Bind(data.size);
                objectName.Bind(data.name);
                velocity.Bind(data.velocity);
                cloneCount.Bind(data.cloneCount);
                icon.sprite = data.metaData.data.sprite;
                icon.color = data.metaData.data.color;
                DisplayReferenceObject(data.refData);
            }
            public override void UnbindData()
            {
                delay.Unbind();
                size.Unbind();
                velocity.Unbind();
                objectName.Unbind();
                cloneCount.Unbind();
            }
        }
    }
}