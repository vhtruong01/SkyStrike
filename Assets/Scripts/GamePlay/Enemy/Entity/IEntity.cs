namespace SkyStrike
{
    namespace Game
    {
        public interface IEntity
        {
            public void TakeDamage(int damage);
            public void Die();
        }
    }
}