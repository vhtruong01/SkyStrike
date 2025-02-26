using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : MonoBehaviour, ISubMenu, IObserverMenu
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private Vector2Property scale;
            [SerializeField] private Vector2Property velocity;
            [SerializeField] private FloatProperty rotation;
            //[SerializeField] private TMP_InputField enemyName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button add1ShipBtn;
            private EnemyDataObserver curEnemyData;

            public void Awake()
            {
                add1ShipBtn.onClick.AddListener(CreateEnemy);
            }
            public bool CanDisplay() => curEnemyData != null;
            public void CreateEnemy()
            {
                if (curEnemyData != null)
                    MenuManager.CreateEnemy(curEnemyData);
            }
            public void Display(IData data)
            {
                bool isNewData = SetData(data);
                if (isNewData)
                {
                    UnbindData();
                    if (CanDisplay())
                    {
                        BindData();
                        type.text = curEnemyData.metaData.data.type;
                        icon.sprite = curEnemyData.metaData.data.sprite;
                    }
                    else
                    {
                        Hide();
                        return;
                    }
                }
                Show();
            }
            public bool SetData(IData data)
            {
                EnemyDataObserver newData = data as EnemyDataObserver;
                if (curEnemyData == newData) return false;
                curEnemyData = newData;
                return curEnemyData != null;
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
            public virtual void Hide()
            {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            public virtual void Show()
            {
                if (!gameObject.activeSelf && CanDisplay())
                    gameObject.SetActive(true);
            }
        }
    }
}