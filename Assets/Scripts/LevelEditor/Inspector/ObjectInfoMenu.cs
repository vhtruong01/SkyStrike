using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : SubMenu, IObserverMenu
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
            private ObjectDataObserver curObjectData;
            private WaveDataObserver waveDataObserver;


            public void Awake()
            {
                addObjectBtn.onClick.AddListener(CreateObject);
                EventManager.onSelectWave.AddListener(SelectWave);
                EventManager.onSelectWave.AddListener(SelectWave);
            }
            public override bool CanDisplay() => curObjectData != null;
            private void CreateObject()
            {
                var newData = curObjectData?.Clone();
                if (newData != null)
                {
                    waveDataObserver.AddObject(newData);
                    EventManager.CreateObject(newData);
                }
            }
            public void SelectWave(IEditorData data) => waveDataObserver = data as WaveDataObserver;
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
                    }
                }
                return true;
            }
            public void BindData()
            {
                position.Bind(curObjectData.position);
                scale.Bind(curObjectData.scale);
                rotation.Bind(curObjectData.rotation);
                delay.Bind(curObjectData.delay);
                velocity.Bind(curObjectData.velocity);
                objectName.Bind(curObjectData.name);
            }
            public void UnbindData()
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