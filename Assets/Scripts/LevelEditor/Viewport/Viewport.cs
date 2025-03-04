using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : MonoBehaviour
        {
            [SerializeField] private UIGroupPool enemyGroupPool;
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private GameObject inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private GameObject waveMenu;
            [SerializeField] private Button addObjectMenuBtn;
            [SerializeField] private GameObject addObjectMenu;
            private WaveDataObserver waveDataObserver;

            public void Start()
            {
                inspectorMenuBtn.onClick.AddListener(() => inspectorMenu.SetActive(true));
                waveMenuBtn.onClick.AddListener(() => waveMenu.SetActive(true));
                addObjectMenuBtn.onClick.AddListener(() => addObjectMenu.SetActive(true));
                MenuManager.onCreateEnemy.AddListener(AddEnemy);
                MenuManager.onSelectWave.AddListener(SelectWave);
            }
            public void AddEnemy(IData data)
            {
                var enemyData = (data as EnemyDataObserver).Clone();
                if (enemyData == null) return;
                waveDataObserver.AddEnemy(enemyData);
                enemyGroupPool.CreateItem(out EnemyEditor enemy);
                enemy.SetData(enemyData);
            }
            public void SelectWave(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
                enemyGroupPool.Clear();

                //
            }
        }
    }
}