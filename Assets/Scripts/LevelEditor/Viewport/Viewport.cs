using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : MonoBehaviour
        {
            [SerializeField] private UIGroupPool objectGroupPool;
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private GameObject inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private GameObject waveMenu;
            [SerializeField] private Button addObjectMenuBtn;
            [SerializeField] private GameObject addObjectMenu;
            private WaveDataObserver waveDataObserver;

            public void Awake()
            {
                inspectorMenuBtn.onClick.AddListener(() => inspectorMenu.SetActive(true));
                waveMenuBtn.onClick.AddListener(() => waveMenu.SetActive(true));
                addObjectMenuBtn.onClick.AddListener(() => addObjectMenu.SetActive(true));
                MenuManager.onCreateObject.AddListener(CreateObject);
                MenuManager.onSelectWave.AddListener(SelectWave);
                objectGroupPool.selectDataCall = MenuManager.SelectObject;
            }
            public void CreateObject(IData data)
            {
                var enemyData = (data as EnemyDataObserver).Clone();
                if (enemyData == null) return;
                DisplayObject(enemyData);
                waveDataObserver.AddEnemy(enemyData);
            }
            public void SelectWave(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
                objectGroupPool.Clear();
                foreach(var enemyData in waveDataObserver.enemies)
                    DisplayObject(enemyData);
            }
            private void DisplayObject(EnemyDataObserver enemyData)
            {
                objectGroupPool.CreateItem(out ViewportItemUI enemy);
                enemy.SetData(enemyData);
            }
        }
    }
}