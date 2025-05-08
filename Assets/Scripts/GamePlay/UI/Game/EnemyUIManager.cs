using SkyStrike.Game;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyStrike.UI
{
    public class EnemyUIManager : MonoBehaviour
    {
        [SerializeField] private DamageVisualizer hpTextPrefab;
        [SerializeField] private BossHpUI hpUIPrefab;
        [SerializeField] private Transform hpTextContainer;
        [SerializeField] private Transform bossHpBarContainer;
        private ObjectPool<DamageVisualizer> hpTextPool;

        public void Awake()
        {
            hpTextPool = new(CreateObject);

        }
        public void OnEnable()
        {
            EventManager.Subscribe<BossEventData>(DisplayBoss);
            EventManager.Subscribe<DamageVisualizerEventData>(DisplayDamage);
        }
        public void OnDisable()
        {
            EventManager.Unsubscribe<DamageVisualizerEventData>(DisplayDamage);
            EventManager.Unsubscribe<BossEventData>(DisplayBoss);
        }
        private DamageVisualizer CreateObject()
        {
            var obj = Instantiate(hpTextPrefab, hpTextContainer, false);
            obj.onDestroy = hpTextPool.Release;
            return obj;
        }
        public void DisplayDamage(DamageVisualizerEventData visualizer)
        {
            var c = visualizer.damageType switch
            {
                EDamageType.Normal => Color.white,
                EDamageType.Slashing => Color.cyan,
                EDamageType.Piercing => Color.magenta,
                _ => Color.red,
            };
            hpTextPool.Get().SetData(visualizer.position, visualizer.damage, c);
        }
        private void DisplayBoss(BossEventData eventData)
        {
            var hpBar = Instantiate(hpUIPrefab, bossHpBarContainer, false);
            hpBar.SetData(eventData.bossData);
        }
    }
}