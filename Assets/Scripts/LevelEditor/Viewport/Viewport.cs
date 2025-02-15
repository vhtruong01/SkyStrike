using SkyStrike.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : MonoBehaviour
        {
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private GameObject inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private GameObject waveMenu;
            [SerializeField] private Button addObjectMenuBtn;
            [SerializeField] private GameObject addObjectMenu;
            [SerializeField] private UIGroup enemyGroup;

            public void Awake()
            {
                inspectorMenuBtn.onClick.AddListener(() => inspectorMenu.SetActive(true));
                waveMenuBtn.onClick.AddListener(() => waveMenu.SetActive(true));
                addObjectMenuBtn.onClick.AddListener(() => addObjectMenu.SetActive(true));
                MenuManager.onCreateEnemy.AddListener(AddEnemy);
            }
            public void AddEnemy(IData data)
            {
                EnemyEditor enemy = enemyGroup.CreateItem<EnemyEditor>();
                enemy.enemyDataObserver = (data as EnemyDataObserver).Clone();
            }
            public void RemoveEnemy(EnemyEditor enemyEditor)
            {
                enemyGroup.RemoveItem(enemyEditor.gameObject);
                //wave
            }
            ////
            //public void Display(WaveData wave)
            //{
            //    ClearWave();
            //    foreach (IEnemyData e in wave.enemies)
            //        AddEnemy(e);
            //}
            //public void SaveWave()
            //{

            //}
            //public void ClearWave()
            //{
            //    //foreach (EnemyEditor e in enemyContainer.GetComponentsInChildren<EnemyEditor>())
            //    //    RemoveEnemy(e);
            //}
        }
    }
}