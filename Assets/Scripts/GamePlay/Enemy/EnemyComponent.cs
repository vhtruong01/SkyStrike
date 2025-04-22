using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public abstract class EnemyComponent : MonoBehaviour, IInterruptible, IInitializable<EnemyData>
    {
        protected EnemyData data;
        public UnityAction<EEnemyAction> notifyAction { get; set; }
        public abstract void Interrupt();
        public virtual void SetData(EnemyData data)
            => this.data = data;
    }
}