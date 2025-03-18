using System.Collections;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public abstract class Entity : MonoBehaviour, IEntity
        {
            protected ObjectData objectData;
            protected PhaseData phaseData;
            public int hp { get; protected set; }
            public bool isDie { get; protected set; }

            public void TakeDamage(int damage)
            {
                hp -= damage;
                if (hp <= 0)
                {
                    hp = 0;
                    Die();
                }
            }
            public abstract void Die();
            public abstract void SetData(IGameData data);
            public abstract IEnumerator Appear();
        }
    }
}