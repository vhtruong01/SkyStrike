using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyShield : MonoBehaviour, IEnemyComponent, ISkill
    {
        public EnemyData enemyData { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IEntity entity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Active()
        {
            throw new System.NotImplementedException();
        }

        public void Deactive()
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Interrupt()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateData()
        {
            throw new System.NotImplementedException();
        }
    }
}