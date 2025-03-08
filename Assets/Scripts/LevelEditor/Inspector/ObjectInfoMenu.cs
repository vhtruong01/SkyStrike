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
            //[SerializeField] private TMP_InputField enemyName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button addEnemyBtn;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private Image referenceObjectIcon;
            [SerializeField] private Button referenceObjectBtn;
            private EnemyDataObserver curEnemyData;

            public void Awake()
            {
                addEnemyBtn.onClick.AddListener(CreateObject);
            }
            public override bool CanDisplay() => curEnemyData != null;
            private void CreateObject()
            {
                if (curEnemyData != null)
                    MenuManager.CreateObject(curEnemyData);
            }
            public override void Display(IData data)
            {
                bool isNewData = SetData(data);
                if (!CanDisplay() || (!isNewData && curEnemyData.isMetaData))
                {
                    curEnemyData = null;
                    UnbindData();
                    Hide();
                    return;
                }
                if (isNewData)
                {
                    UnbindData();
                    BindData();
                    type.text = curEnemyData.metaData.data.type;
                    icon.sprite = curEnemyData.metaData.data.sprite;
                    icon.color = curEnemyData.metaData.data.color;
                }
            }
            public override bool SetData(IData data)
            {
                EnemyDataObserver newData = data as EnemyDataObserver;
                if (curEnemyData == newData) return false;
                curEnemyData = newData;
                if (curEnemyData != null)
                {

                    if (!curEnemyData.isMetaData)
                    {
                        addEnemyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Duplicate";
                        referenceObjectBtn.interactable = true;
                    }
                    else
                    {
                        addEnemyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Create";
                        referenceObjectBtn.interactable = false;
                        referenceObjectIcon.color = new();
                    }
                }
                return true;
            }
            public void BindData()
            {
                position.Bind(curEnemyData.position);
                scale.Bind(curEnemyData.scale);
                rotation.Bind(curEnemyData.rotation);
            }
            public void UnbindData()
            {
                position.Unbind();
                scale.Unbind();
                rotation.Unbind();
                if (curEnemyData == null) return;
                if (curEnemyData.isMetaData)
                    curEnemyData.ResetData();
            }
        }
    }
}