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
            private EnemyDataObserver curEnemyData;

            public void Start()
            {
                addEnemyBtn.onClick.AddListener(CreateEnemy);
            }
            public override bool CanDisplay() => curEnemyData != null;
            public void CreateEnemy()
            {
                if (curEnemyData != null)
                    MenuManager.CreateEnemy(curEnemyData);
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
                }
            }
            public override bool SetData(IData data)
            {
                EnemyDataObserver newData = data as EnemyDataObserver;
                if (curEnemyData == newData) return false;
                curEnemyData = newData;
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