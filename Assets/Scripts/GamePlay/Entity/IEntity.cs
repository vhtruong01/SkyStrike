using UnityEngine;

namespace SkyStrike.Game
{
    public interface IEntity : IEntityComponent, IDamageable
    {
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Transform transform { get; }
        public void Disappear();
        public void Die();
    }
}