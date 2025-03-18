namespace SkyStrike
{
    namespace Game
    {
        public interface IEntity : IGameObject
        {
            public void TakeDamage(int damage);
            public void Die();
        }
    }
}